/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using System.ComponentModel.DataAnnotations;

namespace GoldSim.Web.Forms.Models.Partials {

  /*============================================================================================================================
  | MODEL: ADDRESS
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for representing street address.
  /// </summary>
  public class Address {

    /*==========================================================================================================================
    | PROPERTY: STREET (1)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the primary street address.
    /// </summary>
    [Required]
    [StringLength(255)]
    [Display(Name="Address Line 1")]
    public string Street1 { get; set; }

    /*==========================================================================================================================
    | PROPERTY: STREET (2)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the seconday street address.
    /// </summary>
    [StringLength(255)]
    [Display(Name="Address Line 2")]
    public string Street2 { get; set; }

    /*==========================================================================================================================
    | PROPERTY: CITY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the city name.
    /// </summary>
    [Required]
    [StringLength(255)]
    public string City { get; set; }

    /*==========================================================================================================================
    | PROPERTY: PROVINCE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the province (or state in America).
    /// </summary>
    [Required]
    [StringLength(255)]
    [Display(Name="State/Province")]
    public string Province { get; set; }

    /*==========================================================================================================================
    | PROPERTY: POSTAL CODE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the postal code (or zip code in America).
    /// </summary>
    [Required]
    [StringLength(50)]
    [Display(Name="ZIP/Postal Code")]
    public string PostalCode { get; set; }

  }

}