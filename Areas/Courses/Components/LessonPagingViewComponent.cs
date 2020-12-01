/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       GoldSim Website
\=============================================================================================================================*/
using System.Linq;
using System.Threading.Tasks;
using GoldSim.Web.Courses.Models;
using Microsoft.AspNetCore.Mvc;
using OnTopic;
using OnTopic.AspNetCore.Mvc;
using OnTopic.Mapping;
using OnTopic.Repositories;

namespace GoldSim.Web.Courses.Components {

  /*============================================================================================================================
  | CLASS: LESSON PAGING VIEW COMPONENT
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Defines a <see cref="ViewComponent"/> which provides access to next buttons for navigating between lessons.
  /// </summary>
  public class LessonPagingViewComponent: ViewComponent {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private                     Topic                           _currentTopic;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="LessonPagingViewComponent"/> with necessary dependencies.
    /// </summary>
    public LessonPagingViewComponent(
      ITopicRepository topicRepository,
      ITopicMappingService topicMappingService
    ) {
      TopicRepository           = topicRepository;
      TopicMappingService       = topicMappingService;
    }

    /*==========================================================================================================================
    | TOPIC REPOSITORY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a reference to the <see cref="ITopicRepository"/> in order to allow the current topic to be identified based
    ///   on the route data.
    /// </summary>
    /// <returns>
    ///   The <see cref="ITopicRepository"/> associated with the <see cref="LessonPagingViewComponent"/>.
    /// </returns>
    protected ITopicRepository TopicRepository { get; }

    /*==========================================================================================================================
    | TOPIC MAPPING SERVICE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a reference to the <see cref="ITopicMappingService"/> in order to allow the target topic to be mapped to an
    ///   appropriate view model.
    /// </summary>
    /// <returns>
    ///   The <see cref="ITopicMappingService"/> associated with the <see cref="LessonPagingViewComponent"/>.
    /// </returns>
    protected ITopicMappingService TopicMappingService { get; }

    /*==========================================================================================================================
    | CURRENT TOPIC
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a reference to the current topic associated with the request.
    /// </summary>
    /// <returns>The Topic associated with the current request.</returns>
    protected Topic CurrentTopic {
      get {
        if (_currentTopic is null) {
          _currentTopic = TopicRepository.Load(RouteData);
        }
        return _currentTopic;
      }
    }

    /*==========================================================================================================================
    | METHOD: INVOKE (ASYNC)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the pagel-level navigation menu for the current page, which exposes one tier of navigation from the nearest
    ///   page group.
    /// </summary>
    public async Task<IViewComponentResult> InvokeAsync(bool moveNext) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Identify adjacent topic
      \-----------------------------------------------------------------------------------------------------------------------*/
      var adjacentTopic         = GetAdjacentTopic(CurrentTopic.Parent, CurrentTopic, moveNext);
      var label                 = moveNext? "Next Lesson" : "Previous Lesson";

      /*------------------------------------------------------------------------------------------------------------------------
      | Fallback to adjacent unit
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (adjacentTopic is null) {
        var adjacentUnit        = GetAdjacentTopic(CurrentTopic.Parent.Parent, CurrentTopic.Parent, moveNext);
        if (adjacentUnit is not null) {
          adjacentTopic         = moveNext? adjacentUnit.Children.FirstOrDefault() : adjacentUnit.Children.LastOrDefault();
          label                 = moveNext? "Next Unit" : "Previous Unit";
        }
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Fallback to course
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (adjacentTopic is null) {
        adjacentTopic           = CurrentTopic.Parent.Parent;
        label                   = moveNext? "Finish Course" : "Return to Syllabus";
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Map adjacent topic
      \-----------------------------------------------------------------------------------------------------------------------*/
      var topicViewModel        = await TopicMappingService.MapAsync<LessonPagingTopicViewModel>(adjacentTopic).ConfigureAwait(true);
      topicViewModel.Label      = label;
      topicViewModel.MoveNext   = moveNext;

      /*------------------------------------------------------------------------------------------------------------------------
      | Return the corresponding view
      \-----------------------------------------------------------------------------------------------------------------------*/
      return View(topicViewModel);

    }

    /*==========================================================================================================================
    | METHOD: GET ADJACENT TOPIC
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Given a <paramref name="rootTopic"/>, <paramref name="currentTopic"/>, and <paramref name="moveNext"/> direction,
    ///   determines the adjacent topic—if one exists.
    /// </summary>
    /// <param name="rootTopic">The root topic from which to find the adjacent topic.</param>
    /// <param name="currentTopic">The current topic from among the siblings.</param>
    /// <param name="moveNext">Determines if the next or last sibling should be selected.</param>
    private static Topic GetAdjacentTopic(Topic rootTopic, Topic currentTopic, bool moveNext) {
      var siblings              = rootTopic.Children;
      var lessonIndex            = siblings.IndexOf(currentTopic);
      var adjacentIndex         = lessonIndex + (moveNext? 1 : -1);
      var noAdjacentTopic       = adjacentIndex < 0 || adjacentIndex >= siblings.Count;
      return noAdjacentTopic    ? null : siblings[adjacentIndex];
    }

  } //Class
} //Namespace