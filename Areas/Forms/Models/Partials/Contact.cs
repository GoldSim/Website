/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/

namespace GoldSim.Web.Areas.Forms.Models.Partials {

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
    | PROPERTY: PROVINCE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the province (or state in America).
    /// </summary>
    /// <remarks>
    ///   This is also supplied by the <see cref="Address"/> via e.g., <see cref="ExtendedContact"/>, where it is
    ///   required. It is included here to provide support for the <see cref="DemoFormBindingModel"/> and <see cref=
    ///   "TrialFormBindingModel"/> forms.
    /// </remarks>
    [DisableMapping]
    [StringLength(255)]
    [Display(Name="State/Province")]
    public string Province { get; set; }

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