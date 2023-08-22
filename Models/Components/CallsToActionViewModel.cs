/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using GoldSim.Web.Components;
using OnTopic.AspNetCore.Mvc.Models;

namespace GoldSim.Web.Models.Components {

  /*============================================================================================================================
  | VIEW MODEL: CALLS TO ACTION
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about the <see cref="
  ///   CallsToActionViewComponent"/>.
  /// </summary>
  public class CallsToActionViewModel: NavigationViewModel<NavigationTopicViewModel> {

    /*==========================================================================================================================
    | HAS ANNOUNCEMENT?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Flags whether the site currently has the <see cref="AnnouncementViewComponent"/> enabled. If it does, this can be used
    ///   by the <see cref="CallsToActionViewComponent"/> to anchor to the <see cref="AnnouncementViewComponent"/> instead of
    ///   the <see cref="FooterViewComponent"/>.
    /// </summary>
    public bool HasAnnouncement { get; set; }

  } // Class
} // Namespace