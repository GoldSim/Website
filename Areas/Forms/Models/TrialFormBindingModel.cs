/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using GoldSim.Web.Areas.Forms.Models.Partials;

namespace GoldSim.Web.Areas.Forms.Models {

  /*============================================================================================================================
  | BINDING MODEL: REQUEST A TRIAL FORM
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed binding model representing the Request a Trial form.
  /// </summary>
  [ModelMetadataType(typeof(FormFields))]
  public sealed record TrialFormBindingModel : ExtendedProfile {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="TrialFormBindingModel"/> object.
    /// </summary>
    public TrialFormBindingModel() {
    }

    /*==========================================================================================================================
    | PROPERTY: JOB TITLE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <inheritdoc cref="FormFields.JobTitle"/>
    public string JobTitle { get; init; }

    /*==========================================================================================================================
    | PROPERTY: PROVINCE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <inheritdoc cref="FormFields.Province"/>
    /// <remarks>
    ///   This cannot be defined on <see cref="Contact"/> because that causes a conflict with <see cref="Address.Province"/>.
    /// </remarks>
    public string Province { get; set; }

    /*==========================================================================================================================
    | PROPERTY: IS TRAINING COURSE?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets whether the user is using the trial for a training course; if so, the <see cref="Trainer"/> contact
    ///   information is enabled on the form.
    /// </summary>
    [Display(Name="Are you using this Trial Version for a training course?")]
    public bool IsTrainingCourse { get; set; }

    /*==========================================================================================================================
    | PROPERTY: TRAINER
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Optional. Gets or sets the contact information for the user's training provider, if applicable.
    /// </summary>
    [MapToParent]
    [Display(Name="Trainer Contact Information")]
    public CoreContact Trainer { get; set; }

    /*==========================================================================================================================
    | PROPERTY: OTHER TOOLS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <inheritdoc cref="FormFields.OtherTools"/>
    public string OtherTools { get; set; }

  } //Class
} //Namespace