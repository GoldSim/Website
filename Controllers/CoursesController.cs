/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnTopic.AspNetCore.Mvc.Controllers;
using OnTopic.Mapping;
using OnTopic.Repositories;

namespace GoldSim.Web.Controllers {

  /*============================================================================================================================
  | CLASS: COURSES CONTROLLER
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides common processing for GoldSim courseware.
  /// </summary>
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
      return await base.IndexAsync(path);

    }

  } // Class
} // Namespace