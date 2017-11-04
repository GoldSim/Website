/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ignia.Topics;
using Ignia.Topics.Repositories;
using Ignia.Topics.Web.Mvc;

namespace GoldSim.Web.Models {

  /*============================================================================================================================
  | CLASS: NAVIGATION VIEW MODEL
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   A view model for various types of navigation menues.
  /// </summary>
  /// <remarks>
  ///   In addition to a reference to the <see cref="Ignia.Topics.Repositories.ITopicRepository"/>, the 
  ///   <see cref="NavigationViewModel"/> also provides access to the parent topic of the topics that should be displayed in 
  ///   the menu as well as a reference to the child item corresponding to the current page.
  /// </remarks>
  public class NavigationViewModel : TopicViewModel {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private     Topic                           _navigationRootTopic    = null;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a Navigation View Model with appropriate dependencies.
    /// </summary>
    /// <returns>A Topic view model.</returns>
    public NavigationViewModel(ITopicRepository topicRepository, Topic navigationRootTopic, Topic topic) : base(topicRepository, topic) {
      _navigationRootTopic = navigationRootTopic;
    }

    /*==========================================================================================================================
    | NAVIGATION ROOT TOPIC
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a reference to the topic under which the navigation should be rendered.
    /// </summary>
    /// <returns>The <see cref="Ignia.Topics.Topic"/> whose children represent the navigaton elements.</returns>
    public Topic NavigationRootTopic {
      get {
        return _navigationRootTopic;
      }
    }

  } //Class
} //Namespace
