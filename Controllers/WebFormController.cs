/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System.Web.Mvc;

namespace GoldSim.Web.Controllers {

  /*============================================================================================================================
  | CLASS: WEB FORM CONTROLLER
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Handles pass-through rendering of MVC partial views for Web Forms (ASPX) pages.
  /// </summary>
  /// <remarks>
  ///   Adapted from solution described at https://stackoverflow.com/questions/702746/how-to-include-a-partial-view-inside-a-webform#answer-24867151
  /// </remarks>
  public class WebFormController : Controller {

    /*==========================================================================================================================
    | PARTIAL RENDER
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the pass-through partial view.
    /// </summary>
    public ActionResult PartialRender() => PartialView();

  } // Class

} // Namespace