/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using GoldSim.Web.Components;

namespace GoldSim.Web.Models.Components {

  /*============================================================================================================================
  | VIEW MODEL: PAGE-LEVEL NAVIGATION TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about the navigation.
  /// </summary>
  /// <remarks>
  ///   No topics are expected to have a <c>Navigation</c> content type. Instead, this view model is expected to be manually
  ///   constructed by the <see cref="LayoutController"/>.
  /// </remarks>
  public class PageLevelNavigationTopicViewModel: CoreNavigationTopicViewModel<PageLevelNavigationTopicViewModel> {

    /*==========================================================================================================================
    | HEADER IMAGE (URL)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the file name for the image to place within the header.
    /// </summary>
    /// <remarks>
    ///   This is primarily used by the <see cref="PageLevelNavigationViewComponent"/>.
    /// </remarks>
    public Uri HeaderImageUrl { get; set; }

  } // Class
} // Namespace