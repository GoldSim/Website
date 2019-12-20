/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using System.ComponentModel.DataAnnotations;
using Ignia.Topics.Mapping.Annotations;

namespace GoldSim.Web.Models.Forms.BindingModels {

  /*============================================================================================================================
  | BINDING MODEL: TRAINING FORM
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed binding model representing the Training Request form.
  /// </summary>
  public class TrainingFormBindingModel : ExtendedContact {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="TrainingFormBindingModel"/> object.
    /// </summary>
    public TrainingFormBindingModel() : base() {
    }

    /*==========================================================================================================================
    | PROPERTY: FAX NUMBER
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the fax number for the user (or their organization) so that the quote may be faxed to them.
    /// </summary>
    [Phone]
    [StringLength(50)]
    [Display(Name = "Fax")]
    public string FaxNumber { get; set; }

    /*==========================================================================================================================
    | PROPERTY: ACCOUNTS PAYABLE (CONTACT)
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
    | PROPERTY: INSTRUCTIONS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets any additional instructions the user wants assessed as part of their training purchase.
    /// </summary>
    [StringLength(1000)]
    [Display(Name = "Additional Instructions")]
    public string Instructions { get; set; }

    /*==========================================================================================================================
    | PROPERTY: WITH PAPER RECEIPT?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets whether the attendee requires a paper receipt be delivered for accounting purposes.
    /// </summary>
    [Display(Name = "I would prefer a paper invoice or receipt.")]
    public bool WithPaperReceipt { get; set; }

  }

}