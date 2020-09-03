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
  | BINDING MODEL: NEWSLETTER FORM
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed binding model representing the instructor version of the newsletter signup form.
  /// </summary>
  public class NewsletterFormBindingModel : CoreContact {

    /*==========================================================================================================================
    | PROPERTY: COUNTRY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the country name.
    /// </summary>
    [Required]
    [StringLength(75)]
    [Metadata("Country")]
    public virtual string Country { get; set; } = "United States of America";

  } //Class
} //Namespace