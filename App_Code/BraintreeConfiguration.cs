/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System.Configuration;
using Braintree;
using Ignia.Topics;
using Ignia.Topics.Web;

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
    private                     IBraintreeGateway               _braintreeGateway                = null;
    private readonly            ITopicRoutingService            _topicRoutingService            = null;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Establishes a new instance of the <see cref="BraintreeConfiguration"/>, including any shared dependencies to be used
    ///   across instances of controllers.
    /// </summary>
    public BraintreeConfiguration(ITopicRoutingService topicRoutingService) {
      _topicRoutingService      = topicRoutingService;
    }

    /*==========================================================================================================================
    | PUBLIC PROPERTIES
    \-------------------------------------------------------------------------------------------------------------------------*/
    public string Environment { get; set; }
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
      | Establish variables
      \-----------------------------------------------------------------------------------------------------------------------*/
      Topic PaymentsTopic       = TopicRepository.DataProvider.Load("Root:Web:Purchase:Payments");
      Environment               = "sandbox";

      /*------------------------------------------------------------------------------------------------------------------------
      | Get API credentials from Payments Topic
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (Environment.Equals("sandbox")) {
        MerchantId              = PaymentsTopic.Attributes.GetValue("BraintreeDevelopmentMerchantId");
        PublicKey               = PaymentsTopic.Attributes.GetValue("BraintreeDevelopmentPublicApiKey");
        PrivateKey              = PaymentsTopic.Attributes.GetValue("BraintreeDevelopmentPrivateApiKey");
      }
      else {
        MerchantId              = PaymentsTopic.Attributes.GetValue("BraintreeProductionMerchantId");
        PublicKey               = PaymentsTopic.Attributes.GetValue("BraintreeProductionPublicApiKey");
        PrivateKey              = PaymentsTopic.Attributes.GetValue("BraintreeProductionPrivateApiKey");
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Get API credentials from Environment Variables
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (MerchantId == null || PublicKey == null || PrivateKey == null) {
        if (Environment.Equals("sandbox")) {
          MerchantId            = System.Environment.GetEnvironmentVariable("BraintreeDevelopmentMerchantId");
          PublicKey             = System.Environment.GetEnvironmentVariable("BraintreeDevelopmentPublicApiKey");
          PrivateKey            = System.Environment.GetEnvironmentVariable("BraintreeDevelopmentPrivateApiKey");
        }
        else {
          MerchantId            = System.Environment.GetEnvironmentVariable("BraintreeProductionMerchantId");
          PublicKey             = System.Environment.GetEnvironmentVariable("BraintreeProductionPublicApiKey");
          PrivateKey            = System.Environment.GetEnvironmentVariable("BraintreeProductionPrivateApiKey");
        }
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Get API credentials from App Settings
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (MerchantId == null || PublicKey == null || PrivateKey == null) {
        if (Environment.Equals("sandbox")) {
          MerchantId            = GetConfigurationSetting("BraintreeDevelopmentMerchantId");
          PublicKey             = GetConfigurationSetting("BraintreeDevelopmentPublicApiKey");
          PrivateKey            = GetConfigurationSetting("BraintreeDevelopmentPrivateApiKey");
        }
        else {
          MerchantId            = GetConfigurationSetting("BraintreeProductionMerchantId");
          PublicKey             = GetConfigurationSetting("BraintreeProductionPublicApiKey");
          PrivateKey            = GetConfigurationSetting("BraintreeProductionPrivateApiKey");
        }
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Return the Braintree API gateway
      \-----------------------------------------------------------------------------------------------------------------------*/
      return new BraintreeGateway(Braintree.Environment.ParseEnvironment(Environment), MerchantId, PublicKey, PrivateKey);

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
        _braintreeGateway        = CreateGateway();
      }

      return _braintreeGateway;
    }

    /*==========================================================================================================================
    | GET CONFIGURATION SETTING
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Conveniene method for looking up configuration settings; currently pulls from the application's `AppSettings`
    ///   configuration.
    /// </summary>
    public string GetConfigurationSetting(string setting) {
      return ConfigurationManager.AppSettings[setting];
    }

  } // Class

} // Namespace