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

namespace GoldSim.Web.Models.Forms.BindingModels {

  /*============================================================================================================================
  | BINDING MODEL: ACADEMIC FORM (INSTRUCTOR)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed binding model representing the instructor version of the academic form.
  /// </summary>
  public class InstructorAcademicFormBindingModel : AcademicFormBindingModel {

    /*==========================================================================================================================
    | PROPERTY: WEB PAGE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the user's faculty web page address.
    /// </summary>
    [Url]
    [StringLength(255)]
    [Display(Name ="Faculty Web Page")]
    public string Webpage { get; set; }

  }

}