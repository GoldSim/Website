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

    public PurchaseBindingModel() {
      BuyerContact=new ExtendedContact();
      Modules=new ModuleSelection();
    }

    [Display(Name="Contact Information")]
    public ExtendedContact BuyerContact { get; }

    [StringLength(20)]
    public string Product { get; set; }

    [Required]
    [StringLength(30)]
    [Display(Name="License Type")]
    public string LicenseType { get; set; }

    [Required]
    [Range(1, 1000, ErrorMessage="At least one license is required.")]
    [Display(Name="License Type")]
    public int Quantity { get; set; }

    [StringLength(1000)]
    [Display(Name="Additional Quote Instructions")]
    public string Instructions { get; set; }

    [Required]
    [Display(Name="Add-On Modules:")]
    public ModuleSelection Modules { get; }

  }

}