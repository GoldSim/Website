/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System.Globalization;
using GoldSim.Web.Administration.Models.Invoices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnTopic;
using OnTopic.Attributes;
using OnTopic.Internal.Diagnostics;
using OnTopic.Mapping;
using OnTopic.Repositories;

namespace GoldSim.Web.Administration.Controllers {

  /*============================================================================================================================
  | CLASS: INVOICES CONTROLLER
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Allows GoldSim to create, view, and edit invoices.
  /// </summary>
  [Authorize]
  [Area("Administration")]
  public class InvoicesController : Controller {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private readonly            ITopicRepository                _topicRepository;
    private readonly            ITopicMappingService            _topicMappingService;
    private readonly            string                          _invoiceRoot                    = "Root:Administration:Invoices";

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref ="OrdersController"/> with necessary dependencies.
    /// </summary>
    /// <returns>A topic controller for loading OnTopic views.</returns>
    public InvoicesController(
      ITopicRepository topicRepository,
      ITopicMappingService topicMappingService
    ) {
      _topicRepository          = topicRepository;
      _topicMappingService      = topicMappingService;
    }

    /*==========================================================================================================================
    | HELPER: CREATE INVOICE VIEW MODEL
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Constructs a new view model containing the
    /// </summary>
    private async Task<InvoiceTopicViewModel> GetInvoiceViewModel(int invoiceNumber) {
      var invoice = _topicRepository.Load($"Administration:Invoices:{invoiceNumber}");
      var viewModel = await _topicMappingService.MapAsync<InvoiceTopicViewModel>(invoice).ConfigureAwait(true);
      return viewModel;
    }

    /*==========================================================================================================================
    | HELPER: CREATE EDIT INVOICE VIEW MODEL
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Constructs a new view model containing the
    /// </summary>
    private async Task<EditInvoiceViewModel> CreateEditViewModel(int? invoiceNumber = null) =>
      await CreateEditViewModel(
        invoiceNumber is null? null : await GetInvoiceViewModel(invoiceNumber.Value).ConfigureAwait(true)
      ).ConfigureAwait(true);

    private async Task<EditInvoiceViewModel> CreateEditViewModel(InvoiceTopicViewModel invoice = null) {
      var pageContent           = _topicRepository.Load("Administration:Invoices:Edit");
      var viewModel             = new EditInvoiceViewModel {
        Invoice                 = invoice
      };
      viewModel = (EditInvoiceViewModel)await _topicMappingService.MapAsync(pageContent, viewModel).ConfigureAwait(true);
      return viewModel;
    }

    /*==========================================================================================================================
    | ACTION: INDEX (ASYNC)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a list of invoices already entered on the system.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> IndexAsync() => View(
      await _topicMappingService.MapAsync<InvoiceListViewModel>(
        _topicRepository.Load("Administration:Invoices")
      ).ConfigureAwait(true)
    );

    /*==========================================================================================================================
    | ACTION: EDIT (ASYNC)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Creates an invoice for a new purchase.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> EditAsync(int? id = null) => View(await CreateEditViewModel(id).ConfigureAwait(true));

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditAsync(InvoiceTopicViewModel invoice) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate model
      \-----------------------------------------------------------------------------------------------------------------------*/
      Contract.Requires(invoice, nameof(invoice));
      if (!ModelState.IsValid) {
        var viewModel = await CreateEditViewModel(invoice).ConfigureAwait(true);
        return View(viewModel);
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Save to topic
      \-----------------------------------------------------------------------------------------------------------------------*/
      SaveToTopic(invoice);

      /*------------------------------------------------------------------------------------------------------------------------
      | Return
      \-----------------------------------------------------------------------------------------------------------------------*/
      return RedirectToAction("Index");

    }

    /*==========================================================================================================================
    | HELPER: SAVE TO TOPIC
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Adds the form values to a new <see cref="Topic"/>, and saves it to the <see cref="ITopicRepository"/>.
    /// </summary>
    private void SaveToTopic(InvoiceTopicViewModel invoice) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish topic
      \-----------------------------------------------------------------------------------------------------------------------*/
      var parentTopic           = _topicRepository.Load(_invoiceRoot);
      var topic                 = _topicRepository.Load($"{_invoiceRoot}:{invoice.Key?? invoice.InvoiceNumber}");

      if (topic is null) {
        topic                   = TopicFactory.Create(
          invoice.InvoiceNumber.ToString(CultureInfo.InvariantCulture),
          "Invoice",
          parentTopic
        );
      }
      else if (invoice.Key != invoice.InvoiceNumber) {
        topic.Key = invoice.InvoiceNumber.ToString(CultureInfo.InvariantCulture);
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Set attributes
      \-----------------------------------------------------------------------------------------------------------------------*/
      topic.Attributes.SetInteger("InvoiceNumber", invoice.InvoiceNumber);
      topic.Attributes.SetValue("InvoiceAmount", invoice.InvoiceAmount.ToString(CultureInfo.InvariantCulture));
      topic.Attributes.SetValue("DatePaid", invoice.DatePaid.ToString());
      topic.Attributes.SetValue("LastModifiedBy", HttpContext.User.Identity.Name?? "System");
      topic.LastModified = DateTime.Now;
      topic.IsHidden = true;

      /*------------------------------------------------------------------------------------------------------------------------
      | Save form Topic
      \-----------------------------------------------------------------------------------------------------------------------*/
      _topicRepository.Save(topic);

    }

    /*==========================================================================================================================
    | DELETE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Deletes selected licenses.
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int[] topics) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate input
      \-----------------------------------------------------------------------------------------------------------------------*/
      Contract.Requires(topics, nameof(topics));

      /*------------------------------------------------------------------------------------------------------------------------
      | Delete topics
      \-----------------------------------------------------------------------------------------------------------------------*/
      foreach (var topicId in topics) {
        if (topicId < 0) {
          continue;
        }
        var topic = _topicRepository.Load(topicId);
        if (!topic.GetUniqueKey().StartsWith(_invoiceRoot, StringComparison.InvariantCultureIgnoreCase)) {
          continue;
        }
        _topicRepository.Delete(topic);
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Return default view
      \-----------------------------------------------------------------------------------------------------------------------*/
      return RedirectToAction(nameof(Index));

    }

    /*==========================================================================================================================
    | ACTION: VERIFY INVOICE NUMBER
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Given an invoice number and, optionally, a <see cref="Topic.Id"/>, ensures that the value is unique.
    /// </summary>
    /// <remarks>
    ///   The <see cref="Topic.Id"/> is required for existing invoices, as they should still be considered unique if the value
    ///   has not been modified.
    /// </remarks>
    [HttpGet]
    public IActionResult VerifyInvoiceNumber(
      [Bind(Prefix="Invoice.InvoiceNumber")] int? invoiceNumber = null,
      [Bind(Prefix="Invoice.Key")] int? key = null
    ) {
      if (invoiceNumber is null) return Json(data: true);
      if (invoiceNumber == key) return Json(data: true);
      var existingInvoice = _topicRepository.Load($"Administration:Invoices:{invoiceNumber}");
      if (existingInvoice is not null) {
        var invoiceAmount = existingInvoice.Attributes.GetValue("InvoiceAmount");
        return Json($"The invoice number {invoiceNumber} has already been entered, with the amount {invoiceAmount}");
      }
      return Json(data: true);
    }

  } // Class
} // Namespace