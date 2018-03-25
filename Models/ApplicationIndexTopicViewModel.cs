/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using Ignia.Topics.ViewModels;
using Ignia.Topics.Mapping;
using System.ComponentModel;
using System.Linq;

namespace GoldSim.Web.Models {

  /*============================================================================================================================
  | VIEW MODEL: APPLICATION INDEX TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about a <c>ApplicationIndex</c> topic.
  /// </summary>
  public class ApplicationIndexTopicViewModel : TopicViewModel {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    bool                        _isFirst                        = true;

    /*==========================================================================================================================
    | GENERIC PROPERTIES
    \-------------------------------------------------------------------------------------------------------------------------*/
    public string FilteredDocumentType { get; set; }
    [Metadata("ApplicationCategories")]
    public TopicViewModelCollection<LookupListItemTopicViewModel> Categories { get; set; }
    public TopicViewModelCollection<ApplicationBasePageTopicViewModel> EnvironmentalApplications { get; set; }
    public TopicViewModelCollection<ApplicationBasePageTopicViewModel> BusinessApplications { get; set; }
    public TopicViewModelCollection<ApplicationBasePageTopicViewModel> SystemsApplications { get; set; }

    /*==========================================================================================================================
    | GET CATEGORY TITLE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Looks up a category from the <see cref="Categories"/> collection based on the <see
    ///   cref="LookupListItemTopicViewModel.Key"/> and returns the corresponding <see
    ///   cref="LookupListItemTopicViewModel.Title"/>.
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    public string GetCategoryTitle(string category) {
      return Categories.Where(t => t.Key.Equals(category)).FirstOrDefault().Title;
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
