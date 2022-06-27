/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using OnTopic.Models;

namespace GoldSim.Web.Forms.Models.Partials {

  /*============================================================================================================================
  | MODEL: CORE CONTACT
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for representing the core contact information required of every request.
  /// </summary>
  /// <remarks>
  ///   Every form has, at its base, core contact information representing the <see cref="FirstName"/>, <see cref="LastName"/>,
  ///   <see cref="Organization"/>, and <see cref="Email"/>. Thus the <see cref="CoreContact"/> represents the base class for
  ///   nearly every form binding model used by GoldSim.
  /// </remarks>
  public record CoreContact: ITopicBindingModel {

    /*==========================================================================================================================
    | PROPERTY: KEY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the key for the topic, in the case this is saved to a topic.
    /// </summary>
    [DisableMapping]
    [StringLength(255)]
    public virtual string Key { get; init; }

    /*==========================================================================================================================
    | PROPERTY: CONTENT TYPE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the content type for the topic in the case this is saved to a topic.
    /// </summary>
    [DisableMapping]
    [StringLength(255)]
    public virtual string ContentType { get; init; }

    /*==========================================================================================================================
    | PROPERTY: FIRST NAME
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the user's first name.
    /// </summary>
    [Required]
    [StringLength(255)]
    [Display(Name = "First Name")]
    public virtual string FirstName { get; init; }

    /*==========================================================================================================================
    | PROPERTY: LAST NAME
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the user's last name.
    /// </summary>
    [Required]
    [StringLength(255)]
    [Display(Name = "Last Name")]
    public virtual string LastName { get; init; }

    /*==========================================================================================================================
    | PROPERTY: ORGANIZATION
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the user's organization or institution name.
    /// </summary>
    [Required]
    [StringLength(255)]
    [Display(Name = "Organization Name")]
    public virtual string Organization { get; init; }

    /*==========================================================================================================================
    | PROPERTY: EMAIL ADDRESS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the user's email address.
    /// </summary>
    [Required]
    [EmailAddress]
    [Display(Name = "Email Address")]
    [Remote(action: "VerifyEmail", controller: "Forms")]
    public virtual string Email { get; init; }

    /*==========================================================================================================================
    | PROPERTY: RECAPTCHA TOKEN
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   The response token provided by the reCAPTCHA client
    /// </summary>
    [DisableMapping]
    public string RecaptchaToken { get; set; }

  } //Class
} //Namespace