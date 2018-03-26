/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Ignia.Topics.ViewModels;

namespace GoldSim.Web.Models {

  /*============================================================================================================================
  | VIEW MODEL: NAVIGATION TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about the navigation.
  /// </summary>
  /// <remarks>
  ///   No topics are expected to have a <c>Navigation</c> content type. Instead, this view model is expected to be manually
  ///   constructed by the <see cref="LayoutController"/>.
  /// </remarks>
  public class NavigationViewModel: PageTopicViewModel {

    public string HeaderImageUrl { get; set; }

    [DefaultValue(false)]
    public bool IsSelected { get; set; }

    public TopicViewModelCollection<NavigationViewModel> Children { get; set; }

  } //Class
} //Namespace