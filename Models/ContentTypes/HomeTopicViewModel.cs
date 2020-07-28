/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using OnTopic.Mapping.Annotations;
using OnTopic.ViewModels;

namespace GoldSim.Web.Models.ContentTypes {

  /*============================================================================================================================
  | VIEW MODEL: HOME TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about a <c>Home</c> topic.
  /// </summary>
  public class HomeTopicViewModel: PageTopicViewModel {

    public string Introduction { get; set; }

    [Follow(Relationships.Children)]
    public TopicViewModelCollection<ApplicationContainerTopicViewModel> Applications { get; set; }

  } // Class

} // Namespace
