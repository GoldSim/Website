﻿/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System.Web.Mvc;
using Ignia.Topics;
using Ignia.Topics.Web;

namespace GoldSim.Web.Controllers
{

  /*============================================================================================================================
  | CLASS: HOME CONTROLLER
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to the default homepage for the site.
  /// </summary>
  public class HomeController : Controller {

    /*==========================================================================================================================
    | GET: /
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the landing page experience for the site.
    /// </summary>
    /// <returns>The site's homepage view.</returns>
    public ActionResult Index() {

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish Page Topic
      \-----------------------------------------------------------------------------------------------------------------------*/
      Topic topic               = TopicRepository.RootTopic.GetTopic("Web:Home");
      ViewBag.Topic             = topic;

      /*------------------------------------------------------------------------------------------------------------------------
      | Return the homepage view
      \-----------------------------------------------------------------------------------------------------------------------*/
      return View();

    }

  }

}