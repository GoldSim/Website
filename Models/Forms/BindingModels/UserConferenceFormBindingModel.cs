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
  | BINDING MODEL: USER CONFERENCE REGISTRATION FORM
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed binding model representing the User Conference Registration form.
  /// </summary>
  public class UserConferenceFormBindingModel : ExtendedContact {

    public UserConferenceFormBindingModel() : base() {
      TrainingTopics = new AdvancedTrainingTopicsSelection();
    }

    [Display(Name="I am interested in submitting a poster.")]
    public bool WillSubmitPoster { get; set; }

    [StringLength(15)]
    [Display(Name="Purchase Order Number")]
    public string PurchaseOrderNumber { get; set; }

    [StringLength(1000)]
    [Display(Name="Additional Instructions(e.g., interest in spouse / partner attending dinners)")]
    public string AdditionalInstructions { get; set; }

    [Display(Name="Advanced Training Topics")]
    public AdvancedTrainingTopicsSelection TrainingTopics { get; }

    [Display(Name="Apply student discount")]
    public bool WithStudentDiscount { get; set; }

    [Display(Name="Basic Training and Conference(September 10 - 12): $1, 500")]
    public bool IncludeTraining { get; set; }

    [Display(Name="I would prefer a paper invoice or receipt.")]
    public bool WithPaperReceipt { get; set; }

  }

}