/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/

namespace GoldSim.Web.Courses.Models {

    /*============================================================================================================================
    | VIEW MODEL: COURSE TRACKING EVENT
    \---------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a data transfer object for relaying Google Analytics tracking events to the client.
    /// </summary>
    public class CourseTrackingEventViewModel {

        /*==========================================================================================================================
        | CONSTRUCTOR
        \-------------------------------------------------------------------------------------------------------------------------*/
        /// <summary>
        ///   Constructs a new instance of a <see cref="CourseTrackingEventViewModel"/>.
        /// </summary>
        public CourseTrackingEventViewModel() { }

        /// <summary>
        ///   Constructs a new instance of a <see cref="CourseTrackingEventViewModel"/> with predetermined values.
        /// </summary>
        /// <param name="eventName">The event name.</param>
        /// <param name="courseName">The course name.</param>
        public CourseTrackingEventViewModel(string eventName, string courseName) {
            EventName = eventName;
            CourseName = courseName;
        }

        /*==========================================================================================================================
        | EVENT NAME
        \-------------------------------------------------------------------------------------------------------------------------*/
        /// <summary>
        ///   The event name.
        /// </summary>
        public string EventName { get; set; }

        /*==========================================================================================================================
        | COURSE NAME
        \-------------------------------------------------------------------------------------------------------------------------*/
        /// <summary>
        ///   The course name.
        /// </summary>
        public string CourseName { get; set; }

    } // Class
} // Namespace