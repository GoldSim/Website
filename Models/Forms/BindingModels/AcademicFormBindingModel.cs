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
  | BINDING MODEL: ACADEMIC FORM
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed binding model representing the academic form.
  /// </summary>
  /// <remarks>
  ///   There are actually two specific academic forms—the <see cref="StudentAcademicFormBindingModel"/> and the <see
  ///   cref="InstructorAcademicFormBindingModel"/>—which this operates as a base class for.
  /// </remarks>
  public class AcademicFormBindingModel : ExtendedProfile {

    public AcademicFormBindingModel() {
      Address=new Address();
    }

    public Address Address { get; }

    [Required]
    [StringLength(255)]
    public string Department { get; set; }

  }

}