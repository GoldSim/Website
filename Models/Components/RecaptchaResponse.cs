/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace GoldSim.Web.Models.Components {

  /*============================================================================================================================
  | MODEL: RECAPTCHA RESPONSE
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for modeling the JSON response from reCAPTCHA.
  /// </summary>
  internal sealed record RecaptchaResponse {

    /*==========================================================================================================================
    | HOSTNAME
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   The site which solved the reCAPTCHA.
    /// </summary>
    internal string Hostname { get; init; }

    /*==========================================================================================================================
    | ACTION
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   The action which the reCAPTCHA was associated with.
    /// </summary>
    internal string Action { get; init; }

    /*==========================================================================================================================
    | SUCCESS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines if the request was successfully completed.
    /// </summary>
    internal bool Success { get; init; }

    /*==========================================================================================================================
    | SCORE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the assessed score from the reCAPTCHA service.
    /// </summary>
    internal float Score { get; init; }

    /*==========================================================================================================================
    | TIMESTAMP
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the challenge response timestap from the reCAPTCHA service.
    /// </summary>
    [JsonPropertyName("challenge_ts")]
    internal DateTime Timestamp { get; init; }

    /*==========================================================================================================================
    | ERROR CODES
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a list of errors returned from the reCAPTCHA service, if appropriate.
    /// </summary>
    [JsonPropertyName("error-codes")]
    internal Collection<string> ErrorCodes { get; } = [];

  } // Class
} // Namespace