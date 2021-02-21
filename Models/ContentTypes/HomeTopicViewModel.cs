/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using OnTopic.Mapping.Annotations;
using OnTopic.ViewModels;
using OnTopic.ViewModels.Collections;

namespace GoldSim.Web.Models.ContentTypes {

  /*============================================================================================================================
  | VIEW MODEL: HOME TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about a <c>Home</c> topic.
  /// </summary>
  public record HomeTopicViewModel: PageTopicViewModel {

    /*==========================================================================================================================
    | INTRODUCTION
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the introductory text to display at the top of the page.
    /// </summary>
    public string Introduction { get; set; }

    /*==========================================================================================================================
    | RELATIONSHIP: APPLICATIONS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a list of <see cref="ApplicationContainerTopicViewModel"/>s, each of which contain a list of <see cref=
    ///   "ApplicationPageTopicViewModel"/>s to be displayed on the homepage.
    /// </summary>
    [Include(AssociationTypes.Children)]
    public TopicViewModelCollection<ApplicationContainerTopicViewModel> Applications { get; } = new();

  } // Class
} // Namespace