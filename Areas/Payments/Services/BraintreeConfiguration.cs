/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using Braintree;
using OnTopic;
using OnTopic.AspNetCore.Mvc;
using OnTopic.Repositories;

namespace GoldSim.Web.Payments.Services {

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
    private                     IBraintreeGateway               _braintreeGateway;
    private readonly            ITopicRepository                _topicRepository;
    private readonly            IConfiguration                  _configuration;
    private readonly            RouteData                       _routeData;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Establishes a new instance of the <see cref="BraintreeConfiguration"/>, including any shared dependencies to be used
    ///   across instances of controllers.
    /// </summary>
    public BraintreeConfiguration(ITopicRepository topicRepository, IConfiguration configuration, RouteData routeData) {
      _topicRepository          = topicRepository;
      _configuration            = configuration;
      _routeData                = routeData;
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
    public IBraintreeGateway CreateGateway() =>
      new BraintreeGateway(
        Braintree.Environment.ParseEnvironment(Environment),
        GetConfigurationSetting(nameof(MerchantId), MerchantId),
        GetConfigurationSetting(nameof(PublicKey), PublicKey),
        GetConfigurationSetting(nameof(PrivateKey), PrivateKey)
      );

    /*==========================================================================================================================
    | GET GATEWAY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Looks up the currently configured Braintree payments gateway. If the gateway is not currently available, it is
    ///   manually created.
    /// </summary>
    /// <returns>The configured Braintree payments gateway.</returns>
    public IBraintreeGateway GetGateway() {
      if (_braintreeGateway is null) {
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
    public string GetConfigurationSetting(string setting, string defaultValue = null) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish variables
      \-----------------------------------------------------------------------------------------------------------------------*/
      var paymentsTopic         = _topicRepository.Load(_routeData);
      var environmentVariable   = Environment.Equals("sandbox", StringComparison.OrdinalIgnoreCase) ? "Development" : "Production";
      var compositeVariable     = $"Braintree:{environmentVariable}:{setting}";
      var compositeAttributeKey = $"Braintree{environmentVariable}{setting}";
      var value                 = defaultValue;

      /*------------------------------------------------------------------------------------------------------------------------
      | Get API credentials from Payments Topic
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (String.IsNullOrEmpty(value)) {
        value = paymentsTopic.Attributes.GetValue(compositeAttributeKey);
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