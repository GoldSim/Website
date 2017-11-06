/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System.Web.Mvc;
using Ignia.Topics;
using Ignia.Topics.Repositories;
using Ignia.Topics.Web;
using Ignia.Topics.Web.Mvc;

namespace GoldSim.Web.Controllers {

  /*============================================================================================================================
  | CLASS: ERROR CONTROLLER
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to the views associated with 400 and 500 error results.
  /// </summary>
  public class ErrorController : Controller {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private ITopicRepository _topicRepository = null;
    private Topic _currentTopic = null;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a Topic Controller with necessary dependencies.
    /// </summary>
    /// <returns>A topic controller for loading OnTopic views.</returns>
    public ErrorController(ITopicRepository topicRepository, Topic currentTopic) {
      _topicRepository = topicRepository;
      _currentTopic = currentTopic;
    }

    /*==========================================================================================================================
    | GET: /Error
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the default custom error page for the site.
    /// </summary>
    /// <returns>The site's default error view.</returns>
    public ActionResult Index() {
      return View("Error");
    }

    /*==========================================================================================================================
    | GET: /Error/NotFound
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the custom 404 error page for the site.
    /// </summary>
    /// <returns>The site's 404 (not found) error view.</returns>
    public ActionResult NotFound() {
      return View("NotFound");
    }

    /*==========================================================================================================================
    | GET: /Error/InternalServer
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the custom 500 error page for the site.
    /// </summary>
    /// <returns>The site's 500 (internal server) error view.</returns>
    public ActionResult InternalServer() {
      return View("NotFound");
    }

  }
}