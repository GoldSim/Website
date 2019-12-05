/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System.Collections.Generic;
using Ignia.Topics.Mapping.Annotations;
using Ignia.Topics.ViewModels;

namespace GoldSim.Web.Administration.Models.Invoices {

  /*============================================================================================================================
  | CLASS: INVOICE PAGE (VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   A view model for rendering an invoice form page.
  /// </summary>
  public class InvoicePageTopicViewModel: PageTopicViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="EditInvoicePageTopicViewModel"/> with appropriate dependencies.
    /// </summary>
    /// <returns>A <see cref="EditInvoicePageTopicViewModel"/>.</returns>
    public InvoicePageTopicViewModel() {}

    /*==========================================================================================================================
    | INVOICES
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a list of invoices currently available in the system.
    /// </summary>
    [Relationship(RelationshipType.Children)]
    public List<InvoiceTopicViewModel> Invoices { get; } = new List<InvoiceTopicViewModel>();

  } // Class

} // Namespace
