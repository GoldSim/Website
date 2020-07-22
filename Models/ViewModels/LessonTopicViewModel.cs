/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using OnTopic.Mapping.Annotations;
using OnTopic.ViewModels;

namespace GoldSim.Web.Models.ViewModels {

  /*============================================================================================================================
  | VIEW MODEL: LESSON TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about a <c>Lesson</c> topic.
  /// </summary>
  public class LessonTopicViewModel: PageTopicViewModel {

    /*==========================================================================================================================
    | UNIT
    \-------------------------------------------------------------------------------------------------------------------------*/
    [AttributeKey("Parent")]
    [Follow(Relationships.Parents)]
    public UnitTopicViewModel Unit { get; set; }

  } // Class
} // Namespace