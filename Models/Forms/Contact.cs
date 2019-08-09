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

    [Required]
    [StringLength(3)]
    public string Country { get; set; }

    [Required]
    [Phone]
    [StringLength(50)]
    [Display(Name="Phone Number")]
    public string PhoneNumber { get; set; }

  }

}