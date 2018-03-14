/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System.Web.Mvc;
using Ignia.Topics;
using Ignia.Topics.Repositories;
using Ignia.Topics.Web.Mvc;
using GoldSim.Web.App_Code;

namespace GoldSim.Web.Controllers {

  /*============================================================================================================================
  | CLASS: REPORTING CONTROLLER
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to GoldSim data reporting routes.
  /// </summary>
  public class ReportingController : TopicController {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a Reporting Controller with necessary dependencies.
    /// </summary>
    /// <returns>A topic controller for loading OnTopic views.</returns>
    public ReportingController(ITopicRepository topicRepository, Topic currentTopic) : base(topicRepository, currentTopic) {
    }

    /*==========================================================================================================================
    | LICENSE REQUESTS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a downloadable file stream containing the Excel spreadsheet report for License Request data.
    /// </summary>
    public FileStreamResult LicenseRequests() {
      var memoryStream = ExcelReporting.DownloadLicenseRequests();
      return File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "MultipleFreeLicenses.xlsx");
    }

  }

}
