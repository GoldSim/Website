/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using System.ComponentModel.DataAnnotations;
using GoldSim.Web.Forms.Models.Partials;
using OnTopic.Mapping.Annotations;

namespace GoldSim.Web.Forms.Models {

  /*============================================================================================================================
  | BINDING MODEL: ACADEMIC FORM (STUDENT)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed binding model representing the student version of the academic form.
  /// </summary>
  public class StudentAcademicFormBindingModel : AcademicFormBindingModel {

    /*==========================================================================================================================
    | PROPERTY: SPONSOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the academic sponsor of the student (usually their instructor or academic advisor).
    /// </summary>
    [MapToParent]
    [Display(Name="Student Sponsor")]
    public AcademicSponsor Sponsor { get; } = new();

  } //Class
} //Namespace