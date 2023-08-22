﻿/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       GoldSim Website
\=============================================================================================================================*/
using GoldSim.Web.Models.Components;

namespace GoldSim.Web.Courses.Components {

  /*============================================================================================================================
  | CLASS: RECAPTCHA VIEW COMPONENT
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Defines a <see cref="ViewComponent"/> which renders a script for embedding a reCAPTCHA component onto the page.
  /// </summary>
  public class RecaptchaViewComponent: ViewComponent {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="ReCaptchaViewComponent"/> with necessary dependencies.
    /// </summary>
    public RecaptchaViewComponent(string siteKey) {
      SiteKey                   = siteKey;
    }

    /*==========================================================================================================================
    | SITE KEY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the sitekey used by the reCAPTCHA service.
    /// </summary>
    public string SiteKey { get; init; }

    /*==========================================================================================================================
    | METHOD: INVOKE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Renders a database.
    /// </summary>
    public IViewComponentResult Invoke(string action) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var viewModel             = new RecaptchaViewModel() {
        SiteKey                 = SiteKey,
        Action                  = action
      };

      /*------------------------------------------------------------------------------------------------------------------------
      | Return view
      \-----------------------------------------------------------------------------------------------------------------------*/
      return View(viewModel);

    }

  } //Class
} //Namespace