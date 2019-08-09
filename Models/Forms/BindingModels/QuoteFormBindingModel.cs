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

    [StringLength(50)]
    [Display(Name ="Fax")]
    public string FaxNumber { get; set; }

  }

}