/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using System;
using System.Linq;
using OnTopic.Mapping.Annotations;
using OnTopic.ViewModels;
using OnTopic.ViewModels.Collections;
using OnTopic.ViewModels.Items;

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
  public record ApplicationBasePageTopicViewModel : PageTopicViewModel, ICardViewModel {

    /*==========================================================================================================================
    | THUMBNAIL IMAGE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a thumbnail image to display in the card format. This is not usually displayed on the page itself.
    /// </summary>
    public string ThumbnailImage { get; init; }

    /*==========================================================================================================================
    | CATEGORY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the category key that the current topic is associated with (e.g., "EnvironmentalSystems").
    /// </summary>
    /// <remarks>
    ///   This is typically used to optionally group a list of applications by category on an index page.
    /// </remarks>
    public string Category { get; init; }

    /*==========================================================================================================================
    | CATEGORIES
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a full list of available categories. This will not change between applications.
    /// </summary>
    /// <remarks>
    ///   The purpose of the categories lookup is to provide the <see cref="GetCategoryTitle(String)"/> method a way to lookup
    ///   the friendly name of the current title based on the key name. This allows the label of the category to be changed at
    ///   any time without needing to update the <see cref="Category"/> of each application.
    /// </remarks>
    [Metadata("ApplicationCategories")]
    public TopicViewModelCollection<LookupListItemTopicViewModel> Categories { get; } = new();

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
    public string GetCategoryTitle(string category) =>
      Categories.Where(t => t.Key.Equals(category, StringComparison.Ordinal)).FirstOrDefault().Title;

  } // Class
} // Namespace