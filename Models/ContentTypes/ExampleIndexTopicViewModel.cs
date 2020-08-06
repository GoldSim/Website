/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using OnTopic.Mapping.Annotations;
using OnTopic.ViewModels;

namespace GoldSim.Web.Models.ContentTypes {

  /*============================================================================================================================
  | VIEW MODEL: EXAMPLE APPLICATION INDEX TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about a <c>ExampleApplicationIndex</c>
  ///   topic.
  /// </summary>
  public class ExampleIndexTopicViewModel: ApplicationIndexTopicViewModel {

    [Relationship("EnvironmentalExamples")]
    public override TopicViewModelCollection<ApplicationBasePageTopicViewModel> EnvironmentalSystems { get; set; }

    [Relationship("BusinessExamples")]
    public override TopicViewModelCollection<ApplicationBasePageTopicViewModel> BusinessSystems { get; set; }

    [Relationship("EngineeredSystemsExamples")]
    public override TopicViewModelCollection<ApplicationBasePageTopicViewModel> EngineeredSystems { get; set; }

  } // Class
} // Namespace