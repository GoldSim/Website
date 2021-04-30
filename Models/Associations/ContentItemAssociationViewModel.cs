/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System;
using OnTopic.ViewModels;

namespace GoldSim.Web.Models.Associations {

  /*============================================================================================================================
  | CLASS: CONTENT ITEM ASSOCIATION TOPIC VIEW MODEL
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a model for tracking associations to <see cref="ContentItemTopicViewModel"/>s. This model supports navigable
  ///   lists.
  /// </summary>
  public record ContentItemAssociationViewModel: AssociatedTopicViewModel {

    /*==========================================================================================================================
    | LEARN MORE (URL)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides an optional link for the <see cref="AssociatedTopicViewModel"/>.
    /// </summary>
    public Uri LearnMoreUrl { get; init; }

  } // Interface
} // Namespace