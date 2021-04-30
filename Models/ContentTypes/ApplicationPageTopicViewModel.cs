/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using GoldSim.Web.Models.Associations;
using GoldSim.Web.Models.ContentTypes.ContentItems;
using OnTopic.Mapping.Annotations;

namespace GoldSim.Web.Models.ContentTypes {

  /*============================================================================================================================
  | VIEW MODEL: APPLICATION PAGE TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about a <c>ApplicationPage</c> topic.
  /// </summary>
  public record ApplicationPageTopicViewModel: ApplicationBasePageTopicViewModel {

    /*==========================================================================================================================
    | ABSTRACT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a brief description that summarizes the content of the page. Can optionally be used in indexes to provide a
    ///   synopsis.
    /// </summary>
    public string Abstract { get; init; }

    /*==========================================================================================================================
    | MODEL IMAGE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a canonical screenshot of the model output. Other screenshots may be placed within the body, if appropriate.
    /// </summary>
    public string ModelImage { get; init; }

    /*==========================================================================================================================
    | COMPARE TO…?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   There are pages comparing certain modules or applications to other tools, such as Excel. This provides a key related
    ///   to those options so that templates can provide canned text and a link to such comparisons.
    /// </summary>
    [DefaultValue("")]
    public string CompareTo { get; init; }

    /*==========================================================================================================================
    | LEARN MORE (URL)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a URL for learning more about this application. This may point to a case study or white pager, for instance,
    ///   which the customer can download.
    /// </summary>
    public Uri LearnMoreUrl { get; init; }

    /*==========================================================================================================================
    | LEARN MORE (LABEL)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Optionally sets the label for the <see cref="LearnMoreUrl"/>.
    /// </summary>
    [DefaultValue("Learn More")]
    public string LearnMoreLabel { get; init; }

    /*==========================================================================================================================
    | RELATIONSHIP: MODELS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a reference to any module pages associated with the current application.
    /// </summary>
    [MapAs(typeof(CardViewModel))]
    [Collection(CollectionType.Relationship)]
    public Collection<CardViewModel> Modules { get; } = new();

    /*==========================================================================================================================
    | RELATIONSHIP: EXAMPLE APPLICATIONS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a reference to any <see cref="ExampleApplicationTopicViewModel"/>s associated with the current application.
    /// </summary>
    [MapAs(typeof(CardViewModel))]
    [FilterByContentType("ExampleApplication")]
    [Collection("Applications", Type = CollectionType.IncomingRelationship)]
    public Collection<CardViewModel> ExampleApplications { get; } = new();

    /*==========================================================================================================================
    | RELATIONSHIP: WHITE PAPERS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a reference to any white papers associated with the current application.
    /// </summary>
    [MapAs(typeof(AssociatedContentItemViewModel))]
    [FilterByContentType("WhitePaper")]
    [Collection("Applications", Type = CollectionType.IncomingRelationship)]
    public Collection<AssociatedContentItemViewModel> WhitePapers { get; } = new();

    /*==========================================================================================================================
    | RELATIONSHIP: TECHNICAL PAPERS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a reference to any <see cref="TechnicalPaperTopicViewModel"/>s associated with the current application.
    /// </summary>
    [Collection("Applications", Type = CollectionType.IncomingRelationship)]
    public Collection<TechnicalPaperTopicViewModel> TechnicalPapers { get; } = new();

  } // Class
} // Namespace