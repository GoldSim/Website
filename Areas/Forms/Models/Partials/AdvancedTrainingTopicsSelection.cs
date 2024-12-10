/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/

namespace GoldSim.Web.Forms.Models.Partials {

  /*============================================================================================================================
  | MODEL: ADVANCED TRAINING TOPIC SELECTION
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for representing a selection of advanced topics that an attendee might
  ///   wish to learn about at the GoldSim User Conference.
  /// </summary>
  [Obsolete("This has been retired in preference for a single form field.", true)]
  internal class AdvancedTrainingTopicsSelection {

    [Display(Name="Scripts and DLLs")]
    internal bool Scripts { get; set; }

    [Display(Name="Discrete Event Modeling")]
    internal bool DiscreteEventModeling { get; set; }

    [Display(Name="Modeling Scenarios")]
    internal bool ModelingScenarios { get; set; }

    [Display(Name="Advanced Timestepping Techniques")]
    internal bool TimesteppingTechniques { get; set; }

    [Display(Name="Calibrating a Model")]
    internal bool ModelCalibration { get; set; }

    [Display(Name="Building Effective Dashboards")]
    internal bool DashboardAuthoring { get; set; }

    [Display(Name="Understanding and Controlling the Causality Sequence")]
    internal bool CausalitySequence { get; set; }

    [Display(Name="Introduction to Reliability Modeling")]
    internal bool ReliabilityModeling { get; set; }

    [Display(Name="Linking GoldSim to PHREEQC for Geochemical Calculations")]
    internal bool GeochemicalCalculations { get; set; }

    [Display(Name="Modeling Pumps and Energy Use in a Water Management Model")]
    internal bool ModelingPumps { get; set; }

    [Display(Name="Representing Reservoir and Dam Operations")]
    internal bool ModelingReservoirOperations { get; set; }

    [Display(Name="Modeling Runoff")]
    internal bool ModelingRunoff { get; set; }

    [Display(Name="Stochastic Weather Generation")]
    internal bool StochasticWeatherGeneration { get; set; }

    [Display(Name="River Routing")]
    internal bool RiverRouting { get; set; }

    [Display(Name="Representing Flow Networks")]
    internal bool ModelingFlowNetworks { get; set; }

    [Display(Name="Modeling Population Growth")]
    internal bool ModelingPopulationGrowth { get; set; }

    [Display(Name = "Other (Please Specify)")]
    internal bool Other { get; set; }

    [StringLength(255)]
    [Display(Name="Other Topic(s) of Interest")]
    internal string OtherDetails { get; set; }

  } //Class
} //Namespace