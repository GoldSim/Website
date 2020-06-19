/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       GoldSim Website
\=============================================================================================================================*/
using System;
using System.Linq;
using System.Threading.Tasks;
using GoldSim.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using OnTopic;
using OnTopic.AspNetCore.Mvc.Components;
using OnTopic.AspNetCore.Mvc.Models;
using OnTopic.Mapping.Hierarchical;
using OnTopic.Repositories;

namespace GoldSim.Web.Components {

  /*============================================================================================================================
  | CLASS: LESSON LIST VIEW COMPONENT
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Defines a <see cref="ViewComponent"/> which provides access to a menu of <see cref="TrackedNavigationTopicViewModel"/>
  ///   instances representing the siblings of the current <see cref="Topic"/>—which will <i>include</i> the current topic.
  /// </summary>
  /// <remarks>
  ///   In addition to <i>displaying</i> the navigation, this view component will <i>track</i> the users progress through them,
  ///   by setting a cookie for each URL that the user accesses—and then using that cookie to conditionally set a property on
  ///   the corresponding <see cref="TrackedNavigationTopicViewModel"/>
  /// </remarks>
  public class LessonListViewComponent: NavigationTopicViewComponentBase<TrackedNavigationTopicViewModel> {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="LessonListViewComponent"/> with necessary dependencies.
    /// </summary>
    public LessonListViewComponent(
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
    protected Topic GetNavigationRoot() => CurrentTopic?.Parent;

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
      var navigationViewModel = new NavigationViewModel<TrackedNavigationTopicViewModel>() {
        NavigationRoot = await MapNavigationTopicViewModels(navigationRootTopic).ConfigureAwait(true),
        CurrentKey = CurrentTopic?.GetUniqueKey()?? HttpContext.Request.Path
      };

      /*------------------------------------------------------------------------------------------------------------------------
      | Write lesson cookie
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (!IsVisited(CurrentTopic.Key)) {
        HttpContext.Response.Cookies.Append(
          $"Visited{CurrentTopic.Key}",
          "1",
          new Microsoft.AspNetCore.Http.CookieOptions() {
            Path = CurrentTopic.Parent.GetWebPath()
          }
        );
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Set visit status
      \-----------------------------------------------------------------------------------------------------------------------*/
      foreach (var trackedNavigationViewModel in navigationViewModel.NavigationRoot.Children) {
        trackedNavigationViewModel.IsVisited =
          IsVisited(trackedNavigationViewModel.Key) ||
          CurrentTopic.Key.Equals(trackedNavigationViewModel.Key, StringComparison.OrdinalIgnoreCase);
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Return the corresponding view
      \-----------------------------------------------------------------------------------------------------------------------*/
      return View(navigationViewModel);

    }

    /*==========================================================================================================================
    | METHOD: IS VISITED?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   A helper method for determining if a given key has been previously visited.
    /// </summary>
    private bool IsVisited(string key) =>
      HttpContext.Request.Cookies.TryGetValue($"Visited{key}", out _);

  } //Class
} //Namespace