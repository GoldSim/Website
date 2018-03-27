/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System.Web.Mvc;
using Ignia.Topics;
using Ignia.Topics.Repositories;

namespace GoldSim.Web.Controllers {

  /*============================================================================================================================
  | CLASS: SITEMAP CONTROLLER
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to the default homepage for the site.
  /// </summary>
  public class SitemapController : Controller {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private     ITopicRepository        _topicRepository        = null;
    private     Topic                   _currentTopic           = null;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a Topic Controller with necessary dependencies.
    /// </summary>
    /// <returns>A topic controller for loading OnTopic views.</returns>
    public SitemapController(ITopicRepository topicRepository, Topic currentTopic) {
      _topicRepository          = topicRepository;
      _currentTopic             = currentTopic;
    }

    /*==========================================================================================================================
    | GET: /
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the Sitemap.org sitemap for the site.
    /// </summary>
    /// <returns>The site's homepage view.</returns>
    public ActionResult Index() {

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish Page Topic
      \-----------------------------------------------------------------------------------------------------------------------*/
      Topic                     topic           = _topicRepository.Load();
      TopicEntityViewModel      topicViewModel  = new TopicEntityViewModel(_topicRepository, _topicRepository.Load());

      /*------------------------------------------------------------------------------------------------------------------------
      | DEFINE CONTENT TYPE
      \-----------------------------------------------------------------------------------------------------------------------*/
      Response.ContentType      = "text/xml";

      /*------------------------------------------------------------------------------------------------------------------------
      | Return the homepage view
      \-----------------------------------------------------------------------------------------------------------------------*/
      return View("Sitemap", topicViewModel);

    }

  } // Class

} // Namespace