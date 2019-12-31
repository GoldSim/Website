/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System.Collections.Generic;
using OnTopic.Mapping.Annotations;
using OnTopic.ViewModels;

namespace GoldSim.Web.Administration.Models.Licenses {

  /*============================================================================================================================
  | CLASS: LICENSE ADMINISTRATION (VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   A view model for the license reporting tool.
  /// </summary>
  public class LicenseAdministrationTopicViewModel: PageTopicViewModel {

    /*==========================================================================================================================
    | REQUESTS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Maps topics under the current container which can be converted to a <see cref="LicenseRequestTopicViewModel"/>.
    /// </summary>
    [AttributeKey("Children")]
    public List<LicenseRequestTopicViewModel> Requests { get; set; }

  } // Class
} // Namespace