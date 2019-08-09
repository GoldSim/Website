/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoldSim.Web.Models.Forms {

  /*============================================================================================================================
  | MODEL: MODULE SELECTION
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for representing a selection of GoldSim modules that the user might be
  ///   interested in trying.
  /// </summary>
  public class ModuleSelection {

    [Display(Name ="Reliability Module")]
    public bool Reliability { get; set; }

    [Display(Name ="Radionuclide Transport(RT) Module")]
    public bool RadionuclideTransport { get; set; }

    [Display(Name ="Contaminant Transport(CT) Module")]
    public bool ContaminantTransport { get; set; }

    [Display(Name ="Distributed Processing(DP - Plus) Module")]
    public bool DistributedProcessing { get; set; }

    [Display(Name ="Quick Start Package")]
    public bool QuickStartPackage { get; set; }

  }

}