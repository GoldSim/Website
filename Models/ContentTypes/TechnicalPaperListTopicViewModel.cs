﻿/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using System.Collections.ObjectModel;
using GoldSim.Web.Models.ContentTypes.ContentItems;

namespace GoldSim.Web.Models.ContentTypes {

  /*============================================================================================================================
  | VIEW MODEL: TECHNICAL PAPER LIST TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about a Technical Paper List topic.
  /// </summary>
  public record TechnicalPaperListTopicViewModel : PageTopicViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new <see cref="TechnicalPaperListTopicViewModel"/> with an <paramref name="attributes"/> dictionary.
    /// </summary>
    /// <param name="attributes">An <see cref="AttributeDictionary"/> of attribute values.</param>
    public TechnicalPaperListTopicViewModel(AttributeDictionary attributes) : base(attributes) { }

    /// <summary>
    ///   Initializes a new <see cref="TechnicalPaperListTopicViewModel"/> with no parameters.
    /// </summary>
    public TechnicalPaperListTopicViewModel() { }

    /*==========================================================================================================================
    | CONTENT ITEMS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a list of <see cref="TechnicalPaperTopicViewModel"/> topics, representing the contents of the <see
    ///   cref="TechnicalPaperTopicViewModel"/>.
    /// </summary>
    public Collection<TechnicalPaperTopicViewModel> ContentItems { get; } = new();

    /*==========================================================================================================================
    | FIELD CATEGORIES
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a list of categories potential associated with each <see cref="TechnicalPaperTopicViewModel"/>.
    /// </summary>
    [Metadata("FieldCategories")]
    public Collection<LookupListItemTopicViewModel> FieldCategories { get; } = new();

    /*==========================================================================================================================
    | GET TECHNICAL PAPERS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a helper function for retrieving a list of <see cref="TechnicalPaperTopicViewModel"/>s based on a category
    ///   key.
    /// </summary>
    public Collection<TechnicalPaperTopicViewModel> GetTechnicalPapers(string category) =>
      new(
        ContentItems
        .Where(t => (t.Category ?? "").Equals(category, StringComparison.Ordinal))
        .OrderByDescending(p => p.PublicationDate)
        .ToList()
      );

  } // Class
} // Namespace