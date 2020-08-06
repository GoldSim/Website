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

    public string ThumbnailImage { get; set; }

  } // Class
} // Namespace