/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoldSim.Web.Models {

  /*============================================================================================================================
  | CLASS: RENDER ACTION VIEW MODEL
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Storage class for properties/parameters passed through the WebFormMvcBridge.RenderAction() method.
  /// </summary>
  /// <remarks>
  ///   Adapted from solution described at https://stackoverflow.com/questions/702746/how-to-include-a-partial-view-inside-a-webform#answer-24867151
  /// </remarks>
  public class RenderActionViewModel {

    /*==========================================================================================================================
    | RENDERACTION PARAMETERS
    \-------------------------------------------------------------------------------------------------------------------------*/
    public string ControllerName { get; set; }
    public string ActionName { get; set; }
    public object RouteValues { get; set; }

  } // Class

} // Namespace