/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using OnTopic.AspNetCore.Mvc;
using OnTopic.Editor.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace GoldSim.Web {

  /*============================================================================================================================
  | CLASS: STARTUP
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Configures the application and sets up dependencies.
  /// </summary>
  public class Startup {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Constructs a new instances of the <see cref="Startup"/> class. Accepts an <see cref="IConfiguration"/>.
    /// </summary>
    /// <param name="configuration">
    ///   The shared <see cref="IConfiguration"/> dependency.
    /// </param>
    public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment) {
      Configuration = configuration;
      HostingEnvironment = webHostEnvironment;
    }

    /*==========================================================================================================================
    | PROPERTY: CONFIGURATION
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a (public) reference to the application's <see cref="IConfiguration"/> service.
    /// </summary>
    public IConfiguration Configuration { get; }

    /*==========================================================================================================================
    | PROPERTY: HOSTING ENVIRONMENT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a (public) reference to the application's <see cref="IWebHostEnvironment"/> service.
    /// </summary>
    public IWebHostEnvironment HostingEnvironment { get; }

    /*==========================================================================================================================
    | METHOD: CONFIGURE SERVICES
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides configuration of services. This method is called by the runtime to bootstrap the server configuration.
    /// </summary>
    public void ConfigureServices(IServiceCollection services) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Use the Microsoft Identity Platform for authentication
      >-------------------------------------------------------------------------------------------------------------------------
      | ### NOTE JJC20191122: OpenId only allows authentication against a single Azure AD tenant, or ALL Azure AD tenants. In
      | order to permit authentication against both Ignia (as the development partner) and GoldSim (as the primary user) we
      | need to validate the issuer. The issuer addresses can be retrieved per tenant by looking at the "issuer" attribute of
      | the https://login.microsoftonline.com/{TenantId}/v2.0/.well-known/openid-configuration feed (where {TenantId} is e.g.
      | "GoldSim.com").
      \-----------------------------------------------------------------------------------------------------------------------*/
      services.AddAuthentication(options => {
        options.DefaultAuthenticateScheme = OpenIdConnectDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
      })
      .AddOpenIdConnect(options => {
        Configuration.GetSection("OpenIdConnect").Bind(options);
        options.TokenValidationParameters = new TokenValidationParameters {
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

      /*------------------------------------------------------------------------------------------------------------------------
      | Configure: MVC
      \-----------------------------------------------------------------------------------------------------------------------*/
      var mvcBuilder = services.AddControllersWithViews()

        //Add OnTopic support
        .AddTopicSupport()

        //Add OnTopic editor support
        .AddTopicEditor();

      //Conditionally add runtime compilation in development
      if (HostingEnvironment.IsDevelopment()) {
        mvcBuilder.AddRazorRuntimeCompilation();
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Register: Activators
      \-----------------------------------------------------------------------------------------------------------------------*/
      var activator = new GoldSimActivator(Configuration, HostingEnvironment);

      services.AddSingleton<IControllerActivator>(activator);
      services.AddSingleton<IViewComponentActivator>(activator);

      /*------------------------------------------------------------------------------------------------------------------------
      | Configure Application Insights
      \-----------------------------------------------------------------------------------------------------------------------*/
      services.AddApplicationInsightsTelemetry(Configuration);

    }

    /*==========================================================================================================================
    | METHOD: CONFIGURE (APPLICATION)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides configuration the application. This method is called by the runtime to bootstrap the application
    ///   configuration, including the HTTP pipeline.
    /// </summary>
    public static void Configure(IApplicationBuilder app, IWebHostEnvironment env) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Configure: Error Pages
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (env.IsDevelopment()) {
        app.UseDeveloperExceptionPage();
      }
      else {
        app.UseExceptionHandler("/Error/InternalServer/");
        app.UseHttpsRedirection();
        app.UseHsts();
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Enable downloads
      \-----------------------------------------------------------------------------------------------------------------------*/
      var provider = new FileExtensionContentTypeProvider();

      provider.Mappings[".webmanifest"]                         = "application/manifest+json";
      provider.Mappings[".exe"]                                 = "application/vnd.microsoft.portable-executable";
      provider.Mappings[".gsm"]                                 = "application/octet-stream";
      provider.Mappings[".gsp"]                                 = "application/octet-stream";
      provider.Mappings[".mpd"]                                 = "application/dash+xml";
      provider.Mappings[".m4s"]                                 = "video/mp4";

      var staticFileOptions = new StaticFileOptions { ContentTypeProvider = provider };

      app.UseStaticFiles(staticFileOptions);

      /*------------------------------------------------------------------------------------------------------------------------
      | Configure: Server defaults
      \-----------------------------------------------------------------------------------------------------------------------*/
      app.UseStaticFiles();
      app.UseRouting();
      app.UseAuthentication();
      app.UseAuthorization();
      app.UseCors("default");

      /*------------------------------------------------------------------------------------------------------------------------
      | Configure: MVC
      \-----------------------------------------------------------------------------------------------------------------------*/
      app.UseEndpoints(endpoints => {
        endpoints.MapTopicEditorRoute().RequireAuthorization();
        endpoints.MapControllerRoute(
          name: "Payments",
          pattern: "Web/Purchase/PayInvoice/",
          defaults: new { controller = "Payments", action = "Index", path = "Web/Purchase/PayInvoice" }
        );
        endpoints.MapAreaControllerRoute(
          name: "Administration",
          areaName: "Administration",
          pattern: "Administration/{controller}/{action=Index}/{id?}",
          defaults: new { area = "Administration" }
        ).RequireAuthorization();
        endpoints.MapAreaControllerRoute(
          name: "Courses",
          areaName: "Courses",
          pattern: "Courses/{*path}",
          defaults: new { controller = "Courses", action = "Index", rootTopic = "Courses" }
        );
        endpoints.MapAreaControllerRoute(
          name: "Forms",
          areaName: "Forms",
          pattern: "Forms/{*path}",
          defaults: new { controller = "Forms", action = "Index", rootTopic = "Forms" }
        );
        endpoints.MapAreaControllerRoute(
          name: "Forms",
          areaName: "Forms",
          pattern: "Forms/{action=Index}",
          defaults: new { controller = "Forms" }
        );
        endpoints.MapControllerRoute(
          name: "default",
          pattern: "{controller}/{action=Index}/"
        );
        endpoints.MapTopicRoute("Web");
        endpoints.MapTopicRoute("Error", "Error");
        endpoints.MapTopicRedirect();
        endpoints.MapControllerRoute(
          name: "LegacyRedirect",
          pattern: "Page/{pageId}",
          defaults: new { controller = "LegacyRedirect", action = "Redirect" }
        );
        endpoints.MapControllers();
      });
    }

  } //Class
} //Namespace