/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using OnTopic.ViewModels;

namespace GoldSim.Web.Models.ContentTypes {

  /*============================================================================================================================
  | VIEW MODEL: DOCUMENT POINTER TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about a <c>DocumentPointer</c> topic.
  /// </summary>
  public class DocumentPointerTopicViewModel: ApplicationBasePageTopicViewModel {

    public string DocumentType { get; set; }
    public string Url { get; set; }
    public TopicViewModelCollection<ApplicationPageTopicViewModel> Applications { get; set; }

  } // Class
} // Namespace