/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/

namespace GoldSim.Web.Courses.Models {

  /*============================================================================================================================
  | VIEW MODEL: UNIT TRACKING EVENT
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a data transfer object for relaying Google Analytics tracking events to the client.
  /// </summary>
  internal sealed class UnitTrackingEventViewModel: CourseTrackingEventViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Constructs a new instance of a <see cref="UnitTrackingEventViewModel"/>.
    /// </summary>
    internal UnitTrackingEventViewModel() { }

    /// <inheritdoc cref="CourseTrackingEventViewModel" />
    /// <summary>
    ///   Constructs a new instance of a <see cref="UnitTrackingEventViewModel"/> with predetermined values.
    /// </summary>
    /// <param name="unitNumber">The unit number.</param>
    internal  UnitTrackingEventViewModel(string eventName, string courseName, int unitNumber): base(eventName, courseName) {
      UnitNumber = unitNumber;
    }

    /*==========================================================================================================================
    | UNIT NUMBER
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   The unit number.
    /// </summary>
    internal  int UnitNumber { get; set; }

    } // Class
} // Namespace