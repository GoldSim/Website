/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/

namespace GoldSim.Web.Models {

  /*============================================================================================================================
  | VIEW MODEL: TRACKING EVENT
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a data transfer object for relaying Google Analytics tracking events to the client.
  /// </summary>
  internal sealed class TrackingEventViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Constructs a new instance of a <see cref="TrackingEventViewModel"/>.
    /// </summary>
    internal TrackingEventViewModel() {}

    /// <summary>
    ///   Constructs a new instance of a <see cref="TrackingEventViewModel"/> with predetermined values.
    /// </summary>
    /// <param name="category">The event category.</param>
    /// <param name="action">The event action.</param>
    /// <param name="label">The optional event label.</param>
    internal TrackingEventViewModel(string category, string action, string label = null) {
      Category                  = category;
      Action                    = action;
      Label                     = label;
    }

    /*==========================================================================================================================
    | CATEGORY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   The event category.
    /// </summary>
    internal string Category { get; set; }

    /*==========================================================================================================================
    | ACTION
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   The event action.
    /// </summary>
    internal string Action { get; set; }

    /*==========================================================================================================================
    | LABEL
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   The event label.
    /// </summary>
    internal string Label { get; set; }

  } // Class
} // Namespace