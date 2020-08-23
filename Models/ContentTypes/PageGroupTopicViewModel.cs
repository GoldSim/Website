/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/

namespace GoldSim.Web.Models.ContentTypes {

  /*============================================================================================================================
  | VIEW MODEL: PAGE GROUP TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about a page group topic.
  /// </summary>
  /// <remarks>
  ///   There is already a centralized <see cref="OnTopic.ViewModels.PageGroupTopicViewModel"/>. It doesn't implement the
  ///   necessary <see cref="ICardViewModel"/> interface needed for e.g. Modules to be treated as cards.
  /// </remarks>
  public class PageGroupTopicViewModel : OnTopic.ViewModels.PageGroupTopicViewModel, ICardViewModel {

    /*==========================================================================================================================
    | THUMBNAIL IMAGE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the URL for a thumbnail image which will be used as part of a card layout, should a page be referenced as
    ///   part of a relationship to another page.
    /// </summary>
    /// <remarks>
    ///   While this can be used by any <see cref="PageGroupTopicViewModel"/>, it is specifically used as part of the <see cref=
    ///   "ApplicationPageTopicViewModel"/> which provides a reference to module pages—all of which use the <see cref=
    ///   "PageGroupTopicViewModel"/>.
    /// </remarks>
    public string ThumbnailImage { get; set; }

  } // Class
} // Namespace