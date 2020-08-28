/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using System.Collections.ObjectModel;
using GoldSim.Web.Components;
using OnTopic.Models;
using OnTopic.ViewModels;

namespace GoldSim.Web.Models {

  /*============================================================================================================================
  | VIEW MODEL: NAVIGATION TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about the navigation.
  /// </summary>
  /// <remarks>
  ///   No topics are expected to have a <c>Navigation</c> content type. Instead, this view model is expected to be manually
  ///   constructed by the <see cref="LayoutController"/>.
  /// </remarks>
  public class NavigationTopicViewModel: PageTopicViewModel, INavigationTopicViewModel<NavigationTopicViewModel> {

    /*==========================================================================================================================
    | HEADER IMAGE (URL)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the file name for the image to place within the header.
    /// </summary>
    /// <remarks>
    ///   This is primarily used by the <see cref="PageLevelNavigationViewComponent"/>.
    /// </remarks>
    public string HeaderImageUrl { get; set; }

    /*==========================================================================================================================
    | CHILDREN
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a list of nested <see cref="NavigationTopicViewModel"/> topics.
    /// </summary>
    public virtual Collection<NavigationTopicViewModel> Children { get; } = new Collection<NavigationTopicViewModel>();

    /*==========================================================================================================================
    | IS SELECTED?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   A simple helper function to determine if the current <see cref="NavigationTopicViewModel"/> is part of the currently
    ///   selected topic's path.
    /// </summary>
    public bool IsSelected(string uniqueKey) => $"{uniqueKey}:"?.StartsWith($"{UniqueKey}:") ?? false;

  } // Class
} // Namespace