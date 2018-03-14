/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using Ignia.Topics;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace GoldSim.Web {

  /*============================================================================================================================
  | CLASS: EXCEL REPORTING SERVICE
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides utility methods for assembling Excel spreadsheet reports for GoldSim data.
  /// </summary>
  public class ExcelReportingService : IReportingService {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    public ExcelReportingService() {
    }

    /*==========================================================================================================================
    | MIME TYPE
    \-------------------------------------------------------------------------------------------------------------------------*/
    public string MimeType {
      get {
        return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
      }
    }

    /*==========================================================================================================================
    | FILE EXTENSION
    \-------------------------------------------------------------------------------------------------------------------------*/
    public string FileExtension {
      get {
        return ".xlsx";
      }
    }

    /*==========================================================================================================================
    | DOWNLOAD LICENSE REQUESTS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Assembles an Excel spreadsheet with pending License Request data as a memory stream.
    /// </summary>
    /// <remarks>
    ///   Makes use of Jan Kallman's <see href="https://github.com/JanKallman/EPPlus">EPPlus</see> OfficeOpenXML wrapping
    ///   library.
    /// </remarks>
    /// <returns>The memory stream representing the spreadsheet.</returns>
    public MemoryStream GetLicenseRequests(IEnumerable<Topic> licenseRequests) {
      MemoryStream memoryStream;

      using (ExcelPackage excelPackage = new ExcelPackage()) {

        /*----------------------------------------------------------------------------------------------------------------------
        | Create the worksheet
        \---------------------------------------------------------------------------------------------------------------------*/
        ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Working");

        /*----------------------------------------------------------------------------------------------------------------------
        | Get and load the data from the License Request DataTable
        \---------------------------------------------------------------------------------------------------------------------*/
        DataTable licenseRequestData = GetLicenseRequestData(licenseRequests);
        worksheet.Cells.LoadFromDataTable(licenseRequestData, true);

        /*----------------------------------------------------------------------------------------------------------------------
        | Format the column headers
        \---------------------------------------------------------------------------------------------------------------------*/
        using (var cellRange = worksheet.Cells[1, 1, 1, 24]) {
          cellRange.Style.Font.Bold = true;
          cellRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
          cellRange.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
          cellRange.Style.WrapText = true;
        }
        worksheet.View.FreezePanes(2, 1);

        /*----------------------------------------------------------------------------------------------------------------------
        | Auto-fit data rows to their contents
        \---------------------------------------------------------------------------------------------------------------------*/
        worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

        /*----------------------------------------------------------------------------------------------------------------------
        | Apply the spreadsheet to the stream
        \---------------------------------------------------------------------------------------------------------------------*/
        memoryStream = new MemoryStream(excelPackage.GetAsByteArray());

      }

      return memoryStream;
    }

    /*============================================================================================================================
    | GET LICENSE REQUEST DATA
    \---------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Creates a DataTable with columns corresponding to Evaluation and Academic Request Attributes, fills the table with
    ///   pending License Request Topics data.
    /// </summary>
    private DataTable GetLicenseRequestData(IEnumerable<Topic> licenseRequests) {

      /*--------------------------------------------------------------------------------------------------------------------------
      | Establish DataTable
      \-------------------------------------------------------------------------------------------------------------------------*/
      DataTable licenseRequestData = new DataTable();

      /*--------------------------------------------------------------------------------------------------------------------------
      | Set up column headers
      \-------------------------------------------------------------------------------------------------------------------------*/
      licenseRequestData.Columns.Add("Email Address", typeof(string));
      licenseRequestData.Columns.Add("First Name", typeof(string));
      licenseRequestData.Columns.Add("Last Name", typeof(string));
      licenseRequestData.Columns.Add("Company Name", typeof(string));
      licenseRequestData.Columns.Add("Free Type", typeof(string));
      licenseRequestData.Columns.Add("Product Option", typeof(string));
      licenseRequestData.Columns.Add("Should Email?", typeof(string));
      licenseRequestData.Columns.Add("Department", typeof(string));
      licenseRequestData.Columns.Add("Address", typeof(string));
      licenseRequestData.Columns.Add("City", typeof(string));
      licenseRequestData.Columns.Add("State", typeof(string));
      licenseRequestData.Columns.Add("Postal", typeof(string));
      licenseRequestData.Columns.Add("Country", typeof(string));
      licenseRequestData.Columns.Add("Phone", typeof(string));
      licenseRequestData.Columns.Add("Focus Area", typeof(string));
      licenseRequestData.Columns.Add("Referral Source", typeof(string));
      licenseRequestData.Columns.Add("Referral Details", typeof(string));
      licenseRequestData.Columns.Add("Problem Description", typeof(string));
      licenseRequestData.Columns.Add("Existing Tools Description", typeof(string));
      licenseRequestData.Columns.Add("Sponsor First Name", typeof(string));
      licenseRequestData.Columns.Add("Sponsor Last Name", typeof(string));
      licenseRequestData.Columns.Add("Sponsor Department", typeof(string));
      licenseRequestData.Columns.Add("Sponsor Email", typeof(string));
      licenseRequestData.Columns.Add("Sponsor Phone", typeof(string));

      /*--------------------------------------------------------------------------------------------------------------------------
      | Set row data
      \-------------------------------------------------------------------------------------------------------------------------*/
      foreach (Topic licenseRequest in licenseRequests) {

        // Set variable values
        string requestType = (licenseRequest.ContentType.StartsWith("eval", StringComparison.InvariantCultureIgnoreCase) ? "Evaluation" : "Academic");
        string productOption = "Config_1";
        if (licenseRequest.Attributes.GetValue("RL") == "True" && licenseRequest.Attributes.GetValue("CT") == "True") {
          productOption = "Config_9";
        }
        else if (licenseRequest.Attributes.GetValue("RL") == "True") {
          productOption = "Config_3";
        }
        else if (licenseRequest.Attributes.GetValue("CT") == "True") {
          productOption = "Config_4";
        }

        // Add data row for each request
        licenseRequestData.Rows.Add(
          licenseRequest.Attributes.GetValue("Email", ""),
          licenseRequest.Attributes.GetValue("FirstName", ""),
          licenseRequest.Attributes.GetValue("LastName", ""),
          licenseRequest.Attributes.GetValue("Organization", ""),
          requestType,
          productOption,
          "TRUE",
          licenseRequest.Attributes.GetValue("Department", ""),
          (licenseRequest.Attributes.GetValue("Address1", "") + (!String.IsNullOrEmpty(licenseRequest.Attributes.GetValue("Address2", "")) ? ", " + licenseRequest.Attributes.GetValue("Address2", "") : "")),
          licenseRequest.Attributes.GetValue("City", ""),
          licenseRequest.Attributes.GetValue("State", ""),
          licenseRequest.Attributes.GetValue("Postal", ""),
          licenseRequest.Attributes.GetValue("CountryList", ""),
          licenseRequest.Attributes.GetValue("Phone", ""),
          licenseRequest.Attributes.GetValue("AreaOfFocusList", ""),
          licenseRequest.Attributes.GetValue("ReferralSelectionList", ""),
          licenseRequest.Attributes.GetValue("ReferralDetails", ""),
          licenseRequest.Attributes.GetValue("ProblemDescription", ""),
          licenseRequest.Attributes.GetValue("ExistingToolsDescription", ""),
          licenseRequest.Attributes.GetValue("SponsorFirstName", ""),
          licenseRequest.Attributes.GetValue("SponsorLastName", ""),
          licenseRequest.Attributes.GetValue("SponsorDepartment", ""),
          licenseRequest.Attributes.GetValue("SponsorEmail", ""),
          licenseRequest.Attributes.GetValue("SponsorPhone", "")
        );

      }

      /*--------------------------------------------------------------------------------------------------------------------------
      | Return DataTable
      \-------------------------------------------------------------------------------------------------------------------------*/
      return licenseRequestData;

    }

  }

}
