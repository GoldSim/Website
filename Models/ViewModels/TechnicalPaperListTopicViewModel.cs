/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using System.Linq;
using Ignia.Topics.Mapping.Annotations;
using Ignia.Topics.ViewModels;

namespace GoldSim.Web.Models.ViewModels {

  /*============================================================================================================================
  | VIEW MODEL: TECHNICAL PAPER LIST TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about a Technical Paper List topic.
  /// </summary>
  public class TechnicalPaperListTopicViewModel : ContentListTopicViewModel {

    [Metadata("FieldCategories")]
    public TopicViewModelCollection<LookupListItemTopicViewModel> FieldCategories { get; set; }

    public TopicViewModelCollection<TechnicalPaperTopicViewModel> GetTechnicalPapers(string category) =>
      new TopicViewModelCollection<TechnicalPaperTopicViewModel>(
        ContentItems
          .Where(t => (t.Category ?? "")
          .Equals(category))
        .Cast<TechnicalPaperTopicViewModel>()
        .OrderByDescending(p => p.PublicationDate)
        .AsEnumerable()
      );

  } // Class

} // Namespace
