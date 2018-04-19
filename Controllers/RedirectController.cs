/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System.Web.Mvc;
using Ignia.Topics;
using Ignia.Topics.Repositories;
using OnTopic = Ignia.Topics.Web.Mvc.Controllers;

namespace GoldSim.Web.Controllers {

  /*============================================================================================================================
  | CLASS: REDIRECT CONTROLLER
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Handles redirect based on e.g., TopicID or legacy PageID.
  /// </summary>
  public class RedirectController : OnTopic.RedirectController {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private                     ITopicRepository                _topicRepository                = null;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a Topic Controller with necessary dependencies.
    /// </summary>
    /// <returns>A topic controller for loading OnTopic views.</returns>
    public RedirectController(ITopicRepository topicRepository) : base(topicRepository) {
      _topicRepository          = topicRepository;
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
      var topic = FindTopicWithAttribute(_topicRepository.Load(), "PageID", pageId.ToString());

      /*-------------------------------------------------------------------------------------------------------------------------
      | Provide error handling
      \------------------------------------------------------------------------------------------------------------------------*/
      if (topic == null) {
        return HttpNotFound("Invalid PageID.");
      }

      /*-------------------------------------------------------------------------------------------------------------------------
      | Perform redirect
      \------------------------------------------------------------------------------------------------------------------------*/
      return RedirectPermanent(topic.GetWebPath());

    }

    /*===========================================================================================================================
    | METHOD: FIND TOPIC WITH ATTRIBUTE
    \--------------------------------------------------------------------------------------------------------------------------*/
    private Topic FindTopicWithAttribute(Topic rootTopic, string attributeName, string attributeValue) {
      if (rootTopic.Attributes.GetValue(attributeName) == attributeValue) {
        return rootTopic;
      }
      foreach (var topic in rootTopic.Children) {
        var returnTopic = FindTopicWithAttribute(topic, attributeName, attributeValue);
        if (returnTopic != null) return returnTopic;
      }
      return null;
    }

  } // Class

} // Namespace