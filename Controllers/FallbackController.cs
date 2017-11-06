/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoldSim.Web.Controllers {

  /*============================================================================================================================
  | CLASS: FALLBACK CONTROLLER
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides an empty fallback controller, which will be called if no other controller is identified. The primary purpose of
  ///   this controller is to throw a 404.
  /// </summary>

  public class FallbackController : Controller {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a DefaultController.
    /// </summary>
    /// <returns>A DefaultController.</returns>
    public FallbackController() : base() {
    }

    /*==========================================================================================================================
    | GET: INDEX
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the default page for the site.
    /// </summary>
    /// <returns>The site's default view.</returns>
    public ActionResult Index() => HttpNotFound("No controller available to handle this request.");

  } //Class

} //Namespace