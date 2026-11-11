/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using GoldSim.Web.Areas.Forms.Models.Partials;

namespace GoldSim.Web.Areas.Forms.Models {

  /*============================================================================================================================
  | BINDING MODEL: REQUEST A DEMO FORM
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed binding model representing the Request a Demo(nstration) form.
  /// </summary>
  [ModelMetadataType(typeof(FormFields))]
  public sealed record DemoFormBindingModel : Profile {

    /*==========================================================================================================================
    | PROPERTY: JOB TITLE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <inheritdoc cref="FormFields.JobTitle"/>
    public string JobTitle { get; init; }

    /*==========================================================================================================================
    | PROPERTY: PROVINCE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <inheritdoc cref="FormFields.Province"/>
    /// <remarks>
    ///   This cannot be defined on <see cref="Contact"/> because that causes a conflict with <see cref="Address.Province"/>.
    /// </remarks>
    public string Province { get; set; }

    /*==========================================================================================================================
    | PROPERTY: OTHER TOOLS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <inheritdoc cref="FormFields.OtherTools"/>
    public string OtherTools { get; init; }

  } //Class
} //Namespace