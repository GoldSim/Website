/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System.Collections.ObjectModel;

namespace GoldSim.Web.Administration.Models.Licenses {

  /*============================================================================================================================
  | CLASS: LICENSE ADMINISTRATION (VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   A view model for the license reporting tool.
  /// </summary>
  public record LicenseAdministrationTopicViewModel: PageTopicViewModel {

    /*==========================================================================================================================
    | REQUESTS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Maps topics under the current container which can be converted to a <see cref="LicenseRequestTopicViewModel"/>.
    /// </summary>
    [AttributeKey("Children")]
    public Collection<LicenseRequestTopicViewModel> Requests { get; } = new();

  } // Class
} // Namespace