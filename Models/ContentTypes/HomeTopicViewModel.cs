/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/

namespace GoldSim.Web.Models.ContentTypes {

  /*============================================================================================================================
  | VIEW MODEL: HOME TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about a <c>Home</c> topic.
  /// </summary>
  public record HomeTopicViewModel: PageTopicViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new <see cref="HomeTopicViewModel"/> with an <paramref name="attributes"/> dictionary.
    /// </summary>
    /// <param name="attributes">An <see cref="AttributeDictionary"/> of attribute values.</param>
    public HomeTopicViewModel(AttributeDictionary attributes): base(attributes) {
      Contract.Requires(attributes, nameof(attributes));
      Introduction = attributes.GetValue(nameof(Introduction));
    }

    /// <summary>
    ///   Initializes a new <see cref="HomeTopicViewModel"/> with no parameters.
    /// </summary>
    public HomeTopicViewModel() { }

    /*==========================================================================================================================
    | INTRODUCTION
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the introductory text to display at the top of the page.
    /// </summary>
    public string Introduction { get; set; }

    /*==========================================================================================================================
    | RELATIONSHIP: APPLICATIONS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a list of <see cref="ApplicationContainerTopicViewModel"/>s, each of which contain a list of <see cref=
    ///   "ApplicationPageTopicViewModel"/>s to be displayed on the homepage.
    /// </summary>
    [Include(AssociationTypes.Children)]
    [FilterByContentType("ApplicationContainer")]
    public TopicViewModelCollection<ApplicationContainerTopicViewModel> Applications { get; } = new();

  } // Class
} // Namespace