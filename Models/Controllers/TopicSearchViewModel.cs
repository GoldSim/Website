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
  public record TopicSearchViewModel: PageTopicViewModel {

    /*==========================================================================================================================
    | QUERY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the requested query, so it can be reapplied to the search box.
    /// </summary>
    public string Query{ get; init; }

    /*==========================================================================================================================
    | RESULTS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a list of results from the query.
    /// </summary>
    public ReadOnlyCollection<AssociatedTopicViewModel> Results { get; init; }

  } // Class
} // Namespace