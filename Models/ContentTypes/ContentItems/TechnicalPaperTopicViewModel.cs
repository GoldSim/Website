/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using System;
using OnTopic.ViewModels;

namespace GoldSim.Web.Models.ContentTypes.ContentItems {

  /*============================================================================================================================
  | VIEW MODEL: TECHNICAL PAPER TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about a <c>TechnicalPaper</c> topic.
  /// </summary>
  public class TechnicalPaperTopicViewModel: ContentItemTopicViewModel {

    /*==========================================================================================================================
    | AUTHORS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a list of authors associated with the paper.
    /// </summary>
    public string Authors { get; set; }

    /*==========================================================================================================================
    | PUBLICATION
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the name of the publication or journal that the technical paper was originally published in.
    /// </summary>
    public string Publication { get; set; }

    /*==========================================================================================================================
    | PUBLICATION (URL)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the URL for the publication or journal that the technical paper was originally published in.
    /// </summary>
    public string PublicationUrl { get; set; }

    /*==========================================================================================================================
    | PUBLICATION DATE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the date that the technical paper was originally published on.
    /// </summary>
    public DateTime PublicationDate { get; set; }

    /*==========================================================================================================================
    | DOWNLOAD (LABEL)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides an optional label for the download link.
    /// </summary>
    public string DownloadLabel { get; set; }

    /*==========================================================================================================================
    | RELATIONSHIP: APPLICATIONS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a list of <see cref="ApplicationPageTopicViewModel"/>s that this <see cref="TechnicalPaperTopicViewModel"/>
    ///   is associated with.
    /// </summary>
    public TopicViewModelCollection<ApplicationPageTopicViewModel> Applications { get; set; }

  } // Class
} // Namespace