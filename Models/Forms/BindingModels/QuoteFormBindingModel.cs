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
  | BINDING MODEL: QUOTE FORM
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed binding model representing the Request a Quote form.
  /// </summary>
  public class QuoteFormBindingModel : PurchaseBindingModel {

    /*==========================================================================================================================
    | PROPERTY: FAX NUMBER
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the fax number for the user (or their organization) so that the quote may be faxed to them.
    /// </summary>
    [StringLength(50)]
    [Display(Name ="Fax")]
    public string FaxNumber { get; set; }

  }

}