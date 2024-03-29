﻿/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using System.Collections.ObjectModel;
using GoldSim.Web.Models.Associations;

namespace GoldSim.Web.Models.ContentTypes {

  /*============================================================================================================================
  | VIEW MODEL: APPLICATION INDEX TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about a <c>ApplicationIndex</c> topic.
  /// </summary>
  public record ApplicationIndexTopicViewModel : PageTopicViewModel {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    bool                        _isFirst                        = true;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new <see cref="ApplicationIndexTopicViewModel"/> with an <paramref name="attributes"/> dictionary.
    /// </summary>
    /// <param name="attributes">An <see cref="AttributeDictionary"/> of attribute values.</param>
    public ApplicationIndexTopicViewModel(AttributeDictionary attributes) : base(attributes) {
      Contract.Requires(attributes, nameof(attributes));
      FilteredDocumentType      = attributes.GetValue(nameof(FilteredDocumentType));
    }

    /// <summary>
    ///   Initializes a new <see cref="ApplicationIndexTopicViewModel"/> with no parameters.
    /// </summary>
    public ApplicationIndexTopicViewModel() { }

    /*==========================================================================================================================
    | FILTERED DOCUMENT TYPE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   The type of document to be displayed in the index.
    /// </summary>
    /// <remarks>
    ///   Indexed can, optionally, contain multiple document types—e.g., <see cref="ApplicationPageTopicViewModel"/>, <see
    ///   cref="ExampleApplicationTopicViewModel"/>, &c. The <see cref="FilteredDocumentType"/> allows the current view to be
    ///   filtered by one specific type.
    /// </remarks>
    public string FilteredDocumentType { get; init; }

    /*==========================================================================================================================
    | CATEGORIES
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Returns a list of <see cref="LookupListItemTopicViewModel"/>s, which represent the available categories to group the
    ///   applications by.
    /// </summary>
    [Metadata("ApplicationCategories")]
    public TopicViewModelCollection<LookupListItemTopicViewModel> Categories { get; } = new();

    /*==========================================================================================================================
    | CATEGORY: ENVIRONMENTAL SYSTEMS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a list of <see cref="ApplicationBasePageTopicViewModel"/>s associated with the <c>EnvironmentalSystems</c>
    ///   <see cref="ApplicationContainerTopicViewModel"/>.
    /// </summary>
    [MapAs(typeof(CardViewModel))]
    public virtual Collection<CardViewModel> EnvironmentalSystems { get; } = new();

    /*==========================================================================================================================
    | CATEGORY: BUSINESS SYSTEMS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a list of <see cref="ApplicationBasePageTopicViewModel"/>s associated with the <c>BusinessSystems</c>
    ///   <see cref="ApplicationContainerTopicViewModel"/>.
    /// </summary>
    [MapAs(typeof(CardViewModel))]
    public virtual Collection<CardViewModel> BusinessSystems { get; } = new();

    /*==========================================================================================================================
    | CATEGORY: ENGINEERED SYSTEMS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a list of <see cref="ApplicationBasePageTopicViewModel"/>s associated with the <c>EngineeredSystems</c>
    ///   <see cref="ApplicationContainerTopicViewModel"/>.
    /// </summary>
    [MapAs(typeof(CardViewModel))]
    public virtual Collection<CardViewModel> EngineeredSystems { get; } = new();

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
    public string GetCategoryTitle(string category) => Categories
      .Where(t => t.Key.Equals(category.Replace("Systems", "", StringComparison.Ordinal), StringComparison.Ordinal))
      .FirstOrDefault()?.Title?? category;

    /*==========================================================================================================================
    | GET ALL APPLICATIONS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Returns a consolidated list of <i>all</i> applications from the corresponding properties.
    /// </summary>
    /// <returns>A consolidated list of applications.</returns>
    public Collection<CardViewModel> GetAllApplications() =>
      new(EnvironmentalSystems.Concat(BusinessSystems).Concat(EngineeredSystems).Distinct().ToList());

    /*==========================================================================================================================
    | GET CATEGORIZED APPLICATIONS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Returns a dictionary of applications keyed by <see cref="Categories"/>.
    /// </summary>
    /// <returns>A consolidated list of applications.</returns>
    public Dictionary<string, Collection<CardViewModel>> GetCategorizedApplications() {
      var categorizedApplications = new Dictionary<string, Collection<CardViewModel>> {
        { nameof(EnvironmentalSystems), EnvironmentalSystems },
        { nameof(BusinessSystems), BusinessSystems },
        { nameof(EngineeredSystems), EngineeredSystems }
      };
      return categorizedApplications;
    }

    /*==========================================================================================================================
    | IS FIRST?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Returns true if the item is the first item in the list. Automatically toggles to false after the first time it is
    ///   called.
    /// </summary>
    /// <returns></returns>
    public bool IsFirst() {
      if (_isFirst) {
        _isFirst = false;
        return true;
      }
      return false;
    }

  } // Class
} // Namespace