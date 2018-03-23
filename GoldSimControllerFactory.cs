/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using GoldSim.Web.Controllers;
using Ignia.Topics;
using Ignia.Topics.Web;
using Ignia.Topics.Web.Mvc;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace GoldSim.Web {

  /*============================================================================================================================
  | CLASS: HOME CONTROLLER
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to the default homepage for the site.
  /// </summary>
  class GoldSimControllerFactory : DefaultControllerFactory {

    /*==========================================================================================================================
    | GET CONTROLLER INSTANCE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Overrides the factory method for creating new instances of controllers.
    /// </summary>
    /// <returns>A concrete instance of an <see cref="IController"/>.</returns>
    protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Register
      \-----------------------------------------------------------------------------------------------------------------------*/
      #pragma warning disable CS0618
      var topicRepository               = TopicRepository.DataProvider;
      var rootTopic                     = TopicRepository.RootTopic;
      var mvcTopicRoutingService        = new MvcTopicRoutingService(
        topicRepository,
        requestContext.HttpContext.Request.Url,
        requestContext.RouteData
      );
      var topicMappingService           = new TopicMappingService();
      #pragma warning restore CS0618

      //Set default controller
      if (controllerType == null) {
        controllerType = typeof(FallbackController);
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Resolve
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (controllerType == typeof(RedirectController)) {
        return new RedirectController(topicRepository);
      }

      if (controllerType == typeof(SitemapController)) {
        return new SitemapController(topicRepository, null);
      }

      if (controllerType == typeof(ErrorController)) {
        return new ErrorController(topicRepository);
      }

      if (controllerType == typeof(ReportingController)) {
        return new ReportingController(topicRepository, new ExcelReportingService());
      }

      if (controllerType == typeof(LayoutController)) {
        return new LayoutController(topicRepository, mvcTopicRoutingService.GetCurrentTopic());
      }

      if (controllerType == typeof(TopicController)) {
        return new TopicController(topicRepository, mvcTopicRoutingService, topicMappingService);
      }

      return base.GetControllerInstance(requestContext, controllerType);

      /*------------------------------------------------------------------------------------------------------------------------
      | Release
      \-----------------------------------------------------------------------------------------------------------------------*/
      //There are no resources to release

    }

  } //Class
} //Namespace
