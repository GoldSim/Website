/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using System;
using System.Collections.ObjectModel;
using OnTopic.Models;

namespace GoldSim.Web.Models {

  /*============================================================================================================================
  | VIEW MODEL: CORE NAVIGATION TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed base model for feeding views with information about the navigation.
  /// </summary>
  /// <remarks>
  ///   No topics are expected to have a <c>Navigation</c> content type. Instead, this view model is expected to be manually
  ///   constructed by the <see cref="LayoutController"/>.
  /// </remarks>
  public abstract class CoreNavigationTopicViewModel<T>: INavigationTopicViewModel<T> where T: INavigationTopicViewModel<T> {

    /*==========================================================================================================================
    | KEY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <inheritdoc cref="OnTopic.ViewModels.TopicViewModel.Key" />
    public string Key { get; init; }

    /*==========================================================================================================================
    | TITLE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <inheritdoc />
    public string Title { get; init; }

    /*==========================================================================================================================
    | SHORT TITLE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a short title to be used in the navigation, for cases where the normal title is too long.
    /// </summary>
    public string ShortTitle { get; init; }

    /*==========================================================================================================================
    | WEB PATH
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <inheritdoc />
    public string WebPath { get; init; }

    /*==========================================================================================================================
    | CHILDREN
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a list of nested <see cref="NavigationTopicViewModel"/> topics.
    /// </summary>
    public virtual Collection<T> Children { get; } = new();

    /*==========================================================================================================================
    | IS SELECTED?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   A simple helper function to determine if the current <see cref="NavigationTopicViewModel"/> is part of the currently
    ///   selected topic's path.
    /// </summary>
    public bool IsSelected(string webPath) =>
      $"{webPath}/".StartsWith($"{WebPath}", StringComparison.OrdinalIgnoreCase);

  } // Class
} // Namespace