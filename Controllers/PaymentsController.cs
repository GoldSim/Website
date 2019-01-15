/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Braintree;
using Ignia.Topics.Repositories;
using Ignia.Topics.Web.Mvc.Models;
using GoldSim.Web.Models;
using Ignia.Topics;

namespace GoldSim.Web.Controllers {

  /*============================================================================================================================
  | CLASS: PAYMENTS CONTROLLER
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to the Payments page of the website, with Braintree Payments integration functionality.
  /// </summary>
  public class PaymentsController : Controller {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private     readonly        ITopicRepository                _topicRepository                = null;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a Topic Controller with necessary dependencies.
    /// </summary>
    /// <returns>A topic controller for loading OnTopic views.</returns>
    public PaymentsController(ITopicRepository topicRepository) {
      _topicRepository = topicRepository;
    }

    /*==========================================================================================================================
    | BRAINTREE CONFIGURATION
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides access to the Braintree Gateway configuration.
    /// </summary>
    public IBraintreeConfiguration BraintreeConfiguration = new BraintreeConfiguration();

    /*==========================================================================================================================
    | TRANSACTION SUCESS STATUSES
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
    public virtual ActionResult Index(string id = "") {

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish variables
      \-----------------------------------------------------------------------------------------------------------------------*/
      var braintreeGateway      = BraintreeConfiguration.GetGateway();
      var clientToken           = braintreeGateway.ClientToken.Generate();
      Transaction transaction   = null;
      Topic paymentsTopic       = _topicRepository.Load("Root:Web:Purchase:Payments");
      if (!String.IsNullOrEmpty(id)) {
        transaction             = braintreeGateway.Transaction.Find(id);
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish Page Topic
      \-----------------------------------------------------------------------------------------------------------------------*/
      var topicViewModel        = new TopicEntityViewModel(_topicRepository, paymentsTopic);

      /*------------------------------------------------------------------------------------------------------------------------
      | Pass client token to view
      \-----------------------------------------------------------------------------------------------------------------------*/
      ViewBag.ClientToken       = clientToken;
      ViewBag.DebugData         = "test";

      /*------------------------------------------------------------------------------------------------------------------------
      | Handle transaction results
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (transaction != null && TransactionSuccessStatuses.Contains(transaction.Status)) {
        TempData["ConfirmationMessage"]         = "Your payment has been successfully submitted.";
      }
      else {
        TempData["ConfirmationMessage"]         = "There was a problem with your submission. Please check your information and try again.";
      };

      ViewBag.Transaction = transaction;

      /*------------------------------------------------------------------------------------------------------------------------
      | Return view
      \-----------------------------------------------------------------------------------------------------------------------*/
      return View("Payments", topicViewModel);

    }

    /*==========================================================================================================================
    | POST: CREATE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides payments form processing
    /// </summary>
    /// <returns>A view associated with the requested topic's Content Type and view.</returns>
    public ActionResult Create() {

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish variables
      \-----------------------------------------------------------------------------------------------------------------------*/
      var gateway               = BraintreeConfiguration.GetGateway();
      Decimal amount;

      /*------------------------------------------------------------------------------------------------------------------------
      | Verify payment amount format
      \-----------------------------------------------------------------------------------------------------------------------*/
      try {
        amount                  = Convert.ToDecimal(Request["amount"]);
      }
      catch (FormatException e) {
        return RedirectToAction("Index");
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Assemble and send Braintree transaction
      \-----------------------------------------------------------------------------------------------------------------------*/
      var nonce                 = Request["paymentMethodNonce"];
      var request               = new TransactionRequest {
        Amount                  = amount,
        PaymentMethodNonce      = nonce,
        CustomFields            = new Dictionary<string, string> {
          { "company", Request["company"] },
          { "invoice", Request["invoice"] }
        },
        Options                 = new TransactionOptionsRequest {
          SubmitForSettlement   = true
        }
      };

      /*------------------------------------------------------------------------------------------------------------------------
      | Process transaction result
      \-----------------------------------------------------------------------------------------------------------------------*/
      Result<Transaction> result                = gateway.Transaction.Sale(request);
      if (result.IsSuccess()) {
        Transaction transaction                 = result.Target;
        return RedirectToAction("Index", new { id = transaction.Id });
      }
      else if (result.Transaction != null) {
        return RedirectToAction("Index", new { id = result.Transaction.Id } );
      }
      else {
        string errorMessages                    = "";
        foreach (ValidationError error in result.Errors.DeepAll()) {
          errorMessages += "Error: " + (int)error.Code + " - " + error.Message + "\n";
        }
        // TempData["Flash"]                    = errorMessages;
        return RedirectToAction("Index");
      }

    }

  } // Class

} // Namespace