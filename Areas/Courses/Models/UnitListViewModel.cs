/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using System.Collections.ObjectModel;
using GoldSim.Web.Models;
using OnTopic.AspNetCore.Mvc.Models;

namespace GoldSim.Web.Courses.Models {

  /*============================================================================================================================
  | VIEW MODEL: UNIT LIST
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about all child <c>Unit</c>
  ///   topics.
  /// </summary>
  public class UnitListViewModel: NavigationViewModel<TrackedNavigationTopicViewModel> {

    /*==========================================================================================================================
    | TRACKING EVENTS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a list of events that should be tracked by Google Universal Analytics.
    /// </summary>
    public Collection<TrackingEventViewModel> TrackingEvents { get; } = new();

    /*==========================================================================================================================
    | COURSE TRACKING EVENTS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a list of events that should be tracked by Google Analytics 4.
    /// </summary>
    public Collection<CourseTrackingEventViewModel> CourseTrackingEvents { get; } = new();

  } // Class
} // Namespace