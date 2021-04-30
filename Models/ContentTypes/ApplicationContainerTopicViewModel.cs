/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using System;
using OnTopic.Mapping.Annotations;
using OnTopic.Models;
using OnTopic.ViewModels;

namespace GoldSim.Web.Models.ContentTypes {

  /*============================================================================================================================
  | VIEW MODEL: APPLICATION CONTAINER TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about a <c>ApplicationContainer</c>
  ///   topic.
  /// </summary>
  public record ApplicationContainerTopicViewModel: ICoreTopicViewModel {

    /*==========================================================================================================================
    | KEY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <inheritdoc/>
    public string Key { get; init; }

    /*==========================================================================================================================
    | CONTENT TYPE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <inheritdoc/>
    public string ContentType { get; init; }

    /*==========================================================================================================================
    | CHILDREN
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a reference to all <see cref="ApplicationPageTopicViewModel"/> instances within the current container.
    /// </summary>
    [MapAs(typeof(AssociatedTopicViewModel))]
    public TopicViewModelCollection<AssociatedTopicViewModel> Children { get; } = new();

    /*==========================================================================================================================
    | DISPLAY ORDER
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a mechanism to control sorting of the application containers on e.g. an index page.
    /// </summary>
    public string DisplayOrder { get; init; }

    /*==========================================================================================================================
    | GET IMAGE KEY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Returns a key for the current category which can be used to lookup an appropriate image for the current category.
    /// </summary>
    /// <remarks>
    ///   Each category of application uses an image associated with one of the modules. Since the image names can't be sourced
    ///   based on the <see cref="TopicViewModel.Key"/>, this method provides a mapping between the application category key
    ///   and the module key.
    /// </remarks>
    /// <returns></returns>
    public string GetImageKey() =>
      Key switch {
        "EnvironmentalSystems"  => "CT",
        "EngineeredSystems"     => "RL",
        "BusinessSystems"       => "FN",
        _                       => "CT"
      };

    /*==========================================================================================================================
    | GET CONTAINER KEY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   While the container keys end with <c>Systems</c> (e.g., <c>EnvironmentalSystems</c>, the category keys do not. This
    ///   method simply strips <c>Systems</c> off the container key.
    /// </summary>
    /// <returns></returns>
    public string GetContainerKey() => Key.Substring(0, Key.IndexOf("Systems", StringComparison.OrdinalIgnoreCase));

  } // Class
} // Namespace