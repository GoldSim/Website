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
  | MODEL: CONTACT
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for representing the contact information for a user.
  /// </summary>
  /// <remarks>
  ///   The <see cref="Contact"/> class extends the <see cref="CoreContact"/> by adding <see cref="Contact.Country"/> and
  ///   <see cref="Contact.PhoneNumber"/>. These fields are required for anything beyond informational requests.
  /// </remarks>
  public class Contact : CoreContact {

    /*==========================================================================================================================
    | PROPERTY: COUNTRY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the ISO 3155 (alpha-3) country code.
    /// </summary>
    [Required]
    [StringLength(3)]
    public virtual string Country { get; set; }

    /*==========================================================================================================================
    | PROPERTY: PHONE NUMBER
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the user's phone number.
    /// </summary>
    [Required]
    [Phone]
    [StringLength(50)]
    [Display(Name="Phone Number")]
    public virtual string PhoneNumber { get; set; }

  }

}