/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using GoldSim.Web.Models.Components;

namespace GoldSim.Web.Courses.Models {

  /*============================================================================================================================
  | VIEW MODEL: TRACKED NAVIGATION TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about the navigation alongside
  ///   progress tracking information.
  /// </summary>
  /// <remarks>
  ///   No topics are expected to have a <c>Navigation</c> content type. Instead, this view model is expected to be manually
  ///   constructed by the <see cref="LayoutController"/>.
  /// </remarks>
  public class TrackedNavigationTopicViewModel: CoreNavigationTopicViewModel<TrackedNavigationTopicViewModel> {

    public string Abstract { get; init; }

    public bool? IsVisited { get; set; }

    public string GetCssClass() =>
      IsVisited switch {
        null => "unstarted",
        false => "incomplete",
        true => "complete"
      };

  } // Class
} // Namespace