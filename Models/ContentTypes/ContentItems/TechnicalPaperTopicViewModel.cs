﻿/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using System;
using GoldSim.Web.Models.Associations;
using OnTopic.Mapping.Annotations;
using OnTopic.ViewModels;

namespace GoldSim.Web.Models.ContentTypes.ContentItems {

  /*============================================================================================================================
  | VIEW MODEL: TECHNICAL PAPER TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about a <c>TechnicalPaper</c> topic.
  /// </summary>
  public record TechnicalPaperTopicViewModel: ContentItemTopicViewModel {

    /*==========================================================================================================================
    | AUTHORS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a list of authors associated with the paper.
    /// </summary>
    public string Authors { get; init; }

    /*==========================================================================================================================
    | PUBLICATION
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the name of the publication or journal that the technical paper was originally published in.
    /// </summary>
    public string Publication { get; init; }

    /*==========================================================================================================================
    | PUBLICATION (URL)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the URL for the publication or journal that the technical paper was originally published in.
    /// </summary>
    public Uri PublicationUrl { get; init; }

    /*==========================================================================================================================
    | PUBLICATION DATE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the date that the technical paper was originally published on.
    /// </summary>
    public DateTime PublicationDate { get; init; }

    /*==========================================================================================================================
    | DOWNLOAD (LABEL)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides an optional label for the download link.
    /// </summary>
    public string DownloadLabel { get; init; }

  } // Class
} // Namespace