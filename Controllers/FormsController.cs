/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using GoldSim.Web.Models.Forms.BindingModels;
using Ignia.Topics;
using Ignia.Topics.Mapping;
using Ignia.Topics.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GoldSim.Web.Controllers {

  /*============================================================================================================================
  | CLASS: FORMS CONTROLLER
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides common processing for all GoldSim forms pages. Each form will be represented by a unique action on this
  ///   controller.
  /// </summary>
  public class FormsController : Controller {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private readonly            ITopicRepository                _topicRepository;
    private readonly            IReverseTopicMappingService     _reverseMappingService;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a Topic Controller with necessary dependencies.
    /// </summary>
    /// <returns>A topic controller for loading OnTopic views.</returns>
    public FormsController(ITopicRepository topicRepository, IReverseTopicMappingService reverseTopicMappingService) : base() {
      _topicRepository          = topicRepository;
      _reverseMappingService    = reverseTopicMappingService;
    }

    /*==========================================================================================================================
    | FORM: TRIAL
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Request a trial of the product.
    /// </summary>
    [HttpGet]
    public IActionResult Trial() => View(new TrialFormBindingModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Trial(TrialFormBindingModel bindingModel) {
      if (ModelState.IsValid) {
        return RedirectToAction("Index");
      }
      return View(bindingModel);
    }

    /*==========================================================================================================================
    | FORM: DEMO
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Request a demonstration of the product.
    /// </summary>
    [HttpGet]
    public IActionResult Demo() => View(new DemoFormBindingModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Demo(DemoFormBindingModel bindingModel) {
      if (ModelState.IsValid) {
        return RedirectToAction("Index");
      }
      return View(bindingModel);
    }

    /*==========================================================================================================================
    | FORM: QUOTE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Request a quote for the product
    /// </summary>
    [HttpGet]
    public IActionResult Quote() => View(new QuoteFormBindingModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Quote(QuoteFormBindingModel bindingModel) {
      if (ModelState.IsValid) {
        return RedirectToAction("Index");
      }
      return View(bindingModel);
    }

    /*==========================================================================================================================
    | FORM: PURCHASE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Request to purchase a license of the product
    /// </summary>
    [HttpGet]
    public IActionResult Purchase() => View(new PurchaseFormBindingModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Purchase(PurchaseFormBindingModel bindingModel) {
      if (ModelState.IsValid) {
        return RedirectToAction("Index");
      }
      return View(bindingModel);
    }


    /*==========================================================================================================================
    | FORM: NEWSLETTER
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Signup for the GoldSim newsletter
    /// </summary>
    [HttpGet]
    public IActionResult Newsletter() => View(new NewsletterFormBindingModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Newsletter(NewsletterFormBindingModel bindingModel) {
      if (ModelState.IsValid) {
        return RedirectToAction("Index");
      }
      return View(bindingModel);
    }


    /*==========================================================================================================================
    | FORM: ACADEMIC (INSTRUCTOR)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Request an academic license of the product for faculty.
    /// </summary>
    [HttpGet]
    public IActionResult InstructorAcademic() => View(new InstructorAcademicFormBindingModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult InstructorAcademic(InstructorAcademicFormBindingModel bindingModel) {
      if (ModelState.IsValid) {
        return RedirectToAction("Index");
      }
      return View(bindingModel);
    }

    /*==========================================================================================================================
    | FORM: ACADEMIC (STUDENT)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Request an academic license of the product for faculty.
    /// </summary>
    [HttpGet]
    public IActionResult StudentAcademic() => View(new StudentAcademicFormBindingModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult StudentAcademic(StudentAcademicFormBindingModel bindingModel) {
      if (ModelState.IsValid) {
        return RedirectToAction("Index");
      }
      return View(bindingModel);
    }


  } // Class

} // Namespace