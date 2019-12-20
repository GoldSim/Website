/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using GoldSim.Web.Models.Forms.BindingModels;
using Ignia.Topics.ViewModels;

namespace GoldSim.Web.Models.ViewModels {

  /*============================================================================================================================
  | VIEW MODEL: PAYMENTS TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about a <c>Payments</c> topic.
  /// </summary>
  public class PaymentsTopicViewModel : PageTopicViewModel {

    public bool? IsValid { get; set; }
    public string ClientToken { get; set; }
    public string ErrorMessagesIntroduction { get; set; }
    public string AmountErrorMessage { get; set; }
    public string EmptyFieldsErrorMessage { get; set; }
    public string CreditCardNumberErrorMessage { get; set; }
    public string ExpirationMonthErrorMessage { get; set; }
    public string ExpirationYearErrorMessage { get; set; }
    public string CvvErrorMessage { get; set; }
    public string PostalCodeErrorMessage { get; set; }
    public string ConfirmationMessageSuccess { get; set; }
    public PaymentFormBindingModel BindingModel { get; set; }

  } // Class

} // Namespace
