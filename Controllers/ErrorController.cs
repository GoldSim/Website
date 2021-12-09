/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System.Globalization;
using OnTopic.AspNetCore.Mvc.Controllers;
using OnTopic.Mapping;

namespace GoldSim.Web.Controllers {

  /*============================================================================================================================
  | CLASS: ERROR CONTROLLER
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides common processing for all error pages.
  /// </summary>
  public class ErrorController : TopicController {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of an <see cref="ErrorController"/> with necessary dependencies.
    /// </summary>
    /// <returns>An <see cref="ErrorController"/> for loading OnTopic views.</returns>
    public ErrorController(
      ITopicRepository topicRepository,
      ITopicMappingService topicMappingService
    ) : base(
      topicRepository,
      topicMappingService
    ) {}

    /*==========================================================================================================================
    | ERROR: NOT FOUND
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Handles 404 errors.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> NotFoundAsync() {
      HttpContext.Response.StatusCode = 404;
      return await IndexAsync("NotFound").ConfigureAwait(true);
    }

    /*==========================================================================================================================
    | ERROR: UNAUTHORIZED
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Handles 401 errors.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> UnauthorizedAsync() {
      HttpContext.Response.StatusCode = 401;
      return await IndexAsync("Unauthorized").ConfigureAwait(true);
    }

    /*==========================================================================================================================
    | ERROR: INTERNAL SERVER
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Handles 500 errors.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> InternalServerAsync() {
      HttpContext.Response.StatusCode = 500;
      return await IndexAsync("InternalServer").ConfigureAwait(true);
    }

    /*==========================================================================================================================
    | ERROR: TRIGGER
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Triggers a runtime exception for the purposes of testing error responses.
    /// </summary>
    [HttpGet]
    public IActionResult Trigger(int divisor = 0) => Content((5/divisor).ToString(CultureInfo.InvariantCulture));

  } // Class
} // Namespace