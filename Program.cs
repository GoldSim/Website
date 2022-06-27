/*==============================================================================================================================
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

#pragma warning disable CA1812 // Avoid uninstantiated internal classes

/*==============================================================================================================================
| ENABLE SERVICES
\-----------------------------------------------------------------------------------------------------------------------------*/
var builder = WebApplication.CreateBuilder(args);

/*------------------------------------------------------------------------------------------------------------------------------
| Enable: Microsoft Identity Platform (for authentication)
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
| Enable: MVC, OnTopic, and OnTopic Editor
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
| Enable: Dependency Injection via Composition Root
\-----------------------------------------------------------------------------------------------------------------------------*/
var activator = new GoldSimActivator(builder.Configuration, builder.Environment);

builder.Services.AddSingleton<IControllerActivator>(activator);
builder.Services.AddSingleton<IViewComponentActivator>(activator);

/*------------------------------------------------------------------------------------------------------------------------------
| Enable: Application Insights
\-----------------------------------------------------------------------------------------------------------------------------*/
builder.Services.AddApplicationInsightsTelemetry(builder.Configuration);

/*==============================================================================================================================
| CONFIGURE: APPLICATION
\-----------------------------------------------------------------------------------------------------------------------------*/
var app = builder.Build();

/*------------------------------------------------------------------------------------------------------------------------------
| Configure: Environment-specific features
\-----------------------------------------------------------------------------------------------------------------------------*/
if (app.Environment.IsProduction()) {
  app.UseStatusCodePagesWithReExecute("/Error/{0}/");
  app.UseExceptionHandler("/Error/500/");
  app.UseHttpsRedirection();
  app.UseHsts();
}

/*------------------------------------------------------------------------------------------------------------------------------
| Configure: Static file handling with downloads and cache headers
\-----------------------------------------------------------------------------------------------------------------------------*/
var provider                    = new FileExtensionContentTypeProvider();
const int duration              = 60*60*24*365*2;

provider.Mappings[".webmanifest"]                           = "application/manifest+json";

var staticFileOptions           = new StaticFileOptions {
  ContentTypeProvider           = provider,
  OnPrepareResponse             = context => {
    context.Context.Response.Headers[HeaderNames.CacheControl] = "public,max-age=" + duration;
  }
};
app.UseStaticFiles(staticFileOptions);

/*------------------------------------------------------------------------------------------------------------------------------
| Configure: Default services
\-----------------------------------------------------------------------------------------------------------------------------*/
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("default");

/*------------------------------------------------------------------------------------------------------------------------------
| Configure: Routes
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

app.MapTopicErrors(includeStaticFiles: false);                  // Error/{statusCode}
app.MapDefaultControllerRoute();                                // {controller=Home}/{action=Index}/{id?}

app.MapTopicRoute(rootTopic: "Web");                            // Web/{**path}
app.MapTopicRoute(rootTopic: "Error");                          // Error/{**path}
app.MapTopicRedirect();                                         // Topic/{topicId}
app.MapControllers();

/*------------------------------------------------------------------------------------------------------------------------------
| Run application
\-----------------------------------------------------------------------------------------------------------------------------*/
app.Run();

#pragma warning restore CA1812 // Avoid uninstantiated internal classes