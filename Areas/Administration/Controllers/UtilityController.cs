/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OnTopic;
using OnTopic.Collections;
using OnTopic.Querying;
using OnTopic.Repositories;

namespace GoldSim.Web.Administration.Controllers {

  /*============================================================================================================================
  | CLASS: REPORT CONTROLLER
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Allows GoldSim to report on embedded images and links.
  /// </summary>
  [Authorize]
  [Area("Administration")]
  public class UtilityController: Controller {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private readonly            ITopicRepository                _topicRepository;
    private readonly            IWebHostEnvironment             _hostingEnvironment;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref ="UtilityController"/> with necessary dependencies.
    /// </summary>
    /// <returns>A topic controller for loading OnTopic views.</returns>
    public UtilityController(
      ITopicRepository topicRepository,
      IWebHostEnvironment hostingEnvironment
    ) {
      _topicRepository          = topicRepository;
      _hostingEnvironment       = hostingEnvironment;
    }

    /*==========================================================================================================================
    | GET MATCHED TOPICS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a list of topics that match the search critera.
    /// </summary>
    private ReadOnlyTopicCollection<Topic> GetMatchedTopics(string searchText) =>
      _topicRepository.Load().FindAll(t => {
        return t.Attributes.Any(a =>
          a.Value.Contains(searchText, StringComparison.OrdinalIgnoreCase)
        );
      });

    /*==========================================================================================================================
    | GET ALL TOPICS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a list of all topics.
    /// </summary>
    private IEnumerable<Topic> GetAllTopics(Topic parentTopic = null) {
      parentTopic ??= _topicRepository.Load();
      foreach (var topic in parentTopic.Children) {
        yield return topic;
        foreach (var childTopic in GetAllTopics(topic)) {
          yield return childTopic;
        }
      }
    }

    /*==========================================================================================================================
    | ACTION: FIND
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provided a search term, will find all instances of text in the OnTopic database.
    /// </summary>
    [HttpGet]
    public IActionResult Find(string searchText) {

      var targetTopics          = GetMatchedTopics(searchText);
      var results               = new List<Tuple<string, string, string>>();

      foreach (var topic in targetTopics) {
        var url                 = topic.GetWebPath();
        foreach (var attribute in topic.Attributes) {
          if (attribute.Value.Contains(searchText, StringComparison.OrdinalIgnoreCase)) {
            results.Add(new Tuple<string, string, string>(url, attribute.Key, attribute.Value));
          }
        }
      }

      return Json(results);

    }

    /*==========================================================================================================================
    | ACTION: REPLACE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provided a search term and a replacement term, will find all instances of text in the OnTopic database and replace
    ///   them.
    /// </summary>
    [HttpGet]
    public IActionResult Replace(string searchText, string replaceText) {

      var targetTopics          = GetMatchedTopics(searchText);
      var results               = new List<Tuple<string, string, string>>();

      foreach (var topic in targetTopics) {
        var url                 = topic.GetWebPath();
        foreach (var attribute in topic.Attributes) {
          if (attribute.Value.Contains(searchText, StringComparison.OrdinalIgnoreCase)) {
            var newValue        = attribute.Value.Replace(searchText, replaceText, StringComparison.OrdinalIgnoreCase);
            topic.Attributes.SetValue(attribute.Key, newValue);
            results.Add(new Tuple<string, string, string>(url, attribute.Key, newValue));
          }
        }
        _topicRepository.Save(topic);
      }

      return Json(results);

    }

    /*==========================================================================================================================
    | CONSTANT: CONTENT FOLDERS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a hard-coded list of content directories that used to live on `www` but have now been moved to other servers—
    ///   notably `media`. This helps identify legacy references, which currently rely on the redirect.
    /// </summary>
    private List<string> SearchTerms = new List<string>() {
      "Downloads",
      "eNews",
      "FormImages",
      "Help",
      "Images/Content",
      "Images/Courses",
      "Images/Email",
      "Images/Newsletter",
      "KnowledgeBase",
      "Media",
      "Videos",
      "Webinars"
    };

    /*==========================================================================================================================
    | ACTION: UPDATE PATHS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Updates paths based on hard-coded values.
    /// </summary>
    [HttpGet]
    public IActionResult UpdatePaths() {

      var oldHostName           = "www.goldsim.com";
      var targetHostName        = "media.GoldSim.com";
      var targetTopics          = GetAllTopics();
      var results               = new List<Tuple<string, string, string>>();

      var searchTermString      = String.Join('|', SearchTerms.ToArray()).Replace(@"/", @"\/");
      var searchPattern          = @$"(""|^)(http(?:s)?:\/\/(?:www.)?(?:{oldHostName}))?\/((?:({searchTermString}))\/[^""]*)(""|$)";
      var replacementString     = $"$1https://{targetHostName}/$3$5";
      var regularExpression     = new Regex(searchPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

      foreach (var topic in targetTopics) {
        var url                 = topic.GetWebPath();
        foreach (var attribute in topic.Attributes.ToList()) {
          if (regularExpression.IsMatch(attribute.Value)) {
            var newValue        = regularExpression.Replace(attribute.Value, replacementString);
            topic.Attributes.SetValue(attribute.Key, newValue);
            results.Add(new Tuple<string, string, string>(url, attribute.Key, newValue));
          }
        }
        _topicRepository.Save(topic);
      }

      return Json(results);

    }

    /*==========================================================================================================================
    | ACTION: RESAVE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Resaves all topics to ensure any schema changes are reflected in the persistence store.
    /// </summary>
    [HttpGet]
    public IActionResult Resave() {
      _topicRepository.Save(_topicRepository.Load(), true);
      return Json(new object());
    }

  } // Class
} // Namespace