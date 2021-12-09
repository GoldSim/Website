/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using GoldSim.Web.Forms.Models;

namespace GoldSim.Web.Payments.Models {

  /*============================================================================================================================
  | VIEW MODEL: PAYMENTS TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about a <c>Payments</c> topic.
  /// </summary>
  public record PaymentsTopicViewModel : PageTopicViewModel {

    public bool? IsValid { get; init; }
    public string ClientToken { get; init; }
    public string ErrorMessagesIntroduction { get; init; }
    public string AmountErrorMessage { get; init; }
    public string EmptyFieldsErrorMessage { get; init; }
    public string CreditCardNumberErrorMessage { get; init; }
    public string ExpirationMonthErrorMessage { get; init; }
    public string ExpirationYearErrorMessage { get; init; }
    public string CvvErrorMessage { get; init; }
    public string PostalCodeErrorMessage { get; init; }
    public string ConfirmationMessageSuccess { get; init; }
    public PaymentFormBindingModel BindingModel { get; init; }

  } // Class
} // Namespace