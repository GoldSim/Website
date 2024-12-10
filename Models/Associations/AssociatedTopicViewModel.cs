/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using OnTopic.Models;

namespace GoldSim.Web.Models.Associations {

  /*============================================================================================================================
  | CLASS: ASSOCIATED TOPIC VIEW MODEL
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a model for tracking associations to topics. This model supports both card formats as well as navigable lists.
  /// </summary>
  internal record AssociatedTopicViewModel: INavigableTopicViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new <see cref="AssociatedTopicViewModel"/> with an <paramref name="attributes"/> dictionary.
    /// </summary>
    /// <param name="attributes">An <see cref="AttributeDictionary"/> of attribute values.</param>
    internal AssociatedTopicViewModel(AttributeDictionary attributes) {
      Contract.Requires(attributes, nameof(attributes));
      ShortTitle = attributes.GetValue(nameof(ShortTitle));
    }

    /// <summary>
    ///   Initializes a new <see cref="AssociatedTopicViewModel"/> with no parameters.
    /// </summary>
    internal AssociatedTopicViewModel() { }

    /*==========================================================================================================================
    | WEB PATH
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <inheritdoc/>
    public string WebPath { get; init; }

    /*==========================================================================================================================
    | TITLE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <inheritdoc/>
    public string Title { get; init; }

    /*==========================================================================================================================
    | SHORT TITLE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <inheritdoc/>
    public string ShortTitle { get; init; }

  } // Interface
} // Namespace