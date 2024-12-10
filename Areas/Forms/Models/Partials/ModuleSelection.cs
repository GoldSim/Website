/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/

namespace GoldSim.Web.Areas.Forms.Models.Partials {

  /*============================================================================================================================
  | MODEL: MODULE SELECTION
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for representing a selection of GoldSim modules that the user might be
  ///   interested in trying.
  /// </summary>
  internal sealed class ModuleSelection {

    /*==========================================================================================================================
    | PROPERTY: RELIABILITY MODULE?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the user is interested in the Reliability Module.
    /// </summary>
    [Display(Name="Reliability Module")]
    internal bool Reliability { get; set; }

    /*==========================================================================================================================
    | PROPERTY: RADIONUCLIDE TRANSPORT (RT) MODULE?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the user is interested in the Radionuclide Transport (RT) Module.
    /// </summary>
    [Display(Name="Radionuclide Transport (RT) Module")]
    internal bool RadionuclideTransport { get; set; }

    /*==========================================================================================================================
    | PROPERTY: CONTAMINANT TRANSPORT (CT)MODULE?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the user is interested in the Contaminant Transport (CT) Module.
    /// </summary>
    [Display(Name="Contaminant Transport (CT) Module")]
    internal bool ContaminantTransport { get; set; }

    /*==========================================================================================================================
    | PROPERTY: DISTRIBUTED PROCESSING (DP-PLUS) MODULE?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the user is interested in the Reliability Module.
    /// </summary>
    [Display(Name="Distributed Processing (DP-Plus) Module")]
    internal bool DistributedProcessing { get; set; }

  } //Class
} //Namespace