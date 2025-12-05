/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using Braintree;
using OnTopic;
using OnTopic.AspNetCore.Mvc;

namespace GoldSim.Web.Areas.Payments.Services {

  /*============================================================================================================================
  | CLASS: BRAINTREE CONFIGURATION
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for communication with the Braintree Payment Gateway.
  /// </summary>
  /// <remarks>
  ///   Reference: <see href="https://www.braintreepayments.com/">https://www.braintreepayments.com/</see>.
  /// </remarks>
  internal sealed class BraintreeConfiguration : IBraintreeConfiguration {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private                     IBraintreeGateway               _braintreeGateway;
    private readonly            ITopicRepository                _topicRepository;
    private readonly            IConfiguration                  _configuration;
    private readonly            RouteData                       _routeData;
    private const               string                          _environment                    = "production";

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Establishes a new instance of the <see cref="BraintreeConfiguration"/>, including any shared dependencies to be used
    ///   across instances of controllers.
    /// </summary>
    internal BraintreeConfiguration(ITopicRepository topicRepository, IConfiguration configuration, RouteData routeData) {
      _topicRepository          = topicRepository;
      _configuration            = configuration;
      _routeData                = routeData;
    }

    /*==========================================================================================================================
    | CREATE GATEWAY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Instantiates the Braintree communication gateway, utilizing the appropriate Braintree environment and API credentials.
    /// </summary>
    /// <returns>The configured Braintree payments gateway.</returns>
    private BraintreeGateway CreateGateway() =>
      new(
        Braintree.Environment.ParseEnvironment(_environment),
        GetConfigurationSetting("MerchantId"),
        GetConfigurationSetting("PublicKey"),
        GetConfigurationSetting("PrivateKey")
      );

    /*==========================================================================================================================
    | GET GATEWAY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <inheritdoc />
    public IBraintreeGateway GetGateway() => _braintreeGateway ??= CreateGateway();

    /*==========================================================================================================================
    | GET CONFIGURATION SETTING
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets the configuration value by first checking the local property, and then falling back to other configuration
    ///   sources.
    /// </summary>
    /// <remarks>
    ///   Fallback configuration sources include, in order, the <see cref="Topic"/>, the <see cref="System.Environment"/>, and,
    ///   finally, the application configuration (i.e., the <see cref="IConfiguration"/> provider).
    /// </remarks>
    /// <returns>The configured value for the given variable.</returns>
    private string GetConfigurationSetting(string setting, string defaultValue = null) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish variables
      \-----------------------------------------------------------------------------------------------------------------------*/
      var paymentsTopic         = _topicRepository.Load(_routeData);
      var environmentVariable   = _environment.Equals("sandbox", StringComparison.OrdinalIgnoreCase) ? "Development" : "Production";
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
      | Get API credentials from the application configuration
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