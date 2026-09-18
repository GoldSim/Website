/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using System.Text.Json;
using OnTopic;

namespace GoldSim.Web.Areas.Forms.HubSpot {

  /*============================================================================================================================
  | CLASS: HUBSPOT CONTACT SYNC SERVICE
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Default implementation of <see cref="IHubSpotContactSyncService"/>, which saves a HubSpot contact via the CRM v3
  ///   batch "upsert" endpoint.
  /// </summary>
  /// <remarks>
  ///   If no HubSpot access token is configured, that's not treated as an error; instead, the
  ///   <see cref="SyncAsync(Topic, HubSpotFormManifest)"/> method returns a skipped <see cref="HubSpotSyncResult"/> without
  ///   attempting a call. Every other failure—e.g., an unreachable host, a non-2xx response, or an exception raised while
  ///   building the payload (e.g., a required field is missing from this particular submission)—is caught and reported via
  ///   the returned <see cref="HubSpotSyncResult"/>, per the interface's contract.
  /// </remarks>
  public sealed class HubSpotContactSyncService : IHubSpotContactSyncService {

    /*==========================================================================================================================
    | CONSTANT: REQUEST URI
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets the relative URI of HubSpot's CRM v3 contacts batch "upsert" endpoint, relative to
    ///   <see cref="HttpClient.BaseAddress"/>.
    /// </summary>
    private const string        _requestUri                     = "crm/v3/objects/contacts/batch/upsert";

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    //### TODO JJC20260918: Switch to a named client resolved via IHttpClientFactory once the site adopts .NET's DI container.
    // For now, this follows the same long-lived, manually constructed HttpClient pattern already used elsewhere, since the
    // controller activator that constructs this service runs before the DI container is built.
    private readonly            HttpClient                      _httpClient;
    private readonly            IHubSpotPayloadBuilder          _payloadBuilder;
    private readonly            string                          _accessToken;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Establishes a new instance of a <see cref="HubSpotContactSyncService"/>.
    /// </summary>
    /// <param name="httpClient">The <see cref="HttpClient"/>, with its base address set to HubSpot's API root.</param>
    /// <param name="payloadBuilder">The <see cref="IHubSpotPayloadBuilder"/> used to translate topics into payloads.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/> that the HubSpot access token is read from.</param>
    public HubSpotContactSyncService(
      HttpClient httpClient,
      IHubSpotPayloadBuilder payloadBuilder,
      IConfiguration configuration
    ) {

      // Validate parameters
      Contract.Requires(httpClient, nameof(httpClient));
      Contract.Requires(payloadBuilder, nameof(payloadBuilder));
      Contract.Requires(configuration, nameof(configuration));

      // Set local values
      _httpClient               = httpClient;
      _payloadBuilder           = payloadBuilder;
      _accessToken              = configuration.GetValue<string>("HubSpot:AccessToken");

    }

    /*==========================================================================================================================
    | METHOD: SYNC (ASYNC)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <inheritdoc/>
    public async Task<HubSpotSyncResult> SyncAsync(Topic topic, HubSpotFormManifest manifest) {

      // Validate parameters
      Contract.Requires(topic, nameof(topic));
      Contract.Requires(manifest, nameof(manifest));

      // Skip sync if no access token is configured
      if (string.IsNullOrEmpty(_accessToken)) {
        return new() {
          IsSuccessful          = false,
          IsSkipped             = true
        };
      }

      // Attempt sync, reporting any failure via the result rather than throwing
      try {

        // Build payload
        var payload             = _payloadBuilder.Build(topic, manifest);
        var uniqueKeyProperty   = manifest.UniqueKeyField.HubSpotProperty;

        // Construct request body
        var requestBody         = new {
          inputs                = new[] {
            new {
              id                = payload[uniqueKeyProperty],
              idProperty        = uniqueKeyProperty,
              properties        = payload
            }
          }
        };

        // Construct request
        using var request       = new HttpRequestMessage(HttpMethod.Post, _requestUri) {
          Content               = JsonContent.Create(requestBody),
          Headers               = {
            Authorization       = new("Bearer", _accessToken)
          }
        };

        // Send request
        using var response      = await _httpClient.SendAsync(request).ConfigureAwait(true);
        var responseBody        = await response.Content.ReadAsStringAsync().ConfigureAwait(true);

        // Report failure, including the response body, for a non-2xx response
        if (!response.IsSuccessStatusCode) {
          return new() {
            IsSuccessful        = false,
            IsSkipped           = false,
            ErrorMessage        = responseBody
          };
        }

        // Report success
        return new() {
          IsSuccessful          = true,
          IsSkipped             = false,
          HubSpotContactId      = TryGetContactId(responseBody)
        };

      }

      // Handle any exceptions
      #pragma warning disable CA1031 // This deliberately swallows the exception to avoid disrupting the rest of form processing
      catch (Exception ex) {
        return new() {
          IsSuccessful          = false,
          IsSkipped             = false,
          ErrorMessage          = ex.Message
        };
      }
      #pragma warning restore CA1031

    }

    /*==========================================================================================================================
    | METHOD: TRY GET CONTACT ID (PRIVATE)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Attempts to extract the HubSpot contact identifier from a successful batch "upsert" response.
    /// </summary>
    /// <param name="responseBody">The raw JSON response body returned by HubSpot.</param>
    /// <returns>The contact identifier, or <c>null</c> if it couldn't be determined.</returns>
    private static string TryGetContactId(string responseBody) {
      try {
        using var document      = JsonDocument.Parse(responseBody);
        if (
          document.RootElement.TryGetProperty("results", out var results) &&
          results.ValueKind is JsonValueKind.Array &&
          results.GetArrayLength() > 0 &&
          results[0].TryGetProperty("id", out var id)
        ) {
          return id.GetString();
        }
      }
      catch (JsonException) {
        // Contact ID is diagnostic only; an unparsable response shouldn't fail an otherwise successful sync
      }
      return null;
    }

  } //Class
} //Namespace