/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using System.ComponentModel.DataAnnotations;
using OnTopic.Mapping.Annotations;

namespace GoldSim.Web.Forms.Models.Partials {

  /*============================================================================================================================
  | MODEL: PROFILE
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for representing a profiled user. This extends <see cref="Contact"/> with
  ///   basic profile information such as <see cref="AreaOfFocus"/>, <see cref="ProblemStatement"/>, and <see
  ///   cref="ReferralSource"/>.
  /// </summary>
  public class Profile : Contact {

    /*==========================================================================================================================
    | PROPERTY: AREA OF FOCUS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the area that the user's area of focus, which helps determine how they intend to use GoldSim.
    /// </summary>
    [Required]
    [StringLength(100)]
    [Display(Name="Area of Focus")]
    [Metadata("FocusArea")]
    public string AreaOfFocus { get; set; }

    /*==========================================================================================================================
    | PROPERTY: AREA OF FOCUS (OTHER)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the user's custom area of focus, for cases where the out-of-the-box options aren't relevant.
    /// </summary>
    [StringLength(255)]
    [Display(Name="Other")]
    public string AreaOfFocusOther { get; set; }

    /*==========================================================================================================================
    | PROPERTY: PROBLEM STATEMENT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the problem that the user is hoping to solve by using GoldSim.
    /// </summary>
    [Required]
    [StringLength(1000)]
    [Display(Name="What problem are you trying to solve?")]
    public string ProblemStatement { get; set; }

    /*==========================================================================================================================
    | PROPERTY: REFERRAL SOURCE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets how the user came to hear about GoldSim (e.g., from a colleague, or via a Google search).
    /// </summary>
    [Required]
    [StringLength(100)]
    [Display(Name="How did you learn about GoldSim?")]
    [Metadata("ReferralSource")]
    public string ReferralSource { get; set; }

    /*==========================================================================================================================
    | PROPERTY: REFERRAL DETAILS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the user's explanation of how they came to hear about GoldSim, should they have additional detail to
    ///   share.
    /// </summary>
    [StringLength(30)]
    [Display(Name="Referral Details")]
    public string ReferralDetails { get; set; }

  }

}