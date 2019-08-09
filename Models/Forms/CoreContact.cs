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

    /*==========================================================================================================================
    | PROPERTY: FIRST NAME
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the user's first name.
    /// </summary>
    [Required]
    [StringLength(255)]
    [Display(Name="First Name")]
    public string FirstName { get; set; }

    /*==========================================================================================================================
    | PROPERTY: LAST NAME
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the user's last name.
    /// </summary>
    [Required]
    [StringLength(255)]
    [Display(Name="Last Name")]
    public string LastName { get; set; }

    /*==========================================================================================================================
    | PROPERTY: ORGANIZATION
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the user's organization or institution name.
    /// </summary>
    [Required]
    [StringLength(255)]
    [Display(Name="Organization Name")]
    public string Organization { get; set; }

    /*==========================================================================================================================
    | PROPERTY: EMAIL ADDRESS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the user's email address.
    /// </summary>
    [Required]
    [EmailAddress]
    [StringLength(255)]
    [Display(Name="Email Address")]
    public string Email { get; set; }

  }

}