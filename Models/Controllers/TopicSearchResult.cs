/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using GoldSim.Web.Controllers;

namespace GoldSim.Web.Models.Controllers {

  /*============================================================================================================================
  | VIEW MODEL: TOPIC SEARCH RESULT
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for reporting an individual match discovered and returned via the <see
  ///   cref="TopicSearchController"/>.
  /// </summary>
  internal sealed record TopicSearchResult {

    /*==========================================================================================================================
    | ATTRIBUTE KEY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   The key of the <see cref="AttributeRecord.Key"/> discovered.
    /// </summary>
    internal string AttributeKey { get; init; }

    /*==========================================================================================================================
    | MATCH
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the string that matched the supplied query.
    /// </summary>
    internal string Match { get; init; }

    /*==========================================================================================================================
    | REPLACE RESULT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the result of the replacement expression, if provided.
    /// </summary>
    internal string RelaceResult { get; init; }

  } // Class
} // Namespace