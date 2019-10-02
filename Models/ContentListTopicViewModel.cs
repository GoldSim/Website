/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using Ignia.Topics.ViewModels;

namespace GoldSim.Web.Models {

  /*============================================================================================================================
  | VIEW MODEL: CONTENT LIST TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about a content list topic.
  /// </summary>
  public class ContentListTopicViewModel : Ignia.Topics.ViewModels.ContentListTopicViewModel {

    /*==========================================================================================================================
    | IS INDEXED?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a flag determining whether or not the content list should be indexed.
    /// </summary>
    /// <returns>True if the content list should be indexed; false otherwise.</returns>
    public bool IsIndexed { get; set; } = false;

  } // Class

} // Namespace
