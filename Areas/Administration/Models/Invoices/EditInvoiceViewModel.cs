﻿/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/

namespace GoldSim.Web.Administration.Models.Invoices {

  /*============================================================================================================================
  | CLASS: EDIT INVOICE (VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   A view model for rendering an invoice form page.
  /// </summary>
  public record EditInvoiceViewModel: PageTopicViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="EditInvoiceViewModel"/> with appropriate dependencies.
    /// </summary>
    /// <returns>A <see cref="EditInvoiceViewModel"/>.</returns>
    public EditInvoiceViewModel() {}

    /*==========================================================================================================================
    | INVOICE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a reference to the actual <see cref="InvoiceTopicViewModel"/> that the invoice page is editing.
    /// </summary>
    /// <returns>The <typeparamref name="T"/> binding model.</returns>
    public InvoiceTopicViewModel Invoice { get; init; }

  } // Class
} // Namespace