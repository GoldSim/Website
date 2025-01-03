/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using System.Collections.ObjectModel;
using GoldSim.Web.Controllers;
using GoldSim.Web.Models.Associations;

namespace GoldSim.Web.Models.Controllers {

  /*============================================================================================================================
  | VIEW MODEL: TOPIC SEARCH
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for values associated with the <see cref="TopicSearchController"/>.
  /// </summary>
  internal sealed record TopicSearchViewModel : PageTopicViewModel {

    /*==========================================================================================================================
    | ACTION
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines the action requested.
    /// </summary>
    internal TopicSearchAction Action { get; init; }

    /*==========================================================================================================================
    | SCOPE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the requested scope.
    /// </summary>
    internal string Scope { get; init; } = "/";

    /*==========================================================================================================================
    | QUERY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the requested query.
    /// </summary>
    internal string Query { get; init; }

    /*==========================================================================================================================
    | REPLACE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the optional replacement expression.
    /// </summary>
    internal string Replace { get; init; }

    /*==========================================================================================================================
    | RESULTS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a list of results from the query.
    /// </summary>
    internal ReadOnlyDictionary<AssociatedTopicViewModel, Collection<TopicSearchResult>> Results { get; init; }

  } // Class
} // Namespace