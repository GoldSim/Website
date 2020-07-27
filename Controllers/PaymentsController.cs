/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Braintree;
using GoldSim.Web.Forms.Models;
using GoldSim.Web.Models.ViewModels;
using GoldSim.Web.Services;
using Microsoft.AspNetCore.Mvc;
using OnTopic;
using OnTopic.AspNetCore.Mvc.Controllers;
using OnTopic.Attributes;
using OnTopic.Mapping;
using OnTopic.Repositories;

namespace GoldSim.Web.Controllers {

  /*============================================================================================================================
  | CLASS: PAYMENTS CONTROLLER
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to the Payments page of the website, with Braintree Payments integration functionality.
  /// </summary>
  public class PaymentsController : TopicController {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private     readonly        ITopicMappingService            _topicMappingService            = null;
    private     readonly        IBraintreeConfiguration         _braintreeConfiguration         = null;
    private     readonly        ISmtpService                    _smtpService                    = null;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a Topic Controller with necessary dependencies.
    /// </summary>
    /// <returns>A topic controller for loading OnTopic views.</returns>
    public PaymentsController(
      ITopicRepository topicRepository,
      ITopicMappingService topicMappingService,
      IBraintreeConfiguration braintreeConfiguration,
      ISmtpService smtpService
    ) : base(
      topicRepository,
      topicMappingService
    ) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Set values locally
      \-----------------------------------------------------------------------------------------------------------------------*/
      _topicMappingService      = topicMappingService;
      _braintreeConfiguration   = braintreeConfiguration;
      _smtpService              = smtpService;

    }

    /*==========================================================================================================================
    | TRANSACTION SUCCESS STATUSES
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Defines a subset of Braintree transaction statuses, specifically associated with successful transactions.
    /// </summary>
    public static readonly TransactionStatus[] TransactionSuccessStatuses = {
      TransactionStatus.AUTHORIZED,
      TransactionStatus.AUTHORIZING,
      TransactionStatus.SETTLED,
      TransactionStatus.SETTLING,
      TransactionStatus.SETTLEMENT_CONFIRMED,
      TransactionStatus.SETTLEMENT_PENDING,
      TransactionStatus.SUBMITTED_FOR_SETTLEMENT
    };

    /*==========================================================================================================================
    | GET VIEW MODEL
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Constructs a new <see cref="PaymentsTopicViewModel"/> based on the <see cref="TopicController.CurrentTopic"/> and,
    ///   optionally, a <see cref="PaymentFormBindingModel"/>.
    /// </summary>
    /// <returns>A fully mapped <see cref="PaymentsTopicViewModel"/>.</returns>
    [HttpGet]
    public async Task<PaymentsTopicViewModel> GetViewModel(PaymentFormBindingModel bindingModel = null) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish variables
      \-----------------------------------------------------------------------------------------------------------------------*/
      var braintreeGateway      = _braintreeConfiguration.GetGateway();
      var clientToken           = braintreeGateway.ClientToken.Generate();

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var viewModel             = await _topicMappingService.MapAsync<PaymentsTopicViewModel>(CurrentTopic);

      viewModel.BindingModel    = bindingModel;

      /*------------------------------------------------------------------------------------------------------------------------
      | Pass client token to model
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (viewModel != null) {
        viewModel.ClientToken = clientToken;
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Return topic view
      \-----------------------------------------------------------------------------------------------------------------------*/
      return viewModel;

    }

    /*==========================================================================================================================
    | GET: INDEX (VIEW TOPIC)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides access to a view associated with the current topic's Content Type, if appropriate, view (as defined by the
    ///   query string or topic's view.
    /// </summary>
    /// <returns>A view associated with the requested topic's Content Type and view.</returns>
    [HttpGet]
    public async override Task<IActionResult> IndexAsync(string path) => TopicView(await GetViewModel());

    /*==========================================================================================================================
    | POST: PROCESS PAYMENT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides payments form processing
    /// </summary>
    /// <returns>A view associated with the requested topic's Content Type and view.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> IndexAsync(PaymentFormBindingModel bindingModel) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate invoice
      \-----------------------------------------------------------------------------------------------------------------------*/
      // ### HACK JJC20200408: One might reasonably expect for the [Remote] model validation attribute to be validated as part
      // of ModelState.IsValid, but it doesn't appear to be. As a result, it needs to be revalidated here.
      var invoice = GetInvoice(bindingModel.InvoiceNumber);
      Double.TryParse(invoice.Attributes.GetValue("InvoiceAmount", "-1"), out var invoiceAmount);
      if (invoice == null) {
        ModelState.AddModelError("InvoiceAmount", $"The invoice #{bindingModel.InvoiceNumber} is not valid.");
      }
      else if (invoiceAmount != bindingModel.InvoiceAmount) {
        ModelState.AddModelError(
          "InvoiceAmount",
          $"The invoice {bindingModel.InvoiceNumber} is correct, but doesn't match the expected invoice amount. Please " +
          $"recheck the amount owed. If it is confirmed to be correct, contact GoldSim."
        );
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate binding model
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (!ModelState.IsValid) {
        return TopicView(await GetViewModel(bindingModel));
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Assemble and send Braintree transaction
      \-----------------------------------------------------------------------------------------------------------------------*/
      var braintreeGateway      = _braintreeConfiguration.GetGateway();
      var request               = new TransactionRequest {
        Amount                  = (decimal)bindingModel.InvoiceAmount,
        PurchaseOrderNumber     = bindingModel.InvoiceNumber.ToString(),
        PaymentMethodNonce      = bindingModel.PaymentMethodNonce,
        CustomFields            = new Dictionary<string, string> {
          { "cardholder"        , bindingModel.CardholderName },
          { "email"             , bindingModel.Email },
          { "company"           , bindingModel.Organization },
          { "invoice"           , bindingModel.InvoiceNumber.ToString() }
        },
        Options                 = new TransactionOptionsRequest {
          SubmitForSettlement   = true
        }
      };
      var result                = braintreeGateway.Transaction.Sale(request);

      /*------------------------------------------------------------------------------------------------------------------------
      | Send email
      \-----------------------------------------------------------------------------------------------------------------------*/
      await SendEmailReceipt(result, bindingModel);

      /*------------------------------------------------------------------------------------------------------------------------
      | Handle success
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (result.IsSuccess()) {
        invoice.Attributes.SetDateTime("DatePaid", DateTime.Now);
        TopicRepository.Save(invoice);
        return Redirect("/Web/Purchase/PaymentConfirmation");
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Return to view to display ModelState errors
      \-----------------------------------------------------------------------------------------------------------------------*/
      return TopicView(await GetViewModel(bindingModel));

    }

    /*==========================================================================================================================
    | FUNCTION: SEND EMAIL RECEIPT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Sends an email receipt to GoldSim providing a record of the transaction.
    /// </summary>
    private async Task SendEmailReceipt(Result<Transaction> result, PaymentFormBindingModel bindingModel) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Set up notification email
      \-----------------------------------------------------------------------------------------------------------------------*/
      var notificationEmail     = new MailMessage(new MailAddress("admin@goldsim.com"), new MailAddress("admin@goldsim.com"));
      var emailSubjectPrefix    = "GoldSim Payments: Credit Card Payment for Invoice";
      var emailBody             = new StringBuilder("");
      var transaction           = result.Target?? result.Transaction;
      var creditCard            = transaction?.CreditCard;

      /*------------------------------------------------------------------------------------------------------------------------
      | Apply common attributes
      \-----------------------------------------------------------------------------------------------------------------------*/
      emailBody.AppendLine();
      emailBody.AppendLine("Transaction details:");
      emailBody.AppendLine(" - Cardholder Name: "               + bindingModel.CardholderName);
      emailBody.AppendLine(" - Customer Email: "                + bindingModel.Email);
      emailBody.AppendLine(" - Company Name: "                  + bindingModel.Organization);
      emailBody.AppendLine(" - Invoice Number: "                + bindingModel.InvoiceNumber);
      emailBody.AppendLine(" - Amount: "                        + "$" + bindingModel.InvoiceAmount);
      emailBody.AppendLine(" - Credit Card (Last Four Digits): "+ creditCard?.LastFour?? "Not Available");
      emailBody.AppendLine(" - Card Type: "                     + creditCard?.CardType?.ToString()?? "Not Available");

      /*------------------------------------------------------------------------------------------------------------------------
      | Process successful result
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (result.IsSuccess() && transaction != null && TransactionSuccessStatuses.Contains(transaction.Status)) {
        notificationEmail.Subject = $"{emailSubjectPrefix} {bindingModel.InvoiceNumber} Successful";
        emailBody.Insert(0, "PAYMENT STATUS: " + transaction.Status.ToString().ToUpper().Replace("_", " "));
        notificationEmail.Body = emailBody.ToString();
        await _smtpService.SendAsync(notificationEmail);
        return;
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Process unsuccessful result
      \-----------------------------------------------------------------------------------------------------------------------*/
      notificationEmail.Subject = $"{emailSubjectPrefix} {bindingModel.InvoiceNumber} Failed";

      if (transaction != null) {
        var status = transaction.ProcessorResponseText;

        if (String.IsNullOrEmpty(status)) {
          status = transaction.Status.ToString();
        }

        emailBody.Insert(0, "PAYMENT STATUS: " + status.ToUpper().Replace("_", " "));
      }
      else {
        emailBody.Insert(0, "PAYMENT STATUS: NOT AVAILABLE");
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Process errors
      \-----------------------------------------------------------------------------------------------------------------------*/

      // Display general error message
      ModelState.AddModelError(
        "Transaction",
        "Your transaction was unsuccessful. Please correct any errors with your submission or contact GoldSim at " +
        "software@goldsim.com or +1 (425)295-6985 for assistance."
      );

      // Display transaction message returned from Braintree
      if (!String.IsNullOrEmpty(result.Message)) {
        ModelState.AddModelError("Transaction", "Payment Status: " + result.Message);
        emailBody.AppendLine(" - Transaction Result: " + result.Message);
      }

      // Display any specific error messages returned from Braintree
      foreach (var error in result.Errors.DeepAll()) {
        ModelState.AddModelError(error.Code.ToString(), "Error: " + error.Message);
        emailBody.AppendLine(" - Error: " + error.Message);
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Send email
      \-----------------------------------------------------------------------------------------------------------------------*/
      notificationEmail.Body = emailBody.ToString();
      await _smtpService.SendAsync(notificationEmail);

    }

    /*==========================================================================================================================
    | ACTION: VERIFY INVOICE NUMBER
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Given an invoice number, ensures the values matches a valid <see cref="InvoiceTopicViewModel"/>.
    /// </summary>
    /// <remarks>
    ///   The purpose of this function is exclusively to validate whether or not an invoice number is valid. If the supplied
    ///   <paramref name="invoiceNumber"/> is null then no error is returned; if the invoice number is required, then the
    ///   view model should implement an e.g. <see cref="RequiredAttribute"/> to enforce that business logic.
    /// </remarks>
    [HttpGet, HttpPost]
    public IActionResult VerifyInvoiceNumber(
      [Bind(Prefix="BindingModel.InvoiceNumber")] int? invoiceNumber = null
    ) {
      if (invoiceNumber == null) return Json(data: true);
      var existingInvoice = GetInvoice(invoiceNumber);
      if (existingInvoice == null) {
        return Json(
          $"The invoice number {invoiceNumber} is not valid. Please recheck your invoice numer. " +
          $"If it is confirmed to be correct, contact GoldSim."
        );
      }
      return Json(data: true);
    }

    /*==========================================================================================================================
    | ACTION: VERIFY INVOICE AMOUNT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Given an invoice number and amount, ensure the values match a valid <see cref="InvoiceTopicViewModel"/>.
    /// </summary>
    /// <remarks>
    ///   This is separated from <see cref="VerifyInvoiceNumber(Int32?)"/> so that we can return a distinct error for the
    ///   <c>InvoiceNumber</c> and <c>InvoiceAmount</c>. To achieve this, the <see cref="VerifyInvoiceAmount(Int32?, Double?)"/>
    ///   action ignores errors that are picked up by the <see cref="VerifyInvoiceNumber(Int32?)"/>. That said, the methods are,
    ///   by some necessity, redundant since we must first validate the <c>InvoiceNumber</c> before we can lookup the associated
    ///   <c>InvoiceAmount</c>.
    /// </remarks>
    [HttpGet, HttpPost]
    public IActionResult VerifyInvoiceAmount(
      [Bind(Prefix="BindingModel.InvoiceNumber")] int? invoiceNumber = null,
      [Bind(Prefix="BindingModel.InvoiceAmount")] double? invoiceAmount = null
    ) {
      var existingInvoice = GetInvoice(invoiceNumber);
      var existingAmount = existingInvoice?.Attributes.GetValue("InvoiceAmount");
      if (existingInvoice == null || existingAmount == null) return Json(data: true);
      if (!existingAmount.Equals(invoiceAmount.ToString(), StringComparison.InvariantCultureIgnoreCase)) {
        return Json(
          $"The invoice number {invoiceNumber} is correct, but doesn't match the expected invoice amount. " +
          $"Please recheck the amount owed. If it is confirmed to be correct, contact GoldSim."
        );
      }
      return Json(data: true);
    }

    /*==========================================================================================================================
    | FUNCTION: GET INVOICE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Given an invoice number, retrieves a corresponding topic.
    /// </summary>
    private Topic GetInvoice(int? invoiceNumber = null) {
      if (invoiceNumber == null) return null;
      var invoice = TopicRepository.Load($"Administration:Invoices:{invoiceNumber}");
      return invoice;
    }


  } // Class

} // Namespace