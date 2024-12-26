/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System.Collections.ObjectModel;

namespace GoldSim.Web.Areas.Administration.Models.Licenses {

  /*============================================================================================================================
  | CLASS: LICENSE ADMINISTRATION (VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   A view model for the license reporting tool.
  /// </summary>
  internal sealed record LicenseAdministrationTopicViewModel: PageTopicViewModel {

    /*==========================================================================================================================
    | REQUESTS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Maps topics under the current container which can be converted to a <see cref="LicenseRequestTopicViewModel"/>.
    /// </summary>
    [AttributeKey("Children")]
    internal Collection<LicenseRequestTopicViewModel> Requests { get; } = [];

  } // Class
} // Namespace