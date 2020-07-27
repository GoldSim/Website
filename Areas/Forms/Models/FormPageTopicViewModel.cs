/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using GoldSim.Web.Models.ViewModels;
using OnTopic.Mapping.Annotations;
using OnTopic.ViewModels;

namespace GoldSim.Web.Forms.Models {

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
    | CUSTOMER EMAIL
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Optional link to a page that should be sent to the customer as a receipt.
    /// </summary>
    [AttributeKey("CustomerEmailTopicId")]
    public EmailTopicViewModel CustomerEmail { get; set; }

    /*==========================================================================================================================
    | SAVE AS TOPIC?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Optionally allows the form's <see cref="FormPageTopicViewModel{T}.BindingModel" /> to be saved as a new <see
    ///   cref="OnTopic.Topic"/> in the configured <see cref="OnTopic.Repositories.ITopicrepository"/>.
    /// </summary>
    public bool SaveAsTopic { get; set; } = false;

  } // Class

} // Namespace
