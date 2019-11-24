/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using Ignia.Topics;
using Ignia.Topics.AspNetCore.Mvc.Controllers;
using Ignia.Topics.Mapping;
using Ignia.Topics.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace GoldSim.Web.Controllers {

  /*============================================================================================================================
  | CLASS: LICENSES CONTROLLER
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to GoldSim data reporting routes.
  /// </summary>
  [Authorize]
  public class LicensesController : TopicController {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private     readonly        ITopicExportService     _topicExportService     = null;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="LicensesController"/> with necessary dependencies.
    /// </summary>
    public LicensesController(
      ITopicRepository          topicRepository,
      ITopicMappingService      topicMappingService,
      ITopicExportService       topicExportService
    ) : base(
      topicRepository,
      topicMappingService
    ) {
      _topicExportService       = topicExportService?? throw new ArgumentNullException(nameof(topicExportService));
    }

    /*==========================================================================================================================
    | EXPORT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a downloadable file stream containing the Excel spreadsheet report for license request data.
    /// </summary>
    public FileStreamResult Export() {

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish variables
      \-----------------------------------------------------------------------------------------------------------------------*/
      var licenseRequestContainer       = TopicRepository.Load("Root:Licenses:Requests")?.Children;
      var validContentTypes             = new string[] { "TrialForm", "InstructorAcademicForm", "StudentAcademicForm"};
      var licenseRequests               = licenseRequestContainer.Where(topic => validContentTypes.Contains(topic.ContentType));
      var memoryStream                  = _topicExportService.Export(licenseRequests);

      /*------------------------------------------------------------------------------------------------------------------------
      | Return the Excel spreadsheet as a file stream
      \-----------------------------------------------------------------------------------------------------------------------*/
      return File(memoryStream, _topicExportService.MimeType, "MultipleFreeLicenses" + _topicExportService.FileExtension);

    }

    /*==========================================================================================================================
    | DELETE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Deletes selected licenses.
    /// </summary>
    [HttpPost]
    public IActionResult Delete(int[] topics) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Delete topics
      \-----------------------------------------------------------------------------------------------------------------------*/
      foreach (var topicId in topics) {
        var topic = TopicRepository.Load(topicId);
        TopicRepository.Delete(topic);
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Return default view
      \-----------------------------------------------------------------------------------------------------------------------*/
      return RedirectToAction(nameof(Index));

    }

  } //Class
} //Namespace