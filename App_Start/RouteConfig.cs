/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System.Web.Mvc;
using System.Web.Routing;

namespace GoldSim.Web {

  /*============================================================================================================================
  | CLASS: ROUTE CONFIGURATION
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides default routing configuration for MVC.
  /// </summary>
  public static class RouteConfig {

    /*==========================================================================================================================
    | METHOD: REGISTER ROUTES
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provided a <see cref="RouteCollection"/>, registers all routes associated with the application.
    /// </summary>
    /// <param name="routes">
    ///   The route collection for the server, typically passed from the <see cref="System.Web.HttpApplication"/> class.
    /// </param>
    public static void RegisterRoutes(RouteCollection routes) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Ignore requests to AXDs (handled by HttpHandler)
      \-----------------------------------------------------------------------------------------------------------------------*/
      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

      /*------------------------------------------------------------------------------------------------------------------------
      | Enable attribute-based routing
      \-----------------------------------------------------------------------------------------------------------------------*/
      routes.MapMvcAttributeRoutes();

      /*------------------------------------------------------------------------------------------------------------------------
      | Handle legacy redirects
      \-----------------------------------------------------------------------------------------------------------------------*/
      //### NOTE JJC041918: Since GoldSim is deriving and customizing the RedirecController, its name is ambiguous between
      //the Ignia and GoldSim namespaces. As a result, the optional namespaces argument is required.
      routes.MapRoute(
        name: "LegacyRedirect",
        url: "Page/{pageId}",
        defaults: new { controller = "Redirect", action = "LegacyRedirect" },
        namespaces: new string[] { "GoldSim.Web.Controllers" }
      );

      /*------------------------------------------------------------------------------------------------------------------------
      | Handle OnTopic redirects
      \-----------------------------------------------------------------------------------------------------------------------*/
      //### NOTE JJC041918: Since GoldSim is deriving and customizing the RedirecController, its name is ambiguous between
      //the Ignia and GoldSim namespaces. As a result, the optional namespaces argument is required.
      routes.MapRoute(
        name: "TopicRedirect",
        url: "Topic/{topicId}",
        defaults: new { controller = "Redirect", action = "TopicRedirect" },
        namespaces: new string[] { "GoldSim.Web.Controllers" }
      );

      /*------------------------------------------------------------------------------------------------------------------------
      | Handle OnTopic Web namespace
      \-----------------------------------------------------------------------------------------------------------------------*/
      routes.MapRoute(
        name: "WebTopics",
        url: "Web/{*path}",
        defaults: new { controller = "Topic", action = "Index", id = UrlParameter.Optional, rootTopic = "Web" }
      );

      /*------------------------------------------------------------------------------------------------------------------------
      | Handle default route convention
      \-----------------------------------------------------------------------------------------------------------------------*/
      routes.MapRoute(
        name: "Default",
        url: "{controller}/{action}/{id}",
        defaults: new { controller = "Fallback", action = "Index", id = UrlParameter.Optional }
      );

    }

  } //Class
} //Namespace
