/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using Ignia.Topics.ViewModels;
using Ignia.Topics.Mapping;
using System.ComponentModel;

namespace GoldSim.Web.Models {

  /*============================================================================================================================
  | VIEW MODEL: APPLICATION INDEX TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about a <c>ApplicationIndex</c> topic.
  /// </summary>
  public class ApplicationIndexTopicViewModel : TopicViewModel {

    public string FilteredDocumentType { get; set; }
    public TopicViewModelCollection<ApplicationBasePageTopicViewModel> EnvironmentalApplications { get; set; }
    public TopicViewModelCollection<ApplicationBasePageTopicViewModel> BusinessApplications { get; set; }
    public TopicViewModelCollection<ApplicationBasePageTopicViewModel> SystemsApplications { get; set; }

  } // Class

} // Namespace
