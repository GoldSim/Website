/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System.Collections.ObjectModel;

namespace GoldSim.Web.Administration.Models.Invoices {

  /*============================================================================================================================
  | CLASS: INVOICE LIST (VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   A view model for rendering a page containing a list of invoices.
  /// </summary>
  public record InvoiceListViewModel: PageTopicViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="InvoiceListViewModel"/> with appropriate dependencies.
    /// </summary>
    /// <returns>A <see cref="InvoiceListViewModel"/>.</returns>
    public InvoiceListViewModel() {}

    /*==========================================================================================================================
    | INVOICES
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a list of invoices currently available in the system.
    /// </summary>
    [Collection(CollectionType.Children)]
    public Collection<InvoiceTopicViewModel> Invoices { get; } = new();

  } // Class
} // Namespace