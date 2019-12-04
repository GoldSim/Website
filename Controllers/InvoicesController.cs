/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using GoldSim.Web.Models.Invoices;
using Ignia.Topics;
using Ignia.Topics.Mapping;
using Ignia.Topics.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GoldSim.Web.Controllers {

  /*============================================================================================================================
  | CLASS: INVOICES CONTROLLER
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Allows GoldSim to create, view, and edit invoices.
  /// </summary>
  [Authorize]
  public class InvoicesController : Controller {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private readonly            ITopicRepository                _topicRepository;
    private readonly            ITopicMappingService            _topicMappingService;

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
      var invoice = _topicRepository.Load($"Invoices:{invoiceNumber}");
      var viewModel = await _topicMappingService.MapAsync<InvoiceTopicViewModel>(invoice);
      return viewModel;
    }

    /*==========================================================================================================================
    | HELPER: CREATE EDIT INVOICE VIEW MODEL
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Constructs a new view model containing the
    /// </summary>
    private async Task<EditInvoicePageTopicViewModel> CreateEditViewModel(int? invoiceNumber = null) =>
      await CreateEditViewModel(invoiceNumber == null? null : await GetInvoiceViewModel(invoiceNumber?? 0));

    private async Task<EditInvoicePageTopicViewModel> CreateEditViewModel(InvoiceTopicViewModel invoice = null) {
      var pageContent = _topicRepository.Load("Invoices:Edit");
      var viewModel = await _topicMappingService.MapAsync<EditInvoicePageTopicViewModel>(pageContent);
      viewModel.Invoice = invoice;
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
      await _topicMappingService.MapAsync<InvoicePageTopicViewModel>(
        _topicRepository.Load("Invoices")
      )
    );

    /*==========================================================================================================================
    | ACTION: EDIT (ASYNC)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Creates an invoice for a new purchase.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> EditAsync(int? invoiceNumber = null) => View(await CreateEditViewModel(invoiceNumber));

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditAsync(InvoiceTopicViewModel invoice) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate model
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (!ModelState.IsValid) {
        var viewModel = await CreateEditViewModel(invoice);
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
      var parentTopic           = _topicRepository.Load($"Invoices");
      var topic                 = _topicRepository.Load($"Invoices:{invoice.InvoiceNumber}");

      if (topic == null) {
        topic                   = TopicFactory.Create(invoice.InvoiceNumber.ToString(), "Invoice", parentTopic);
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Set attributes
      \-----------------------------------------------------------------------------------------------------------------------*/
      topic.Attributes.SetInteger("InvoiceNumber", invoice.InvoiceNumber);
      topic.Attributes.SetValue("InvoiceAmount", invoice.InvoiceAmount.ToString());
      topic.Attributes.SetValue("LastModifiedBy", HttpContext.User.Identity.Name?? "System");
      topic.LastModified = DateTime.Now;
      topic.IsHidden = true;

      /*------------------------------------------------------------------------------------------------------------------------
      | Save form Topic
      \-----------------------------------------------------------------------------------------------------------------------*/
      _topicRepository.Save(topic);

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
    [HttpGet, HttpPost]
    public IActionResult VerifyInvoiceNumber(
      [Bind(Prefix="Invoice.InvoiceNumber")] int? invoiceNumber = null,
      [Bind(Prefix="Invoice.Key")] int? key = null
    ) {
      if (invoiceNumber == null) return Json(data: true);
      if (invoiceNumber == key) return Json(data: true);
      var existingInvoice = _topicRepository.Load($"Invoices:{invoiceNumber}");
      if (existingInvoice != null) {
        var invoiceAmount = existingInvoice.Attributes.GetValue("InvoiceAmount");
        return Json($"The invoice number {invoiceNumber} has already been entered, with the amount {invoiceAmount}");
      }
      return Json(data: true);
    }


  } // Class
} // Namespace