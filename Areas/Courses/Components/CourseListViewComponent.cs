/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       GoldSim Website
\=============================================================================================================================*/
using GoldSim.Web.Courses.Models;
using OnTopic;
using OnTopic.Mapping.Hierarchical;

namespace GoldSim.Web.Courses.Components {

  /*============================================================================================================================
  | CLASS: COURSE LIST VIEW COMPONENT
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Defines a <see cref="ViewComponent"/> which provides access to a menu of <see cref="TrackedNavigationTopicViewModel"/>
  ///   instances representing the children of the current <see cref="Topic"/>.
  /// </summary>
  /// <remarks>
  ///   In addition to <i>displaying</i> the navigation, this view component will <i>track</i> the users progress through them,
  ///   by evaluating a cookie set by the <see cref="LessonListViewComponent"/> to conditionally set a property on the
  ///   corresponding <see cref="TrackedNavigationTopicViewModel"/>.
  /// </remarks>
  public class CourseListViewComponent: UnitListViewComponent {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="CourseListViewComponent"/> with necessary dependencies.
    /// </summary>
    public CourseListViewComponent(
      ITopicRepository topicRepository,
      IHierarchicalTopicMappingService<TrackedNavigationTopicViewModel> hierarchicalTopicMappingService
    ) : base(
      topicRepository,
      hierarchicalTopicMappingService
    ) { }

  } //Class
} //Namespace