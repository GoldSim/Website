/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using System;
using System.ComponentModel.DataAnnotations;
using OnTopic.Mapping.Annotations;

namespace GoldSim.Web.Forms.Models.Partials {

  /*============================================================================================================================
  | MODEL: EXTENDED PROFILE
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for representing an extended profile. In addition to properties from the
  ///   <see cref="Profile"/> class, this also includes <see cref="Modules"/> the user may be interested in, as well as the
  ///   required <see cref="AcceptTermsOfUse"/> boolean.
  /// </summary>
  public class ExtendedProfile : Profile {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of an <see cref="ExtendedProfile"/> object.
    /// </summary>
    public ExtendedProfile() {
      Modules = new ModuleSelection();
    }

    /*==========================================================================================================================
    | PROPERTY: MODULES
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the modules that the user has selected.
    /// </summary>
    [Required]
    [Display(Name="I am also interested in:")]
    [MapToParent]
    public ModuleSelection Modules { get; }

    /*==========================================================================================================================
    | PROPERTY: ACCEPT TERMS OF USE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets whether or not the user has accepted the terms of user.
    /// </summary>
    /// <remarks>
    ///   It is required that the user accept the terms of use, so this should always be set to true. By having it in the model,
    ///   however, we're able to provide form validation and error messaging.
    /// </remarks>
    [Range(typeof(bool), "true", "true", ErrorMessage="The terms of service must be accepted.")]
    [Display(Name="I agree to these terms of use. I also agree to receive the GoldSim newsletter.")]
    public bool AcceptTermsOfUse { get; set; }

  }

}