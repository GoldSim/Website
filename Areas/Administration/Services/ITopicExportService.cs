/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using OnTopic;

namespace GoldSim.Web.Administration.Services {

  /*============================================================================================================================
  | INTERFACE: TOPIC EXPORT SERVICE
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Given a list of topics, will export in the format determined by the concrete implementation. Results will be returned
  ///   as a <see cref="MemoryStream"/>.
  /// </summary>
  public interface ITopicExportService {

    /*==========================================================================================================================
    | MIME TYPE
    \--------------------------------I-----------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets the mime type associated with the output file.
    /// </summary>
    string MimeType { get; }

    /*==========================================================================================================================
    | FILE EXTENSION
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets the file extension associated withoutput file.
    /// </summary>
    string FileExtension { get; }

    /*==========================================================================================================================
    | EXPORT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Returns the topics data as a memory stream for download.
    /// </summary>
    MemoryStream Export(IEnumerable<Topic> topics);

  } // Interface
} // Namespace