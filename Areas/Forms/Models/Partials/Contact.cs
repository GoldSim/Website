﻿/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/

namespace GoldSim.Web.Forms.Models.Partials {

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
  public record Contact : CoreContact {

    /*==========================================================================================================================
    | PROPERTY: COUNTRY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the country name.
    /// </summary>
    [Required]
    [StringLength(75)]
    [Metadata("Country")]
    public virtual string Country { get; init; } = "United States of America";

    /*==========================================================================================================================
    | PROPERTY: PHONE NUMBER
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the user's phone number.
    /// </summary>
    [Required]
    [Phone]
    [StringLength(50)]
    [Display(Name="Telephone")]
    public virtual string PhoneNumber { get; init; }

  } //Class
} //Namespace