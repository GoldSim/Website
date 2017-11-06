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
    | GET: /
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the default custom error page for the site.
    /// </summary>
    /// <returns>The site's default error view.</returns>
    public ActionResult Index() {
      return View();
    }

  }
}