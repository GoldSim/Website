/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using GoldSim.Web.Areas.Forms.Models.Partials;

namespace GoldSim.Web.Areas.Forms.Models {

  /*============================================================================================================================
  | BINDING MODEL: REQUEST A DEMO FORM
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed binding model representing the Request a Demo(nstration) form.
  /// </summary>
  public sealed record DemoFormBindingModel : ExtendedProfile {

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
    public string Province { get; set; }

    /*==========================================================================================================================
    | PROPERTY: OTHER TOOLS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets what other risk analysis tools the user is currently using or is evaluating.
    /// </summary>
    [Required]
    [StringLength(1000)]
    [Display(Name="*What other risk analysis tools do you use, or are evaluating ?")]
    public string OtherTools { get; init; }

  } //Class
} //Namespace