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
  | MODEL: EXTENDED CONTACT
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for representing an extended contact, which includes a <see
  ///   cref="Address"/> on top of the normal <see cref="Contact"/> properties.
  /// </summary>
  public class ExtendedContact : Contact {

    public ExtendedContact() {
      Address=new Address();
    }

    [Required]
    public Address Address { get; }

  }

}