/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using Ignia.Topics.Mapping.Annotations;
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

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="PurchaseFormBindingModel"/> object.
    /// </summary>
    public PurchaseFormBindingModel() : base() {
    //UserContact = new ExtendedContact();
    //AccountsPayableContact = new ExtendedContact();
    }

    /*==========================================================================================================================
    | PROPERTY: USER (CONTACT)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the user's contact information.
    /// </summary>
    [MapToParent]
    [Display(Name="Intended User Contact Information")]
    public ExtendedContact UserContact { get; set; }

    /*==========================================================================================================================
    | PROPERTY: ACCOUNTS PAYABLE (contact)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the contact information for the accounts payable department of the user's organization.
    /// </summary>
    [MapToParent]
    [Display(Name="Accounts Payable Contact Information")]
    public ExtendedContact AccountsPayableContact { get; set; }

    /*==========================================================================================================================
    | PROPERTY: PURCHASE ORDER NUMBER
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the purchase order number for the purchase.
    /// </summary>
    [StringLength(15)]
    [Display(Name="Purchase Order Number")]
    public string PurchaseOrderNumber { get; set; }

    /*==========================================================================================================================
    | PROPERTY: PURCHASE NOTES
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets any additional notes that the user wants to provide as part of their order.
    /// </summary>
    [StringLength(1000)]
    [Display(Name="Purchase Notes")]
    public string PurchaseNotes { get; set; }

  }

}