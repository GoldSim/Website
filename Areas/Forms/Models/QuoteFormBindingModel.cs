/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using GoldSim.Web.Areas.Forms.Models.Partials;

namespace GoldSim.Web.Areas.Forms.Models {

  /*============================================================================================================================
  | BINDING MODEL: QUOTE FORM
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed binding model representing the Request a Quote form.
  /// </summary>
  public sealed record QuoteFormBindingModel : Contact {

    /*==========================================================================================================================
    | PROPERTY: PRODUCT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the product the user is interested in purchasing (or getting a quote for).
    /// </summary>
    [StringLength(20)]
    [Metadata("Products")]
    public string Product { get; set; }

    /*==========================================================================================================================
    | PROPERTY: LICENSE TYPE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets what type of license the user wishes to purchase (e.g., stand-alone, leased, enterprise).
    /// </summary>
    [StringLength(30)]
    [Display(Name="License Type")]
    [Metadata("LicenseTypes")]
    public string LicenseType { get; set; }

    /*==========================================================================================================================
    | PROPERTY: QUANTITY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the number of licenses the user wishes to purchase.
    /// </summary>
    [Required]
    [Range(1, 1000, ErrorMessage="At least one license is required.")]
    [Display(Name="License Quantity")]
    public int Quantity { get; set; } = 1;

    /*==========================================================================================================================
    | PROPERTY: MODULES
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the list of modules the user would like to purchase (or receive a quote for).
    /// </summary>
    [Required]
    [Display(Name="Add-On Modules:")]
    [MapToParent]
    public ModuleSelection Modules { get; } = new();

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
    | PROPERTY: ADDRESS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the user's physical address.
    /// </summary>
    [Required]
    [MapToParent(AttributePrefix="")]
    public Address Address { get; } = new();

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