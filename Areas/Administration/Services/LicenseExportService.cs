/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System.Data;
using System.Drawing;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OnTopic;

namespace GoldSim.Web.Areas.Administration.Services {

  /*============================================================================================================================
  | CLASS: LICENSE EXPORT SERVICE
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides utility methods for assembling Excel spreadsheet reports for GoldSim data.
  /// </summary>
  internal sealed class LicenseExportService : ITopicExportService {

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
      using var excelPackage    = new ExcelPackage();

      /*------------------------------------------------------------------------------------------------------------------------
      | Create the worksheet
      \-----------------------------------------------------------------------------------------------------------------------*/
      var worksheet             = excelPackage.Workbook.Worksheets.Add("Entitlements");

      /*------------------------------------------------------------------------------------------------------------------------
      | Get and load the data from the License Request DataTable
      \-----------------------------------------------------------------------------------------------------------------------*/
      using var requests        = GetLicenseRequestData(topics);
      using var headers         = worksheet.Cells[1, 1, 1, requests.Columns.Count];
      worksheet.Cells.LoadFromDataTable(requests, true);

      /*------------------------------------------------------------------------------------------------------------------------
      | Format the column headers
      \-----------------------------------------------------------------------------------------------------------------------*/
      var headerRowBackgroundColor = ColorTranslator.FromHtml("#d1d1d1");

      headers.Style.Font.Color.SetColor(Color.Black);
      headers.Style.Font.Bold = true;
      headers.Style.Fill.PatternType = ExcelFillStyle.Solid;
      headers.Style.Fill.BackgroundColor.SetColor(headerRowBackgroundColor);
      headers.Style.WrapText = false;

      worksheet.View.FreezePanes(2, 2);

      /*------------------------------------------------------------------------------------------------------------------------
      | Set the font for the worksheet
      \-----------------------------------------------------------------------------------------------------------------------*/
      worksheet.Cells[worksheet.Dimension.Address].Style.Font.Name = "Calibri";
      worksheet.Cells[worksheet.Dimension.Address].Style.Font.Size = 11;

      /*------------------------------------------------------------------------------------------------------------------------
      | Auto-fit data rows to their contents
      \-----------------------------------------------------------------------------------------------------------------------*/
      worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

      /*----------------------------------------------------------------------------------------------------------------------
      | Set column filters and give the Free Type column extra width to account for the filter
      \---------------------------------------------------------------------------------------------------------------------*/
      headers.AutoFilter        = true;

      for (var i = 1; i <= headers.Columns; i++) {
        var column              = worksheet.Column(i);
        column.Width            += 2;
      }

      /*----------------------------------------------------------------------------------------------------------------------
      | Apply the spreadsheet to the stream
      \---------------------------------------------------------------------------------------------------------------------*/
      return new(excelPackage.GetAsByteArray());

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
      var requestData           = new DataTable();

      /*--------------------------------------------------------------------------------------------------------------------------
      | Set up column headers
      \-------------------------------------------------------------------------------------------------------------------------*/
      requestData.Columns.Add("Email Address",                  typeof(string));
      requestData.Columns.Add("First Name",                     typeof(string));
      requestData.Columns.Add("Last Name",                      typeof(string));
      requestData.Columns.Add("Company Name",                   typeof(string));
      requestData.Columns.Add("Product",                        typeof(string));
      requestData.Columns.Add("Should Email?",                  typeof(string));
      requestData.Columns.Add("Account ID",                     typeof(string));
      requestData.Columns.Add("Expiration Date",                typeof(string));
      requestData.Columns.Add("Free Type",                      typeof(string));
      requestData.Columns.Add("Department",                     typeof(string));
      requestData.Columns.Add("Address",                        typeof(string));
      requestData.Columns.Add("City",                           typeof(string));
      requestData.Columns.Add("State",                          typeof(string));
      requestData.Columns.Add("Postal",                         typeof(string));
      requestData.Columns.Add("Country",                        typeof(string));
      requestData.Columns.Add("Phone",                          typeof(string));
      requestData.Columns.Add("Focus Area",                     typeof(string));
      requestData.Columns.Add("Referral Source",                typeof(string));
      requestData.Columns.Add("Referral Details",               typeof(string));
      requestData.Columns.Add("Problem Description",            typeof(string));
      requestData.Columns.Add("Existing Tools Description",     typeof(string));
      requestData.Columns.Add("Sponsor First Name",             typeof(string));
      requestData.Columns.Add("Sponsor Last Name",              typeof(string));
      requestData.Columns.Add("Sponsor Department",             typeof(string));
      requestData.Columns.Add("Sponsor Email",                  typeof(string));
      requestData.Columns.Add("Sponsor Phone",                  typeof(string));

      /*--------------------------------------------------------------------------------------------------------------------------
      | Set row data
      \-------------------------------------------------------------------------------------------------------------------------*/
      foreach (var request in licenseRequests) {

        // Determine type.
        var isTrial             = request.ContentType.StartsWith("Trial", StringComparison.InvariantCultureIgnoreCase);

        // Determine part number.
        var partNumber          = (isTrial? "LMTD-" : "ACAD-")
                                + (requestedModule(request, "DistributedProcessing")? "DP-" : "00-")
                                + (requestedModule(request, "Reliability")? "RL-" : "00-")
                                + (
                                    requestedModule(request, "ContaminantTransport")? "CT-" :
                                    requestedModule(request, "RadionuclideTransport")? "RT-" :
                                    "00-"
                                  )
                                + (isTrial? "V." : "A.")
                                + "15.0";

        //Define composite street address
        var street1             = request.Attributes.GetValue("Street1", "");
        var street2             = request.Attributes.GetValue("Street2", "");
        var address             = street1 + (!String.IsNullOrWhiteSpace(street2)? $", {street2}" : "");

        // Add data row for each request
        requestData.Rows.Add(
          request.Attributes.GetValue("Email", ""),
          request.Attributes.GetValue("FirstName", ""),
          request.Attributes.GetValue("LastName", ""),
          request.Attributes.GetValue("Organization", ""),
          partNumber,
          "TRUE",
          "",
          "",
          isTrial? "Trial" : "Academic",
          request.Attributes.GetValue("Department", ""),
          address,
          request.Attributes.GetValue("City", ""),
          request.Attributes.GetValue("Province", ""),
          request.Attributes.GetValue("PostalCode", ""),
          request.Attributes.GetValue("Country", ""),
          request.Attributes.GetValue("PhoneNumber", ""),
          request.Attributes.GetValue("AreaOfFocus", ""),
          request.Attributes.GetValue("ReferralSource", ""),
          request.Attributes.GetValue("ReferralDetails", ""),
          request.Attributes.GetValue("ProblemStatement", ""),
          request.Attributes.GetValue("OtherTools", ""),
          request.Attributes.GetValue("SponsorFirstName", ""),
          request.Attributes.GetValue("SponsorLastName", ""),
          request.Attributes.GetValue("SponsorOrganization", ""),
          request.Attributes.GetValue("SponsorEmail", ""),
          request.Attributes.GetValue("SponsorPhoneNumber", "")
        );

      }

      /*--------------------------------------------------------------------------------------------------------------------------
      | Return DataTable
      \-------------------------------------------------------------------------------------------------------------------------*/
      return requestData;

      /*--------------------------------------------------------------------------------------------------------------------------
      | Local Functions
      \-------------------------------------------------------------------------------------------------------------------------*/
      static bool requestedModule(Topic request, string module) => request.Attributes.GetBoolean($"Modules{module}");

    }

  } //Class
} //Namespace