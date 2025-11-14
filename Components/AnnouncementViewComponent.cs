/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       GoldSim Website
\=============================================================================================================================*/
using GoldSim.Web.Models.Components;

namespace GoldSim.Web.Components {

  /*============================================================================================================================
  | CLASS: ANNOUNCEMENT VIEW COMPONENT
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Defines a <see cref="ViewComponent"/> which renders an announcement banner in a view.
  /// </summary>
  public sealed class AnnouncementViewComponent : ViewComponent {

    /*==========================================================================================================================
    | DEPENDENCIES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private readonly            ITopicRepository                _topicRepository;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="AnnouncementViewComponent"/> with necessary dependencies.
    /// </summary>
    internal AnnouncementViewComponent(ITopicRepository topicRepository) {
      _topicRepository          = topicRepository;
    }

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
      var homepage              = _topicRepository.Load("Web:Home");
      var announcementLabel     = homepage?.Attributes.GetValue("AnnouncementLabel");
      var announcementUrl       = homepage?.Attributes.GetUri("AnnouncementUrl");

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var viewModel             = new AnnouncementViewModel {
        Label                   = announcementLabel,
        Url                     = announcementUrl
      };

      /*------------------------------------------------------------------------------------------------------------------------
      | Conditionally return view
      \-----------------------------------------------------------------------------------------------------------------------*/
      return String.IsNullOrWhiteSpace(announcementLabel) ? Content("") : View(viewModel);

    }

  } //Class
} //Namespace