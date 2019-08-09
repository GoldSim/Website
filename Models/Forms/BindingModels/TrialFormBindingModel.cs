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

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="TrialFormBindingModel"/> object.
    /// </summary>
    public TrialFormBindingModel() {
      Trainer=new CoreContact();
    }

    /*==========================================================================================================================
    | PROPERTY: TRAINER
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Optional. Gets or sets the contact information for the user's training provider, if applicable.
    /// </summary>
    [Display(Name ="Trainer Contact Information")]
    public CoreContact Trainer { get; }

    /*==========================================================================================================================
    | PROPERTY: OTHER TOOLS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets what other risk analysis tools the user is currently using or is evaluating.
    /// </summary>
    [Required]
    [StringLength(1000)]
    [Display(Name ="*What other risk analysis tools do you use, or are evaluating ?")]
    public string OtherTools { get; set; }

  }

}