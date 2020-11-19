/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using OnTopic.ViewModels;

namespace GoldSim.Web.Models.ContentTypes.ContentItems {

  /*============================================================================================================================
  | VIEW MODEL: WHITE PAPER TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about a <c>WhitePaper</c> topic.
  /// </summary>
  public class WhitePaperTopicViewModel: ContentItemTopicViewModel {

    /*==========================================================================================================================
    | RELATIONSHIP: APPLICATIONS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a list of <see cref="ApplicationPageTopicViewModel"/>s that this <see cref="WhitePaperTopicViewModel"/>
    ///   is associated with.
    /// </summary>
    public TopicViewModelCollection<ApplicationPageTopicViewModel> Applications { get; } = new();

  } // Class
} // Namespace