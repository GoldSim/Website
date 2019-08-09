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

namespace GoldSim.Web.Models.Forms {

  /*============================================================================================================================
  | MODEL: EXTENDED PROFILE
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for representing an extended profile. In addition to properties from the
  ///   <see cref="Profile"/> class, this also includes <see cref="Modules"/> the user may be interested in, as well as the
  ///   required <see cref="AcceptTermsOfUse"/> boolean.
  /// </summary>
  public class ExtendedProfile : Profile {

    public ExtendedProfile() {
      Modules=new ModuleSelection();
    }

    [Required]
    [Display(Name ="I am also interested in:")]
    public ModuleSelection Modules { get; }

    [Range(typeof(bool), "true", "true", "The terms of service must be accepted.")]
    [Display(Name ="I agree to these terms of use.I also agree to receive the GoldSim newsletter.")]
    public bool AcceptTermsOfUse { get; set; }

  }

}