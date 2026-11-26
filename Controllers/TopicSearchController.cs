/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using GoldSim.Web.Models.Associations;
using GoldSim.Web.Models.Controllers;
using Microsoft.AspNetCore.Authorization;
using OnTopic;
using OnTopic.Querying;

namespace GoldSim.Web.Controllers {

  /*============================================================================================================================
  | CLASS: TOPIC SEARCH CONTROLLER
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Allows searching for topics containing particular search patterns based on Regular Expressions.
  /// </summary>
  /// <remarks>
  ///   Initializes a new instance of a Topic Search Controller with necessary dependencies.
  /// </remarks>
  /// <returns>A topic search controller for loading OnTopic results.</returns>
  [Authorize]
  public sealed class TopicSearchController(ITopicRepository topicRepository) : Controller {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private const               RegexOptions                    _options = RegexOptions.Compiled | RegexOptions.IgnoreCase;

    /*==========================================================================================================================
    | INDEX
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Searches all topics in the supplied <see cref="topicRepository"/> for the <paramref name="query"/>, if provided.
    /// </summary>
    /// <remarks>
    ///   Requests for <see cref="TopicSearchAction.ReplaceConfirm"/> are treated as <see cref=
    ///   "TopicSearchAction.ReplacePreview"/>; confirmed replacements must be submitted via <see cref="Replace"/>.
    /// </remarks>
    /// <param name="action">The type of request being submitted, based on <see cref="TopicSearchAction"/>.</param>
    /// <param name="scope">The scope of the topic tree to search; defaults to "Web".</param>
    /// <param name="isRegularExpression">Determines whether the search should support regular expressions; defaults to false.</param>
    /// <param name="query">The search term to look for in each attribute.</param>
    /// <param name="replace">The optional expression to replace all search results with.</param>
    [HttpGet]
    public IActionResult Index(
      [FromQuery]TopicSearchAction action,
      string scope = "Web",
      bool isRegularExpression = false,
      string query = null,
      string replace = null
    ) {

      /*-------------------------------------------------------------------------------------------------------------------------
      | Prevent confirmed replacements via GET
      \------------------------------------------------------------------------------------------------------------------------*/
      if (action is TopicSearchAction.ReplaceConfirm) {
        action                  = TopicSearchAction.ReplacePreview;
      }

      /*-------------------------------------------------------------------------------------------------------------------------
      | Show results
      \------------------------------------------------------------------------------------------------------------------------*/
      return View(Search(action, scope, isRegularExpression, query, replace));

    }

    /*==========================================================================================================================
    | REPLACE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Replaces all matches of the <paramref name="query"/> in the supplied <see cref="topicRepository"/> with the <paramref
    ///   name="replace"/> expression, and saves the changes.
    /// </summary>
    /// <remarks>
    ///   Requires a <c>POST</c> request with a valid antiforgery token, to prevent a link from modifying topics.
    /// </remarks>
    /// <param name="scope">The scope of the topic tree to search; defaults to "Web".</param>
    /// <param name="isRegularExpression">
    ///   Determines whether the search should support regular expressions; defaults to false.
    /// </param>
    /// <param name="query">The search term to look for in each attribute.</param>
    /// <param name="replace">The optional expression to replace all search results with.</param>
    [HttpPost, ActionName("Index")]
    [ValidateAntiForgeryToken]
    public IActionResult Replace(
      string scope = "Web",
      bool isRegularExpression = false,
      string query = null,
      string replace = null
    ) {

      /*-------------------------------------------------------------------------------------------------------------------------
      | Replace and show results
      \------------------------------------------------------------------------------------------------------------------------*/
      return View(Search(TopicSearchAction.ReplaceConfirm, scope, isRegularExpression, query, replace));

    }

    /*==========================================================================================================================
    | SEARCH
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Searches all topics in the supplied <see cref="topicRepository"/> for the <paramref name="query"/>, if provided, and
    ///   returns a <see cref="TopicSearchViewModel"/> containing the results.
    /// </summary>
    /// <param name="action">The type of request being submitted, based on <see cref="TopicSearchAction"/>.</param>
    /// <param name="scope">The scope of the topic tree to search.</param>
    /// <param name="isRegularExpression">Determines whether the search should support regular expressions.</param>
    /// <param name="query">The search term to look for in each attribute.</param>
    /// <param name="replace">The optional expression to replace all search results with.</param>
    [SuppressMessage("Security", "CA3012", Justification = "Internal tool, so source is trusted")]
    private TopicSearchViewModel Search(
      TopicSearchAction action,
      string scope,
      bool isRegularExpression,
      string query,
      string replace
    ) {

      /*-------------------------------------------------------------------------------------------------------------------------
      | Find topics
      \------------------------------------------------------------------------------------------------------------------------*/
      var results               = new Dictionary<AssociatedTopicViewModel, Collection<TopicSearchResult>>();
      var errors                = new Collection<string>();

      /*-------------------------------------------------------------------------------------------------------------------------
      | Find scope
      \------------------------------------------------------------------------------------------------------------------------*/
      var uniqueKey             = $"Root:{scope?.Replace("/", ":", StringComparison.Ordinal).Trim(':')}";
      var scopedTopic           = topicRepository.Load()?.GetByUniqueKey(uniqueKey);

      /*-------------------------------------------------------------------------------------------------------------------------
      | Validate inputs
      \------------------------------------------------------------------------------------------------------------------------*/

      // Validate scope
      if (scopedTopic is null) {
        errors.Add("No topic could be found at the scope. Please confirm the path.");
      }

      // Validate regular expression
      if (isRegularExpression && query is not null) {
        try {
          _ = Regex.Match(String.Empty, query);
        }
        catch (ArgumentException) {
          errors.Add("The regular expression provided is not valid. Please check the syntax.");
        }
      }

      /*-------------------------------------------------------------------------------------------------------------------------
      | Find the topic with the correct PageID.
      \------------------------------------------------------------------------------------------------------------------------*/
      if (errors.Count is 0 && scopedTopic is not null && !String.IsNullOrWhiteSpace(query)) {
        FindReplaceTopics(scopedTopic, isRegularExpression? query : Regex.Escape(query), replace, action, results);
      }

      /*-------------------------------------------------------------------------------------------------------------------------
      | Assemble view model
      \------------------------------------------------------------------------------------------------------------------------*/
      return new() {
        Id                      = -1,
        WebPath                 = "/TopicSearch/",
        UniqueKey               = "TopicSearch",
        Key                     = "Root:TopicSearch",
        Title                   = "Topic Search",
        Action                  = action,
        Scope                   = scope,
        IsRegularExpression     = isRegularExpression,
        Query                   = query,
        Replace                 = replace,
        Results                 = new(results),
        Errors                  = new(errors)
      };

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
    [SuppressMessage("Security", "CA3012", Justification = "Risk of RegEx injection acceptable for admin tool")]
    private void FindReplaceTopics(
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
          topicReference        ??= new() {
            Title               = topic.Title,
            ShortTitle          = topic.Title,
            WebPath             = topic.GetWebPath()
          };
          if (!results.TryGetValue(topicReference, out var attributeResults)) {
            attributeResults    = [];
            results.Add(topicReference, attributeResults);
          }
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
        topicRepository.Save(topic);
      }

      // Recursively replace results for each child topic
      foreach (var childTopic in topic.Children) {
        FindReplaceTopics(childTopic, query, replace, action, results);
      }

    }

  } // Class
} // Namespace