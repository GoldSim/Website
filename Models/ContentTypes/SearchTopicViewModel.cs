/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/

namespace GoldSim.Web.Models.ContentTypes {

  /*============================================================================================================================
  | VIEW MODEL: SEARCH TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about a <c>Search</c> topic.
  /// </summary>
  public record SearchTopicViewModel : PageTopicViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new <see cref="SearchTopicViewModel"/> with an <paramref name="attributes"/> dictionary.
    /// </summary>
    /// <param name="attributes">An <see cref="AttributeDictionary"/> of attribute values.</param>
    public SearchTopicViewModel(AttributeDictionary attributes) : base(attributes) { }

    /// <summary>
    ///   Initializes a new <see cref="FaqTopicViewModel"/> with no parameters.
    /// </summary>
    public SearchTopicViewModel() { }

  } // Class
} // Namespace