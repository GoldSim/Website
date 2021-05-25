/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using System;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace GoldSim.Web.Models.Recaptcha {

  /*============================================================================================================================
  | MODEL: RECAPTCHA RESPONSE
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for modeling the JSON response from reCAPTCHA.
  /// </summary>
  public record RecaptchaResponse {

    /*==========================================================================================================================
    | HOSTNAME
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   The site which solved the reCAPTCHA.
    /// </summary>
    public string Hostname { get; init; }

    /*==========================================================================================================================
    | ACTION
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   The action which the reCAPTCHA was associated with.
    /// </summary>
    public string Action { get; init; }

    /*==========================================================================================================================
    | SUCCESS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines if the request was successfully completed.
    /// </summary>
    public bool Success { get; init; }

    /*==========================================================================================================================
    | SCORE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the assessed score from the reCAPTCHA service.
    /// </summary>
    public float Score { get; init; }

    /*==========================================================================================================================
    | TIMESTAMP
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the challenge response timestap from the reCAPTCHA service.
    /// </summary>
    [JsonPropertyName("challenge_ts")]
    public DateTime Timestamp { get; init; }

    /*==========================================================================================================================
    | ERROR CODES
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a list of errors returned from the reCAPTCHA service, if appropriate.
    /// </summary>
    [JsonPropertyName("error-codes")]
    public Collection<string> ErrorCodes { get; } = new();

  } // Class
} // Namespace