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
  | MODEL: ADDRESS
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for representing street address.
  /// </summary>
  public class Address {

    [Required]
    [StringLength(255)]
    [Display(Name="Address Line 1")]
    public string Street1 { get; set; }

    [StringLength(255)]
    [Display(Name="Address Line 2")]
    public string Street2 { get; set; }

    [Required]
    [StringLength(255)]
    public string City { get; set; }

    [Required]
    [StringLength(255)]
    [Display(Name="State/Province")]
    public string Province { get; set; }

    [Required]
    [StringLength(50)]
    [Display(Name="ZIP/Postal Code")]
    public string PostalCode { get; set; }

  }

}