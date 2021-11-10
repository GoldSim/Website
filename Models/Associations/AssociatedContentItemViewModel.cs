﻿/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using OnTopic.ViewModels;

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