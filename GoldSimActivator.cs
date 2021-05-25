/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System;
using GoldSim.Web.Administration.Controllers;
using GoldSim.Web.Administration.Services;
using GoldSim.Web.Components;
using GoldSim.Web.Controllers;
using GoldSim.Web.Courses.Components;
using GoldSim.Web.Courses.Controllers;
using GoldSim.Web.Courses.Models;
using GoldSim.Web.Forms.Components;
using GoldSim.Web.Forms.Controllers;
using GoldSim.Web.Payments.Controllers;
using GoldSim.Web.Payments.Services;
using GoldSim.Web.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Configuration;
using OnTopic.AspNetCore.Mvc;
using OnTopic.AspNetCore.Mvc.Controllers;
using OnTopic.Data.Caching;
using OnTopic.Data.Sql;
using OnTopic.Editor.AspNetCore.Attributes;
using OnTopic.Editor.AspNetCore.Controllers;
using OnTopic.Editor.AspNetCore.Infrastructure;
using OnTopic.Internal.Diagnostics;
using OnTopic.Lookup;
using OnTopic.Mapping;
using OnTopic.Mapping.Hierarchical;
using OnTopic.Mapping.Reverse;
using OnTopic.Repositories;
using OnTopic.ViewModels;
using PostmarkDotNet;

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
    private readonly            ITypeLookupService              _typeLookupService;
    private readonly            ITopicMappingService            _topicMappingService;
    private readonly            ITopicRepository                _topicRepository;
    private readonly            ISmtpService                    _smtpService;
    private readonly            IRequestValidator               _requestValidator;
    private readonly            IWebHostEnvironment             _webHostEnvironment;
    private readonly            StandardEditorComposer          _standardEditorComposer;

    /*==========================================================================================================================
    | HIERARCHICAL TOPIC MAPPING SERVICE
    \-------------------------------------------------------------------------------------------------------------------------*/
    private readonly IHierarchicalTopicMappingService<Models.NavigationTopicViewModel> _hierarchicalTopicMappingService;
    private readonly IHierarchicalTopicMappingService<TrackedNavigationTopicViewModel> _coursewareTopicMappingService;

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
    public GoldSimActivator(IConfiguration configuration, IWebHostEnvironment webHostEnvironment) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Verify dependencies
      \-----------------------------------------------------------------------------------------------------------------------*/
      Contract.Requires(configuration, nameof(configuration));
      Contract.Requires(webHostEnvironment, nameof(webHostEnvironment));

      /*------------------------------------------------------------------------------------------------------------------------
      | SAVE STANDARD DEPENDENCIES
      \-----------------------------------------------------------------------------------------------------------------------*/
          _configuration        = configuration;
          _webHostEnvironment   = webHostEnvironment;
          _requestValidator     = new RecaptchaValidator(configuration.GetValue<string>("reCaptcha:Secret"));
      var connectionString      = configuration.GetConnectionString("OnTopic");
      var sqlTopicRepository    = new SqlTopicRepository(connectionString);
      var cachedTopicRepository = new CachedTopicRepository(sqlTopicRepository);

      /*------------------------------------------------------------------------------------------------------------------------
      | PRELOAD REPOSITORY
      \-----------------------------------------------------------------------------------------------------------------------*/
      _topicRepository          = cachedTopicRepository;
      _typeLookupService        = new CompositeTypeLookupService(
                                    new GoldSimTopicViewModelLookupService(),
                                    new TopicViewModelLookupService(),
                                    new EditorViewModelLookupService()
                                  );
      _topicMappingService      = new TopicMappingService(_topicRepository, _typeLookupService);

      _topicRepository.Load();

      /*------------------------------------------------------------------------------------------------------------------------
      | INITIALIZE EDITOR COMPOSER
      \-----------------------------------------------------------------------------------------------------------------------*/
      _standardEditorComposer   = new(_topicRepository, _webHostEnvironment);

      /*------------------------------------------------------------------------------------------------------------------------
      | CONSTRUCT SMTP CLIENT
      \-----------------------------------------------------------------------------------------------------------------------*/
      var postmarkApiKey        = _configuration.GetValue<string>("Postmark:ApiKey");
      var postmarkClient        = new PostmarkClient(postmarkApiKey);

      _smtpService              = new PostmarkSmtpService(postmarkClient);

      /*------------------------------------------------------------------------------------------------------------------------
      | CONSTRUCT HIERARCHICAL TOPIC MAPPING SERVICES
      \-----------------------------------------------------------------------------------------------------------------------*/
      _hierarchicalTopicMappingService = new CachedHierarchicalTopicMappingService<Models.NavigationTopicViewModel>(
        new HierarchicalTopicMappingService<Models.NavigationTopicViewModel>(
          _topicRepository,
          _topicMappingService
        )
      );

      _coursewareTopicMappingService = new CachedHierarchicalTopicMappingService<TrackedNavigationTopicViewModel>(
        new HierarchicalTopicMappingService<TrackedNavigationTopicViewModel>(
          _topicRepository,
          _topicMappingService
        )
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
      | Validate input
      \-----------------------------------------------------------------------------------------------------------------------*/
      Contract.Requires(context, nameof(context));

      /*------------------------------------------------------------------------------------------------------------------------
      | Determine controller type
      \-----------------------------------------------------------------------------------------------------------------------*/
      var controllerType = context.ActionDescriptor.ControllerTypeInfo.AsType();

      /*------------------------------------------------------------------------------------------------------------------------
      | Register
      \-----------------------------------------------------------------------------------------------------------------------*/
      // Force controller recognition for specific Content Types
      if (controllerType.Equals(typeof(TopicController))) {
        switch (_topicRepository.Load(context.RouteData)?.ContentType) {
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

        nameof(ErrorController) => new ErrorController(
          _topicRepository,
          _topicMappingService
        ),

        nameof(CoursesController) => new CoursesController(
          _topicRepository,
          _topicMappingService
        ),

        nameof(PaymentsController) => new PaymentsController(
            _topicRepository,
            _topicMappingService,
            new BraintreeConfiguration(_topicRepository, _configuration, context.RouteData),
            _smtpService,
            _requestValidator
          ),

        nameof(FormsController) => new FormsController(
          _topicRepository,
          _topicMappingService,
          new ReverseTopicMappingService(_topicRepository),
          _smtpService,
          _requestValidator
        ),

        nameof(LicensesController) => new LicensesController(
          _topicRepository,
          _topicMappingService,
          new LicenseExportService()
        ),

        nameof(InvoicesController) => new InvoicesController(
          _topicRepository,
          _topicMappingService
        ),

        nameof(TopicController) => new TopicController(_topicRepository, _topicMappingService),

        nameof(EditorController) => new EditorController(_topicRepository, _topicMappingService),

      _ => throw new InvalidOperationException($"Unknown controller {controllerType.Name}")

      };
    }

    /// <summary>
    ///   Registers dependencies, and injects them into new instances of view components in response to each request.
    /// </summary>
    /// <returns>A concrete instance of an <see cref="IController"/>.</returns>
    public object Create(ViewComponentContext context) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate input
      \-----------------------------------------------------------------------------------------------------------------------*/
      Contract.Requires(context, nameof(context));

      /*------------------------------------------------------------------------------------------------------------------------
      | Determine view component type
      \-----------------------------------------------------------------------------------------------------------------------*/
      var viewComponentType = context.ViewComponentDescriptor.TypeInfo.AsType();

      /*------------------------------------------------------------------------------------------------------------------------
      | Resolve
      \-----------------------------------------------------------------------------------------------------------------------*/

      //Handle standard topic editor view components
      if (StandardEditorComposer.IsEditorComponent(viewComponentType)) {
        return _standardEditorComposer.ActivateEditorComponent(
          viewComponentType,
          _topicRepository
        );
      }

      //Handle GoldSim-specific view components
      return viewComponentType.Name switch {

        nameof(CookiesNoticeViewComponent)
          => new CookiesNoticeViewComponent(),

        nameof(MetadataLookupViewComponent)
          => new MetadataLookupViewComponent(_topicRepository),

        nameof(MenuViewComponent)
          => new MenuViewComponent(_topicRepository, _hierarchicalTopicMappingService),

        nameof(PageLevelNavigationViewComponent)
          => new PageLevelNavigationViewComponent(_topicRepository, _hierarchicalTopicMappingService),

        nameof(CallsToActionViewComponent)
          => new CallsToActionViewComponent(_topicRepository, _hierarchicalTopicMappingService),

        nameof(FooterViewComponent)
          => new FooterViewComponent(_topicRepository, _hierarchicalTopicMappingService),

        nameof(CourseListViewComponent)
          => new CourseListViewComponent(_topicRepository, _coursewareTopicMappingService),

        nameof(UnitListViewComponent)
          => new UnitListViewComponent(_topicRepository, _coursewareTopicMappingService),

        nameof(LessonListViewComponent)
          => new LessonListViewComponent(_topicRepository, _coursewareTopicMappingService),

        nameof(LessonPagingViewComponent)
          => new LessonPagingViewComponent(_topicRepository, _topicMappingService),

        nameof(RecaptchaViewComponent)
          => new RecaptchaViewComponent(_configuration.GetValue<string>("reCaptcha:SiteKey")),

        _ => throw new InvalidOperationException($"Unknown view component {viewComponentType.Name}")

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