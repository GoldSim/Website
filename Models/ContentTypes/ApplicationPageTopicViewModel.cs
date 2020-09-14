/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using System.ComponentModel;
using GoldSim.Web.Models.ContentTypes.ContentItems;
using OnTopic.Mapping.Annotations;
using OnTopic.ViewModels;

namespace GoldSim.Web.Models.ContentTypes {

  /*============================================================================================================================
  | VIEW MODEL: APPLICATION PAGE TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about a <c>ApplicationPage</c> topic.
  /// </summary>
  public class ApplicationPageTopicViewModel: ApplicationBasePageTopicViewModel {

    /*==========================================================================================================================
    | ABSTRACT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a brief description that summarizes the content of the page. Can optionally be used in indexes to provide a
    ///   synopsis.
    /// </summary>
    public string Abstract { get; set; }

    /*==========================================================================================================================
    | MODEL IMAGE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a canonical screenshot of the model output. Other screenshots may be placed within the body, if appropriate.
    /// </summary>
    public string ModelImage { get; set; }

    /*==========================================================================================================================
    | COMPARE TO…?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   There are pages comparing certain modules or applications to other tools, such as Excel. This provides a key related
    ///   to those options so that templates can provide canned text and a link to such comparisons.
    /// </summary>
    [DefaultValue("")]
    public string CompareTo { get; set; }

    /*==========================================================================================================================
    | LEARN MORE (URL)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a URL for learning more about this application. This may point to a case study or white pager, for instance,
    ///   which the customer can download.
    /// </summary>
    public string LearnMoreUrl { get; set; }

    /*==========================================================================================================================
    | LEARN MORE (LABEL)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Optionally sets the label for the <see cref="LearnMoreUrl"/>.
    /// </summary>
    [DefaultValue("Learn More")]
    public string LearnMoreLabel { get; set; }

    /*==========================================================================================================================
    | RELATIONSHIP: MODELS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a reference to any module pages associated with the current application.
    /// </summary>
    [Relationship(RelationshipType.Relationship)]
    public TopicViewModelCollection<PageGroupTopicViewModel> Modules { get; set; }

    /*==========================================================================================================================
    | RELATIONSHIP: EXAMPLE APPLICATIONS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a reference to any <see cref="ExampleApplicationTopicViewModel"/>s associated with the current application.
    /// </summary>
    [Relationship("Applications", Type=RelationshipType.IncomingRelationship)]
    public TopicViewModelCollection<ExampleApplicationTopicViewModel> ExampleApplications { get; set; }

    /*==========================================================================================================================
    | RELATIONSHIP: WHITE PAPERS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a reference to any white papers associated with the current application.
    /// </summary>
    [Relationship("Applications", Type=RelationshipType.IncomingRelationship)]
    [FilterByAttribute("ContentType", "ContentItem")]
    public TopicViewModelCollection<ContentItemTopicViewModel> WhitePapers { get; set; }

    /*==========================================================================================================================
    | RELATIONSHIP: TECHNICAL PAPERS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a reference to any <see cref="TechnicalPaperTopicViewModel"/>s associated with the current application.
    /// </summary>
    [Relationship("Applications", Type=RelationshipType.IncomingRelationship)]
    public TopicViewModelCollection<TechnicalPaperTopicViewModel> TechnicalPapers { get; set; }

  } // Class
} // Namespace