/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using Braintree;
using GoldSim.Web.Models.Forms.BindingModels;
using GoldSim.Web.Models.ViewModels;
using GoldSim.Web.Services;
using Ignia.Topics;
using Ignia.Topics.AspNetCore.Mvc;
using Ignia.Topics.AspNetCore.Mvc.Controllers;
using Ignia.Topics.Mapping;
using Ignia.Topics.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Decimal = System.Decimal;

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
      | Validate binding model
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (!ModelState.IsValid) {
        return TopicView(await GetViewModel(bindingModel));
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish variables
      \-----------------------------------------------------------------------------------------------------------------------*/
      var invoice               = GetInvoice(bindingModel.InvoiceNumber);
      var braintreeGateway      = _braintreeConfiguration.GetGateway();
      var emailSubjectPrefix    = "GoldSim Payments: Credit Card Payment for Invoice";
      var emailBody             = new StringBuilder("");

      /*------------------------------------------------------------------------------------------------------------------------
      | Assemble and send Braintree transaction
      \-----------------------------------------------------------------------------------------------------------------------*/
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

      /*------------------------------------------------------------------------------------------------------------------------
      | Set up notification email
      \-----------------------------------------------------------------------------------------------------------------------*/
      var notificationEmail     = new MailMessage(new MailAddress("admin@goldsim.com"), new MailAddress("admin@goldsim.com"));

      emailBody.AppendLine();
      emailBody.AppendLine("Transaction details:");
      emailBody.AppendLine(" - Cardholder Name: "               + bindingModel.CardholderName);
      emailBody.AppendLine(" - Customer Email: "                + bindingModel.Email);
      emailBody.AppendLine(" - Company Name: "                  + bindingModel.Organization);
      emailBody.AppendLine(" - Invoice Number: "                + bindingModel.InvoiceNumber);
      emailBody.AppendLine(" - Amount: "                        + "$" + bindingModel.InvoiceAmount);

      /*------------------------------------------------------------------------------------------------------------------------
      | Process transaction result
      \-----------------------------------------------------------------------------------------------------------------------*/
      var result                = braintreeGateway.Transaction.Sale(request);
      var transaction           = result.Target?? result.Transaction;

      if (result.IsSuccess()) {

        // Add (subject and body) details to notification email based on transaction details
        if (transaction != null) {

          if (transaction.Status != null) {
            if (TransactionSuccessStatuses.Contains(transaction.Status)) {
              notificationEmail.Subject           = $"{emailSubjectPrefix} {bindingModel.InvoiceNumber} Successful";
            }
            else {
              notificationEmail.Subject           = $"{emailSubjectPrefix} {bindingModel.InvoiceNumber} Failed";
            }
          }

          emailBody.Insert(0, "PAYMENT STATUS: " + transaction.Status.ToString().ToUpper().Replace("_", " "));
          emailBody.AppendLine(" - Credit Card (Last Four Digits): " + transaction.CreditCard.LastFour);
          emailBody.AppendLine(" - Card Type: " + transaction.CreditCard.CardType.ToString());

        }

        // Set notification email body and send email
        notificationEmail.Body                  = emailBody.ToString();

        await _smtpService.SendAsync(notificationEmail);

        // Redirect to confirmation view
        return Redirect("/Web/Purchase/PaymentConfirmation");

      }
      else {

        // Add (subject and body) details to notification email based on transaction details
        notificationEmail.Subject               = $"{emailSubjectPrefix} {bindingModel.InvoiceNumber} Failed";
        if (transaction != null) {
          var status                            = (!String.IsNullOrEmpty(transaction.ProcessorResponseText) ? transaction.ProcessorResponseText : transaction.Status.ToString());

          emailBody.Insert(0, "PAYMENT STATUS: " + status.ToUpper().Replace("_", " "));
          emailBody.AppendLine(" - Credit Card (Last Four Digits): " + (transaction.CreditCard?.LastFour ?? "Not Available"));
        }
        else {
          emailBody.Insert(0, "PAYMENT STATUS: NOT AVAILABLE");
        }

        // Display general error message
        ModelState.AddModelError(
          "TransactionStatus",
          @"Your transaction was unsuccessful. Please correct any errors with your submission or contact " +
          @"<a href=""mailto:software@goldsim.com"">GoldSim</a> (<a href=""tel:1-425-295-7985"">+1 (425) 295-6985</a>) for assistance."
        );

        // Display transaction message returned from Braintree
        if (!String.IsNullOrEmpty(result.Message)) {
          ModelState.AddModelError("TransactionMessage", "Payment Status: " + result.Message);
          emailBody.AppendLine(" - Transaction Result: " + result.Message);
        }

        // Display any specific error messages returned from Braintree
        foreach (var error in result.Errors.DeepAll()) {
          ModelState.AddModelError(error.Code.ToString(), "Error: " + error.Message);
          emailBody.AppendLine(" - Error: " + error.Message);
        }

        // Set notification email body and send email
        notificationEmail.Body  = emailBody.ToString();

        await _smtpService.SendAsync(notificationEmail);

        // Return form view with error messages
        return TopicView(await GetViewModel(bindingModel));
      }

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
    ///   This is separated from <see cref="VerifyInvoiceNumber(int?)"/> so that we can return a distinct error for the
    ///   <c>InvoiceNumber</c> and <c>InvoiceAmount</c>. To achieve this, the <see cref="VerifyInvoiceAmount(int?, double?)"/>
    ///   action ignores errors that are picked up by the <see cref="VerifyInvoiceNumber(int?)"/>. That said, the methods are,
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