/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       GoldSim Website
\=============================================================================================================================*/
using GoldSim.Web.Models.Associations;

namespace GoldSim.Web.Components {

  /*============================================================================================================================
  | CLASS: ANNOUNCEMENT VIEW COMPONENT
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Defines a <see cref="ViewComponent"/> which renders an announcement banner in a view.
  /// </summary>
  public class AnnouncementViewComponent: ViewComponent {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="AnnouncementViewComponent"/> with necessary dependencies.
    /// </summary>
    public AnnouncementViewComponent(ITopicRepository topicRepository) {
      TopicRepository           = topicRepository;
    }

    /*==========================================================================================================================
    | TOPIC REPOSITORY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a reference to the <see cref="ITopicRepository"/> in order to allow the relevant data to be retrieved from
    ///   the <c>Web:Home</c> topic.
    /// </summary>
    /// <returns>
    ///   The <see cref="ITopicRepository"/>
    /// </returns>
    protected ITopicRepository TopicRepository { get; }

    /*==========================================================================================================================
    | METHOD: INVOKE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Renders a database.
    /// </summary>
    /// <remarks>
    ///   While it's not a perfect match, for simplicity this will reuse the existing <see cref="AssociatedTopicViewModel"/>,
    ///   which is broadly compatible with the basic requirements for the <see cref="AnnouncementViewComponent"/> view.
    /// </remarks>
    public IViewComponentResult Invoke() {

      return Content("");

    }

  } //Class
} //Namespace