/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       GoldSim Website
\=============================================================================================================================*/
using GoldSim.Web.Courses.Models;
using GoldSim.Web.Models;
using Microsoft.AspNetCore.Mvc;
using OnTopic;
using OnTopic.AspNetCore.Mvc.Components;
using OnTopic.Mapping.Hierarchical;
using OnTopic.Repositories;

namespace GoldSim.Web.Courses.Components {

  /*============================================================================================================================
  | CLASS: UNIT LIST VIEW COMPONENT
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Defines a <see cref="ViewComponent"/> which provides access to a menu of <see cref="TrackedNavigationTopicViewModel"/>
  ///   instances representing the children of the current <see cref="Topic"/>.
  /// </summary>
  /// <remarks>
  ///   In addition to <i>displaying</i> the navigation, this view component will <i>track</i> the users progress through them,
  ///   by evaluating a cookie set by the <see cref="LessonListViewComponent"/> to conditionally set a property on the
  ///   corresponding <see cref="TrackedNavigationTopicViewModel"/>.
  /// </remarks>
  public class UnitListViewComponent: NavigationTopicViewComponentBase<TrackedNavigationTopicViewModel> {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="UnitListViewComponent"/> with necessary dependencies.
    /// </summary>
    public UnitListViewComponent(
      ITopicRepository topicRepository,
      IHierarchicalTopicMappingService<TrackedNavigationTopicViewModel> hierarchicalTopicMappingService
    ) : base(
      topicRepository,
      hierarchicalTopicMappingService
    ) { }

    /*==========================================================================================================================
    | METHOD: GET NAVIGATION ROOT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Retrieves the root <see cref="Topic"/> from which to map the <see cref="TrackedNavigationTopicViewModel"/> objects.
    /// </summary>
    /// <remarks>
    ///   The navigation root in the case of the child navigation is simply the <see cref="CurrentTopic.Parent"/>.
    /// </remarks>
    protected Topic GetNavigationRoot() => CurrentTopic;

    /*==========================================================================================================================
    | METHOD: MAP NAVIGATION TOPIC VIEW MODELS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Maps a list of <see cref="TrackedNavigationTopicViewModel"/> instances based on the <paramref
    ///   name="navigationRootTopic"/>.
    /// </summary>
    protected async Task<TrackedNavigationTopicViewModel> MapNavigationTopicViewModels(Topic navigationRootTopic) =>
      await HierarchicalTopicMappingService.GetRootViewModelAsync(navigationRootTopic).ConfigureAwait(true);

    /*==========================================================================================================================
    | METHOD: INVOKE (ASYNC)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the pagel-level navigation menu for the current page, which exposes one tier of navigation from the nearest
    ///   page group.
    /// </summary>
    public async Task<IViewComponentResult> InvokeAsync() {

      /*------------------------------------------------------------------------------------------------------------------------
      | Retrieve root topic
      \-----------------------------------------------------------------------------------------------------------------------*/
      var navigationRootTopic = GetNavigationRoot();

      /*------------------------------------------------------------------------------------------------------------------------
      | Construct view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var navigationViewModel = new UnitListViewModel() {
        NavigationRoot = await MapNavigationTopicViewModels(navigationRootTopic).ConfigureAwait(true),
        CurrentWebPath = CurrentTopic?.GetWebPath()?? HttpContext.Request.Path
      };

      /*------------------------------------------------------------------------------------------------------------------------
      | Set current status
      \-----------------------------------------------------------------------------------------------------------------------*/
      foreach (var trackedNavigationViewModel in navigationViewModel.NavigationRoot.Children) {
        trackedNavigationViewModel.IsVisited = IsComplete(trackedNavigationViewModel.Key);
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Write course cookie
      \-----------------------------------------------------------------------------------------------------------------------*/
      var isCourseNowComplete   = navigationViewModel.NavigationRoot.Children.All(t => t.IsVisited is true);
      var wasCourseComplete     = IsComplete(CurrentTopic.Key);

      if (isCourseNowComplete != wasCourseComplete) {
        HttpContext.Response.Cookies.Append(
          $"Status{CurrentTopic.Key}",
          isCourseNowComplete.ToString(),
          new() {
            Path                = CurrentTopic.Parent.GetWebPath(),
            Expires             = DateTime.Now.AddYears(20)
          }
        );
        navigationViewModel.TrackingEvents.Add(
          new TrackingEventViewModel("Courses", isCourseNowComplete? "EndCourse" : "StartCourse", CurrentTopic.Key)
        );
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Return the corresponding view
      \-----------------------------------------------------------------------------------------------------------------------*/
      return View(navigationViewModel);

    }

    /*==========================================================================================================================
    | METHOD: IS COMPLETE?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   A helper method for determining if a given unit is completed.
    /// </summary>
    private bool? IsComplete(string key) =>
      HttpContext.Request.Cookies.TryGetValue($"Status{key}", out var isComplete)?
        (bool?)isComplete.Equals("True", StringComparison.OrdinalIgnoreCase) :
        null;

  } //Class
} //Namespace