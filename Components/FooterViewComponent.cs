﻿/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ignia.Topics.Mapping;
using Ignia.Topics;
using Ignia.Topics.AspNetCore.Mvc.Models;
using Ignia.Topics.AspNetCore.Mvc.Components;
using Ignia.Topics.Repositories;
using GoldSim.Web.Models.ViewModels;

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
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private readonly            ITopicRepository                _topicRepository;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="FooterViewComponent"/> with necessary dependencies.
    /// </summary>
    /// <returns>A <see cref="FooterViewComponent"/>.</returns>
    public FooterViewComponent(
      ITopicRoutingService topicRoutingService,
      ITopicRepository topicRepository,
      IHierarchicalTopicMappingService<NavigationTopicViewModel> hierarchicalTopicMappingService
    ) : base(
      topicRoutingService,
      hierarchicalTopicMappingService
    ) {
      _topicRepository = topicRepository;
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
      var navigationRootTopic = _topicRepository.Load("Web:Company");
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