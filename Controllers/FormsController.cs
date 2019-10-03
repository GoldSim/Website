/*==============================================================================================================================
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
      IReverseTopicMappingService reverseTopicMappingService
    ) : base(
      topicRepository,
      topicRoutingService,
      topicMappingService
    ) {
      _topicMappingService      = topicMappingService;
      _reverseMappingService    = reverseTopicMappingService;
    }

    /*==========================================================================================================================
    | CREATE VIEW MODEL
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Constructs a new view model
    /// </summary>
    public async Task<FormPageTopicViewModel<T>> CreateViewModel<T>(T bindingModel = null) where T: class, new() {
      var viewModel = await _topicMappingService.MapAsync(CurrentTopic, new FormPageTopicViewModel<T>(bindingModel));
      var viewModelTyped = viewModel as FormPageTopicViewModel<T>;
      return viewModelTyped;
    }

    /*==========================================================================================================================
    | FORM: TRIAL
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Request a trial of the product.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> TrialAsync() {
      var bindingModel = await CreateViewModel<TrialFormBindingModel>();
      return View(bindingModel);
      }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> TrialAsync(TrialFormBindingModel bindingModel) {
      if (ModelState.IsValid) {
        return RedirectToAction("Index");
      }
      return View(await CreateViewModel<TrialFormBindingModel>(bindingModel));
    }

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
    public async Task<IActionResult> DemoAsync(DemoFormBindingModel bindingModel) {
      if (ModelState.IsValid) {
        return RedirectToAction("Index");
      }
      return View(await CreateViewModel<DemoFormBindingModel>(bindingModel));
    }

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
    public async Task<IActionResult> QuoteAsync(QuoteFormBindingModel bindingModel) {
      if (ModelState.IsValid) {
        return RedirectToAction("Index");
      }
      return View(await CreateViewModel<QuoteFormBindingModel>(bindingModel));
    }

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
    public async Task<IActionResult> PurchaseAsync(PurchaseFormBindingModel bindingModel) {
      if (ModelState.IsValid) {
        return RedirectToAction("Index");
      }
      return View(await CreateViewModel<PurchaseFormBindingModel>(bindingModel));
    }


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
    public async Task<IActionResult> NewsletterAsync(NewsletterFormBindingModel bindingModel) {
      if (ModelState.IsValid) {
        return RedirectToAction("Index");
      }
      return View(await CreateViewModel<NewsletterFormBindingModel>(bindingModel));
    }


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
    public async Task<IActionResult> InstructorAcademicAsync(InstructorAcademicFormBindingModel bindingModel) {
      if (ModelState.IsValid) {
        return RedirectToAction("Index");
      }
      return View(await CreateViewModel<InstructorAcademicFormBindingModel>(bindingModel));
    }

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
    public async Task<IActionResult> StudentAcademicAsync(StudentAcademicFormBindingModel bindingModel) {
      if (ModelState.IsValid) {
        return RedirectToAction("Index");
      }
      return View(await CreateViewModel<StudentAcademicFormBindingModel>(bindingModel));
    }


  } // Class

} // Namespace