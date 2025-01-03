/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System.Collections.ObjectModel;
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
  public sealed class TopicSearchController : Controller {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private readonly            ITopicRepository                _topicRepository;
    private const               RegexOptions                    _options = RegexOptions.Compiled | RegexOptions.IgnoreCase;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a Topic Search Controller with necessary dependencies.
    /// </summary>
    /// <returns>A topic search controller for loading OnTopic results.</returns>
    public TopicSearchController(ITopicRepository topicRepository) {
      _topicRepository          = topicRepository;
    }

    /*==========================================================================================================================
    | INDEX
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Searches all topics in the supplied <see cref="_topicRepository"/> for the <paramref name="query"/>, if provided.
    /// </summary>
    /// <param name="action">The type of request being submitted, based on <see cref="TopicSearchAction"/>.</param>
    /// <param name="query">The search term to look for in each attribute.</param>
    /// <param name="replace">The optional expression to replace all search results with.</param>
    [HttpGet, HttpPost]
    public IActionResult Index([FromQuery]TopicSearchAction action, string query = null, string replace = null) {

      /*-------------------------------------------------------------------------------------------------------------------------
      | Find topics
      \------------------------------------------------------------------------------------------------------------------------*/
      var results               = new Dictionary<AssociatedTopicViewModel, Collection<TopicSearchResult>>();

      /*-------------------------------------------------------------------------------------------------------------------------
      | Find the topic with the correct PageID.
      \------------------------------------------------------------------------------------------------------------------------*/
      if (!String.IsNullOrWhiteSpace(query)) {
        FindReplaceTopics(_topicRepository.Load(), query, replace, action, results);
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
        Action                  = action,
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
    | REPLACE TOPICS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Recursively searches all topics in the <paramref name="topic"/> tree for the supplied <paramref name="query"/> and
    ///   adds any results to the <paramref name="results"/> set. Optionally replaces the references using the <paramref
    ///   name="replace"/> expression, if available, and if <paramref name="action"/> is set to <see
    ///   cref="TopicSearchAction.ReplaceConfirm"/>.
    /// </summary>
    /// <param name="topic">The <see cref="Topic"/> to search within.</param>
    /// <param name="query">The search term to look for in each attribute.</param>
    /// <param name="replace">The expression to replace all search results with.</param>
    /// <param name="action">The action being performed.</param>
    /// <param name="results">The collection of positive matches.</param>
    [HttpGet]
    public void FindReplaceTopics(
      Topic topic,
      string query,
      string replace,
      TopicSearchAction action,
      Dictionary<AssociatedTopicViewModel, Collection<TopicSearchResult>> results
    ) {

      AssociatedTopicViewModel topicReference = null;

      // Search each attribute for the given query
      foreach (var attribute in topic.Attributes.ToList()) {
        var matches             = Regex.Matches(attribute.Value, query, _options);
        if (matches.Count > 0) {
          topicReference        = topicReference?? new AssociatedTopicViewModel() {
            Title               = topic.Title,
            ShortTitle          = topic.Title,
            WebPath             = topic.GetWebPath()
          };
          if (!results.TryGetValue(topicReference, out var attributeResults)) {
            attributeResults    = new Collection<TopicSearchResult>();
            results.Add(topicReference, attributeResults);
          };
          foreach (Match match in matches) {
            var result          = String.IsNullOrEmpty(replace)? null : match.Result(replace);
            attributeResults.Add(
              new() {
                AttributeKey    = attribute.Key,
                Match           = match.Value,
                RelaceResult    = result
              }
            );
          }
          if (action is TopicSearchAction.ReplaceConfirm) {
            var result          = Regex.Replace(attribute.Value, query, replace, _options);
            topic.Attributes.SetValue(attribute.Key, result);
          }
        }
      }

      if (topicReference is not null && action is TopicSearchAction.ReplaceConfirm) {
        _topicRepository.Save(topic);
      }

      // Recursively replace results for each child topic
      foreach (var childTopic in topic.Children) {
        FindReplaceTopics(childTopic, query, replace, action, results);
      }

    }

  } // Class
} // Namespace