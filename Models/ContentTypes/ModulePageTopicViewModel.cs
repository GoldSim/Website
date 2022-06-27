/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/

namespace GoldSim.Web.Models.ContentTypes {

  /*============================================================================================================================
  | VIEW MODEL: MODULE PAGE TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about a <c>ModulePage</c>
  ///   topic.
  /// </summary>
  public record ModulePageTopicViewModel : PageTopicViewModel, ICardViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new <see cref="ModulePageTopicViewModel"/> with an <paramref name="attributes"/> dictionary.
    /// </summary>
    /// <param name="attributes">An <see cref="AttributeDictionary"/> of attribute values.</param>
    public ModulePageTopicViewModel(AttributeDictionary attributes) : base(attributes) {
      Contract.Requires(attributes, nameof(attributes));
      ThumbnailImage            = attributes.GetValue(nameof(ThumbnailImage));
    }

    /// <summary>
    ///   Initializes a new <see cref="ModulePageTopicViewModel"/> with no parameters.
    /// </summary>
    public ModulePageTopicViewModel() { }

    /*==========================================================================================================================
    | THUMBNAIL
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the URL for a thumbnail image which will be used as part of a card layout, when a module page is displayed
    ///   on another page.
    /// </summary>
    public string ThumbnailImage { get; init; }

  } // Class
} // Namespace