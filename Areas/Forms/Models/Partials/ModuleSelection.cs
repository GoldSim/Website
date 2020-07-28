/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using System.ComponentModel.DataAnnotations;

namespace GoldSim.Web.Forms.Models.Partials {

  /*============================================================================================================================
  | MODEL: MODULE SELECTION
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for representing a selection of GoldSim modules that the user might be
  ///   interested in trying.
  /// </summary>
  public class ModuleSelection {

    /*==========================================================================================================================
    | PROPERTY: RELIABILITY MODULE?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the user is interested in the Reliability Module.
    /// </summary>
    [Display(Name="Reliability Module")]
    public bool Reliability { get; set; }

    /*==========================================================================================================================
    | PROPERTY: RADIONUCLIDE TRANSPORT (RT) MODULE?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the user is interested in the Radionuclide Transport (RT) Module.
    /// </summary>
    [Display(Name="Radionuclide Transport (RT) Module")]
    public bool RadionuclideTransport { get; set; }

    /*==========================================================================================================================
    | PROPERTY: CONTAMINANT TRANSPORT (CT)MODULE?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the user is interested in the Contaminant Transport (CT) Module.
    /// </summary>
    [Display(Name="Contaminant Transport (CT) Module")]
    public bool ContaminantTransport { get; set; }

    /*==========================================================================================================================
    | PROPERTY: DISTRIBUTED PROCESSING (DP-PLUS) MODULE?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the user is interested in the Reliability Module.
    /// </summary>
    [Display(Name="Distributed Processing (DP-Plus) Module")]
    public bool DistributedProcessing { get; set; }

    /*==========================================================================================================================
    | PROPERTY: QUICK START PACKAGE?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the user is interested in the quick start package.
    /// </summary>
    [Display(Name="Quick Start Package")]
    public bool QuickStartPackage { get; set; }

  } //Class
} //Namespace