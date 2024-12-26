/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using GoldSim.Web.Areas.Forms.Models.Partials;

namespace GoldSim.Web.Areas.Forms.Models {

  /*============================================================================================================================
  | BINDING MODEL: ACADEMIC FORM (STUDENT)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed binding model representing the student version of the academic form.
  /// </summary>
  internal sealed record StudentAcademicFormBindingModel : AcademicFormBindingModel {

    /*==========================================================================================================================
    | PROPERTY: SPONSOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the academic sponsor of the student (usually their instructor or academic advisor).
    /// </summary>
    [MapToParent]
    [Display(Name="Student Sponsor")]
    internal AcademicSponsor Sponsor { get; } = new();

  } //Class
} //Namespace