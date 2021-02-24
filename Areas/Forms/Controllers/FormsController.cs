/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using GoldSim.Web.Forms.Models;
using GoldSim.Web.Forms.Models.Partials;
using GoldSim.Web.Models.ContentTypes;
using GoldSim.Web.Services;
using Microsoft.AspNetCore.Mvc;
using OnTopic;
using OnTopic.AspNetCore.Mvc;
using OnTopic.AspNetCore.Mvc.Controllers;
using OnTopic.Mapping;
using OnTopic.Mapping.Reverse;
using OnTopic.Models;
using OnTopic.Repositories;
using OnTopic.ViewModels;

namespace GoldSim.Web.Forms.Controllers {

  /*============================================================================================================================
  | CLASS: FORMS CONTROLLER
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides common processing for all GoldSim forms pages. Each form will be represented by a unique action on this
  ///   controller.
  /// </summary>
  [Area("Forms")]
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
      ITopicMappingService topicMappingService,
      IReverseTopicMappingService reverseTopicMappingService,
      ISmtpService smtpService
    ) : base(
      topicRepository,
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
    private async Task<FormPageTopicViewModel<T>> CreateViewModel<T>(T bindingModel = null)
      where T: class, ITopicBindingModel, new() =>
        await _topicMappingService.MapAsync(
          CurrentTopic,
          new FormPageTopicViewModel<T>(bindingModel)
        ).ConfigureAwait(true) as FormPageTopicViewModel<T>;

    /*==========================================================================================================================
    | HELPER: PROCESS FORM
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Helper function to process a form postback request.
    /// </summary>
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ProcessForm<T>(
      T bindingModel,
      string requestType = ""
    ) where T: CoreContact, ITopicBindingModel, new() {

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var viewModel = await CreateViewModel<T>(bindingModel).ConfigureAwait(true);
      if (!ModelState.IsValid) {
        return View(viewModel);
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Optionally send internal receipt
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (!viewModel.DisableEmailReceipt) {
        var subject = (viewModel.EmailSubject + " " + requestType).Trim();
        await SendInternalReceipt(subject, viewModel.EmailRecipient, viewModel.EmailSender).ConfigureAwait(true);
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Optionally send customer receipt
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (viewModel.CustomerEmail is not null && bindingModel is CoreContact coreContact) {
        await SendCustomerReceipt(viewModel.CustomerEmail, coreContact.Email, viewModel.EmailSender).ConfigureAwait(true);
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Optionally save as topic
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (viewModel.SaveAsTopic) {
        await SaveToTopic(viewModel.BindingModel).ConfigureAwait(true);
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Redirect to configured follow-up page
      \-----------------------------------------------------------------------------------------------------------------------*/
      return RedirectToAction("Redirect", "Redirect", new { topicId = viewModel.FollowUpPage, area = "" });

    }

    /*==========================================================================================================================
    | FORM: TRIAL
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Request a trial of the product.
    /// </summary>
    [HttpGet]
    [ValidateTopic]
    public async Task<IActionResult> TrialAsync() =>
      View(await CreateViewModel<TrialFormBindingModel>().ConfigureAwait(true));

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> TrialAsync(TrialFormBindingModel bindingModel) =>
      await ProcessForm<TrialFormBindingModel>(bindingModel).ConfigureAwait(true);

    /*==========================================================================================================================
    | FORM: DEMO
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Request a demonstration of the product.
    /// </summary>
    [HttpGet]
    [ValidateTopic]
    public async Task<IActionResult> DemoAsync() =>
      View(await CreateViewModel<DemoFormBindingModel>().ConfigureAwait(true));

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DemoAsync(DemoFormBindingModel bindingModel) =>
      await ProcessForm<DemoFormBindingModel>(bindingModel).ConfigureAwait(true);

    /*==========================================================================================================================
    | FORM: QUOTE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Request a quote for the product
    /// </summary>
    [HttpGet]
    [ValidateTopic]
    public async Task<IActionResult> QuoteAsync() =>
      View(await CreateViewModel<QuoteFormBindingModel>().ConfigureAwait(true));

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> QuoteAsync(QuoteFormBindingModel bindingModel) =>
      await ProcessForm<QuoteFormBindingModel>(bindingModel).ConfigureAwait(true);

    /*==========================================================================================================================
    | FORM: PURCHASE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Request to purchase a license of the product
    /// </summary>
    [HttpGet]
    [ValidateTopic]
    public async Task<IActionResult> PurchaseAsync() =>
      View(await CreateViewModel<PurchaseFormBindingModel>().ConfigureAwait(true));

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> PurchaseAsync(PurchaseFormBindingModel bindingModel) =>
      await ProcessForm<PurchaseFormBindingModel>(bindingModel).ConfigureAwait(true);

    /*==========================================================================================================================
    | FORM: NEWSLETTER
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Signup for the GoldSim newsletter
    /// </summary>
    [HttpGet]
    [ValidateTopic]
    public async Task<IActionResult> NewsletterAsync() =>
      View(await CreateViewModel<NewsletterFormBindingModel>().ConfigureAwait(true));

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> NewsletterAsync(NewsletterFormBindingModel bindingModel, string requestType = null) =>
      await ProcessForm<NewsletterFormBindingModel>(bindingModel, requestType).ConfigureAwait(true);

    /*==========================================================================================================================
    | FORM: ACADEMIC (INSTRUCTOR)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Request an academic license of the product for faculty.
    /// </summary>
    [HttpGet]
    [ValidateTopic]
    public async Task<IActionResult> InstructorAcademicAsync()
      => View(await CreateViewModel<InstructorAcademicFormBindingModel>().ConfigureAwait(true));

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> InstructorAcademicAsync(InstructorAcademicFormBindingModel bindingModel) =>
      await ProcessForm<InstructorAcademicFormBindingModel>(bindingModel).ConfigureAwait(true);

    /*==========================================================================================================================
    | FORM: ACADEMIC (STUDENT)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Request an academic license of the product for faculty.
    /// </summary>
    [HttpGet]
    [ValidateTopic]
    public async Task<IActionResult> StudentAcademicAsync() =>
      View(await CreateViewModel<StudentAcademicFormBindingModel>().ConfigureAwait(true));

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> StudentAcademicAsync(StudentAcademicFormBindingModel bindingModel) =>
      await ProcessForm<StudentAcademicFormBindingModel>(bindingModel).ConfigureAwait(true);

    /*==========================================================================================================================
    | FORM: USER CONFERENCE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Request an academic license of the product for faculty.
    /// </summary>
    [HttpGet]
    [ValidateTopic]
    public async Task<IActionResult> UserConferenceAsync() =>
      View(await CreateViewModel<UserConferenceFormBindingModel>().ConfigureAwait(true));

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UserConferenceAsync(UserConferenceFormBindingModel bindingModel) =>
      await ProcessForm<UserConferenceFormBindingModel>(bindingModel).ConfigureAwait(true);

    /*==========================================================================================================================
    | FORM: TRAINING
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Signup for a training session with GoldSim.
    /// </summary>
    [HttpGet]
    [ValidateTopic]
    public async Task<IActionResult> TrainingAsync() =>
      View(await CreateViewModel<TrainingFormBindingModel>().ConfigureAwait(true));

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> TrainingAsync(TrainingFormBindingModel bindingModel) =>
      await ProcessForm<TrainingFormBindingModel>(bindingModel).ConfigureAwait(true);

    /*==========================================================================================================================
    | ACTION: VERIFY EMAIL
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Given an email address, ensures that it doesn't contain any of the public email domains.
    /// </summary>
    [HttpGet]
    public IActionResult VerifyEmail([Bind(Prefix="BindingModel.Email")] string email) {
      if (String.IsNullOrWhiteSpace(email)) return Json(data: true);
      var domains = TopicRepository.Load("Root:Configuration:Metadata:GenericEmailDomains:LookupList").Children;
      var invalidDomain = domains?.FirstOrDefault(m => email.Contains(m.Title, StringComparison.InvariantCultureIgnoreCase));
      if (invalidDomain is not null) {
        return Json($"Please use an email address with an institutional domain; '@{invalidDomain.Title}' is not valid.");
      }
      return Json(data: true);
    }

    /*==========================================================================================================================
    | HELPER: SEND INTERNAL RECEIPT (ASYNC)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Send an email to GoldSim containing all of the form values.
    /// </summary>
    private async Task SendInternalReceipt(string subject = null, string recipient = null, string sender = null) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish variables
      \-----------------------------------------------------------------------------------------------------------------------*/
      subject                   ??= "GoldSim.com/Forms: " + CurrentTopic.Key;
      recipient                 ??= "Software@GoldSim.com";
      sender                    ??= "Software@GoldSim.com";

      /*------------------------------------------------------------------------------------------------------------------------
      | Assemble email
      \-----------------------------------------------------------------------------------------------------------------------*/
      using var mail            = new MailMessage(new MailAddress(sender), new MailAddress(recipient)) {
        Subject                 = subject,
        Body                    = GetEmailBody(),
        IsBodyHtml              = true
      };

      /*------------------------------------------------------------------------------------------------------------------------
      | Send email
      \-----------------------------------------------------------------------------------------------------------------------*/
      await _smptService.SendAsync(mail).ConfigureAwait(true);

    }

    /*==========================================================================================================================
    | HELPER: SEND CUSTOMER RECEIPT (ASYNC)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Send an email to the customer containing the contents of a configured webpage.
    /// </summary>
    private async Task SendCustomerReceipt(EmailTopicViewModel webpage, string recipient, string sender = null) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish variables
      \-----------------------------------------------------------------------------------------------------------------------*/
      var subject               = webpage.ShortTitle?? webpage.Title?? webpage.Key?? "GoldSim Request";
      var request               = HttpContext.Request;
      var url                   = new Uri($"{request.Scheme}://{request.Host}{webpage.WebPath}");
      sender                    ??= "Software@GoldSim.com";

      /*------------------------------------------------------------------------------------------------------------------------
      | Assemble body
      \-----------------------------------------------------------------------------------------------------------------------*/
      using var client          = new HttpClient();
      var response              = await client.GetAsync(url).ConfigureAwait(true);
      var pageContents          = await response.Content.ReadAsStringAsync().ConfigureAwait(true);

      /*------------------------------------------------------------------------------------------------------------------------
      | Assemble email
      \-----------------------------------------------------------------------------------------------------------------------*/
      using var mail            = new MailMessage(new MailAddress(sender), new MailAddress(recipient)) {
        Subject                 = subject,
        Body                    = pageContents,
        IsBodyHtml              = true
      };

      /*------------------------------------------------------------------------------------------------------------------------
      | Send email
      \-----------------------------------------------------------------------------------------------------------------------*/
      await _smptService.SendAsync(mail).ConfigureAwait(true);

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
        var fieldName = ToTitleCase(field.Key.Replace(".", ": ", StringComparison.Ordinal));
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
    private async Task SaveToTopic(CoreContact bindingModel) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish variables
      \-----------------------------------------------------------------------------------------------------------------------*/
      bindingModel              = bindingModel with {
        ContentType             = bindingModel.GetType().Name.Replace("BindingModel", "", StringComparison.Ordinal),
        Key                     = bindingModel.ContentType + "_" + DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture)
      };

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate Topic Parent
      \-----------------------------------------------------------------------------------------------------------------------*/
      var       parentKey       = "Administration:Licenses";
      var       parentTopic     = TopicRepository.Load(parentKey);

      /*------------------------------------------------------------------------------------------------------------------------
      | Map binding model to new topic
      \-----------------------------------------------------------------------------------------------------------------------*/
      var       topic           = await _reverseMappingService.MapAsync(bindingModel).ConfigureAwait(true);
      var       errorMessage    = $"The topic '{parentKey}' could not be found. A root topic to store forms to is required.";

      /*------------------------------------------------------------------------------------------------------------------------
      | Set Topic values
      \-----------------------------------------------------------------------------------------------------------------------*/
      topic.Parent              = parentTopic?? throw new InvalidOperationException(errorMessage);
      topic.LastModified        = DateTime.Now;

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
      if (_formValues is not null) {
        return _formValues;
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Define variables
      \-----------------------------------------------------------------------------------------------------------------------*/
      _formValues = new();

      /*------------------------------------------------------------------------------------------------------------------------
      | Loop over form values
      \-----------------------------------------------------------------------------------------------------------------------*/
      foreach (var field in HttpContext.Request.Form.Keys.Where(key => key.StartsWith("BindingModel", StringComparison.OrdinalIgnoreCase))) {
        var fieldName = field.Replace("_", ".", StringComparison.Ordinal).Replace("BindingModel.", "", StringComparison.Ordinal);
        HttpContext.Request.Form.TryGetValue(field, out var fieldValues);
        if (fieldValues.Count > 1 && fieldValues[0] is "true") {
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

      if (String.IsNullOrEmpty(input)) return input;

      var sb = new StringBuilder();
      sb.Append(Char.ToUpper(input[0], CultureInfo.InvariantCulture));

      for(var i=1; i < input.Length; i++) {
        if(Char.IsUpper(input[i]) || Char.IsDigit(input[i])) sb.Append(' ');
        sb.Append(input[i]);
      }

      return sb.ToString();
    }

  } // Class
} // Namespace