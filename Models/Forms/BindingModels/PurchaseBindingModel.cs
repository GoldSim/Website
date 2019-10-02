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

namespace GoldSim.Web.Models.Forms.BindingModels {

  /*============================================================================================================================
  | BINDING MODEL: PURCHASE
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed binding model representing the basic data model used by both the <see
  ///   cref="PurchaseFormBindingModel"/> as well as the <see cref="QuoteFormBindingModel"/>.
  /// </summary>
  public class PurchaseBindingModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="PurchaseBindingModel"/> object.
    /// </summary>
    public PurchaseBindingModel() {
      BuyerContact = new ExtendedContact();
      Modules = new ModuleSelection();
    }

    /*==========================================================================================================================
    | PROPERTY: BUYER (CONTACT0
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the buyer's contact information; this will typically represent the organization the user is employed by.
    /// </summary>
    [Display(Name="Contact Information")]
    public ExtendedContact BuyerContact { get; }

    /*==========================================================================================================================
    | PROPERTY: PRODUCT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the product the user is interested in purchasing (or getting a quote for).
    /// </summary>
    [StringLength(20)]
    public string Product { get; set; }

    /*==========================================================================================================================
    | PROPERTY: LICENSE TYPE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets what type of license the user wishes to purchase (e.g., stand-alone, leased, enterprise).
    /// </summary>
    [Required]
    [StringLength(30)]
    [Display(Name="License Type")]
    public string LicenseType { get; set; }

    /*==========================================================================================================================
    | PROPERTY: QUANTITY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the number of licenses the user wishes to purchase.
    /// </summary>
    [Required]
    [Range(1, 1000, ErrorMessage="At least one license is required.")]
    [Display(Name="License Type")]
    public int Quantity { get; set; }

    /*==========================================================================================================================
    | PROPERTY: INSTRUCTIONS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets any additional instructions the user wants assessed as part of their quote or purchase.
    /// </summary>
    [StringLength(1000)]
    [Display(Name="Additional Quote Instructions")]
    public string Instructions { get; set; }

    /*==========================================================================================================================
    | PROPERTY: MODULES
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the list of modules the user would like to purchase (or receive a quote for).
    /// </summary>
    [Required]
    [Display(Name="Add-On Modules:")]
    public ModuleSelection Modules { get; }

  }

}