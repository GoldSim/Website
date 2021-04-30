/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using GoldSim.Web.Models.Associations;
using OnTopic.Mapping.Annotations;
using OnTopic.ViewModels;

namespace GoldSim.Web.Models.ContentTypes.ContentItems {

  /*============================================================================================================================
  | VIEW MODEL: WHITE PAPER TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about a <c>WhitePaper</c> topic.
  /// </summary>
  public record WhitePaperTopicViewModel: ContentItemTopicViewModel {

    /*==========================================================================================================================
    | RELATIONSHIP: APPLICATIONS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a list of <see cref="ApplicationPageTopicViewModel"/>s that this <see cref="WhitePaperTopicViewModel"/>
    ///   is associated with.
    /// </summary>
    [MapAs(typeof(AssociatedTopicViewModel))]
    public TopicViewModelCollection<AssociatedTopicViewModel> Applications { get; } = new();

  } // Class
} // Namespace