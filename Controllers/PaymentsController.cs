/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using Braintree;
using GoldSim.Web.Models.ViewModels;
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

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a Topic Controller with necessary dependencies.
    /// </summary>
    /// <returns>A topic controller for loading OnTopic views.</returns>
    public PaymentsController(
      ITopicRepository topicRepository,
      ITopicRoutingService topicRoutingService,
      ITopicMappingService topicMappingService,
      IBraintreeConfiguration braintreeConfiguration
    ) : base(
      topicRepository,
      topicRoutingService,
      topicMappingService
    ) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Set values locally
      \-----------------------------------------------------------------------------------------------------------------------*/
      _topicMappingService      = topicMappingService;
      _braintreeConfiguration   = braintreeConfiguration;

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
    | GET: INDEX (VIEW TOPIC)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides access to a view associated with the current topic's Content Type, if appropriate, view (as defined by the
    ///   query string or topic's view.
    /// </summary>
    /// <returns>A view associated with the requested topic's Content Type and view.</returns>
    public async override Task<IActionResult> IndexAsync(string path) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish variables
      \-----------------------------------------------------------------------------------------------------------------------*/
      var braintreeGateway      = _braintreeConfiguration.GetGateway();
      var clientToken           = braintreeGateway.ClientToken.Generate();

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var topicViewModel        = await _topicMappingService.MapAsync<PaymentsTopicViewModel>(CurrentTopic);

      /*------------------------------------------------------------------------------------------------------------------------
      | Pass client token to model
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (topicViewModel != null) {
        topicViewModel.ClientToken = clientToken;
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Return topic view
      \-----------------------------------------------------------------------------------------------------------------------*/
      return TopicView(topicViewModel, CurrentTopic.View);

    }

    /*==========================================================================================================================
    | POST: CREATE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides payments form processing
    /// </summary>
    /// <returns>A view associated with the requested topic's Content Type and view.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> IndexAsync() {

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish variables
      \-----------------------------------------------------------------------------------------------------------------------*/
      var topicViewModel        = await _topicMappingService.MapAsync<PaymentsTopicViewModel>(CurrentTopic);
      var topicViewResult       = TopicView(topicViewModel, CurrentTopic.View);
      var braintreeGateway      = _braintreeConfiguration.GetGateway();
      var clientToken           = braintreeGateway.ClientToken.Generate();
      var cardholderName        = HttpContext.Request.Form["cardholderName"];
      var customerEmail         = HttpContext.Request.Form["customerEmail"];
      var companyName           = HttpContext.Request.Form["company"];
      var invoiceNumber         = HttpContext.Request.Form["invoice"];
      var emailSubjectPrefix    = "GoldSim Payments: Credit Card Payment for Invoice ";
      var emailBody             = new StringBuilder("");

      /*------------------------------------------------------------------------------------------------------------------------
      | Pass client token to model
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (topicViewModel != null) {
        topicViewModel.ClientToken = clientToken;
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Verify payment amount format
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (!Decimal.TryParse(HttpContext.Request.Form["amount"], out var amount)) {
        topicViewModel.IsValid = false;
        topicViewModel.ErrorMessages.Add("AmountFormat", CurrentTopic.Attributes.GetValue("AmountErrorMessage"));
        return topicViewResult;
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Assemble and send Braintree transaction
      \-----------------------------------------------------------------------------------------------------------------------*/
      var nonce                 = HttpContext.Request.Form["paymentMethodNonce"];
      var request               = new TransactionRequest {
        Amount                  = amount,
        PurchaseOrderNumber     = HttpContext.Request.Form["invoice"],
        PaymentMethodNonce      = nonce,
        CustomFields            = new Dictionary<string, string> {
          { "cardholder"        , cardholderName },
          { "email"             , customerEmail },
          { "company"           , companyName },
          { "invoice"           , invoiceNumber }
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
      emailBody.AppendLine();
      emailBody.Append("Transaction details:");
      emailBody.AppendLine();
      emailBody.Append(" - Cardholder Name: "   + cardholderName);
      emailBody.AppendLine();
      emailBody.Append(" - Customer Email: "    + customerEmail);
      emailBody.AppendLine();
      emailBody.Append(" - Company Name: "      + companyName);
      emailBody.AppendLine();
      emailBody.Append(" - Invoice Number: "    + invoiceNumber);
      emailBody.AppendLine();
      emailBody.Append(" - Amount: "            + "$" + amount);
      emailBody.AppendLine();

      /*------------------------------------------------------------------------------------------------------------------------
      | Process transaction result
      \-----------------------------------------------------------------------------------------------------------------------*/
      var result                                = braintreeGateway.Transaction.Sale(request);
      Transaction transaction                   = null;
      if (result.Target != null) {
        transaction                             = result.Target;
      }
      else if (result.Transaction != null) {
        transaction                             = result.Transaction;
      }

      if (result.IsSuccess()) {

        // Add (subject and body) details to notification email based on transaction details
        if (transaction != null) {

          if (transaction.Status != null) {
            if (TransactionSuccessStatuses.Contains(transaction.Status)) {
              notificationEmail.Subject           = emailSubjectPrefix + invoiceNumber + " Successful";
            }
            else {
              notificationEmail.Subject           = emailSubjectPrefix + invoiceNumber + " Failed";
            }
          }

          emailBody.Insert(0, "PAYMENT STATUS: " + transaction.Status.ToString().ToUpper().Replace("_", " "));
          emailBody.Append(" - Credit Card (Last Four Digits): " + transaction.CreditCard.LastFour);
          emailBody.AppendLine();
          emailBody.Append(" - Card Type: " + transaction.CreditCard.CardType.ToString());
          emailBody.AppendLine();

        }

        // Set notification email body and send email
        notificationEmail.Body                  = emailBody.ToString();

        using (var smtpClient = new SmtpClient()) {
          smtpClient.Send(notificationEmail);
        }

        // Redirect to confirmation view
        return Redirect("/Web/Purchase/PaymentConfirmation");

      }
      else {

        // Add (subject and body) details to notification email based on transaction details
        notificationEmail.Subject               = emailSubjectPrefix + invoiceNumber + " Failed";
        if (transaction != null) {
          var status                            = (!String.IsNullOrEmpty(transaction.ProcessorResponseText) ? transaction.ProcessorResponseText : transaction.Status.ToString());

          emailBody.Insert(0, "PAYMENT STATUS: " + status.ToUpper().Replace("_", " "));
          emailBody.Append(" - Credit Card (Last Four Digits): " + (transaction.CreditCard?.LastFour ?? "Not Available"));
          emailBody.AppendLine();
        }
        else {
          emailBody.Insert(0, "PAYMENT STATUS: NOT AVAILABLE");
        }

        // Display general error message
        topicViewModel.ErrorMessages.Add("TransactionStatus", "Your transaction was unsuccessful. Please correct any errors with your submission or contact <a href=\"mailto:software@goldsim.com\">GoldSim</a> (<a href=\"tel:1-425-295-7985\">+1 (425) 295-6985</a>) for assistance.");

        // Display transaction message returned from Braintree
        if (!String.IsNullOrEmpty(result.Message)) {
          topicViewModel.ErrorMessages.Add("TransactionMessage", "Payment Status: " + result.Message);
          emailBody.Append(" - Transaction Result: " + result.Message);
          emailBody.AppendLine();
        }

        // Display any specific error messages returned from Braintree
        foreach (var error in result.Errors.DeepAll()) {
          topicViewModel.ErrorMessages.Add(error.Code.ToString(), "Error: " + error.Message);
          emailBody.Append(" - Error: " + error.Message);
          emailBody.AppendLine();
        }

        // Set notification email body and send email
        notificationEmail.Body  = emailBody.ToString();

        using (var smtpClient = new SmtpClient()) {
          smtpClient.Send(notificationEmail);
          }

        // Return form view with error messages
        return topicViewResult;
      }

    }

  } // Class

} // Namespace