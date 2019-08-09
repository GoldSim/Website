/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoldSim.Web.Models.Forms.BindingModels {

  /*============================================================================================================================
  | BINDING MODEL: REQUEST A DEMO FORM
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed binding model representing the Request a Demo(nstration) form.
  /// </summary>
  public class DemoFormBindingModel : ExtendedProfile {

    /*==========================================================================================================================
    | PROPERTY: OTHER TOOLS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets what other risk analysis tools the user is currently using or is evaluating.
    /// </summary>
    [Required]
    [StringLength(1000)]
    [Display(Name="*What other risk analysis tools do you use, or are evaluating ?")]
    public string OtherTools { get; set; }

  }

}