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
  | VIEW MODEL: EXAMPLE APPLICATION TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about an <c>ExampleApplication</c>
  ///   topic.
  /// </summary>
  public class ExampleApplicationTopicViewModel: ApplicationBasePageTopicViewModel {

    public string Customer { get; set; }
    public string CustomerUrl { get; set; }
    public string DownloadUrl { get; set; }
    public TopicViewModelCollection<ApplicationPageTopicViewModel> Applications { get; set; }

  } // Class

} // Namespace
