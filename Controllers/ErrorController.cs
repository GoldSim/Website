/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System.Web.Mvc;
using Ignia.Topics;
using Ignia.Topics.Repositories;
using Ignia.Topics.ViewModels;

namespace GoldSim.Web.Controllers {

  /*============================================================================================================================
  | CLASS: ERROR CONTROLLER
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to the views associated with 400 and 500 error results.
  /// </summary>
  public class ErrorController : Controller {

    /*==========================================================================================================================
    | GET: /Error/Error
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the default custom error page for the site.
    /// </summary>
    /// <returns>The site's default error view.</returns>
    public ActionResult Error() {

      /*------------------------------------------------------------------------------------------------------------------------
      | Instantiate view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var viewModel             = new PageTopicViewModel();
      viewModel.Key             = "Error";
      viewModel.UniqueKey       = "Error:Error";
      viewModel.ContentType     = "Page";
      viewModel.Title           = "General Error";
      viewModel.MetaKeywords    = "";
      viewModel.MetaDescription = "";

      /*------------------------------------------------------------------------------------------------------------------------
      | Return the view
      \-----------------------------------------------------------------------------------------------------------------------*/
      return View("Error", viewModel);

    }

    /*==========================================================================================================================
    | GET: /Error/NotFound
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the custom 404 error page for the site.
    /// </summary>
    /// <returns>The site's 404 (not found) error view.</returns>
    public ActionResult NotFound() {

      /*------------------------------------------------------------------------------------------------------------------------
      | Return the proper status code
      \-----------------------------------------------------------------------------------------------------------------------*/
      Response.StatusCode       = 404;

      /*------------------------------------------------------------------------------------------------------------------------
      | Instantiate view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var viewModel             = new PageTopicViewModel();
      viewModel.Key             = "NotFound";
      viewModel.UniqueKey       = "Error:NotFound";
      viewModel.ContentType     = "Page";
      viewModel.Title           = "Page Not Found";
      viewModel.MetaKeywords    = "";
      viewModel.MetaDescription = "";

      /*------------------------------------------------------------------------------------------------------------------------
      | Return the view
      \-----------------------------------------------------------------------------------------------------------------------*/
      return View("NotFound", viewModel);

    }

    /*==========================================================================================================================
    | GET: /Error/InternalServer
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the custom 500 error page for the site.
    /// </summary>
    /// <returns>The site's 500 (internal server) error view.</returns>
    public ActionResult InternalServer() {

      /*------------------------------------------------------------------------------------------------------------------------
      | Return the proper status code
      \-----------------------------------------------------------------------------------------------------------------------*/
      Response.StatusCode       = 500;

      /*------------------------------------------------------------------------------------------------------------------------
      | Instantiate model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var viewModel             = new PageTopicViewModel();
      viewModel.Key             = "InternalServer";
      viewModel.UniqueKey       = "Error:InternalServer";
      viewModel.ContentType     = "Page";
      viewModel.Title           = "Internal Server Error";
      viewModel.MetaKeywords    = "";
      viewModel.MetaDescription = "";

      /*------------------------------------------------------------------------------------------------------------------------
      | Return the view
      \-----------------------------------------------------------------------------------------------------------------------*/
      return View("InternalServer", viewModel);

    }

  } // Class

} // Namespace