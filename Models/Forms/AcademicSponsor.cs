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
  | MODEL: ACADEMIC SPONSOR
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for representing the contact information of an academic sponsor.
  /// </summary>
  public class AcademicSponsor: Contact {

    /*==========================================================================================================================
    | PROPERTY: FIRST NAME
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the user's first name.
    /// </summary>
    [Display(Name="Sponsor First Name")]
    public override string FirstName { get; set; }

    /*==========================================================================================================================
    | PROPERTY: LAST NAME
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the user's last name.
    /// </summary>
    [Display(Name="Sponsor Last Name")]
    public override string LastName { get; set; }

    /*==========================================================================================================================
    | PROPERTY: ORGANIZATION
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the user's organization or institution name.
    /// </summary>
    [Display(Name="Sponsor Department")]
    public override string Organization { get; set; }

    /*==========================================================================================================================
    | PROPERTY: EMAIL ADDRESS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the user's email address.
    /// </summary>
    [Display(Name="Sponsor Email")]
    public override string Email { get; set; }

    /*==========================================================================================================================
    | PROPERTY: COUNTRY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the ISO 3155 (alpha-3) country code.
    /// </summary>
    /// <remarks>
    ///   The country is not required for an academic sponsor, but needs to be accounted for as part of <see
    ///   cref="CoreContact"/>. As such, it is modified to allow empty strings, and set to a default of empty.
    /// </remarks>
    [Required(AllowEmptyStrings = true)]
    public override string Country { get; set; } = "";

    /*==========================================================================================================================
    | PROPERTY: PHONE NUMBER
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the user's phone number.
    /// </summary>
    [Display(Name="Sponsor Phone Number")]
    public override string PhoneNumber { get; set; }



  }

}