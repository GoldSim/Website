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
  | BINDING MODEL: PURCHASE FORM
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed binding model representing the Purchase GoldSim form.
  /// </summary>
  public class PurchaseFormBindingModel : PurchaseBindingModel {

    public PurchaseFormBindingModel() : base() {
      UserContact=new ExtendedContact();
      AccountsPayableContact=new ExtendedContact();
    }

    [Display(Name="Intended User Contact Information")]
    public ExtendedContact UserContact { get; }

    [Display(Name="Accounts Payable Contact Information")]
    public ExtendedContact AccountsPayableContact { get; }

    [StringLength(15)]
    [Display(Name="Purchase Order Number")]
    public string PurchaseOrderNumber { get; set; }

    [StringLength(1000)]
    [Display(Name="Purchase Notes")]
    public string PurchaseNotes { get; set; }

  }

}