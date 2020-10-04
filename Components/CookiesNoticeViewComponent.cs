/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       GoldSim Website
\=============================================================================================================================*/
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GoldSim.Web.Courses.Components {

  /*============================================================================================================================
  | CLASS: COOKIES NOTICE VIEW COMPONENT
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Defines a <see cref="ViewComponent"/> which provides access to a cookie consent form, assuming the user hasn't already
  ///   consented.
  /// </summary>
  public class CookiesNoticeViewComponent: ViewComponent {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="CourseListViewComponent"/> with necessary dependencies.
    /// </summary>
    public CookiesNoticeViewComponent() { }

    /*==========================================================================================================================
    | METHOD: INVOKE (ASYNC)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the cookie consent notification for the current page, assuming the user hasn't already consented.
    /// </summary>
    public async Task<IViewComponentResult> InvokeAsync() {

      /*------------------------------------------------------------------------------------------------------------------------
      | Get cookie
      \-----------------------------------------------------------------------------------------------------------------------*/
      HttpContext.Request.Cookies.TryGetValue("CookiesConsent", out var consentCookie);

      /*------------------------------------------------------------------------------------------------------------------------
      | Conditionally return view
      \-----------------------------------------------------------------------------------------------------------------------*/
      return String.IsNullOrWhiteSpace(consentCookie)? (IViewComponentResult) View() : Content("");

    }

  } //Class
} //Namespace