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
  | MODEL: CORE CONTACT
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for representing the core contact information required of every request.
  /// </summary>
  /// <remarks>
  ///   Every form has, at its base, core contact information representing the <see cref="FirstName"/>, <see cref="LastName"/>,
  ///   <see cref="Organization"/>, and <see cref="Email"/>. Thus the <see cref="CoreContact"/> represents the base class for
  ///   nearly every form binding model used by GoldSim.
  /// </remarks>
  public class CoreContact {

    [Required]
    [StringLength(255)]
    [Display(Name="First Name")]
    public string FirstName { get; set; }

    [Required]
    [StringLength(255)]
    [Display(Name="Last Name")]
    public string LastName { get; set; }

    [Required]
    [StringLength(255)]
    [Display(Name="Organization Name")]
    public string Organization { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(255)]
    [Display(Name="Email Address")]
    public string Email { get; set; }

  }

}