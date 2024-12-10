/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using GoldSim.Web.Forms.Models.Partials;

namespace GoldSim.Web.Forms.Models {

  /*============================================================================================================================
  | BINDING MODEL: ACADEMIC FORM
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed binding model representing the academic form.
  /// </summary>
  /// <remarks>
  ///   There are actually two specific academic forms—the <see cref="StudentAcademicFormBindingModel"/> and the <see
  ///   cref="InstructorAcademicFormBindingModel"/>—which this operates as a base class for.
  /// </remarks>
  internal record AcademicFormBindingModel : ExtendedProfile {

    /*==========================================================================================================================
    | PROPERTY: ORGANIZATION
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the user's organization or institution name.
    /// </summary>
    [Display(Name="Name of Institution")]
    internal override string Organization { get; init; }

    /*==========================================================================================================================
    | PROPERTY: ADDRESS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the user's address.
    /// </summary>
    [MapToParent(AttributePrefix="")]
    internal Address Address { get; } = new();

    /*==========================================================================================================================
    | PROPERTY: DEPARTMENT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets what department the user is associated with.
    /// </summary>
    [Required]
    [StringLength(255)]
    internal string Department { get; init; }

  } //Class
} //Namespace