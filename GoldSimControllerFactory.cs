/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System;
using System.Web.Mvc;
using System.Web.Routing;
using GoldSim.Web.Controllers;
using GoldSim.Web.Models;
using Ignia.Topics;
using Ignia.Topics.Mapping;
using Ignia.Topics.Repositories;
using Ignia.Topics.Web;
using Ignia.Topics.Web.Mvc;
using Ignia.Topics.Web.Mvc.Controllers;

namespace GoldSim.Web {

  /*============================================================================================================================
  | CLASS: CONTROLLER FACTORY
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Responsible for creating instances of factories in response to web requests. Represents the Composition Root for
  ///   Dependency Injection.
  /// </summary>
  class GoldSimControllerFactory : DefaultControllerFactory {

    /*==========================================================================================================================
    | PRIVATE INSTANCES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private readonly            ITypeLookupService              _typeLookupService              = null;
    private readonly            ITopicMappingService            _topicMappingService            = null;
    private readonly            ITopicRepository                _topicRepository                = null;
    private readonly            Topic                           _rootTopic                      = null;

    private readonly IHierarchicalTopicMappingService<NavigationTopicViewModel> _hierarchicalTopicMappingService = null;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Establishes a new instance of the <see cref="GoldSimControllerFactory"/>, including any shared dependencies to be used
    ///   across instances of controllers.
    /// </summary>
    public GoldSimControllerFactory() : base() {
      #pragma warning disable CS0618

      /*------------------------------------------------------------------------------------------------------------------------
      | SAVE STANDARD DEPENDENCIES
      \-----------------------------------------------------------------------------------------------------------------------*/
      _topicRepository                                          = TopicRepository.DataProvider;
      _typeLookupService                                        = new GoldSimTopicViewModelLookupService();
      _topicMappingService                                      = new TopicMappingService(_topicRepository, _typeLookupService);
      _rootTopic                                                = TopicRepository.RootTopic;

      /*------------------------------------------------------------------------------------------------------------------------
      | CONSTRUCT HIERARCHICAL TOPIC MAPPING SERVICE
      \-----------------------------------------------------------------------------------------------------------------------*/
      var service = new HierarchicalTopicMappingService<NavigationTopicViewModel>(
        _topicRepository,
        _topicMappingService
      );

      _hierarchicalTopicMappingService = new CachedHierarchicalTopicMappingService<NavigationTopicViewModel>(
        service
      );

      #pragma warning restore CS0618
    }

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
      var mvcTopicRoutingService        = new MvcTopicRoutingService(
        _topicRepository,
        requestContext.HttpContext.Request.Url,
        requestContext.RouteData
      );

      //Set default controller
      if (controllerType == null) {
        controllerType = typeof(FallbackController);
      }

      // Force controller recognition for specific Content Types
      if (controllerType.Equals(typeof(TopicController))) {
        switch (mvcTopicRoutingService.GetCurrentTopic()?.ContentType) {
          case "Payments":
            controllerType      = typeof(PaymentsController);
            break;
          default:
            break;
        }
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Resolve
      \-----------------------------------------------------------------------------------------------------------------------*/
      switch (controllerType.Name) {

        case nameof(RedirectController):
          return new RedirectController(_topicRepository);

        case nameof(LegacyRedirectController):
          return new LegacyRedirectController(_topicRepository);

        case nameof(SitemapController):
          return new SitemapController(_topicRepository);

        case nameof(PaymentsController):
          return new PaymentsController(_topicRepository, mvcTopicRoutingService, _topicMappingService, new BraintreeConfiguration(mvcTopicRoutingService));

        case nameof(ErrorController):
          return new ErrorController();

        case nameof(ReportingController):
          return new ReportingController(_topicRepository, new ExcelReportingService());

        case nameof(LayoutController):
          return new LayoutController(mvcTopicRoutingService, _hierarchicalTopicMappingService, _topicRepository);

        case nameof(TopicController):
          return new TopicController(_topicRepository, mvcTopicRoutingService, _topicMappingService);

        default:
          return base.GetControllerInstance(requestContext, controllerType);

      }

    }

  } //Class
} //Namespace
