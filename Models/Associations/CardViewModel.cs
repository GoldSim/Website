/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System;

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

  } // Interface
} // Namespace