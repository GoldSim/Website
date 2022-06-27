/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using System.Collections.ObjectModel;
using GoldSim.Web.Models.Associations;

namespace GoldSim.Web.Models.ContentTypes {

  /*============================================================================================================================
  | VIEW MODEL: EXAMPLE APPLICATION TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about an <c>ExampleApplication</c>
  ///   topic.
  /// </summary>
  public record ExampleApplicationTopicViewModel: ApplicationBasePageTopicViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new <see cref="ExampleApplicationTopicViewModel"/> with an <paramref name="attributes"/> dictionary.
    /// </summary>
    /// <param name="attributes">An <see cref="AttributeDictionary"/> of attribute values.</param>
    public ExampleApplicationTopicViewModel(AttributeDictionary attributes) : base(attributes) { }

    /// <summary>
    ///   Initializes a new <see cref="ExampleApplicationTopicViewModel"/> with no parameters.
    /// </summary>
    public ExampleApplicationTopicViewModel() { }

    /*==========================================================================================================================
    | RELATIONSHIP: APPLICATIONS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a list of <see cref="ApplicationPageTopicViewModel"/>s that this <see cref="ExampleApplicationTopicViewModel"
    ///   /> is associated with.
    /// </summary>
    [MapAs(typeof(AssociatedTopicViewModel))]
    public Collection<AssociatedTopicViewModel> Applications { get; } = new();

  } // Class
} // Namespace