/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System.Net;
using System.Text.Json;
using GoldSim.Web.Models.Recaptcha;

namespace GoldSim.Web.Services {

  /*============================================================================================================================
  | CLASS: RECAPTCHA (REQUEST VALIDATOR)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Validates that a given request is from a human.
  /// </summary>
  public class RecaptchaValidator: IRequestValidator {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private readonly            string                          _secret;
    private readonly            string                          _serviceUrl;
    private static readonly     HttpClient                      _client                         = new();

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="RecaptchaValidator"/> with necessary dependencies.
    /// </summary>
    /// <returns>A new instance of the <see cref="RecaptchaValidator"/>.</returns>
    public RecaptchaValidator(string secret) {
      _secret                   = secret?? throw new ArgumentNullException(nameof(secret));
      _serviceUrl               = "https://www.google.com/recaptcha/api/siteverify";
    }

    /*==========================================================================================================================
    | IS VALID?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines that the request is valid, given a <paramref name="requestToken"/>.
    /// </summary>
    public async Task<bool> IsValid(string requestType, string requestToken) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate input
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (String.IsNullOrEmpty(requestToken)) {
        return false;
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Retrieve value
      \-----------------------------------------------------------------------------------------------------------------------*/
      var uri                   = new Uri($"{_serviceUrl}?secret={_secret}&response={requestToken}");
      var httpResponse          = await _client.GetAsync(uri).ConfigureAwait(false);

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate response
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (httpResponse.StatusCode != HttpStatusCode.OK) {
        return false;
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate score
      \-----------------------------------------------------------------------------------------------------------------------*/
      var jsonResponse          = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
      var recaptchaResponse     = JsonSerializer.Deserialize<RecaptchaResponse>(
        jsonResponse,
        new JsonSerializerOptions() {
          PropertyNameCaseInsensitive = true
        }
      );

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate response
      \-----------------------------------------------------------------------------------------------------------------------*/
      return
        recaptchaResponse.Success &&
        recaptchaResponse.Score >= 0.5 &&
        recaptchaResponse.Action.Equals(requestType, StringComparison.Ordinal);

    }

  } // Class
} // Namespace