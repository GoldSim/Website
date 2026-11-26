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
    private const               RegexOptions                    _options = RegexOptions.Compiled;

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
    /// <param name="scope">The scope of the topic tree to search; defaults to the entire topic tree.</param>
    /// <param name="isRegularExpression">Determines whether the search should support regular expressions; defaults to false.</param>
    /// <param name="isCaseSensitive">Determines whether the search should be case sensitive; defaults to false.</param>
    /// <param name="query">The search term to look for in each attribute.</param>
    /// <param name="replace">The optional expression to replace all search results with.</param>
    [HttpGet]
    public IActionResult Index(
      [FromQuery]TopicSearchAction action,
      string scope = null,
      bool isRegularExpression = false,
      bool isCaseSensitive = false,
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
      return View(Search(action, scope, isRegularExpression, isCaseSensitive, query, replace));

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
    /// <param name="scope">The scope of the topic tree to search; defaults to the entire topic tree.</param>
    /// <param name="isRegularExpression">
    ///   Determines whether the search should support regular expressions; defaults to false.
    /// </param>
    /// <param name="isCaseSensitive">Determines whether the search should be case sensitive; defaults to false.</param>
    /// <param name="query">The search term to look for in each attribute.</param>
    /// <param name="replace">The optional expression to replace all search results with.</param>
    [HttpPost, ActionName("Index")]
    [ValidateAntiForgeryToken]
    public IActionResult Replace(
      string scope = null,
      bool isRegularExpression = false,
      bool isCaseSensitive = false,
      string query = null,
      string replace = null
    ) {

      /*-------------------------------------------------------------------------------------------------------------------------
      | Find topics
      \------------------------------------------------------------------------------------------------------------------------*/
      var viewModel             = Search(
        TopicSearchAction.ReplaceConfirm,
        scope,
        isRegularExpression,
        isCaseSensitive,
        query,
        replace
      );

      /*-------------------------------------------------------------------------------------------------------------------------
      | Replace topics
      \------------------------------------------------------------------------------------------------------------------------*/
      var scopedTopic           = GetScopedTopic(scope);

      if (scopedTopic is not null && !String.IsNullOrWhiteSpace(query) && viewModel.Results.Count > 0) {
        ReplaceTopics(scopedTopic, isRegularExpression? query : Regex.Escape(query), replace, GetOptions(isCaseSensitive));
      }

      /*-------------------------------------------------------------------------------------------------------------------------
      | Show results
      \------------------------------------------------------------------------------------------------------------------------*/
      return View(viewModel);

    }

    /*==========================================================================================================================
    | SEARCH
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Searches all topics in the supplied <see cref="topicRepository"/> for the <paramref name="query"/>, if provided, and
    ///   returns a <see cref="TopicSearchViewModel"/> containing the results. Does not modify any topics.
    /// </summary>
    /// <param name="action">The type of request being submitted, based on <see cref="TopicSearchAction"/>.</param>
    /// <param name="scope">The scope of the topic tree to search.</param>
    /// <param name="isRegularExpression">Determines whether the search should support regular expressions.</param>
    /// <param name="isCaseSensitive">Determines whether the search should be case sensitive.</param>
    /// <param name="query">The search term to look for in each attribute.</param>
    /// <param name="replace">The optional expression to replace all search results with.</param>
    [SuppressMessage("Security", "CA3012", Justification = "Internal tool, so source is trusted")]
    private TopicSearchViewModel Search(
      TopicSearchAction action,
      string scope,
      bool isRegularExpression,
      bool isCaseSensitive,
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
      var scopedTopic           = GetScopedTopic(scope);

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
        FindTopics(scopedTopic, isRegularExpression? query : Regex.Escape(query), replace, GetOptions(isCaseSensitive), results);
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
        IsCaseSensitive         = isCaseSensitive,
        Query                   = query,
        Replace                 = replace,
        Results                 = new(results),
        Errors                  = new(errors)
      };

    }

    /*==========================================================================================================================
    | FIND TOPICS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Recursively searches all topics in the <paramref name="topic"/> tree for the supplied <paramref name="query"/> and
    ///   adds any results to the <paramref name="results"/> set. Does not modify any topics.
    /// </summary>
    /// <param name="topic">The <see cref="Topic"/> to search within.</param>
    /// <param name="query">The search term to look for in each attribute.</param>
    /// <param name="replace">The expression used to preview the replacement of all search results.</param>
    /// <param name="options">The <see cref="RegexOptions"/> to apply when searching.</param>
    /// <param name="results">The collection of positive matches.</param>
    [SuppressMessage("Security", "CA3012", Justification = "Risk of RegEx injection acceptable for admin tool")]
    private static void FindTopics(
      Topic topic,
      string query,
      string replace,
      RegexOptions options,
      Dictionary<AssociatedTopicViewModel, Collection<TopicSearchResult>> results
    ) {

      AssociatedTopicViewModel topicReference = null;

      // Search each attribute for the given query
      foreach (var attribute in topic.Attributes.ToList()) {
        var matches             = Regex.Matches(attribute.Value, query, options);
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
        }
      }

      // Recursively find results for each child topic
      foreach (var childTopic in topic.Children) {
        FindTopics(childTopic, query, replace, options, results);
      }

    }

    /*==========================================================================================================================
    | REPLACE TOPICS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Recursively replaces all matches for the <paramref name="query"/> within the attributes of the <paramref
    ///   name="topic"/> tree with the <paramref name="replace"/> expression, and saves any modified topics.
    /// </summary>
    /// <param name="topic">The <see cref="Topic"/> to replace within.</param>
    /// <param name="query">The search term to look for in each attribute.</param>
    /// <param name="replace">The expression to replace all search results with.</param>
    /// <param name="options">The <see cref="RegexOptions"/> to apply when searching.</param>
    [SuppressMessage("Security", "CA3012", Justification = "Risk of RegEx injection acceptable for admin tool")]
    private void ReplaceTopics(Topic topic, string query, string replace, RegexOptions options) {

      var isModified            = false;

      // Replace matches in each attribute
      foreach (var attribute in topic.Attributes.ToList()) {
        if (attribute is { Key: not null, Value: not null } && Regex.IsMatch(attribute.Value, query, options)) {
          var result            = Regex.Replace(attribute.Value, query, replace, options);
          topic.Attributes.SetValue(attribute.Key, result);
          isModified            = true;
        }
      }

      // Save the topic, if modified
      if (isModified) {
        topicRepository.Save(topic);
      }

      // Recursively replace results for each child topic
      foreach (var childTopic in topic.Children) {
        ReplaceTopics(childTopic, query, replace, options);
      }

    }

    /*==========================================================================================================================
    | GET OPTIONS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Establishes the <see cref="RegexOptions"/> to use when searching, based on whether the search is case-sensitive.
    /// </summary>
    /// <param name="isCaseSensitive">Determines whether the search should be case-sensitive.</param>
    private static RegexOptions GetOptions(bool isCaseSensitive) {

      /*-------------------------------------------------------------------------------------------------------------------------
      | Establish options
      \------------------------------------------------------------------------------------------------------------------------*/
      return isCaseSensitive? _options : _options | RegexOptions.IgnoreCase;

    }

    /*==========================================================================================================================
    | GET SCOPED TOPIC
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Retrieves the <see cref="Topic"/> from the <see cref="topicRepository"/> that corresponds to the <paramref
    ///   name="scope"/>, if it exists.
    /// </summary>
    /// <param name="scope">The scope of the topic tree to search.</param>
    private Topic GetScopedTopic(string scope) {

      /*-------------------------------------------------------------------------------------------------------------------------
      | Find scope
      \------------------------------------------------------------------------------------------------------------------------*/
      var uniqueKey             = $"Root:{scope?.Replace("/", ":", StringComparison.Ordinal).Trim(':')}";

      return topicRepository.Load()?.GetByUniqueKey(uniqueKey);

    }

  } // Class
} // Namespace