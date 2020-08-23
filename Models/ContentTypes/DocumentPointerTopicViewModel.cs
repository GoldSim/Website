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

    /*==========================================================================================================================
    | DOCUMENT TYPE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   The type of document that the <see cref="DocumentPointerTopicViewModel"/> references; e.g. <c>WhitePaper</c> or
    ///   <c>CaseStudy</c>.
    /// </summary>
    public string DocumentType { get; set; }

    /*==========================================================================================================================
    | URL
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   The URL for the actual document being references.
    /// </summary>
    public string Url { get; set; }

    /*==========================================================================================================================
    | RELATIONSHIP: APPLICATIONS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a list of <see cref="ApplicationPageTopicViewModel"/>s that this <see cref="DocumentPointerTopicViewModel"/>
    ///   is associated with.
    /// </summary>
    public TopicViewModelCollection<ApplicationPageTopicViewModel> Applications { get; set; }

  } // Class
} // Namespace