/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using Ignia.Topics.ViewModels;
using Ignia.Topics.Mapping;
using System.ComponentModel;

namespace GoldSim.Web.Models {

  /*============================================================================================================================
  | VIEW MODEL: APPLICATION PAGE TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about a <c>ApplicationPage</c> topic.
  /// </summary>
  public class ApplicationPageTopicViewModel: ApplicationBasePageTopicViewModel {

    public string ModelImage { get; set; }

    [DefaultValue("")]
    public string CompareTo { get; set; }

    [Relationship(RelationshipType.Relationship)]
    public TopicViewModelCollection<PageGroupTopicViewModel> Modules { get; set; }

    [Relationship("Applications", Type=RelationshipType.IncomingRelationship)]
    public TopicViewModelCollection<ExampleApplicationTopicViewModel> ExampleApplications { get; set; }

    [Relationship("Applications", Type=RelationshipType.IncomingRelationship)]
    [FilterByAttribute("DocumentType", "WhitePaper")]
    public TopicViewModelCollection<DocumentPointerTopicViewModel> WhitePapers { get; set; }

    [Relationship("Applications", Type=RelationshipType.IncomingRelationship)]
    public TopicViewModelCollection<TechnicalPaperTopicViewModel> TechnicalPapers { get; set; }

  } // Class

} // Namespace
