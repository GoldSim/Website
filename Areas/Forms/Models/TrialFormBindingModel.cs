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
    /// <summary>
    ///   Gets or sets the user's job title.
    /// </summary>
    [StringLength(255)]
    [Display(Name="Job Title")]
    public string JobTitle { get; init; }

    /*==========================================================================================================================
    | PROPERTY: PROVINCE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the province (or state in America).
    /// </summary>
    /// <remarks>
    ///   This cannot be defined on <see cref="Contact"/> because that causes a conflict with <see cref="Address.Province"/>.
    /// </remarks>
    [Required]
    [StringLength(255)]
    [Display(Name="State/Province")]
    [Metadata("State")]
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
    /// <summary>
    ///   Gets or sets what other risk analysis tools the user is currently using or is evaluating.
    /// </summary>
    [Required]
    [StringLength(1000)]
    [Display(Name="What other risk analysis tools do you use, or are evaluating?")]
    public string OtherTools { get; set; }

  } //Class
} //Namespace