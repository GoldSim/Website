﻿/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ignia.Topics;
using Ignia.Topics.Repositories;

namespace GoldSim.Web.Controllers {

  /*============================================================================================================================
  | CLASS: REDIRECT CONTROLLER
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Handles redirect based on e.g., TopicID or legacy PageID.
  /// </summary>
  public class RedirectController : Controller {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private     ITopicRepository        _topicRepository        = null;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a Topic Controller with necessary dependencies.
    /// </summary>
    /// <returns>A topic controller for loading OnTopic views.</returns>
    public RedirectController(ITopicRepository topicRepository) : base() {
      _topicRepository          = topicRepository;
    }

    /*==========================================================================================================================
    | REDIRECT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Redirect based on TopicID
    /// </summary>
    public ActionResult TopicRedirect(int topicId) {

      /*-------------------------------------------------------------------------------------------------------------------------
      | Find the topic with the correct PageID.
      \------------------------------------------------------------------------------------------------------------------------*/
      Topic topic = _topicRepository.Load().GetTopic(topicId);

      /*-------------------------------------------------------------------------------------------------------------------------
      | Provide error handling
      \------------------------------------------------------------------------------------------------------------------------*/
      if (topic == null) {
        return HttpNotFound("Invalid TopicID.");
      }

      /*-------------------------------------------------------------------------------------------------------------------------
      | Perform redirect
      \------------------------------------------------------------------------------------------------------------------------*/
      return RedirectPermanent(topic.WebPath);

    }

    /*==========================================================================================================================
    | REDIRECT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Redirect based on TopicID
    /// </summary>
    public ActionResult LegacyRedirect(int pageId) {

      /*-------------------------------------------------------------------------------------------------------------------------
      | Find the topic with the correct PageID.
      \------------------------------------------------------------------------------------------------------------------------*/
      Topic topic = this.FindTopicWithAttribute(_topicRepository.Load(), "PageID", pageId.ToString());

      /*-------------------------------------------------------------------------------------------------------------------------
      | Provide error handling
      \------------------------------------------------------------------------------------------------------------------------*/
      if (topic == null) {
        return HttpNotFound("Invalid PageID.");
      }

      /*-------------------------------------------------------------------------------------------------------------------------
      | Perform redirect
      \------------------------------------------------------------------------------------------------------------------------*/
      return RedirectPermanent(topic.WebPath);

    }

    /*===========================================================================================================================
    | METHOD: FIND TOPIC WITH ATTRIBUTE
    \--------------------------------------------------------------------------------------------------------------------------*/
    private Topic FindTopicWithAttribute(Topic rootTopic, string attributeName, string attributeValue) {
      if (rootTopic.Attributes.Get(attributeName) == attributeValue) {
        return rootTopic;
      }
      foreach (Topic topic in rootTopic) {
        Topic returnTopic = FindTopicWithAttribute(topic, attributeName, attributeValue);
        if (returnTopic != null) return returnTopic;
      }
      return null;
    }

  } //Class

} //Namespace