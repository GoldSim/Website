/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using Ignia.Topics.Mapping;
using Ignia.Topics.ViewModels;

namespace GoldSim.Web.Models {

  /*============================================================================================================================
  | VIEW MODEL: HOME TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about a <c>Home</c> topic.
  /// </summary>
  public class HomeTopicViewModel: PageTopicViewModel {

    public string Introduction { get; set; }

    [Recurse(Relationships.Children)]
    public TopicViewModelCollection<ApplicationContainerTopicViewModel> Applications { get; set; }

  } // Class

} // Namespace
