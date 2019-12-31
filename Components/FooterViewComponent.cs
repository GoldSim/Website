/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System.Threading.Tasks;
using GoldSim.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using OnTopic.AspNetCore.Mvc.Components;
using OnTopic.AspNetCore.Mvc.Models;
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
      var navigationRootTopic = TopicRepository.Load("Web:Company");
      var currentTopic = CurrentTopic;

      /*------------------------------------------------------------------------------------------------------------------------
      | Construct view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var navigationViewModel = new NavigationViewModel<NavigationTopicViewModel>() {
        NavigationRoot = await HierarchicalTopicMappingService.GetRootViewModelAsync(navigationRootTopic),
        CurrentKey = CurrentTopic?.GetUniqueKey()
      };

      /*------------------------------------------------------------------------------------------------------------------------
      | Return the corresponding view
      \-----------------------------------------------------------------------------------------------------------------------*/
      return View(navigationViewModel);

    }

  } // Class

} // Namespace