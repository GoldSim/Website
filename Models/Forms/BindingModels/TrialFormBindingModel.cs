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
  | BINDING MODEL: REQUEST A TRIAL FORM
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed binding model representing the Request a Trial form.
  /// </summary>
  public class TrialFormBindingModel : ExtendedProfile {

    public TrialFormBindingModel() {
      Trainer=new CoreContact();
    }

    [Display(Name ="Trainer Contact Information")]
    public CoreContact Trainer { get; }

    [Required]
    [StringLength(1000)]
    [Display(Name ="*What other risk analysis tools do you use, or are evaluating ?")]
    public string OtherTools { get; set; }

  }

}