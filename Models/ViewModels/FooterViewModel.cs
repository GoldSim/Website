/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using GoldSim.Web.Models.ViewModels;
using OnTopic.AspNetCore.Mvc.Models;

namespace GoldSim.Web.Models.ViewModels {

  /*============================================================================================================================
  | VIEW MODEL: FOOTER
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about the footer.
  /// </summary>
  public class FooterViewModel: NavigationViewModel<NavigationTopicViewModel> {

    /*==========================================================================================================================
    | IS MAIN SITE?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Flags whether the current site is part of the main <c>web</c> or is part of an alternate site, which may have a
    ///   different navigation.
    /// </summary>
    public bool IsMainSite { get; set; }

  } // Class
} // Namespace