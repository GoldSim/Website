/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System.Globalization;
using OnTopic;

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
  public sealed class LegacyRedirectController : Controller {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private readonly            ITopicRepository                _topicRepository;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a Topic Controller with necessary dependencies.
    /// </summary>
    /// <returns>A topic controller for loading OnTopic views.</returns>
    internal LegacyRedirectController(ITopicRepository topicRepository) {
      _topicRepository          = topicRepository;
    }

    /*==========================================================================================================================
    | REDIRECT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Redirect based on PageId.
    /// </summary>
    [HttpGet]
    public IActionResult Redirect(int pageId) {

      /*-------------------------------------------------------------------------------------------------------------------------
      | Find the topic with the correct PageID.
      \------------------------------------------------------------------------------------------------------------------------*/
      var topic = FindTopicWithAttribute(_topicRepository.Load(), "PageID", pageId.ToString(CultureInfo.InvariantCulture));

      /*-------------------------------------------------------------------------------------------------------------------------
      | Provide error handling
      \------------------------------------------------------------------------------------------------------------------------*/
      if (topic is null) {
        return NotFound("Invalid PageID.");
      }

      /*-------------------------------------------------------------------------------------------------------------------------
      | Perform redirect
      \------------------------------------------------------------------------------------------------------------------------*/
      return RedirectPermanent(topic.GetWebPath());

    }

    /*===========================================================================================================================
    | METHOD: FIND TOPIC WITH ATTRIBUTE
    \--------------------------------------------------------------------------------------------------------------------------*/
    private static Topic FindTopicWithAttribute(Topic rootTopic, string attributeName, string attributeValue) {
      if (rootTopic.Attributes.GetValue(attributeName) == attributeValue) {
        return rootTopic;
      }

      return rootTopic.Children
        .Select(topic => FindTopicWithAttribute(topic, attributeName, attributeValue))
        .FirstOrDefault(returnTopic => returnTopic is not null);

    }

  } // Class
} // Namespace