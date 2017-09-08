/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using GoldSim.Web.Controllers;
using GoldSim.Web.Models;

namespace GoldSim.Web.Helpers {

  /*============================================================================================================================
  | CLASS: WEB FORM MVC UTILITY
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Utility methods for enabling rendering of MVC partials in Web Forms (ASPX) pages.
  /// </summary>
  /// <remarks>
  ///   Adapted from solution described at https://stackoverflow.com/questions/702746/how-to-include-a-partial-view-inside-a-webform#answer-24867151
  /// </remarks>
  public class WebFormMVCUtility {

    /*==========================================================================================================================
    | RENDER PARTIAL
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Wires up MVC routing, controller, and view contexts needed for rendering a MVC partial.
    /// </summary>
    private static void RenderPartial(string partialViewName, object model) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Initialize HTTP context
      \-----------------------------------------------------------------------------------------------------------------------*/
      HttpContextBase httpContextBase   = new HttpContextWrapper(HttpContext.Current);

      /*------------------------------------------------------------------------------------------------------------------------
      | Simulate routing for WebFormController
      \-----------------------------------------------------------------------------------------------------------------------*/
      RouteData routeData               = new RouteData();
      routeData.Values.Add("controller", "WebFormController");

      /*------------------------------------------------------------------------------------------------------------------------
      | Initialize Controller context using WebFormController
      \-----------------------------------------------------------------------------------------------------------------------*/
      ControllerContext controllerContext = new ControllerContext(new RequestContext(httpContextBase, routeData), new WebFormController());

      /*------------------------------------------------------------------------------------------------------------------------
      | Find the provided view and render it with simulated View context
      \-----------------------------------------------------------------------------------------------------------------------*/
      IView view                        = FindPartialView(controllerContext, partialViewName);
      ViewContext viewContext           = new ViewContext(controllerContext, view, new ViewDataDictionary { Model = model }, new TempDataDictionary(), httpContextBase.Response.Output);
      view.Render(viewContext, httpContextBase.Response.Output);

    }

    /*==========================================================================================================================
    | FIND PARTIAL VIEW
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Hooks into the MVC View engine to find and return the provided partial view.
    /// </summary>
    /// <returns>The partial view.</returns>
    private static IView FindPartialView(ControllerContext controllerContext, string partialViewName) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Search for partial view
      \-----------------------------------------------------------------------------------------------------------------------*/
      ViewEngineResult result           = ViewEngines.Engines.FindPartialView(controllerContext, partialViewName);
      if (result.View != null) {
        return result.View;
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Build view locations searched message for exception in the case the partial view is not found
      \-----------------------------------------------------------------------------------------------------------------------*/
      StringBuilder locationsText       = new StringBuilder();
      foreach (string location in result.SearchedLocations) {
        locationsText.AppendLine();
        locationsText.Append(location);
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Throw exception if the partial view is not found
      \-----------------------------------------------------------------------------------------------------------------------*/
      throw new InvalidOperationException(String.Format("Partial view {0} not found. Locations Searched: {1}", partialViewName, locationsText));

    }

    /*==========================================================================================================================
    | RENDER ACTION
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Pass-through method for calling RenderPartial() from an ASPX page.
    /// </summary>
    public static void RenderAction(string controllerName, string actionName, object routeValues) {
      RenderPartial("PartialRender", new RenderActionViewModel() { ControllerName = controllerName, ActionName = actionName, RouteValues = routeValues });
    }

  } // Class

} // Namespace
