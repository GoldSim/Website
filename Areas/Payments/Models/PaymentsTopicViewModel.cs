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
  internal sealed record PaymentsTopicViewModel : PageTopicViewModel {

    internal bool? IsValid { get; init; }
    internal string ClientToken { get; init; }
    internal string ErrorMessagesIntroduction { get; init; }
    internal string AmountErrorMessage { get; init; }
    internal string EmptyFieldsErrorMessage { get; init; }
    internal string CreditCardNumberErrorMessage { get; init; }
    internal string ExpirationMonthErrorMessage { get; init; }
    internal string ExpirationYearErrorMessage { get; init; }
    internal string CvvErrorMessage { get; init; }
    internal string PostalCodeErrorMessage { get; init; }
    internal string ConfirmationMessageSuccess { get; init; }
    internal PaymentFormBindingModel BindingModel { get; init; }

  } // Class
} // Namespace