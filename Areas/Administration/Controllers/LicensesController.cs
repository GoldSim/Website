/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System;
using System.Linq;
using GoldSim.Web.Administration.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnTopic.AspNetCore.Mvc.Controllers;
using OnTopic.Internal.Diagnostics;
using OnTopic.Mapping;
using OnTopic.Repositories;

namespace GoldSim.Web.Controllers {

  /*============================================================================================================================
  | CLASS: LICENSES CONTROLLER
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to GoldSim data reporting routes.
  /// </summary>
  [Authorize]
  [Area("Administration")]
  public class LicensesController : TopicController {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private     readonly        ITopicExportService             _topicExportService;
    private     readonly        string                          _licenseRoot                    = "Root:Administration:Licenses";

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
      var licenseRequestContainer       = TopicRepository.Load(_licenseRoot)?.Children;
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
      | Validate input
      \-----------------------------------------------------------------------------------------------------------------------*/
      Contract.Requires(topics, nameof(topics));

      /*------------------------------------------------------------------------------------------------------------------------
      | Delete topics
      \-----------------------------------------------------------------------------------------------------------------------*/
      foreach (var topicId in topics) {
        if (topicId < 0) {
          continue;
        }
        var topic = TopicRepository.Load(topicId);
        if (!topic.GetUniqueKey().StartsWith(_licenseRoot, StringComparison.InvariantCultureIgnoreCase)) {
          continue;
        }
        TopicRepository.Delete(topic);
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Return default view
      \-----------------------------------------------------------------------------------------------------------------------*/
      return RedirectToAction(nameof(Index));

    }

  } //Class
} //Namespace