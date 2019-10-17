/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System;
using GoldSim.Web.Components;
using GoldSim.Web.Controllers;
using GoldSim.Web.Models.ViewModels;
using GoldSim.Web.Services;
using Ignia.Topics;
using Ignia.Topics.AspNetCore.Mvc;
using Ignia.Topics.AspNetCore.Mvc.Controllers;
using Ignia.Topics.Data.Caching;
using Ignia.Topics.Data.Sql;
using Ignia.Topics.Mapping;
using Ignia.Topics.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Configuration;
using SendGrid;

namespace GoldSim.Web {

  /*============================================================================================================================
  | CLASS: GOLDSIM ACTIVATOR
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Responsible for creating instances of factories in response to web requests. Represents the Composition Root for
  ///   Dependency Injection.
  /// </summary>
  public class GoldSimActivator : IControllerActivator, IViewComponentActivator {

    /*==========================================================================================================================
    | PRIVATE INSTANCES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private readonly            IConfiguration                  _configuration;
    private readonly            ITypeLookupService              _typeLookupService              = null;
    private readonly            ITopicMappingService            _topicMappingService            = null;
    private readonly            ITopicRepository                _topicRepository                = null;
    private readonly            ISmtpService                    _smtpService                    = null;

    /*==========================================================================================================================
    | HIERARCHICAL TOPIC MAPPING SERVICE
    \-------------------------------------------------------------------------------------------------------------------------*/
    private readonly IHierarchicalTopicMappingService<NavigationTopicViewModel> _hierarchicalTopicMappingService = null;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Establishes a new instance of the <see cref="GoldSimActivator"/>, including any shared dependencies to be used
    ///   across instances of controllers.
    /// </summary>
    /// <remarks>
    ///   The constructor is responsible for establishing dependencies with the singleton lifestyle so that they are available
    ///   to all requests.
    /// </remarks>
    public GoldSimActivator(IConfiguration configuration) {

      /*------------------------------------------------------------------------------------------------------------------------
      | SAVE STANDARD DEPENDENCIES
      \-----------------------------------------------------------------------------------------------------------------------*/
                                _configuration                  = configuration;
      var                       connectionString                = configuration.GetConnectionString("OnTopic");
      var                       sqlTopicRepository              = new SqlTopicRepository(connectionString);
      var                       cachedTopicRepository           = new CachedTopicRepository(sqlTopicRepository);

      /*------------------------------------------------------------------------------------------------------------------------
      | PRELOAD REPOSITORY
      \-----------------------------------------------------------------------------------------------------------------------*/
      _topicRepository                                          = cachedTopicRepository;
      _typeLookupService                                        = new GoldSimTopicViewModelLookupService();
      _topicMappingService                                      = new TopicMappingService(_topicRepository, _typeLookupService);

      _topicRepository.Load();

      /*------------------------------------------------------------------------------------------------------------------------
      | CONSTRUCT SMTP CLIENT
      \-----------------------------------------------------------------------------------------------------------------------*/
      var sendGridApiKey = _configuration.GetValue<string>("SendGrid:ApiKey");
      var sendGridClient = new SendGridClient(sendGridApiKey);

      _smtpService = new SendGridSmtpService(sendGridClient);

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

    }

    /*==========================================================================================================================
    | METHOD: CREATE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Registers dependencies, and injects them into new instances of controllers in response to each request.
    /// </summary>
    /// <returns>A concrete instance of an <see cref="IController"/>.</returns>
    public object Create(ControllerContext context) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Determine controller type
      \-----------------------------------------------------------------------------------------------------------------------*/
      var controllerType = context.ActionDescriptor.ControllerTypeInfo.AsType();

      /*------------------------------------------------------------------------------------------------------------------------
      | Register
      \-----------------------------------------------------------------------------------------------------------------------*/
      var mvcTopicRoutingService = new MvcTopicRoutingService(
        _topicRepository,
        new Uri($"https://{context.HttpContext.Request.Host}/{context.HttpContext.Request.Path}"),
        context.RouteData
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
      return controllerType.Name switch {

        nameof(RedirectController) => new RedirectController(_topicRepository),

        nameof(LegacyRedirectController) => new LegacyRedirectController(_topicRepository),

        nameof(SitemapController) => new SitemapController(_topicRepository),

        nameof(PaymentsController) => new PaymentsController(
            _topicRepository,
            mvcTopicRoutingService,
            _topicMappingService,
            new BraintreeConfiguration(mvcTopicRoutingService, _configuration)
          ),

        nameof(FormsController) => new FormsController(
          _topicRepository,
          mvcTopicRoutingService,
          _topicMappingService,
          new ReverseTopicMappingService(_topicRepository),
          _smtpService
        ),

        nameof(ErrorController) => new ErrorController(),

        nameof(ReportingController) => new ReportingController(_topicRepository, new ExcelReportingService()),

        nameof(TopicController) => new TopicController(_topicRepository, mvcTopicRoutingService, _topicMappingService),

        _ => throw new Exception($"Unknown controller {controllerType.Name}")

      };
    }


    /// <summary>
    ///   Registers dependencies, and injects them into new instances of view components in response to each request.
    /// </summary>
    /// <returns>A concrete instance of an <see cref="IController"/>.</returns>
    public object Create(ViewComponentContext context) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Determine view component type
      \-----------------------------------------------------------------------------------------------------------------------*/
      var viewComponentType = context.ViewComponentDescriptor.TypeInfo.AsType();

      /*------------------------------------------------------------------------------------------------------------------------
      | Register
      \-----------------------------------------------------------------------------------------------------------------------*/
      var mvcTopicRoutingService = new MvcTopicRoutingService(
        _topicRepository,
        new Uri($"https://{context.ViewContext.HttpContext.Request.Host}/{context.ViewContext.HttpContext.Request.Path}"),
        context.ViewContext.RouteData
      );

      /*------------------------------------------------------------------------------------------------------------------------
      | Resolve
      \-----------------------------------------------------------------------------------------------------------------------*/
      return viewComponentType.Name switch {

        nameof(MenuViewComponent)
          => new MenuViewComponent(mvcTopicRoutingService, _hierarchicalTopicMappingService, _topicRepository),

        nameof(PageLevelNavigationViewComponent)
          => new PageLevelNavigationViewComponent(mvcTopicRoutingService, _hierarchicalTopicMappingService),

        nameof(CallsToActionViewComponent)
          => new CallsToActionViewComponent(mvcTopicRoutingService, _hierarchicalTopicMappingService),

        nameof(FooterViewComponent)
          => new FooterViewComponent(mvcTopicRoutingService, _topicRepository, _hierarchicalTopicMappingService),

        _ => throw new Exception($"Unknown view component {viewComponentType.Name}")

      };
    }

    /*==========================================================================================================================
    | METHOD: RELEASE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Responds to a request to release resources associated with a particular controller.
    /// </summary>
    public void Release(ControllerContext context, object controller) { }

    /// <summary>
    ///   Responds to a request to release resources associated with a particular view component.
    /// </summary>
    public void Release(ViewComponentContext context, object viewComponent) { }


  } //Class
} //Namespace