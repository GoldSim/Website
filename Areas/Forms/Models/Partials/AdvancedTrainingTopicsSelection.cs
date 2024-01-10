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
  public class AdvancedTrainingTopicsSelection {

    [Display(Name="Scripts and DLLs")]
    public bool Scripts { get; set; }

    [Display(Name="Discrete Event Modeling")]
    public bool DiscreteEventModeling { get; set; }

    [Display(Name="Modeling Scenarios")]
    public bool ModelingScenarios { get; set; }

    [Display(Name="Advanced Timestepping Techniques")]
    public bool TimesteppingTechniques { get; set; }

    [Display(Name="Calibrating a Model")]
    public bool ModelCalibration { get; set; }

    [Display(Name="Building Effective Dashboards")]
    public bool DashboardAuthoring { get; set; }

    [Display(Name="Understanding and Controlling the Causality Sequence")]
    public bool CausalitySequence { get; set; }

    [Display(Name="Introduction to Reliability Modeling")]
    public bool ReliabilityModeling { get; set; }

    [Display(Name="Linking GoldSim to PHREEQC for Geochemical Calculations")]
    public bool GeochemicalCalculations { get; set; }

    [Display(Name="Modeling Pumps and Energy Use in a Water Management Model")]
    public bool ModelingPumps { get; set; }

    [Display(Name="Representing Reservoir and Dam Operations")]
    public bool ModelingReservoirOperations { get; set; }

    [Display(Name="Modeling Runoff")]
    public bool ModelingRunoff { get; set; }

    [Display(Name="Stochastic Weather Generation")]
    public bool StochasticWeatherGeneration { get; set; }

    [Display(Name="River Routing")]
    public bool RiverRouting { get; set; }

    [Display(Name="Representing Flow Networks")]
    public bool ModelingFlowNetworks { get; set; }

    [Display(Name="Modeling Population Growth")]
    public bool ModelingPopulationGrowth { get; set; }

    [Display(Name = "Other (Please Specify)")]
    public bool Other { get; set; }

    [StringLength(255)]
    [Display(Name="Other Topic(s) of Interest")]
    public string OtherDetails { get; set; }

  } //Class
} //Namespace