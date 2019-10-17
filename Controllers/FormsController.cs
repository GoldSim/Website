﻿/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using GoldSim.Web.Models.Forms.BindingModels;
using Ignia.Topics;
using Ignia.Topics.Mapping;
using Ignia.Topics.Repositories;
using GoldSim.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Ignia.Topics.AspNetCore.Mvc.Controllers;
using System.Threading.Tasks;
using System.Text;
using GoldSim.Web.Services;
using System.Net.Mail;
using System.Linq;
using System.Collections.Generic;
using System;
using GoldSim.Web.Models.Forms;

namespace GoldSim.Web.Controllers {

  /*============================================================================================================================
  | CLASS: FORMS CONTROLLER
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides common processing for all GoldSim forms pages. Each form will be represented by a unique action on this
  ///   controller.
  /// </summary>
  public class FormsController : TopicController {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private readonly            ITopicMappingService            _topicMappingService;
    private readonly            IReverseTopicMappingService     _reverseMappingService;
    private readonly            ISmtpService                    _smptService;
    private                     Dictionary<string, string>      _formValues;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a Topic Controller with necessary dependencies.
    /// </summary>
    /// <returns>A topic controller for loading OnTopic views.</returns>
    public FormsController(
      ITopicRepository topicRepository,
      ITopicRoutingService topicRoutingService,
      ITopicMappingService topicMappingService,
      IReverseTopicMappingService reverseTopicMappingService,
      ISmtpService smtpService
    ) : base(
      topicRepository,
      topicRoutingService,
      topicMappingService
    ) {
      _topicMappingService      = topicMappingService;
      _reverseMappingService    = reverseTopicMappingService;
      _smptService              = smtpService;
    }

    /*==========================================================================================================================
    | HELPER: CREATE VIEW MODEL
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Constructs a new view model based on the <typeparamref name="T"/> binding model type.
    /// </summary>
    private async Task<FormPageTopicViewModel<T>> CreateViewModel<T>(T bindingModel = null) where T: class, new() =>
      await _topicMappingService.MapAsync(
        CurrentTopic,
        new FormPageTopicViewModel<T>(bindingModel)
      ) as FormPageTopicViewModel<T>;

    /*==========================================================================================================================
    | HELPER: PROCESS FORM
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Helper function to process a form postback request.
    /// </summary>
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ProcessForm<T>(T bindingModel) where T: class, new() {

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var viewModel = await CreateViewModel<T>(bindingModel);
      if (!ModelState.IsValid) {
        return View(viewModel);
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Optionally send email receipt
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (!viewModel.DisableEmailReceipt) {
        SendReceipt(viewModel.EmailSubject, viewModel.EmailSender, viewModel.EmailRecipient);
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Optionally save as topic
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (viewModel.SaveAsTopic) {
        SaveToTopic(viewModel.BindingModel.GetType().Name.Replace("BindingModel", ""));
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Redirect to configured follow-up page
      \-----------------------------------------------------------------------------------------------------------------------*/
      return RedirectToAction("Redirect", "Redirect", new { topicId = viewModel.FollowUpPage });

    }

    /*==========================================================================================================================
    | FORM: TRIAL
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Request a trial of the product.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> TrialAsync() => View(await CreateViewModel<TrialFormBindingModel>());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> TrialAsync(TrialFormBindingModel bindingModel) =>
      await ProcessForm<TrialFormBindingModel>(bindingModel);

    /*==========================================================================================================================
    | FORM: DEMO
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Request a demonstration of the product.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> DemoAsync() => View(await CreateViewModel<DemoFormBindingModel>());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DemoAsync(DemoFormBindingModel bindingModel) =>
      await ProcessForm<DemoFormBindingModel>(bindingModel);

    /*==========================================================================================================================
    | FORM: QUOTE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Request a quote for the product
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> QuoteAsync() => View(await CreateViewModel<QuoteFormBindingModel>());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> QuoteAsync(QuoteFormBindingModel bindingModel) =>
      await ProcessForm<QuoteFormBindingModel>(bindingModel);

    /*==========================================================================================================================
    | FORM: PURCHASE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Request to purchase a license of the product
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> PurchaseAsync() => View(await CreateViewModel<PurchaseFormBindingModel>());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> PurchaseAsync(PurchaseFormBindingModel bindingModel) =>
      await ProcessForm<PurchaseFormBindingModel>(bindingModel);

    /*==========================================================================================================================
    | FORM: NEWSLETTER
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Signup for the GoldSim newsletter
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> NewsletterAsync() => View(await CreateViewModel<NewsletterFormBindingModel>());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> NewsletterAsync(NewsletterFormBindingModel bindingModel) =>
      await ProcessForm<NewsletterFormBindingModel>(bindingModel);

    /*==========================================================================================================================
    | FORM: ACADEMIC (INSTRUCTOR)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Request an academic license of the product for faculty.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> InstructorAcademicAsync()
      => View(await CreateViewModel<InstructorAcademicFormBindingModel>());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> InstructorAcademicAsync(InstructorAcademicFormBindingModel bindingModel) =>
      await ProcessForm<InstructorAcademicFormBindingModel>(bindingModel);

    /*==========================================================================================================================
    | FORM: ACADEMIC (STUDENT)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Request an academic license of the product for faculty.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> StudentAcademicAsync() => View(await CreateViewModel<StudentAcademicFormBindingModel>());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> StudentAcademicAsync(StudentAcademicFormBindingModel bindingModel) =>
      await ProcessForm<StudentAcademicFormBindingModel>(bindingModel);

    /*==========================================================================================================================
    | FORM: USER CONFERENCE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Request an academic license of the product for faculty.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> UserConferenceAsync() => View(await CreateViewModel<UserConferenceFormBindingModel>());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UserConferenceAsync(UserConferenceFormBindingModel bindingModel) =>
      await ProcessForm<UserConferenceFormBindingModel>(bindingModel);

    /*==========================================================================================================================
    | HELPER: SEND RECEIPT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Send an email to GoldSim containing all of the form values.
    /// </summary>
    private void SendReceipt(string subject = null, string sender = null, string recipient = null) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish variables
      \-----------------------------------------------------------------------------------------------------------------------*/
      subject                   = subject??     "GoldSim.com/Forms: " + CurrentTopic.Key;
      recipient                 = recipient??   "Jeremy@Ignia.com";
      sender                    = sender??      "Website@GoldSim.com";

      /*------------------------------------------------------------------------------------------------------------------------
      | Assemble email
      \-----------------------------------------------------------------------------------------------------------------------*/
      var mail                  = new MailMessage(new MailAddress(sender), new MailAddress(recipient));

      mail.Subject              = subject;
      mail.Body                 = GetEmailBody();
      mail.IsBodyHtml           = true;

      /*------------------------------------------------------------------------------------------------------------------------
      | Send email
      \-----------------------------------------------------------------------------------------------------------------------*/
      _smptService.SendAsync(mail);

    }

    /*==========================================================================================================================
    | HELPER: GET EMAIL BODY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Retrieves a list of form values from the <see cref="ControllerContext"/> and returns it as an HTML string.
    /// </summary>
    private string GetEmailBody() {

      /*------------------------------------------------------------------------------------------------------------------------
      | Define variables
      \-----------------------------------------------------------------------------------------------------------------------*/
      var output = new StringBuilder();

      /*------------------------------------------------------------------------------------------------------------------------
      | Loop over form values
      \-----------------------------------------------------------------------------------------------------------------------*/
      foreach (var field in GetFormValues()) {
        var fieldName = ToTitleCase(field.Key.Replace(".", ": "));
        output.Append($"<b>{fieldName}:</b> {field.Value}<br />");
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Return form values
      \-----------------------------------------------------------------------------------------------------------------------*/
      return output.ToString();

    }

    /*==========================================================================================================================
    | HELPER: SAVE TO TOPIC
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Adds the form values to a new <see cref="Topic"/>, and saves it to the <see cref="ITopicRepository"/>.
    /// </summary>
    private void SaveToTopic(string contentType) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish variables
      \-----------------------------------------------------------------------------------------------------------------------*/
      var       topicKey        = contentType + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
      var       topic           = TopicFactory.Create(topicKey, contentType);
      var       parentKey       = "CustomerRequests:LicenseTests";
      var       parentTopic     = TopicRepository.Load(parentKey);
      var       parentId        = -1;

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate Topic Parent
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (parentTopic == null) {
        throw new Exception($"The topic '{parentKey}' could not be found. A root topic to store forms to is required.");
      }
      parentId                  = parentTopic.Id;

      /*------------------------------------------------------------------------------------------------------------------------
      | Set Topic values
      \-----------------------------------------------------------------------------------------------------------------------*/
      topic.Parent              = parentTopic;
      topic.LastModified        = DateTime.Now;

      /*------------------------------------------------------------------------------------------------------------------------
      | Set Topic Attributes based on form input values
      \-----------------------------------------------------------------------------------------------------------------------*/
      foreach (var field in GetFormValues()) {
        topic.Attributes.SetValue(field.Key.Replace(".", ""), field.Value);
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Save form Topic
      \-----------------------------------------------------------------------------------------------------------------------*/
      TopicRepository.Save(topic);

    }

    /*==========================================================================================================================
    | HELPER: GET FORM VALUES
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Retrieves a list of form values from the <see cref="ControllerContext"/> and returns it as dictionary.
    /// </summary>
    private Dictionary<string, string> GetFormValues() {

      /*------------------------------------------------------------------------------------------------------------------------
      | Check cache
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (_formValues != null) {
        return _formValues;
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Define variables
      \-----------------------------------------------------------------------------------------------------------------------*/
      _formValues = new Dictionary<string, string>();

      /*------------------------------------------------------------------------------------------------------------------------
      | Loop over form values
      \-----------------------------------------------------------------------------------------------------------------------*/
      foreach (var field in HttpContext.Request.Form.Keys.OrderBy(key => key).Where(key => key.StartsWith("BindingModel"))) {
        var fieldName = field.Replace("_", ".").Replace("BindingModel.", "");
        HttpContext.Request.Form.TryGetValue(field, out var fieldValues);
        if (fieldValues.Count > 1 && fieldValues[0].Equals("true")) {
          fieldValues = fieldValues[0];
        }
        _formValues.Add(fieldName, fieldValues.ToString());
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Return form values
      \-----------------------------------------------------------------------------------------------------------------------*/
      return _formValues;

    }

    /*==========================================================================================================================
    | HELPER: TO TITLE CASE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Given a string in PascalCase, will conert to Title Case.
    /// </summary>
    public static string ToTitleCase(string input) {

      if (string.IsNullOrEmpty(input)) return input;

      var sb = new StringBuilder();
      sb.Append(char.ToUpper(input[0]));

      for(var i=1; i < input.Length; i++) {
        if(char.IsUpper(input[i]) || char.IsDigit(input[i])) sb.Append(' ');
        sb.Append(input[i]);
      }

      return sb.ToString();
    }


  } // Class
} // Namespace