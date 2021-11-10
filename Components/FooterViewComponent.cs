/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using GoldSim.Web.Models;
using Microsoft.AspNetCore.Mvc;
using OnTopic.AspNetCore.Mvc.Components;
using OnTopic.Mapping.Hierarchical;
using OnTopic.Repositories;

namespace GoldSim.Web.Components {

  /*============================================================================================================================
  | CLASS: FOOTER VIEW COMPONENT
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Defines a <see cref="ViewComponent"/> which provides access to a menu of <typeparamref name="NavigationTopicViewModel"/>
  ///   instances representing the footer of the site.
  /// </summary>
  public class FooterViewComponent: NavigationTopicViewComponentBase<NavigationTopicViewModel> {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="FooterViewComponent"/> with necessary dependencies.
    /// </summary>
    /// <returns>A <see cref="FooterViewComponent"/>.</returns>
    public FooterViewComponent(
      ITopicRepository topicRepository,
      IHierarchicalTopicMappingService<NavigationTopicViewModel> hierarchicalTopicMappingService
    ) : base(
      topicRepository,
      hierarchicalTopicMappingService
    ) {
    }

    /*==========================================================================================================================
    | METHOD: INVOKE (ASYNC)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the footer menu for the site.
    /// </summary>
    public async Task<IViewComponentResult> InvokeAsync() {

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish variables
      \-----------------------------------------------------------------------------------------------------------------------*/
      var navigationRootTopic   = TopicRepository.Load("Web:Company");
      var webPath               = CurrentTopic?.GetWebPath();
      var isInWeb               = CurrentTopic?.GetUniqueKey().StartsWith("Root:Web", StringComparison.OrdinalIgnoreCase);
      var navigationRoot        = CurrentTopic?.Attributes.GetValue("NavigationRoot", null, true);

      /*------------------------------------------------------------------------------------------------------------------------
      | Construct view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var navigationViewModel   = new FooterViewModel() {
        NavigationRoot          = await HierarchicalTopicMappingService.GetRootViewModelAsync(navigationRootTopic).ConfigureAwait(true),
        CurrentWebPath          = webPath,
        IsMainSite              = navigationRoot?.Equals("Web", StringComparison.OrdinalIgnoreCase)?? isInWeb?? true
      };

      /*------------------------------------------------------------------------------------------------------------------------
      | Return the corresponding view
      \-----------------------------------------------------------------------------------------------------------------------*/
      return View(navigationViewModel);

    }

  } // Class
} // Namespace