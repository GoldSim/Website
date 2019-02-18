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
using System.Web.Mvc;
using Braintree;
using GoldSim.Web.Models;
using Ignia.Topics;
using Ignia.Topics.Mapping;
using Ignia.Topics.Repositories;
using Ignia.Topics.Web.Mvc;
using Ignia.Topics.Web.Mvc.Controllers;

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
    public async override Task<ActionResult> IndexAsync(string path) {

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
      return new TopicViewResult(topicViewModel, CurrentTopic.ContentType, CurrentTopic.View);

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
    public async Task<ActionResult> IndexAsync() {

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish variables
      \-----------------------------------------------------------------------------------------------------------------------*/
      var topicViewModel        = await _topicMappingService.MapAsync<PaymentsTopicViewModel>(CurrentTopic);
      var topicViewResult       = new TopicViewResult(topicViewModel, CurrentTopic.ContentType, CurrentTopic.View);
      var braintreeGateway      = _braintreeConfiguration.GetGateway();
      var clientToken           = braintreeGateway.ClientToken.Generate();
      string emailSubjectPrefix = "GoldSim: Payments: Credit Card Payment for Invoice ";
      string emailSubjectStatus = "Successful";
      StringBuilder emailBody   = new StringBuilder("");
      Decimal amount;

      /*------------------------------------------------------------------------------------------------------------------------
      | Pass client token to model
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (topicViewModel != null) {
        topicViewModel.ClientToken = clientToken;
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Verify payment amount format
      \-----------------------------------------------------------------------------------------------------------------------*/
      try {
        amount                  = Convert.ToDecimal(Request["amount"]);
      }
      catch (FormatException e) {
        topicViewModel.IsValid  = false;
        topicViewModel.ErrorMessages.Add("AmountFormat", CurrentTopic.Attributes.GetValue("AmountErrorMessage"));
        return topicViewResult;
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Assemble and send Braintree transaction
      \-----------------------------------------------------------------------------------------------------------------------*/
      var nonce                 = Request["paymentMethodNonce"];
      var request               = new TransactionRequest {
        Amount                  = amount,
        PurchaseOrderNumber     = Request["invoice"],
        PaymentMethodNonce      = nonce,
        CustomFields            = new Dictionary<string, string> {
          { "cardholder", Request["cardholderName"] },
          { "email", Request["customerEmail"] },
          { "company", Request["company"] },
          { "invoice", Request["invoice"] }
        },
        Options                 = new TransactionOptionsRequest {
          SubmitForSettlement   = true
        }
      };

      /*------------------------------------------------------------------------------------------------------------------------
      | Set up notification email
      \-----------------------------------------------------------------------------------------------------------------------*/
      MailMessage notificationEmail             = new MailMessage(new MailAddress("software@goldsim.com"), new MailAddress("software@goldsim.com"));
      emailBody.Append("GoldSim Payments transaction details:");
      emailBody.AppendLine();
      emailBody.Append(" - Cardholder Name: "   + Request["cardholderName"]);
      emailBody.AppendLine();
      emailBody.Append(" - Customer Email: "    + Request["customerEmail"]);
      emailBody.AppendLine();
      emailBody.Append(" - Company Name: "      + Request["company"]);
      emailBody.AppendLine();
      emailBody.Append(" - Invoice Number: "    + Request["invoice"]);
      emailBody.AppendLine();
      emailBody.Append(" - Amount: "            + "$" + amount);
      emailBody.AppendLine();

      /*------------------------------------------------------------------------------------------------------------------------
      | Process transaction result
      \-----------------------------------------------------------------------------------------------------------------------*/
      Result<Transaction> result                = braintreeGateway.Transaction.Sale(request);

      if (result.IsSuccess()) {
        return Redirect("/Web/Purchase/PaymentConfirmation");
      }
      else if (result.Transaction != null) {
        var transaction                         = result.Transaction;

        if (!String.IsNullOrEmpty(result.Message)) {
          topicViewModel.ErrorMessages.Add("TransactionMessage", result.Message);
        }
        topicViewModel.ErrorMessages.Add("TransactionStatus", "Your transaction was unsuccessful. Please correct any errors with your submission or contact <a href=\"mailto:software@goldsim.com\">GoldSim</a> (<a href=\"tel:1-425-295-7985\">+1 (425) 295-6985</a>) for assistance.");
        return topicViewResult;
      }
      else {
        foreach (ValidationError error in result.Errors.DeepAll()) {
          topicViewModel.ErrorMessages.Add(error.Code.ToString(), error.Message);
        }
        return topicViewResult;
      }

    }

  } // Class

} // Namespace