/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using GoldSim.Web.Areas.Forms.Models.Partials;

namespace GoldSim.Web.Areas.Forms.Models {

  /*============================================================================================================================
  | BINDING MODEL: NEWSLETTER FORM
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed binding model representing the instructor version of the newsletter signup form.
  /// </summary>
  internal sealed record NewsletterFormBindingModel : CoreContact {

    /*==========================================================================================================================
    | PROPERTY: COUNTRY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the country name.
    /// </summary>
    [Required]
    [StringLength(75)]
    [Metadata("Country")]
    internal string Country { get; init; } = "United States of America";

    /*==========================================================================================================================
    | PROPERTY: INCLUDE NEWSLETTER?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets whether or not the user wishes to also subscribe to the newsletter.
    /// </summary>
    [Display(Name = "GoldSim Newsletter")]
    internal bool IncludeNewsletter { get; init; } = true;

    /*==========================================================================================================================
    | PROPERTY: INCLUDE WEBINAR?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets whether or not the user wishes to also subscribe to the webinar mailing list.
    /// </summary>
    [Display(Name = "Webinar Email List")]
    internal bool IncludeWebinar { get; init; }

  } //Class
} //Namespace