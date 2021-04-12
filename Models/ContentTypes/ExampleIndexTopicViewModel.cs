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
  public record ExampleIndexTopicViewModel: ApplicationIndexTopicViewModel {

    /*==========================================================================================================================
    | CATEGORY: ENVIRONMENTAL SYSTEMS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a list of <see cref="ApplicationBasePageTopicViewModel"/>s associated with the <c>EnvironmentalSystems</c>
    ///   <see cref="ApplicationContainerTopicViewModel"/>.
    /// </summary>
    [MapAs(typeof(AssociatedTopicViewModel))]
    [Collection("EnvironmentalExamples")]
    public override TopicViewModelCollection<AssociatedTopicViewModel> EnvironmentalSystems { get; } = new();

    /*==========================================================================================================================
    | CATEGORY: BUSINESS SYSTEMS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a list of <see cref="ApplicationBasePageTopicViewModel"/>s associated with the <c>BusinessSystems</c>
    ///   <see cref="ApplicationContainerTopicViewModel"/>.
    /// </summary>
    [MapAs(typeof(AssociatedTopicViewModel))]
    [Collection("BusinessExamples")]
    public override TopicViewModelCollection<AssociatedTopicViewModel> BusinessSystems { get; } = new();

    /*==========================================================================================================================
    | CATEGORY: ENGINEERED SYSTEMS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a list of <see cref="ApplicationBasePageTopicViewModel"/>s associated with the <c>EngineeredSystems</c>
    ///   <see cref="ApplicationContainerTopicViewModel"/>.
    /// </summary>
    [MapAs(typeof(AssociatedTopicViewModel))]
    [Collection("EngineeredSystemsExamples")]
    public override TopicViewModelCollection<AssociatedTopicViewModel> EngineeredSystems { get; } = new();

  } // Class
} // Namespace