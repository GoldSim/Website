/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ignia.Topics;
using Ignia.Topics.Repositories;
using Ignia.Topics.Web.Mvc;
using GoldSim.Web.Models;

namespace GoldSim.Web.Controllers {

  /*============================================================================================================================
  | CLASS: LAYOUT CONTROLLER
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to the default homepage for the site.
  /// </summary>
  public class LayoutController : TopicController {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a Topic Controller with necessary dependencies.
    /// </summary>
    /// <returns>A topic controller for loading OnTopic views.</returns>
    public LayoutController(
      ITopicRepository topicRepository,
      ITopicRoutingService mvcTopicRoutingService,
      ITopicMappingService topicMappingService
    ) : base(topicRepository, mvcTopicRoutingService, topicMappingService) {
    }

    /*==========================================================================================================================
    | MENU
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the global menu for the site layout, which exposes the top two tiers of navigation.
    /// </summary>
    public PartialViewResult Menu() {

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish variables
      \-----------------------------------------------------------------------------------------------------------------------*/
      var currentTopic          = CurrentTopic;
      Topic navigationRootTopic = null;

      /*------------------------------------------------------------------------------------------------------------------------
      | Identify current topic
      >-------------------------------------------------------------------------------------------------------------------------
      | Since the current topic logic for the main menu is complicated by the fact that a) it is a hierarchical menu, and b)
      | not all menu items are displayed according to their depth (e.g., due to PageGroup and hidden topics), this will simply
      | return the current topic, and allow the view to determine if it is a decendent of the navigation item.
      \-----------------------------------------------------------------------------------------------------------------------*/

      /*------------------------------------------------------------------------------------------------------------------------
      | Identify navigation root
      >-------------------------------------------------------------------------------------------------------------------------
      | The navigation root in the case of the main menu is the namespace; i.e., the first topic underneath the root.
      \-----------------------------------------------------------------------------------------------------------------------*/
      navigationRootTopic = currentTopic;
      while (navigationRootTopic?.Parent?.Parent != null) {
        navigationRootTopic     = navigationRootTopic.Parent;
      }

      if (navigationRootTopic == null) {
        navigationRootTopic     = TopicRepository.Load("Web");
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish a navigation view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var navigationViewModel   = new NavigationViewModel(TopicRepository, navigationRootTopic, currentTopic);

      /*------------------------------------------------------------------------------------------------------------------------
      | Return the corresponding view
      \-----------------------------------------------------------------------------------------------------------------------*/
      return PartialView(navigationViewModel);

    }


    /*==========================================================================================================================
    | PAGE LEVEL NAVIGATION
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides page-level navigation for the current page.
    /// </summary>
    public PartialViewResult PageLevelNavigation() {

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish variables
      \-----------------------------------------------------------------------------------------------------------------------*/
      Topic navigationRootTopic = null;
      var currentTopic          = CurrentTopic;

      /*------------------------------------------------------------------------------------------------------------------------
      | Identify navigation root
      >-------------------------------------------------------------------------------------------------------------------------
      | The navigation root in the case of the page-level navigation any parent of content type "PageGroup".
      \-----------------------------------------------------------------------------------------------------------------------*/
      navigationRootTopic       = currentTopic;
      if (navigationRootTopic != null) {
        while (navigationRootTopic.Parent != null && !navigationRootTopic.ContentType.Equals("PageGroup")) {
          navigationRootTopic   = navigationRootTopic.Parent;
        }
      }

      if (navigationRootTopic?.Parent == null) navigationRootTopic = null;

      /*------------------------------------------------------------------------------------------------------------------------
      | Identify current topic
      >-------------------------------------------------------------------------------------------------------------------------
      | The current topic is an immediate child of the the navigation root, but an ascendent of the current page topic. This
      | should be skipped if the navigation root is null, as that suggests the current page is not a descendent of a group page.
      \-----------------------------------------------------------------------------------------------------------------------*/
      while (
        navigationRootTopic != null &&
        currentTopic != null &&
        currentTopic != navigationRootTopic &&
        currentTopic?.Parent != navigationRootTopic
      ) {
        currentTopic            = currentTopic.Parent;
      }

      if (currentTopic?.Parent != navigationRootTopic) currentTopic = null;

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish a navigation view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var navigationViewModel   = new NavigationViewModel(TopicRepository, navigationRootTopic, currentTopic);

      /*------------------------------------------------------------------------------------------------------------------------
      | Return the corresponding view
      \-----------------------------------------------------------------------------------------------------------------------*/
      return PartialView(navigationViewModel);

    }

    /*==========================================================================================================================
    | CALLS TO ACTION
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the Calls To Action panel for the site layout.
    /// </summary>
    public PartialViewResult CallsToAction() {

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish variables
      \-----------------------------------------------------------------------------------------------------------------------*/
      var currentTopic          = CurrentTopic;
      Topic navigationRootTopic = null;

      /*------------------------------------------------------------------------------------------------------------------------
      | Identify navigation root
      >-------------------------------------------------------------------------------------------------------------------------
      | The navigation root in the case of the main menu is the namespace; i.e., the first topic underneath the root.
      \-----------------------------------------------------------------------------------------------------------------------*/
      navigationRootTopic       = currentTopic;
      while (navigationRootTopic?.Parent?.Parent != null) {
        navigationRootTopic     = navigationRootTopic.Parent;
      }

      if (navigationRootTopic == null) {
        navigationRootTopic     = TopicRepository.Load("Web");
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish a navigation view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var navigationViewModel   = new NavigationViewModel(TopicRepository, navigationRootTopic, currentTopic);

      /*------------------------------------------------------------------------------------------------------------------------
      | Return the corresponding view
      \-----------------------------------------------------------------------------------------------------------------------*/
      return PartialView(navigationViewModel);

    }

    /*==========================================================================================================================
    | FOOTER
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the footer for the site layout, which exposes the navigation from the company.
    /// </summary>
    public PartialViewResult Footer() {

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish variables
      \-----------------------------------------------------------------------------------------------------------------------*/
      var navigationRootTopic   = TopicRepository.Load("Web:Company");
      var currentTopic          = CurrentTopic;

      /*------------------------------------------------------------------------------------------------------------------------
      | Identify current topic
      >-------------------------------------------------------------------------------------------------------------------------
      | The current topic is an immediate child of the the navigation root, but an ascendent of the current page topic. This may
      | be null if the current page is not a descendent of company.
      \-----------------------------------------------------------------------------------------------------------------------*/
      while (currentTopic?.Parent != null && currentTopic?.Parent != navigationRootTopic) {
        currentTopic            = currentTopic.Parent;
      }

      if (currentTopic?.Parent != navigationRootTopic) currentTopic = null;

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish a navigation view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var navigationViewModel   = new NavigationViewModel(TopicRepository, navigationRootTopic, currentTopic);

      /*------------------------------------------------------------------------------------------------------------------------
      | Return the corresponding view
      \-----------------------------------------------------------------------------------------------------------------------*/
      return PartialView(navigationViewModel);

    }

  }
}