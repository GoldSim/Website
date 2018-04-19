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
  | CLASS: LEGACY REDIRECT CONTROLLER
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Prior to migrating to OnTopic, GoldSim had a legacy data model where pages were mapped to /Page/ID/. With the migration
  ///   to OnTopic, those identifiers were stored as a custom attribute, <c>PageId</c>, on each topic that maps to one of the
  ///   legacy pages. To support external references that may still be pointing to these URLs, the <see
  ///   cref="LegacyRedirectController"/> provides routing that looks up topics based on the legacy <c>PageId</c> and then
  ///   redirects to the new, friendly URL.
  /// </summary>
  public class LegacyRedirectController : Controller {

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
    public LegacyRedirectController(ITopicRepository topicRepository) : base() {
      _topicRepository          = topicRepository;
    }

    /*==========================================================================================================================
    | REDIRECT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Redirect based on PageId.
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