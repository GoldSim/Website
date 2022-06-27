/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/

namespace GoldSim.Web.Models.Associations {

  /*============================================================================================================================
  | CLASS: CARD TOPIC VIEW MODEL
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a model for tracking associations to topics. This model supports both card formats as well as navigable lists.
  /// </summary>
  public record CardViewModel: AssociatedTopicViewModel, ICardViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new <see cref="CardViewModel"/> with an <paramref name="attributes"/> dictionary.
    /// </summary>
    /// <param name="attributes">An <see cref="AttributeDictionary"/> of attribute values.</param>
    public CardViewModel(AttributeDictionary attributes) : base(attributes) {
      Contract.Requires(attributes, nameof(attributes));
      ThumbnailImage            = attributes.GetValue(nameof(ThumbnailImage));
    }

    /// <summary>
    ///   Initializes a new <see cref="CardViewModel"/> with no parameters.
    /// </summary>
    public CardViewModel() { }

    /*==========================================================================================================================
    | THUMBNAIL IMAGE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <inheritdoc/>
    public string ThumbnailImage { get; init; }

    /*==========================================================================================================================
    | LAST MODIFIED
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the date that the <see cref="AssociatedTopicViewModel"/> was last modified.
    /// </summary>
    public DateTime LastModified { get; init; }

  } // Interface
} // Namespace