﻿/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using GoldSim.Web.Forms.Models.Partials;

namespace GoldSim.Web.Forms.Models {

  /*============================================================================================================================
  | BINDING MODEL: USER CONFERENCE REGISTRATION FORM
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed binding model representing the User Conference Registration form.
  /// </summary>
  public record UserConferenceFormBindingModel : ExtendedContact {

    /*==========================================================================================================================
    | PROPERTY: FAX NUMBER
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the fax number for the user (or their organization) so that the quote may be faxed to them.
    /// </summary>
    [Phone]
    [StringLength(50)]
    [Display(Name = "Fax")]
    public string FaxNumber { get; init; }

    /*==========================================================================================================================
    | PROPERTY: WILL SUBMIT POSTER?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets whether the user intends to submit a poster for the conference's poster session.
    /// </summary>
    [Display(Name="I am interested in submitting a poster.")]
    public bool WillSubmitPoster { get; init; }

    /*==========================================================================================================================
    | PROPERTY: PURCHASE ORDER NUMBER
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the purchase order number that GoldSim should reference in the invoice for the registration.
    /// </summary>
    [StringLength(15)]
    [Display(Name="Purchase Order Number")]
    public string PurchaseOrderNumber { get; init; }

    /*==========================================================================================================================
    | PROPERTY: ADDITIONAL INSTRUCTIONS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets any special instructions (such as dinner guests or dietary restrictions) needed to accomodate the
    ///   attendee.
    /// </summary>
    [StringLength(1000)]
    [Display(Name="Additional Instructions(e.g., interest in spouse / partner attending dinners)")]
    public string AdditionalInstructions { get; init; }

    /*==========================================================================================================================
    | PROPERTY: ADVANCED TRAINING TOPICS SELECTION
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets what additional training topics the attendee is interested in, if any, as part of their training session.
    /// </summary>
    [MapToParent]
    [Display(Name="Advanced Training Topics")]
    public AdvancedTrainingTopicsSelection TrainingTopics { get; } = new();

    /*==========================================================================================================================
    | PROPERTY: APPLY STUDENT DISCOUNT?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets whether the attendee is a student and, thus, should receive the student discount.
    /// </summary>
    [Display(Name="Apply student discount")]
    public bool WithStudentDiscount { get; init; }

    /*==========================================================================================================================
    | PROPERTY: INCLUDE TRAINING?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets whether the attendee will be attending (and purchasing) the additional training portion of the
    ///   conference.
    /// </summary>
    [Display(Name="Basic Training and Conference(September 10 - 12): $1, 500")]
    public bool IncludeTraining { get; init; }

    /*==========================================================================================================================
    | PROPERTY: WITH PAPER RECEIPT?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets whether the attendee requires a paper receipt be delivered for accounting purposes.
    /// </summary>
    [Display(Name="I would prefer a paper invoice or receipt.")]
    public bool WithPaperReceipt { get; init; }

  } //Class
} //Namespace