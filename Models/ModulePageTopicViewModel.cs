/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using Ignia.Topics.ViewModels;
using Ignia.Topics.Mapping;
using System.ComponentModel;

namespace GoldSim.Web.Models {

  /*============================================================================================================================
  | VIEW MODEL: MODULE PAGE TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about a <c>ModulePage</c>
  ///   topic.
  /// </summary>
  public class ModulePageTopicViewModel : PageTopicViewModel, ICardViewModel {

    public string ThumbnailImage { get; set; }

  } // Class

} // Namespace
