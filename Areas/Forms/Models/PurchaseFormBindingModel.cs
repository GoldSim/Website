/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using GoldSim.Web.Forms.Models.Partials;

namespace GoldSim.Web.Forms.Models {

  /*============================================================================================================================
  | BINDING MODEL: PURCHASE FORM
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed binding model representing the Purchase GoldSim form.
  /// </summary>
  public record PurchaseFormBindingModel : PurchaseBindingModel {

    /*==========================================================================================================================
    | PROPERTY: USER (CONTACT)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the user's contact information.
    /// </summary>
    [MapToParent]
    [Display(Name="Intended User Contact Information")]
    public ExtendedContact UserContact { get; init; }

    /*==========================================================================================================================
    | PROPERTY: ACCOUNTS PAYABLE (contact)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the contact information for the accounts payable department of the user's organization.
    /// </summary>
    [MapToParent]
    [Display(Name="Accounts Payable Contact Information")]
    public ExtendedContact AccountsPayableContact { get; init; }

    /*==========================================================================================================================
    | PROPERTY: PURCHASE ORDER NUMBER
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the purchase order number for the purchase.
    /// </summary>
    [StringLength(15)]
    [Display(Name="Purchase Order Number")]
    public string PurchaseOrderNumber { get; init; }

    /*==========================================================================================================================
    | PROPERTY: PURCHASE NOTES
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets any additional notes that the user wants to provide as part of their order.
    /// </summary>
    [StringLength(1000)]
    [Display(Name="Purchase Notes")]
    public string PurchaseNotes { get; init; }

  } //Class
} //Namespace