/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using Braintree;

namespace GoldSim.Web.Areas.Payments.Services {

  /*============================================================================================================================
  | INTERFACE: BRAINTREE CONFIGURATION
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides an interface for methods used to configure the Braintree payment gateway.
  /// </summary>
  internal interface IBraintreeConfiguration {

    IBraintreeGateway CreateGateway();
    string GetConfigurationSetting(string setting, string defaultValue = null);
    /// <summary>
    ///   Looks up the currently configured Braintree payments gateway. If the gateway is not currently available, it is
    ///   manually created.
    /// </summary>
    /// <returns>The configured Braintree payments gateway.</returns>
    IBraintreeGateway GetGateway();

  } // Class
} // Namespace