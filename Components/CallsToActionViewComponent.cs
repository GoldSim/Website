/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using GoldSim.Web.Models.Components;
using OnTopic.AspNetCore.Mvc.Components;
using OnTopic.Mapping.Hierarchical;

namespace GoldSim.Web.Components {

  /*============================================================================================================================
  | CLASS: CALLS TO ACTION VIEW COMPONENT
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Defines a <see cref="ViewComponent"/> which provides access to a menu of <typeparamref name="NavigationTopicViewModel"/>
  ///   instances representing the nearest calls to action for a given page.
  /// </summary>
  public class CallsToActionViewComponent: NavigationTopicViewComponentBase<NavigationTopicViewModel> {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="MenuViewComponentBase{T}"/> with necessary dependencies.
    /// </summary>
    /// <returns>A topic controller for loading OnTopic views.</returns>
    public CallsToActionViewComponent(
      ITopicRepository topicRepository,
      IHierarchicalTopicMappingService<NavigationTopicViewModel> hierarchicalTopicMappingService
    ) : base(
      topicRepository,
      hierarchicalTopicMappingService
    ) {}

    /*==========================================================================================================================
    | METHOD: INVOKE (ASYNC)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the calls-to-action for the current page, which may change based on the current context.
    /// </summary>
    public async Task<IViewComponentResult> InvokeAsync() {

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish variables
      \-----------------------------------------------------------------------------------------------------------------------*/
      var currentTopic          = CurrentTopic;

      /*------------------------------------------------------------------------------------------------------------------------
      | Identify navigation root
      >-------------------------------------------------------------------------------------------------------------------------
      | The navigation root in the case of the main menu is the namespace; i.e., the first topic underneath the root.
      \-----------------------------------------------------------------------------------------------------------------------*/
      var navigationRootTopic = HierarchicalTopicMappingService.GetHierarchicalRoot(currentTopic, 3, "Web");

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate conditions
      \-----------------------------------------------------------------------------------------------------------------------*/
      Contract.Assume(navigationRootTopic, $"The root topic could not be identified for the page-level navigation.");
      Contract.Assume(CurrentTopic, $"The current topic could not be identified for the page-level navigation.");

      /*------------------------------------------------------------------------------------------------------------------------
      | Determine anchor
      \-----------------------------------------------------------------------------------------------------------------------*/
      var homepage              = TopicRepository.Load("Web:Home");
      var announcementLabel     = homepage.Attributes.GetValue("AnnouncementLabel");

      /*------------------------------------------------------------------------------------------------------------------------
      | Construct view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var navigationViewModel   = new CallsToActionViewModel() {
        NavigationRoot          = await HierarchicalTopicMappingService.GetRootViewModelAsync(navigationRootTopic).ConfigureAwait(true),
        CurrentWebPath          = CurrentTopic?.GetWebPath(),
        HasAnnouncement         = String.IsNullOrWhiteSpace(announcementLabel)
      };

      /*------------------------------------------------------------------------------------------------------------------------
      | Return the corresponding view
      \-----------------------------------------------------------------------------------------------------------------------*/
      return View(navigationViewModel);

    }

  } // Class
} // Namespace