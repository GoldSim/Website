/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using System;
using GoldSim.Web.Administration.Models.Invoices;
using GoldSim.Web.Administration.Models.Licenses;
using GoldSim.Web.Models.Forms.BindingModels;
using GoldSim.Web.Models.ViewModels;
using OnTopic.Editor.AspNetCore.Infrastructure;

namespace GoldSim.Web {

  /*============================================================================================================================
  | CLASS: GOLDSIM TOPIC VIEW MODEL LOOKUP SERVICE
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a mapping between string and class names to be used when mapping <see cref="Topic"/> to a <see
  ///   cref="TopicViewModel"/> or derived class.
  /// </summary>
  public class GoldSimTopicViewModelLookupService : EditorViewModelLookupService {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Instantiates a new instance of the <see cref="GoldSimViewModelLookupService"/>.
    /// </summary>
    /// <returns>A new instance of the <see cref="GoldSimViewModelLookupService"/>.</returns>
    internal GoldSimTopicViewModelLookupService() : base() {

      /*------------------------------------------------------------------------------------------------------------------------
      | Add GoldSim specific view models
      \-----------------------------------------------------------------------------------------------------------------------*/
      Add(typeof(ApplicationBasePageTopicViewModel));
      Add(typeof(ApplicationContainerTopicViewModel));
      Add(typeof(ApplicationIndexTopicViewModel));
      Add(typeof(ApplicationPageTopicViewModel));
      Add(typeof(DocumentPointerTopicViewModel));
      Add(typeof(EmailTopicViewModel));
      Add(typeof(ExampleApplicationTopicViewModel));
      Add(typeof(ExampleIndexTopicViewModel));
      Add(typeof(FaqItemTopicViewModel));
      Add(typeof(FaqTopicViewModel));
      Add(typeof(FollowupTopicViewModel));
      Add(typeof(FormPageTopicViewModel));
      Add(typeof(GlossaryItemTopicViewModel));
      Add(typeof(GlossaryTopicViewModel));
      Add(typeof(HomeTopicViewModel));
      Add(typeof(ModulePageTopicViewModel));
      Add(typeof(PaymentsTopicViewModel));
      Add(typeof(PaymentFormBindingModel));
      Add(typeof(SearchTopicViewModel));
      Add(typeof(TechnicalPaperListTopicViewModel));
      Add(typeof(TechnicalPaperTopicViewModel));
      Add(typeof(CourseTopicViewModel));

      /*------------------------------------------------------------------------------------------------------------------------
      | License administration
      \-----------------------------------------------------------------------------------------------------------------------*/
      Add(typeof(InvoiceTopicViewModel));
      Add(typeof(LicenseAdministrationTopicViewModel));
      Add(typeof(LicenseRequestTopicViewModel));
      Add(typeof(TrialFormTopicViewModel));
      Add(typeof(InstructorAcademicFormTopicViewModel));
      Add(typeof(StudentAcademicFormTopicViewModel));

      /*------------------------------------------------------------------------------------------------------------------------
      | Override Ignia topics
      \-----------------------------------------------------------------------------------------------------------------------*/
      Replace(typeof(ContentListTopicViewModel));
      Replace(typeof(NavigationTopicViewModel));
      Replace(typeof(PageGroupTopicViewModel));

      /*------------------------------------------------------------------------------------------------------------------------
      | Function: Replace
      \-----------------------------------------------------------------------------------------------------------------------*/
      void Replace(Type type) {
        if (Contains(type.Name)) {
          Remove(type.Name);
        }
        Add(type);
      }

    }

  } //Class
} //Namespace
