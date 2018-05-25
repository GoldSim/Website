/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System.Collections.Generic;
using System.IO;
using Ignia.Topics;

namespace GoldSim.Web {

  /*============================================================================================================================
  | INTERFACE: REPORTING SERVICE
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Given contextual information (such as mime and file type), will determine the current reporting method to return.
  /// </summary>
  public interface IReportingService {

    /*==========================================================================================================================
    | MIME TYPE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets the mime type associated with the reporting method output file.
    /// </summary>
    string MimeType { get; }

    /*==========================================================================================================================
    | FILE EXTENSION
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets the file extension associated with the reporting method output file.
    /// </summary>
    string FileExtension { get; }

    /*==========================================================================================================================
    | GET LICENSE REQUESTS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets the License Requests Topics data as a memory stream for download.
    /// </summary>
    MemoryStream GetLicenseRequests(IEnumerable<Topic> licenseRequests);

  } // Interface

} // Namespace
