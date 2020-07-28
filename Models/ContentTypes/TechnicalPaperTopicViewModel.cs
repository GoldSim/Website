/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using System;
using OnTopic.ViewModels;

namespace GoldSim.Web.Models.ContentTypes {

  /*============================================================================================================================
  | VIEW MODEL: TECHNICAL PAPER TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about a <c>TechnicalPaper</c> topic.
  /// </summary>
  public class TechnicalPaperTopicViewModel: ContentItemTopicViewModel {

    public string Authors { get; set; }
    public string Publication { get; set; }
    public string PublicationUrl { get; set; }
    public DateTime PublicationDate { get; set; }
    public string DownloadLabel { get; set; }
    public TopicViewModelCollection<ApplicationPageTopicViewModel> Applications { get; set; }

  } // Class
} // Namespace