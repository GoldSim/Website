/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using GoldSim.Web.Models.Components;
using OnTopic.AspNetCore.Mvc.Components;
using OnTopic.AspNetCore.Mvc.Models;
using OnTopic.Mapping.Hierarchical;

namespace GoldSim.Web.Components {

  /*============================================================================================================================
  | CLASS: PAGE-LEVEL NAVIGATION VIEW COMPONENT
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Defines a <see cref="ViewComponent"/> which provides access to a menu of <typeparamref name="NavigationTopicViewModel"/>
  ///   instances representing the nearest page-level navigation.
  /// </summary>
  /// <remarks>
  ///   <para>
  ///     As a best practice, global data required by the layout view are requested independent of the current page. This
  ///     allows each layout element to be provided with its own layout data, in the form of <see
  ///     cref="NavigationViewModel{T}"/>s, instead of needing to add this data to every view model returned by <see
  ///     cref="TopicController"/>.
  ///   </para>
  /// </remarks>
  public class PageLevelNavigationViewComponent : PageLevelNavigationViewComponentBase<PageLevelNavigationTopicViewModel> {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="MenuViewComponentBase{T}"/> with necessary dependencies.
    /// </summary>
    /// <returns>A topic controller for loading OnTopic views.</returns>
    public PageLevelNavigationViewComponent(
      ITopicRepository topicRepository,
      IHierarchicalTopicMappingService<PageLevelNavigationTopicViewModel> hierarchicalTopicMappingService
    ) : base(
      topicRepository,
      hierarchicalTopicMappingService
    ) {}

  } // Class
} // Namespace