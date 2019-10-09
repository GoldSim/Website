/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System.Collections.Generic;
using Ignia.Topics.ViewModels;

namespace GoldSim.Web.Models {

  /*============================================================================================================================
  | CLASS: FORM PAGE VIEW MODEL {T}
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   A view model for rendering a form page with a strongly-typed binding model.
  /// </summary>
  public class FormPageTopicViewModel<T>: PageTopicViewModel where T : class, new() {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="FormPageTopicViewModel"/> with appropriate dependencies.
    /// </summary>
    /// <returns>A <see cref="FormPageTopicViewModel"/>.</returns>
    public FormPageTopicViewModel(T bindingModel = null) {
      BindingModel = bindingModel?? new T();
    }

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
    public string EmailReceipt { get; set; }

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
    | BINDING MODEL
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a reference to the binding model that should be used for the form itself.
    /// </summary>
    /// <returns>The <typeparamref name="T"/> binding model.</returns>
    public T BindingModel { get; }

  } // Class

} // Namespace
