/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ignia.Topics.Mapping;
using Ignia.Topics.ViewModels;

namespace GoldSim.Web.Models {

  /*============================================================================================================================
  | VIEW MODEL: HOME TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about a <c>Home</c> topic.
  /// </summary>
  public class HomeTopicViewModel: ContentItemTopicViewModel {

    [Recurse(Relationships.Children)]
    public TopicViewModelCollection<PageTopicViewModel> Related { get; set; }

  } //Class
} //Namespace
