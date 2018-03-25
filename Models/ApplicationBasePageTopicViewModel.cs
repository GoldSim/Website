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
  | VIEW MODEL: APPLICATION BASE PAGE TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about a <c>ApplicationBasePage</c>
  ///   topic.
  /// </summary>
  /// <remarks>
  ///   It is not expected that any topics will directly implement the <c>ApplicationBasePage</c> content type that corresponds
  ///   to this view model. That said, it provides a base schema definition for e.g. <see cref="ApplicationPageTopicViewModel"/>
  ///   and <see cref="ExampleApplicationTopicViewModel"/>.
  /// </remarks>
  public class ApplicationBasePageTopicViewModel : TopicViewModel {

    public string ThumbnailImageUrl { get; set; }
    public string Category { get; set; }
    public string LearnMoreUrl { get; set; }
    [DefaultValue("Learn More")]
    public string LearnMoreLabel { get; set; }

  } // Class

} // Namespace
