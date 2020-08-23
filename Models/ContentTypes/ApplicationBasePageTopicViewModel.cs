/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using System.Linq;
using OnTopic.Mapping.Annotations;
using OnTopic.ViewModels;

namespace GoldSim.Web.Models.ContentTypes {

  /*============================================================================================================================
  | VIEW MODEL: APPLICATION BASE PAGE TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about a <c>ApplicationBasePage</c>
  ///   topic.
  /// </summary>
  /// <remarks>
  ///   It is not expected that any topics will directly implement the <c>ApplicationBasePage</c> content type that corresponds
  ///   to this view model. That said, it provides a base schema definition for e.g. <see cref="ApplicationPageTopicViewModel"/>
  ///   and <see cref="ExampleApplicationTopicViewModel"/>.
  /// </remarks>
  public class ApplicationBasePageTopicViewModel : PageTopicViewModel, ICardViewModel {

    public string ThumbnailImage { get; set; }
    public string Category { get; set; }

    [Metadata("ApplicationCategories")]
    public TopicViewModelCollection<LookupListItemTopicViewModel> Categories { get; set; }

    /*==========================================================================================================================
    | GET CATEGORY TITLE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Looks up a category from the <see cref="Categories"/> collection based on the <see
    ///   cref="LookupListItemTopicViewModel.Key"/> and returns the corresponding <see
    ///   cref="LookupListItemTopicViewModel.Title"/>.
    /// </summary>
    /// <param name="category"></param>
    /// <returns>The title corresponding to the category key.</returns>
    public string GetCategoryTitle(string category) => Categories.Where(t => t.Key.Equals(category)).FirstOrDefault().Title;

  } // Class
} // Namespace