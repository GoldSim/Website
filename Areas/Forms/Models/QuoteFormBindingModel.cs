/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/

namespace GoldSim.Web.Forms.Models {

  /*============================================================================================================================
  | BINDING MODEL: QUOTE FORM
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed binding model representing the Request a Quote form.
  /// </summary>
  public record QuoteFormBindingModel : PurchaseBindingModel {

    /*==========================================================================================================================
    | PROPERTY: FAX NUMBER
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the fax number for the user (or their organization) so that the quote may be faxed to them.
    /// </summary>
    [Phone]
    [StringLength(50)]
    [Display(Name="Fax")]
    public string FaxNumber { get; init; }

  } //Class
} //Namespace