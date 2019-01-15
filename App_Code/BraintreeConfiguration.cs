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

    public string Environment { get; set; }
    public string MerchantId { get; set; }
    public string PublicKey { get; set; }
    public string PrivateKey { get; set; }
    private IBraintreeGateway BraintreeGateway { get; set; }

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
        Environment             = System.Environment.GetEnvironmentVariable("BraintreeEnvironment");
        MerchantId              = System.Environment.GetEnvironmentVariable("BraintreeMerchantId");
        PublicKey               = System.Environment.GetEnvironmentVariable("BraintreePublicKey");
        PrivateKey              = System.Environment.GetEnvironmentVariable("BraintreePrivateKey");
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Get API credentials from App Settings
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (MerchantId == null || PublicKey == null || PrivateKey == null) {
        Environment             = GetConfigurationSetting("BraintreeEnvironment");
        MerchantId              = GetConfigurationSetting("BraintreeMerchantId");
        PublicKey               = GetConfigurationSetting("BraintreePublicKey");
        PrivateKey              = GetConfigurationSetting("BraintreePrivateKey");
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

      if (BraintreeGateway == null) {
        BraintreeGateway        = CreateGateway();
      }

      return BraintreeGateway;
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