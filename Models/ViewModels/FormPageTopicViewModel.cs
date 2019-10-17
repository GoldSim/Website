/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System.Collections.Generic;
using Ignia.Topics.ViewModels;

namespace GoldSim.Web.Models.ViewModels {

  /*============================================================================================================================
  | CLASS: FORM PAGE VIEW MODEL
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   A view model for rendering a form page.
  /// </summary>
  public class FormPageTopicViewModel: PageTopicViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="FormPageTopicViewModel"/> with appropriate dependencies.
    /// </summary>
    /// <returns>A <see cref="FormPageTopicViewModel"/>.</returns>
    public FormPageTopicViewModel() {}

    /*==========================================================================================================================
    | FOLLOW-UP PAGE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   References the <see cref="Topic.Id"/> of the page that should be redirected to upon completion.
    /// </summary>
    public int FollowUpPage { get; set; }

    /*==========================================================================================================================
    | SUBMIT BUTTON LABEL
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Defines the label to use for the submit button.
    /// </summary>
    public string SubmitButtonLabel { get; set; } = "Submit";

    /*==========================================================================================================================
    | DISABLE EMAIL RECEIPT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Optionally overrides the email receipt that is sent by default.
    /// </summary>
    public bool DisableEmailReceipt { get; set; } = false;

    /*==========================================================================================================================
    | EMAIL SUBJECT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Optionally overrides the subject of the email receipt.
    /// </summary>
    public string EmailSubject { get; set; }

    /*==========================================================================================================================
    | EMAIL RECIPIENT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Optionally overrides the recipient of the email receipt.
    /// </summary>
    public string EmailRecipient { get; set; }

    /*==========================================================================================================================
    | EMAIL SENDER
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Optionally overrides the sender of the email receipt.
    /// </summary>
    public string EmailSender { get; set; }

    /*==========================================================================================================================
    | SAVE AS TOPIC?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Optionally allows the form's <see cref="FormPageTopicViewModel{T}.BindingModel" /> to be saved as a new <see
    ///   cref="Ignia.Topics.Topic"/> in the configured <see cref="Ignia.Topics.Repositories.ITopicrepository"/>.
    /// </summary>
    public bool SaveAsTopic { get; set; } = false;

  } // Class

} // Namespace
