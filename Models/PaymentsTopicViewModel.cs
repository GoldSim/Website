/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using Ignia.Topics.ViewModels;
using System.Collections.Generic;

namespace GoldSim.Web.Models {

  /*============================================================================================================================
  | VIEW MODEL: PAYMENTS TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about a <c>Payments</c> topic.
  /// </summary>
  public class PaymentsTopicViewModel : PageTopicViewModel {

    public bool? IsValid { get; set; }
    public string ClientToken { get; set; }
    public string ErrorMessageAmount { get; set; }
    public Dictionary<string, string> ErrorMessages { get; set; }

  } // Class

} // Namespace
