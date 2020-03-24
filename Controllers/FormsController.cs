/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using GoldSim.Web.Models.Forms;
using GoldSim.Web.Models.Forms.BindingModels;
using GoldSim.Web.Models.ViewModels;
using GoldSim.Web.Services;
using Microsoft.AspNetCore.Mvc;
using OnTopic;
using OnTopic.AspNetCore.Mvc;
using OnTopic.AspNetCore.Mvc.Controllers;
using OnTopic.Mapping;
using OnTopic.Mapping.Reverse;
using OnTopic.Models;
using OnTopic.Repositories;

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
        ) as FormPageTopicViewModel<T>;

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
    ) where T: class, ITopicBindingModel, new() {

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var viewModel = await CreateViewModel<T>(bindingModel);
      if (!ModelState.IsValid) {
        return View(viewModel);
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Optionally send internal receipt
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (!viewModel.DisableEmailReceipt) {
        var subject = (viewModel.EmailSubject + " " + requestType).Trim();
        await SendInternalReceipt(subject, viewModel.EmailRecipient, viewModel.EmailSender);
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Optionally send customer receipt
      \-----------------------------------------------------------------------------------------------------------------------*/
      var coreContact = bindingModel as CoreContact;
      if (viewModel.CustomerEmail != null && coreContact != null) {
        await SendCustomerReceipt(viewModel.CustomerEmail, coreContact.Email, viewModel.EmailSender);
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Optionally save as topic
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (viewModel.SaveAsTopic) {
        await SaveToTopic(viewModel.BindingModel);
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
    [ValidateTopic]
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
    [ValidateTopic]
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
    [ValidateTopic]
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
    [ValidateTopic]
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
    [ValidateTopic]
    public async Task<IActionResult> NewsletterAsync() => View(await CreateViewModel<NewsletterFormBindingModel>());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> NewsletterAsync(NewsletterFormBindingModel bindingModel, string requestType = null) =>
      await ProcessForm<NewsletterFormBindingModel>(bindingModel, requestType);

    /*==========================================================================================================================
    | FORM: ACADEMIC (INSTRUCTOR)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Request an academic license of the product for faculty.
    /// </summary>
    [HttpGet]
    [ValidateTopic]
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
    [ValidateTopic]
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
    [ValidateTopic]
    public async Task<IActionResult> UserConferenceAsync() => View(await CreateViewModel<UserConferenceFormBindingModel>());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UserConferenceAsync(UserConferenceFormBindingModel bindingModel) =>
      await ProcessForm<UserConferenceFormBindingModel>(bindingModel);

    /*==========================================================================================================================
    | FORM: TRAINING
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Signup for a training session with GoldSim.
    /// </summary>
    [HttpGet]
    [ValidateTopic]
    public async Task<IActionResult> TrainingAsync() => View(await CreateViewModel<TrainingFormBindingModel>());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> TrainingAsync(TrainingFormBindingModel bindingModel) =>
      await ProcessForm<TrainingFormBindingModel>(bindingModel);

    /*==========================================================================================================================
    | ACTION: VERIFY EMAIL
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Given an email address, ensures that it doesn't contain any of the public email domains.
    /// </summary>
    [HttpGet, HttpPost]
    public IActionResult VerifyEmail([Bind(Prefix="BindingModel.Email")] string email) {
      if (String.IsNullOrWhiteSpace(email)) return Json(data: true);
      var domains = TopicRepository.Load("Root:Configuration:Metadata:GenericEmailDomains:LookupList").Children;
      var invalidDomain = domains?.FirstOrDefault(m => email.Contains(m.Title, StringComparison.InvariantCultureIgnoreCase));
      if (invalidDomain != null) {
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
      sender                    ??= "Website@GoldSim.com";

      /*------------------------------------------------------------------------------------------------------------------------
      | Assemble email
      \-----------------------------------------------------------------------------------------------------------------------*/
      var mail                  = new MailMessage(new MailAddress(sender), new MailAddress(recipient)) {
        Subject                 = subject,
        Body                    = GetEmailBody(),
        IsBodyHtml              = true
      };

      /*------------------------------------------------------------------------------------------------------------------------
      | Send email
      \-----------------------------------------------------------------------------------------------------------------------*/
      await _smptService.SendAsync(mail);

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
      var client                = new HttpClient();
      var response              = await client.GetAsync(url);
      var pageContents          = await response.Content.ReadAsStringAsync();

      /*------------------------------------------------------------------------------------------------------------------------
      | Assemble email
      \-----------------------------------------------------------------------------------------------------------------------*/
      var mail                  = new MailMessage(new MailAddress(sender), new MailAddress(recipient)) {
        Subject                 = subject,
        Body                    = pageContents,
        IsBodyHtml              = true
      };

      /*------------------------------------------------------------------------------------------------------------------------
      | Send email
      \-----------------------------------------------------------------------------------------------------------------------*/
      await _smptService.SendAsync(mail);

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
    private async Task SaveToTopic(ITopicBindingModel bindingModel) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish variables
      \-----------------------------------------------------------------------------------------------------------------------*/
      bindingModel.ContentType  = bindingModel.GetType().Name.Replace("BindingModel", "");
      bindingModel.Key          = bindingModel.ContentType + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate Topic Parent
      \-----------------------------------------------------------------------------------------------------------------------*/
      var       parentKey       = "Administration:Licenses";
      var       parentTopic     = TopicRepository.Load(parentKey);

      if (parentTopic == null) {
        throw new Exception($"The topic '{parentKey}' could not be found. A root topic to store forms to is required.");
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Map binding model to new topic
      \-----------------------------------------------------------------------------------------------------------------------*/
      var       topic           = await _reverseMappingService.MapAsync(bindingModel);

      /*------------------------------------------------------------------------------------------------------------------------
      | Set Topic values
      \-----------------------------------------------------------------------------------------------------------------------*/
      topic.Parent              = parentTopic;
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
      foreach (var field in HttpContext.Request.Form.Keys.Where(key => key.StartsWith("BindingModel"))) {
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

      if (String.IsNullOrEmpty(input)) return input;

      var sb = new StringBuilder();
      sb.Append(Char.ToUpper(input[0]));

      for(var i=1; i < input.Length; i++) {
        if(Char.IsUpper(input[i]) || Char.IsDigit(input[i])) sb.Append(' ');
        sb.Append(input[i]);
      }

      return sb.ToString();
    }


  } // Class
} // Namespace