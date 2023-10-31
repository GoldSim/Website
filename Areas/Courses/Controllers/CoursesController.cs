/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using Microsoft.AspNetCore.Mvc.Filters;
using OnTopic.AspNetCore.Mvc;
using OnTopic.AspNetCore.Mvc.Controllers;
using OnTopic.Mapping;

namespace GoldSim.Web.Courses.Controllers {

  /*============================================================================================================================
  | CLASS: COURSES CONTROLLER
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides common processing for GoldSim courseware.
  /// </summary>
  [Area("Courses")]
  public class CoursesController : TopicController {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of an <see cref="CoursesController"/> with necessary dependencies.
    /// </summary>
    /// <returns>An <see cref="CoursesController"/> for loading OnTopic views.</returns>
    public CoursesController(
      ITopicRepository topicRepository,
      ITopicMappingService topicMappingService
    ) : base(
      topicRepository,
      topicMappingService
    ) {}

    /*==========================================================================================================================
    | GET: INDEX (VIEW TOPIC)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <inheritdoc />
    [HttpGet, HttpHead]
    [ValidateTopic]
    public async override Task<IActionResult> IndexAsync(string path) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Handle redirect
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (CurrentTopic.ContentType.Equals("Unit", StringComparison.OrdinalIgnoreCase)) {
        return Redirect(CurrentTopic.Children.Where(t => t.IsVisible()).FirstOrDefault().GetWebPath());
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Call base logic
      \-----------------------------------------------------------------------------------------------------------------------*/
      return await base.IndexAsync(path).ConfigureAwait(true);

    }

    /*==========================================================================================================================
    | EVENT HANDLER: ON ACTION EXECUTING
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Validates whether or not a given page should be displayed, based on the <code>IsPreview</code> attribute.
    /// </summary>
    public override void OnActionExecuting(ActionExecutingContext context) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate parameters
      \-----------------------------------------------------------------------------------------------------------------------*/
      Contract.Requires(context, nameof(context));

      /*------------------------------------------------------------------------------------------------------------------------
      | Determine if user is authenticated
      \-----------------------------------------------------------------------------------------------------------------------*/
      var isAuthenticated       = context.HttpContext.User.Identity.IsAuthenticated;

      /*------------------------------------------------------------------------------------------------------------------------
      | Handle preview areas
      >-------------------------------------------------------------------------------------------------------------------------
      | If a course is marked as preview, restrict access to authenticated users.
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (CurrentTopic is not null && CurrentTopic.Attributes.GetBoolean("IsPreview", false, true) && !isAuthenticated) {
        context.Result = new StatusCodeResult(401);
        return;
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Base processing
      \-----------------------------------------------------------------------------------------------------------------------*/
      base.OnActionExecuting(context);

    }

  } // Class
} // Namespace