/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/

namespace GoldSim.Web.Models.Associations {

  /*============================================================================================================================
  | CLASS: ASSOCIATED CONTENT ITEM VIEW MODEL
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a model for tracking associations to <see cref="ContentItemTopicViewModel"/>s. This model supports navigable
  ///   lists.
  /// </summary>
  public record AssociatedContentItemViewModel: AssociatedTopicViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new <see cref="AssociatedContentItemViewModel"/> with an <paramref name="attributes"/> dictionary.
    /// </summary>
    /// <param name="attributes">An <see cref="AttributeDictionary"/> of attribute values.</param>
    public AssociatedContentItemViewModel(AttributeDictionary attributes) : base(attributes) {
      Contract.Requires(attributes, nameof(attributes));
      LearnMoreUrl              = attributes.GetUri(nameof(LearnMoreUrl));
    }

    /// <summary>
    ///   Initializes a new <see cref="AssociatedContentItemViewModel"/> with no parameters.
    /// </summary>
    public AssociatedContentItemViewModel() { }

    /*==========================================================================================================================
    | KEY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <inheritdoc/>
    public string Key { get; init; }

    /*==========================================================================================================================
    | LEARN MORE (URL)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides an optional link for the <see cref="AssociatedTopicViewModel"/>.
    /// </summary>
    public Uri LearnMoreUrl { get; init; }

  } // Interface
} // Namespace