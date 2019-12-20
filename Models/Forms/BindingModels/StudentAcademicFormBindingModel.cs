/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using System.ComponentModel.DataAnnotations;
using Ignia.Topics.Mapping.Annotations;

namespace GoldSim.Web.Models.Forms.BindingModels {

  /*============================================================================================================================
  | BINDING MODEL: ACADEMIC FORM (STUDENT)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed binding model representing the student version of the academic form.
  /// </summary>
  public class StudentAcademicFormBindingModel : AcademicFormBindingModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="StudentAcademicFormBindingModel"/> object.
    /// </summary>
    public StudentAcademicFormBindingModel() {
      Sponsor = new AcademicSponsor();
    }

    /*==========================================================================================================================
    | PROPERTY: SPONSOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the academic sponsor of the student (usually their instructor or academic advisor).
    /// </summary>
    [MapToParent]
    [Display(Name="Student Sponsor")]
    public AcademicSponsor Sponsor { get; }

  }

}