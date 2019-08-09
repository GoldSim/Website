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

namespace GoldSim.Web.Models.Forms {

  /*============================================================================================================================
  | MODEL: PROFILE
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for representing a profiled user. This extends <see cref="Contact"/> with
  ///   basic profile information such as <see cref="AreaOfFocus"/>, <see cref="ProblemStatement"/>, and <see
  ///   cref="ReferralSource"/>.
  /// </summary>
  public class Profile : Contact {

    [Required]
    [StringLength(100)]
    [Display(Name="Area of Focus")]
    public string AreaOfFocus { get; set; }

    [Required]
    [StringLength(255)]
    [Display(Name="Other")]
    public string AreaOfFocusOther { get; set; }

    [Required]
    [StringLength(1000)]
    [Display(Name="What problem are you trying to solve?")]
    public string ProblemStatement { get; set; }

    [Required]
    [StringLength(100)]
    [Display(Name="How did you learn about GoldSim?")]
    public string ReferralSource { get; set; }

    [Required]
    [StringLength(30)]
    [Display(Name="Referral Details")]
    public string ReferralDetails { get; set; }

  }

}