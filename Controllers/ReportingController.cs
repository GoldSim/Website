/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System;
using System.Web.Mvc;
using Ignia.Topics;
using Ignia.Topics.Repositories;
using Ignia.Topics.Web.Mvc;
using GoldSim.Web;
using System.Linq;

namespace GoldSim.Web.Controllers {

  /*============================================================================================================================
  | CLASS: REPORTING CONTROLLER
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to GoldSim data reporting routes.
  /// </summary>
  public class ReportingController : Controller {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private     readonly        ITopicRepository        _topicRepository        = null;
    private     readonly        IReportingService       _reportingService       = null;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a Reporting Controller with necessary dependencies.
    /// </summary>
    public ReportingController(ITopicRepository topicRepository, IReportingService reportingService) : base() {
      _topicRepository          = topicRepository ?? throw new ArgumentNullException("topicRepository");
      _reportingService         = reportingService ?? throw new ArgumentNullException("reportingService");
    }

    /*==========================================================================================================================
    | LICENSE REQUESTS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a downloadable file stream containing the Excel spreadsheet report for License Request data.
    /// </summary>
    public FileStreamResult LicenseRequests() {

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish variables
      \-----------------------------------------------------------------------------------------------------------------------*/
      var licenseRequestContainer       = _topicRepository.Load().GetTopic("Root:CustomerRequests:Licenses");
      var licenseRequests               = licenseRequestContainer.Children.Where(topic => topic.ContentType == "AcademicRequest" || topic.ContentType == "EvaluationRequest");
      var memoryStream                  = _reportingService.GetLicenseRequests(licenseRequests);

      /*------------------------------------------------------------------------------------------------------------------------
      | Return the Excel spreadsheet as a file stream
      \-----------------------------------------------------------------------------------------------------------------------*/
      return File(memoryStream, _reportingService.MimeType, "MultipleFreeLicenses" + _reportingService.FileExtension);

    }

  }

}
