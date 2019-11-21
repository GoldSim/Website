/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System.Collections.Generic;
using Ignia.Topics.Mapping.Annotations;
using Ignia.Topics.ViewModels;

namespace GoldSim.Web.Models.Licenses {

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
    [Flatten]
    [Follow(Relationships.Children)]
    public List<LicenseRequestTopicViewModel> Requests { get; set; }

  } // Class
} // Namespace