/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System.Collections.Generic;
using Ignia.Topics.Mapping.Annotations;
using Ignia.Topics.ViewModels;

namespace GoldSim.Web.Models.Invoices {

  /*============================================================================================================================
  | CLASS: EDIT INVOICE PAGE (VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   A view model for rendering an invoice form page.
  /// </summary>
  public class EditInvoicePageTopicViewModel: PageTopicViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="EditInvoicePageTopicViewModel"/> with appropriate dependencies.
    /// </summary>
    /// <returns>A <see cref="EditInvoicePageTopicViewModel"/>.</returns>
    public EditInvoicePageTopicViewModel() {}

    /*==========================================================================================================================
    | INVOICE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a reference to the actual <see cref="InvoiceTopicViewModel"/> that the invoice page is editing.
    /// </summary>
    /// <returns>The <typeparamref name="T"/> binding model.</returns>
    public InvoiceTopicViewModel Invoice { get; set; }

  } // Class

} // Namespace
