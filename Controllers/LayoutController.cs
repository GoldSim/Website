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
  public class LayoutController : TopicController<Topic> {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private     ITopicRepository                _topicRepository        = null;
    private     Topic                           _currentTopic           = null;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a Topic Controller with necessary dependencies.
    /// </summary>
    /// <returns>A topic controller for loading OnTopic views.</returns>
    public LayoutController(ITopicRepository topicRepository, Topic currentTopic) : base(topicRepository, currentTopic) {
      _topicRepository = topicRepository;
      _currentTopic    = currentTopic;
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
      Topic currentTopic = _currentTopic;
      Topic navigationRootTopic = null;

      /*------------------------------------------------------------------------------------------------------------------------
      | Identify current topic
      >-------------------------------------------------------------------------------------------------------------------------
      | The navigation root in the case of the main menu is the namespace; i.e., the first topic underneath the root. Because
      | there are potentially three tiers of navigation, however, the currentTopic will be the upwards of four levels from the 
      | root. 
      \-----------------------------------------------------------------------------------------------------------------------*/
      while (currentTopic?.Parent?.Parent?.Parent?.Parent != null) {
        currentTopic = currentTopic.Parent;
        }

      /*------------------------------------------------------------------------------------------------------------------------
      | Identify navigation root
      >-------------------------------------------------------------------------------------------------------------------------
      | The navigation root in the case of the main menu is the namespace; i.e., the first topic underneath the root. 
      \-----------------------------------------------------------------------------------------------------------------------*/
      navigationRootTopic = currentTopic;
      while (navigationRootTopic?.Parent?.Parent != null) {
        navigationRootTopic = navigationRootTopic.Parent;
      }

      if (navigationRootTopic == null) {
        navigationRootTopic = _topicRepository.Load("Web");
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish a navigation view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var navigationViewModel = new NavigationViewModel(_topicRepository, navigationRootTopic, currentTopic);

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
      Topic currentTopic = _currentTopic;

      /*------------------------------------------------------------------------------------------------------------------------
      | Identify navigation root
      >-------------------------------------------------------------------------------------------------------------------------
      | The navigation root in the case of the page-level navigation any parent of content type "PageGroup".
      \-----------------------------------------------------------------------------------------------------------------------*/
      navigationRootTopic = currentTopic;
      if (navigationRootTopic != null) {
        while (navigationRootTopic.Parent != null && !navigationRootTopic.ContentType.Equals("PageGroup")) {
          navigationRootTopic = navigationRootTopic.Parent;
        }
      }

      if (navigationRootTopic?.Parent == null) navigationRootTopic = null;

      /*------------------------------------------------------------------------------------------------------------------------
      | Identify current topic
      >-------------------------------------------------------------------------------------------------------------------------
      | The current topic is an immediate child of the the navigation root, but an ascendent of the current page topic. This 
      | should be skipped if the navigation root is null, as that suggests the current page is not a descendent of a group page. 
      \-----------------------------------------------------------------------------------------------------------------------*/
      while (navigationRootTopic != null && currentTopic?.Parent != navigationRootTopic) {
        currentTopic = currentTopic.Parent;
      }

      if (currentTopic?.Parent != navigationRootTopic) currentTopic = null;

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish a navigation view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var navigationViewModel = new NavigationViewModel(_topicRepository, navigationRootTopic, currentTopic);

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
      Topic navigationRootTopic = _topicRepository.Load("Web:Company");
      Topic currentTopic = _currentTopic;

      /*------------------------------------------------------------------------------------------------------------------------
      | Identify current topic
      >-------------------------------------------------------------------------------------------------------------------------
      | The current topic is an immediate child of the the navigation root, but an ascendent of the current page topic. This may
      | be null if the current page is not a descendent of company. 
      \-----------------------------------------------------------------------------------------------------------------------*/
      while (currentTopic?.Parent != null && currentTopic?.Parent != navigationRootTopic) {
        currentTopic = currentTopic.Parent;
      }

      if (currentTopic?.Parent != navigationRootTopic) currentTopic = null;

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish a navigation view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var navigationViewModel = new NavigationViewModel(_topicRepository, navigationRootTopic, currentTopic);

      /*------------------------------------------------------------------------------------------------------------------------
      | Return the corresponding view
      \-----------------------------------------------------------------------------------------------------------------------*/
      return PartialView(navigationViewModel);

    }

  }
}