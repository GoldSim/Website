/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/

namespace GoldSim.Web.Areas.Forms.Models {

  /*============================================================================================================================
  | METADATA: FORM FIELDS
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a single definition for the annotations and documentation for form fields that are declared on more than one
  ///   unrelated binding model.
  /// </summary>
  /// <remarks>
  ///   <para>
  ///     This class is never instantiated. Instead, binding models opt in by adding the <see cref="ModelMetadataTypeAttribute"
  ///     /> with this type, at which point ASP.NET Core merges the annotations below with those on any property of the same
  ///     name. The model's properties should retain their basic declaration, and inherit their documentation from this class.
  ///     Fields declared exclusively on a shared base class are already defined once, and should remain there.
  ///   </para>
  ///   <para>
  ///     ASP.NET Core does not apply a base class's <see cref="ModelMetadataTypeAttribute"/> to derived classes during
  ///     validation. As such, every class that declares one of these fields must apply the attribute itself.
  ///   </para>
  ///   <para>
  ///     Currently, OnTopic only reads mapping attributes, such as <see cref="MapToParentAttribute"/> and <see cref=
  ///     "DisableMappingAttribute"/>, from the model's own properties. Those should remain on the model properties, and never
  ///     be moved here.
  ///   </para>
  /// </remarks>
  public sealed class FormFields {

    /*==========================================================================================================================
    | PROPERTY: JOB TITLE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the user's job title.
    /// </summary>
    [Required]
    [StringLength(255)]
    [Display(Name="Job Title")]
    public string JobTitle { get; init; }

    /*==========================================================================================================================
    | PROPERTY: PROVINCE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the province (or state in America).
    /// </summary>
    [Required]
    [StringLength(255)]
    [Display(Name="State/Province")]
    [Metadata("State")]
    public string Province { get; set; }

    /*==========================================================================================================================
    | PROPERTY: OTHER TOOLS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets what other risk analysis tools the user is currently using or is evaluating.
    /// </summary>
    [Required]
    [StringLength(1000)]
    [Display(Name="What other risk analysis tools do you use, or are evaluating?")]
    public string OtherTools { get; set; }

  } //Class
} //Namespace