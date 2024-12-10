/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using System.Collections.ObjectModel;
using GoldSim.Web.Models.Associations;

namespace GoldSim.Web.Models.ContentTypes {

  /*============================================================================================================================
  | VIEW MODEL: EXAMPLE APPLICATION INDEX TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about a <c>ExampleApplicationIndex</c>
  ///   topic.
  /// </summary>
  internal record ExampleIndexTopicViewModel: ApplicationIndexTopicViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new <see cref="ExampleIndexTopicViewModel"/> with an <paramref name="attributes"/> dictionary.
    /// </summary>
    /// <param name="attributes">An <see cref="AttributeDictionary"/> of attribute values.</param>
    internal ExampleIndexTopicViewModel(AttributeDictionary attributes) : base(attributes) { }

    /// <summary>
    ///   Initializes a new <see cref="ExampleIndexTopicViewModel"/> with no parameters.
    /// </summary>
    internal ExampleIndexTopicViewModel() { }

    /*==========================================================================================================================
    | CATEGORY: ENVIRONMENTAL SYSTEMS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a list of <see cref="ApplicationBasePageTopicViewModel"/>s associated with the <c>EnvironmentalSystems</c>
    ///   <see cref="ApplicationContainerTopicViewModel"/>.
    /// </summary>
    [MapAs(typeof(CardViewModel))]
    [Collection("EnvironmentalExamples")]
    internal override Collection<CardViewModel> EnvironmentalSystems { get; } = new();

    /*==========================================================================================================================
    | CATEGORY: BUSINESS SYSTEMS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a list of <see cref="ApplicationBasePageTopicViewModel"/>s associated with the <c>BusinessSystems</c>
    ///   <see cref="ApplicationContainerTopicViewModel"/>.
    /// </summary>
    [MapAs(typeof(CardViewModel))]
    [Collection("BusinessExamples")]
    internal override Collection<CardViewModel> BusinessSystems { get; } = new();

    /*==========================================================================================================================
    | CATEGORY: ENGINEERED SYSTEMS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a list of <see cref="ApplicationBasePageTopicViewModel"/>s associated with the <c>EngineeredSystems</c>
    ///   <see cref="ApplicationContainerTopicViewModel"/>.
    /// </summary>
    [MapAs(typeof(CardViewModel))]
    [Collection("EngineeredSystemsExamples")]
    internal override Collection<CardViewModel> EngineeredSystems { get; } = new();

  } // Class
} // Namespace