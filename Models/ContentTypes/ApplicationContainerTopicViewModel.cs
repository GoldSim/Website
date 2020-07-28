/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using OnTopic.ViewModels;

namespace GoldSim.Web.Models.ContentTypes {

  /*============================================================================================================================
  | VIEW MODEL: APPLICATION CONTAINER TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about a <c>ApplicationContainer</c>
  ///   topic.
  /// </summary>
  public class ApplicationContainerTopicViewModel : PageTopicViewModel {

    public TopicViewModelCollection<ApplicationPageTopicViewModel> Children { get; set; }

    public string DisplayOrder { get; set; }

  } // Class

} // Namespace