/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/

namespace GoldSim.Web.Forms.Models.Partials {

  /*============================================================================================================================
  | MODEL: EXTENDED CONTACT
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for representing an extended contact, which includes a <see
  ///   cref="Address"/> on top of the normal <see cref="Contact"/> properties.
  /// </summary>
  public record ExtendedContact : Contact {

    /*==========================================================================================================================
    | PROPERTY: ADDRESS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the user's physical address.
    /// </summary>
    [Required]
    [MapToParent]
    public Address Address { get; } = new();

  } //Class
} //Namespace