/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using System;
using System.Linq;
using GoldSim.Web.Models.ContentTypes.ContentItems;
using OnTopic.Mapping.Annotations;
using OnTopic.ViewModels;

namespace GoldSim.Web.Models.ContentTypes {

  /*============================================================================================================================
  | VIEW MODEL: TECHNICAL PAPER LIST TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about a Technical Paper List topic.
  /// </summary>
  public record TechnicalPaperListTopicViewModel : PageTopicViewModel {

    /*==========================================================================================================================
    | CONTENT ITEMS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a list of <see cref="TechnicalPaperTopicViewModel"/> topics, representing the contents of the <see
    ///   cref="TechnicalPaperTopicViewModel"/>.
    /// </summary>
    public TopicViewModelCollection<TechnicalPaperTopicViewModel> ContentItems { get; } = new();

    /*==========================================================================================================================
    | FIELD CATEGORIES
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a list of categories potential associated with each <see cref="TechnicalPaperTopicViewModel"/>.
    /// </summary>
    [Metadata("FieldCategories")]
    public TopicViewModelCollection<LookupListItemTopicViewModel> FieldCategories { get; } = new();

    /*==========================================================================================================================
    | GET TECHNICAL PAPERS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a helper function for retrieving a list of <see cref="TechnicalPaperTopicViewModel"/>s based on a category
    ///   key.
    /// </summary>
    public TopicViewModelCollection<TechnicalPaperTopicViewModel> GetTechnicalPapers(string category) =>
      new(
        ContentItems
        .Where(t => (t.Category ?? "").Equals(category, StringComparison.Ordinal))
        .OrderByDescending(p => p.PublicationDate)
        .AsEnumerable()
      );

  } // Class
} // Namespace