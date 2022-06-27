/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System.Data;
using System.Drawing;
using System.Globalization;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OnTopic;
using OnTopic.Internal.Diagnostics;

namespace GoldSim.Web.Administration.Services {

  /*============================================================================================================================
  | CLASS: LICENSE EXPORT SERVICE
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides utility methods for assembling Excel spreadsheet reports for GoldSim data.
  /// </summary>
  public class LicenseExportService : ITopicExportService {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    public LicenseExportService() {
    }

    /*==========================================================================================================================
    | MIME TYPE
    \-------------------------------------------------------------------------------------------------------------------------*/
    public string MimeType => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

    /*==========================================================================================================================
    | FILE EXTENSION
    \-------------------------------------------------------------------------------------------------------------------------*/
    public string FileExtension => ".xlsx";

    /*==========================================================================================================================
    | EXPORT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Assembles an Excel spreadsheet with pending License Request data as a memory stream.
    /// </summary>
    /// <remarks>
    ///   Makes use of Jan Kallman's <see href="https://github.com/JanKallman/EPPlus">EPPlus</see> OfficeOpenXML wrapping
    ///   library.
    /// </remarks>
    /// <returns>The memory stream representing the spreadsheet.</returns>
    public MemoryStream Export(IEnumerable<Topic> topics) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate input
      \-----------------------------------------------------------------------------------------------------------------------*/
      Contract.Requires(topics, nameof(topics));

      /*------------------------------------------------------------------------------------------------------------------------
      | Assemble Excel
      \-----------------------------------------------------------------------------------------------------------------------*/
      MemoryStream memoryStream;

      using (var excelPackage = new ExcelPackage()) {

        /*----------------------------------------------------------------------------------------------------------------------
        | Create the worksheet
        \---------------------------------------------------------------------------------------------------------------------*/
        var worksheet = excelPackage.Workbook.Worksheets.Add("Working");

        /*----------------------------------------------------------------------------------------------------------------------
        | Get and load the data from the License Request DataTable
        \---------------------------------------------------------------------------------------------------------------------*/
        using var licenseRequestData = GetLicenseRequestData(topics);
        worksheet.Cells.LoadFromDataTable(licenseRequestData, true);

        /*----------------------------------------------------------------------------------------------------------------------
        | Format the column headers
        \---------------------------------------------------------------------------------------------------------------------*/
        var headerRowBackgroundColor = ColorTranslator.FromHtml("#404040");
        using (var cellRange = worksheet.Cells[1, 1, 1, 24]) {
          cellRange.Style.Font.Color.SetColor(Color.White);
          cellRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
          cellRange.Style.Fill.BackgroundColor.SetColor(headerRowBackgroundColor);
          cellRange.Style.WrapText = true;
        }
        worksheet.View.FreezePanes(2, 1);

        /*----------------------------------------------------------------------------------------------------------------------
        | Set the font for the worksheet
        \---------------------------------------------------------------------------------------------------------------------*/
        worksheet.Cells[worksheet.Dimension.Address].Style.Font.Name = "Tahoma";
        worksheet.Cells[worksheet.Dimension.Address].Style.Font.Size = 10;

        /*----------------------------------------------------------------------------------------------------------------------
        | Auto-fit data rows to their contents
        \---------------------------------------------------------------------------------------------------------------------*/
        worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

        /*----------------------------------------------------------------------------------------------------------------------
        | Set column filters and give the Free Type column extra width to account for the filter
        \---------------------------------------------------------------------------------------------------------------------*/
        worksheet.Cells["E1:F1"].AutoFilter     = true;
        worksheet.Column(5).Width               = 12;

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
    private static DataTable GetLicenseRequestData(IEnumerable<Topic> licenseRequests) {

      /*--------------------------------------------------------------------------------------------------------------------------
      | Establish DataTable
      \-------------------------------------------------------------------------------------------------------------------------*/
      var licenseRequestData = new DataTable();

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
      foreach (var licenseRequest in licenseRequests) {

        // Set variable values
        var requestType = licenseRequest.ContentType.StartsWith("Trial", StringComparison.InvariantCultureIgnoreCase) ? "Trial" : "Academic";
        var productOptionConfiguration  = 1;

        // Determine Product Option configuration
        if (requestedModules("Reliability", "DistributedProcessing", "RadionuclideTransport")) {
          productOptionConfiguration    = 12;
        }
        else if (requestedModules("Reliability", "DistributedProcessing", "ContaminantTransport")) {
          productOptionConfiguration    = 11;
        }
        else if (requestedModules("Reliability", "RadionuclideTransport")) {
          productOptionConfiguration    = 10;
        }
        else if (requestedModules("Reliability", "ContaminantTransport")) {
          productOptionConfiguration    = 9;
        }
        else if (requestedModules("DistributedProcessing", "RadionuclideTransport")) {
          productOptionConfiguration    = 8;
        }
        else if (requestedModules("DistributedProcessing", "ContaminantTransport")) {
          productOptionConfiguration    = 7;
        }
        else if (requestedModules("DistributedProcessing", "Reliability")) {
          productOptionConfiguration    = 6;
        }
        else if (requestedModules("RadionuclideTransport")) {
          productOptionConfiguration    = 5;
        }
        else if (requestedModules("ContaminantTransport")) {
          productOptionConfiguration    = 4;
        }
        else if (requestedModules("Reliability")) {
          productOptionConfiguration    = 3;
        }
        else if (requestedModules("DistributedProcessing")) {
          productOptionConfiguration    = 2;
        }

        bool requestedModules(params string[] moduleList)
          => moduleList.All(m => licenseRequest.Attributes.GetBoolean($"Modules{m}", false));

        //Define composite street address
        var street1 = licenseRequest.Attributes.GetValue("Street1", "");
        var street2 = licenseRequest.Attributes.GetValue("Street2", "");
        var address = street1;

        if (!String.IsNullOrWhiteSpace(street2)) {
          address += $", {street2}";
        }

        // Add data row for each request
        licenseRequestData.Rows.Add(
          licenseRequest.Attributes.GetValue("Email", ""),
          licenseRequest.Attributes.GetValue("FirstName", ""),
          licenseRequest.Attributes.GetValue("LastName", ""),
          licenseRequest.Attributes.GetValue("Organization", ""),
          requestType,
          "Config_" + productOptionConfiguration.ToString(CultureInfo.InvariantCulture),
          "TRUE",
          licenseRequest.Attributes.GetValue("Department", ""),
          address,
          licenseRequest.Attributes.GetValue("City", ""),
          licenseRequest.Attributes.GetValue("Province", ""),
          licenseRequest.Attributes.GetValue("PostalCode", ""),
          licenseRequest.Attributes.GetValue("Country", ""),
          licenseRequest.Attributes.GetValue("PhoneNumber", ""),
          licenseRequest.Attributes.GetValue("AreaOfFocus", ""),
          licenseRequest.Attributes.GetValue("ReferralSource", ""),
          licenseRequest.Attributes.GetValue("ReferralDetails", ""),
          licenseRequest.Attributes.GetValue("ProblemStatement", ""),
          licenseRequest.Attributes.GetValue("OtherTools", ""),
          licenseRequest.Attributes.GetValue("SponsorFirstName", ""),
          licenseRequest.Attributes.GetValue("SponsorLastName", ""),
          licenseRequest.Attributes.GetValue("SponsorOrganization", ""),
          licenseRequest.Attributes.GetValue("SponsorEmail", ""),
          licenseRequest.Attributes.GetValue("SponsorPhoneNumber", "")
        );

      }

      /*--------------------------------------------------------------------------------------------------------------------------
      | Return DataTable
      \-------------------------------------------------------------------------------------------------------------------------*/
      return licenseRequestData;

    }

  } //Class
} //Namespace