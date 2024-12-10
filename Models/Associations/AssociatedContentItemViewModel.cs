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
  internal record AssociatedContentItemViewModel: AssociatedTopicViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new <see cref="AssociatedContentItemViewModel"/> with an <paramref name="attributes"/> dictionary.
    /// </summary>
    /// <param name="attributes">An <see cref="AttributeDictionary"/> of attribute values.</param>
    internal AssociatedContentItemViewModel(AttributeDictionary attributes) : base(attributes) {
      Contract.Requires(attributes, nameof(attributes));
      LearnMoreUrl              = attributes.GetUri(nameof(LearnMoreUrl));
    }

    /// <summary>
    ///   Initializes a new <see cref="AssociatedContentItemViewModel"/> with no parameters.
    /// </summary>
    internal AssociatedContentItemViewModel() { }

    /*==========================================================================================================================
    | KEY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <inheritdoc/>
    internal string Key { get; init; }

    /*==========================================================================================================================
    | LEARN MORE (URL)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides an optional link for the <see cref="AssociatedTopicViewModel"/>.
    /// </summary>
    internal Uri LearnMoreUrl { get; init; }

  } // Interface
} // Namespace