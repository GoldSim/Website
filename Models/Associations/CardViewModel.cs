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