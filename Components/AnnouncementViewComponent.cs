/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       GoldSim Website
\=============================================================================================================================*/
using GoldSim.Web.Models.Associations;
using GoldSim.Web.Models.Components;

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
    public IViewComponentResult Invoke() {

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish variables
      \-----------------------------------------------------------------------------------------------------------------------*/
      var homepage              = TopicRepository.Load("Web:Home");
      var announcementLabel     = homepage.Attributes.GetValue("AnnouncementLabel");
      var announcementUrl       = homepage.Attributes.GetUri("AnnouncementUrl");

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var viewModel             = new AnnouncementViewModel() {
        Label                   = announcementLabel,
        Url                     = announcementUrl
      };

      /*------------------------------------------------------------------------------------------------------------------------
      | Conditionally return view
      \-----------------------------------------------------------------------------------------------------------------------*/
      return String.IsNullOrWhiteSpace(announcementLabel) ? Content("") : (IViewComponentResult)View(viewModel);

    }

  } //Class
} //Namespace