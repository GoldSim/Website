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
using Ignia.Topics.Mapping;
using Ignia.Topics.Repositories;
using Ignia.Topics.Web.Mvc.Controllers;
using Ignia.Topics.Web.Mvc.Models;
using GoldSim.Web.Models;
using Ignia.Topics;
using System.Threading.Tasks;
using Ignia.Topics.Web.Mvc;

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
    private     readonly        ITopicRepository                _topicRepository                = null;
    private     readonly        ITopicRoutingService            _topicRoutingService            = null;
    private     readonly        ITopicMappingService            _topicMappingService            = null;
    private                     Topic                           _currentTopic                   = null;
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
      _topicRepository          = topicRepository;
      _topicRoutingService      = topicRoutingService;
      _topicMappingService      = topicMappingService;
      _currentTopic             = topicRoutingService.GetCurrentTopic();
      _braintreeConfiguration   = braintreeConfiguration;

    }

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
    public async override Task<ActionResult> IndexAsync(string path) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish variables
      \-----------------------------------------------------------------------------------------------------------------------*/
      var braintreeGateway      = _braintreeConfiguration.GetGateway();
      var clientToken           = braintreeGateway.ClientToken.Generate();

      /*------------------------------------------------------------------------------------------------------------------------
      | Pass client token to view
      \-----------------------------------------------------------------------------------------------------------------------*/
      ViewBag.ClientToken       = clientToken;
      ViewBag.DebugData         = "test";

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish default view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var topicViewModel        = await _topicMappingService.MapAsync(CurrentTopic);

      /*------------------------------------------------------------------------------------------------------------------------
      | Return topic view
      \-----------------------------------------------------------------------------------------------------------------------*/
      //return View();
      return new TopicViewResult(topicViewModel, CurrentTopic.ContentType, CurrentTopic.View);

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
      var gateway               = _braintreeConfiguration.GetGateway();
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