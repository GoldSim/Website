/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System.Text.RegularExpressions;
using GoldSim.Web.Models.Associations;
using GoldSim.Web.Models.Controllers;
using Microsoft.AspNetCore.Authorization;
using OnTopic;

namespace GoldSim.Web.Controllers {

  /*============================================================================================================================
  | CLASS: TOPIC SEARCH CONTROLLER
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Allows searching for topics containing particular search patterns based on Regular Expressions.
  /// </summary>
  [Authorize]
  public class TopicSearchController : Controller {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private readonly            ITopicRepository                _topicRepository;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a Topic Search Controller with necessary dependencies.
    /// </summary>
    /// <returns>A topic search controller for loading OnTopic results.</returns>
    public TopicSearchController(ITopicRepository topicRepository) : base() {
      _topicRepository          = topicRepository;
    }

    /*==========================================================================================================================
    | INDEX
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Searches all topics in the supplied <see cref="_topicRepository"/> for the <paramref name="query"/>, if provided.
    /// </summary>
    /// <param name="button">The type of request being submitted; <code>Search</code> or <code>Replace</code>.</param>
    /// <param name="query">The search term to look for in each attribute.</param>
    /// <param name="replace">The optional expression to replace all search results with.</param>
    [HttpGet, HttpPost]
    public IActionResult Index(string button, string? query = null, string? replace = null) {

      /*-------------------------------------------------------------------------------------------------------------------------
      | Find topics
      \------------------------------------------------------------------------------------------------------------------------*/
      var results = new List<AssociatedTopicViewModel>();
      var isReplace = button.Equals("Replace", StringComparison.InvariantCulture);

      /*-------------------------------------------------------------------------------------------------------------------------
      | Find the topic with the correct PageID.
      \------------------------------------------------------------------------------------------------------------------------*/
      if (!String.IsNullOrWhiteSpace(query)) {
        if (isReplace) {
          ReplaceTopics(_topicRepository.Load(), query, replace, results);
        }
        else {
          FindTopics(_topicRepository.Load(), query, results);
        }
      }

      /*-------------------------------------------------------------------------------------------------------------------------
      | Assemble view model
      \------------------------------------------------------------------------------------------------------------------------*/
      var viewModel             = new TopicSearchViewModel() {
        Id                      = -1,
        WebPath                 = "/TopicSearch/",
        UniqueKey               = "TopicSearch",
        Key                     = "Root:TopicSearch",
        Title                   = "Topic Search",
        IsReplace               = isReplace,
        Query                   = query,
        Replace                 = replace,
        Results                 = new(results)
      };

      /*-------------------------------------------------------------------------------------------------------------------------
      | Show results
      \------------------------------------------------------------------------------------------------------------------------*/
      return View(viewModel);

    }

    /*==========================================================================================================================
    | FIND TOPICS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Recursively searches all topics in the <paramref name="topic"/> tree for the supplied <paramref name="query"/> and
    ///   adds any results to the <paramref name="results"/> set.
    /// </summary>
    /// <param name="topic">The <see cref="Topic"/> to search within.</param>
    /// <param name="query">The search term to look for in each attribute.</param>
    /// <param name="results">The collection of positive matches.</param>
    [HttpGet]
    private static void FindTopics(Topic topic, string query, List<AssociatedTopicViewModel> results) {

      // Search each attribute for the given query
      foreach (var attribute in topic.Attributes) {
        if (Regex.IsMatch(attribute.Value, query, RegexOptions.Compiled | RegexOptions.IgnoreCase)) {
          results.Add(
            new() {
               Title            = topic.Title?? topic.Key,
               ShortTitle       = topic.Title?? topic.Key,
               WebPath          = topic.GetWebPath()
            }
          );
          break;
        }
      }

      // Recursively search each child topic
      foreach (var childTopic in topic.Children) {
        FindTopics(childTopic, query, results);
      }

    }

    /*==========================================================================================================================
    | REPLACE TOPICS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Recursively searches all topics in the <paramref name="topic"/> tree for the supplied <paramref name="query"/> and
    ///   adds any results to the <paramref name="results"/> set. Replaces the references using the supplied <paramref
    ///   name="replace"/> expression.
    /// </summary>
    /// <param name="topic">The <see cref="Topic"/> to search within.</param>
    /// <param name="query">The search term to look for in each attribute.</param>
    /// <param name="replace">The expression to replace all search results with.</param>
    /// <param name="results">The collection of positive matches.</param>
    [HttpGet]
    private static void ReplaceTopics(Topic topic, string query, string replace, List<AssociatedTopicViewModel> results) {

      // Search each attribute for the given query
      foreach (var attribute in topic.Attributes) {
        if (Regex.IsMatch(attribute.Value, query, RegexOptions.Compiled | RegexOptions.IgnoreCase)) {
          results.Add(
            new() {
              Title            = topic.Title?? topic.Key,
              ShortTitle       = topic.Title?? topic.Key,
              WebPath          = topic.GetWebPath()
            }
          );
          break;
        }
      }

      // Recursively replace results for each child topic
      foreach (var childTopic in topic.Children) {
        ReplaceTopics(childTopic, query, replace, results);
      }

    }    
    
  } // Class
} // Namespace