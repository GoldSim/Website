/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/

namespace GoldSim.Web.Areas.Courses.Models {

  /*============================================================================================================================
  | VIEW MODEL: COURSE TRACKING EVENT
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a data transfer object for relaying Google Analytics tracking events to the client.
  /// </summary>
  internal class CourseTrackingEventViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Constructs a new instance of a <see cref="CourseTrackingEventViewModel"/>.
    /// </summary>
    internal  CourseTrackingEventViewModel() { }

    /// <summary>
    ///   Constructs a new instance of a <see cref="CourseTrackingEventViewModel"/> with predetermined values.
    /// </summary>
    /// <param name="eventName">The event name.</param>
    /// <param name="courseName">The course name.</param>
    internal CourseTrackingEventViewModel(string eventName, string courseName) {
      EventName                 = eventName;
      CourseName                = courseName;
    }

    /*==========================================================================================================================
    | EVENT NAME
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   The event name.
    /// </summary>
    internal string EventName { get; set; }

    /*==========================================================================================================================
    | COURSE NAME
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   The course name.
    /// </summary>
    internal string CourseName { get; set; }

    } // Class
} // Namespace