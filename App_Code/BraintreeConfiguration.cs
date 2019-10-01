/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System;
using System.Configuration;
using Braintree;
using Ignia.Topics;
using Microsoft.Extensions.Configuration;

namespace GoldSim.Web {

  /*============================================================================================================================
  | CLASS: BRAINTREE CONFIGURATION
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for communication with the Braintree Payment Gateway.
  /// </summary>
  /// <remarks>
  ///   Reference: <see href="https://www.braintreepayments.com/">https://www.braintreepayments.com/</see>
  public class BraintreeConfiguration : IBraintreeConfiguration {

    /*==========================================================================================================================
    | PRIVATE FIELDS
    \-------------------------------------------------------------------------------------------------------------------------*/
    private                     IBraintreeGateway               _braintreeGateway               = null;
    private readonly            ITopicRoutingService            _topicRoutingService            = null;
    private readonly            IConfiguration                  _configuration                  = null;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Establishes a new instance of the <see cref="BraintreeConfiguration"/>, including any shared dependencies to be used
    ///   across instances of controllers.
    /// </summary>
    public BraintreeConfiguration(ITopicRoutingService topicRoutingService, IConfiguration configuration) {
      _topicRoutingService      = topicRoutingService;
      _configuration            = configuration;
    }

    /*==========================================================================================================================
    | PUBLIC PROPERTIES
    \-------------------------------------------------------------------------------------------------------------------------*/
    public string Environment { get; set; } = "production";
    public string MerchantId { get; set; }
    public string PublicKey { get; set; }
    public string PrivateKey { get; set; }

    /*==========================================================================================================================
    | CREATE GATEWAY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Instantiates the Braintree communication gateway, utilizing the appropriate Braintree environment and API credentials.
    /// </summary>
    /// <returns>The configured Braintree payments gateway.</returns>
    public IBraintreeGateway CreateGateway() {

      /*------------------------------------------------------------------------------------------------------------------------
      | Return the Braintree API gateway
      \-----------------------------------------------------------------------------------------------------------------------*/
      return new BraintreeGateway(
        Braintree.Environment.ParseEnvironment(Environment),
        GetConfigurationSetting(nameof(MerchantId), MerchantId),
        GetConfigurationSetting(nameof(PublicKey), PublicKey),
        GetConfigurationSetting(nameof(PrivateKey), PrivateKey)
      );

    }

    /*==========================================================================================================================
    | GET GATEWAY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Looks up the currently configured Braintree payments gateway. If the gateway is not currently available, it is
    ///   manually created.
    /// </summary>
    /// <returns>The configured Braintree payments gateway.</returns>
    public IBraintreeGateway GetGateway() {

      if (_braintreeGateway == null) {
        _braintreeGateway = CreateGateway();
      }

      return _braintreeGateway;
    }


    /*==========================================================================================================================
    | GET CONFIGURATION SETTING
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets the configuration value by first checking the local property, and then falling back to other configuration
    ///   sources.
    /// </summary>
    /// <remarks>
    ///   Fallback configuration sources include, in order, the <see cref="Topic"/>, the <see cref="Environment"/>, and,
    ///   finally, the <see cref="ConfigurationManager.AppSettings"/>.
    /// </remarks>
    /// <returns>The configured value for the given variable.</returns>
    public string GetConfigurationSetting(string variable, string defaultValue = null) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish variables
      \-----------------------------------------------------------------------------------------------------------------------*/
      var paymentsTopic         = _topicRoutingService.GetCurrentTopic();
      var environmentVariable   = Environment.Equals("sandbox", StringComparison.OrdinalIgnoreCase) ? "Development" : "Production";
      var compositeVariable     = $"Braintree{environmentVariable}{variable}";
      var value                 = defaultValue;

      /*------------------------------------------------------------------------------------------------------------------------
      | Get API credentials from Payments Topic
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (String.IsNullOrEmpty(value)) {
        value = paymentsTopic.Attributes.GetValue(compositeVariable);
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Get API credentials from Environment Variables
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (String.IsNullOrEmpty(value)) {
        value = System.Environment.GetEnvironmentVariable(compositeVariable);
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Get API credentials from App Settings
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (String.IsNullOrEmpty(value)) {
        value = _configuration.GetValue<string>(compositeVariable);
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Return the Braintree API gateway
      \-----------------------------------------------------------------------------------------------------------------------*/
      return value;

    }

  } // Class

} // Namespace