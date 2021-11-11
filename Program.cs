﻿/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.StaticFiles;
using GoldSim.Web;
using OnTopic.AspNetCore.Mvc;
using OnTopic.AspNetCore.Mvc.Controllers;
using OnTopic.Editor.AspNetCore;

using HeaderNames = Microsoft.Net.Http.Headers.HeaderNames;

/*==============================================================================================================================
| METHOD: CONFIGURE SERVICES
\-----------------------------------------------------------------------------------------------------------------------------*/
/// <summary>
///   Provides configuration of services. This method is called by the runtime to bootstrap the server configuration.
/// </summary>

var builder = WebApplication.CreateBuilder(args);

/*------------------------------------------------------------------------------------------------------------------------------
| Use the Microsoft Identity Platform for authentication
>-------------------------------------------------------------------------------------------------------------------------------
| ### NOTE JJC20191122: OpenId only allows authentication against a single Azure AD tenant, or ALL Azure AD tenants. In
| order to permit authentication against both Ignia (as the development partner) and GoldSim (as the primary user) we
| need to validate the issuer. The issuer addresses can be retrieved per tenant by looking at the "issuer" attribute of
| the https://login.microsoftonline.com/{TenantId}/v2.0/.well-known/openid-configuration feed (where {TenantId} is e.g.
| "GoldSim.com").
\-----------------------------------------------------------------------------------------------------------------------------*/
builder.Services.AddAuthentication(options => {
  options.DefaultAuthenticateScheme = OpenIdConnectDefaults.AuthenticationScheme;
  options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
  options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddOpenIdConnect(options => {
  builder.Configuration.GetSection("OpenIdConnect").Bind(options);
  options.CorrelationCookie.SameSite = SameSiteMode.None;
  options.SaveTokens = true;
  options.TokenValidationParameters = new() {
    NameClaimType = "name",
    ValidIssuers = new[] {
      //Ignia users
      $"https://login.microsoftonline.com/10dcd9d4-80f7-47c8-ad5a-7efddcd5f868/v2.0",
      //GoldSim users
      $"https://login.microsoftonline.com/abfc6769-97de-4dc7-8284-0ecc2fac5cfc/v2.0"
    }
  };
})
.AddCookie();

/*------------------------------------------------------------------------------------------------------------------------------
| Configure: MVC
\-----------------------------------------------------------------------------------------------------------------------------*/
var mvcBuilder = builder.Services.AddControllersWithViews()

  //Add OnTopic support
  .AddTopicSupport()

  //Add OnTopic editor support
  .AddTopicEditor();

//Conditionally add runtime compilation in development
if (builder.Environment.IsDevelopment()) {
  mvcBuilder.AddRazorRuntimeCompilation();
}

/*------------------------------------------------------------------------------------------------------------------------------
| Configure: Sitemap
\-----------------------------------------------------------------------------------------------------------------------------*/
SitemapController.SkippedContentTypes.Add("Unit");

/*------------------------------------------------------------------------------------------------------------------------------
| Register: Activators
\-----------------------------------------------------------------------------------------------------------------------------*/
var activator = new GoldSimActivator(builder.Configuration, builder.Environment);

builder.Services.AddSingleton<IControllerActivator>(activator);
builder.Services.AddSingleton<IViewComponentActivator>(activator);

/*------------------------------------------------------------------------------------------------------------------------------
| Configure Application Insights
\-----------------------------------------------------------------------------------------------------------------------------*/
builder.Services.AddApplicationInsightsTelemetry(builder.Configuration);

/*==============================================================================================================================
| METHOD: CONFIGURE (APPLICATION)
\-----------------------------------------------------------------------------------------------------------------------------*/
/// <summary>
///   Provides configuration the application. This method is called by the runtime to bootstrap the application
///   configuration, including the HTTP pipeline.
/// </summary>
var app = builder.Build();

/*------------------------------------------------------------------------------------------------------------------------------
| Configure: Error Pages
\-----------------------------------------------------------------------------------------------------------------------------*/
if (app.Environment.IsDevelopment()) {
  app.UseDeveloperExceptionPage();
}
else if (app.Environment.IsProduction()) {
  app.UseExceptionHandler("/Error/InternalServer/");
  app.UseHttpsRedirection();
  app.UseHsts();
}

/*------------------------------------------------------------------------------------------------------------------------------
| Enable downloads and cache headers
\-----------------------------------------------------------------------------------------------------------------------------*/
var provider                    = new FileExtensionContentTypeProvider();
const int duration              = 60*60*24*365*2;

provider.Mappings[".webmanifest"]                           = "application/manifest+json";
provider.Mappings[".exe"]                                   = "application/vnd.microsoft.portable-executable";
provider.Mappings[".gsm"]                                   = "application/octet-stream";
provider.Mappings[".gsp"]                                   = "application/octet-stream";
provider.Mappings[".mpd"]                                   = "application/dash+xml";
provider.Mappings[".m4s"]                                   = "video/mp4";

var staticFileOptions           = new StaticFileOptions {
  ContentTypeProvider           = provider,
  OnPrepareResponse             = context => {
    context.Context.Response.Headers[HeaderNames.CacheControl] = "public,max-age=" + duration;
  }
};
app.UseStaticFiles(staticFileOptions);

/*------------------------------------------------------------------------------------------------------------------------------
| Configure: Server defaults
\-----------------------------------------------------------------------------------------------------------------------------*/
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("default");

/*------------------------------------------------------------------------------------------------------------------------------
| Configure: MVC
\-----------------------------------------------------------------------------------------------------------------------------*/

AppContext.SetSwitch("Microsoft.AspNetCore.Routing.UseCorrectCatchAllBehavior", true);

app.MapAreaControllerRoute(
  name                          : "Payments",
  areaName                      : "Payments",
  pattern                       : "Web/Purchase/PayInvoice/",
  defaults                      : new {
    controller                  = "Payments",
    action                      = "Index",
    path                        = "Web/Purchase/PayInvoice"
  }
);

app.MapControllerRoute(
  name                          : "LegacyRedirect",
  pattern                       : "Page/{pageId}",
  defaults                      : new {
    controller                  = "LegacyRedirect",
    action                      = "Redirect"
  }
);

app.MapTopicEditorRoute().RequireAuthorization();               // OnTopic/{action}/{**path}

app.MapImplicitAreaControllerRoute();                           // {area:exists}/{action=Index}
app.MapDefaultAreaControllerRoute();                            // {area:exists}/{controller}/{action=Index}/{id?}
app.MapTopicAreaRoute();                                        // {area:exists}/{**path}
app.MapDefaultControllerRoute();                                // {controller=Home}/{action=Index}/{id?}

app.MapTopicRoute("Web");                              // Web/{**path}
app.MapTopicRedirect();                                         // Topic/{topicId}
app.MapControllers();

/*------------------------------------------------------------------------------------------------------------------------------
| Run application
\-----------------------------------------------------------------------------------------------------------------------------*/

app.Run();