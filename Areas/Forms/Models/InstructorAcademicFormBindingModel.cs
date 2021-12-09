/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/

namespace GoldSim.Web.Forms.Models {

  /*============================================================================================================================
  | BINDING MODEL: ACADEMIC FORM (INSTRUCTOR)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed binding model representing the instructor version of the academic form.
  /// </summary>
  public record InstructorAcademicFormBindingModel : AcademicFormBindingModel {

    /*==========================================================================================================================
    | PROPERTY: WEB PAGE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the user's faculty web page address.
    /// </summary>
    [Url]
    [StringLength(255)]
    [Display(Name="Faculty Web Page")]
    public string Webpage { get; init; }

  } //Class
} //Namespace