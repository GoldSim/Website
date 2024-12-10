/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/

namespace GoldSim.Web.Models.ContentTypes.ContentItems {

  /*============================================================================================================================
  | VIEW MODEL: FAQ ITEM TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about a <c>FAQItem</c> topic.
  /// </summary>
  internal record FaqItemTopicViewModel: ContentItemTopicViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new <see cref="FaqItemTopicViewModel"/> with an <paramref name="attributes"/> dictionary.
    /// </summary>
    /// <param name="attributes">An <see cref="AttributeDictionary"/> of attribute values.</param>
    internal FaqItemTopicViewModel(AttributeDictionary attributes) : base(attributes) { }

    /// <summary>
    ///   Initializes a new <see cref="FaqItemTopicViewModel"/> with no parameters.
    /// </summary>
    internal FaqItemTopicViewModel() { }

  } // Class
} // Namespace