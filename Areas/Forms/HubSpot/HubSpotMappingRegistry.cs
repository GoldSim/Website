/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using System.Text.Json;

namespace GoldSim.Web.Areas.Forms.HubSpot {

  /*============================================================================================================================
  | CLASS: HUBSPOT MAPPING REGISTRY
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Default implementation of <see cref="IHubSpotMappingRegistry"/>, which loads and validates <see cref=
  ///   "HubSpotFormManifest"/> instances from the JSON files under the <c>Mappings/</c> directory at startup.
  /// </summary>
  /// <remarks>
  ///   Manifests are loaded and validated once, up front, so that a malformed or misconfigured mapping file is reported as soon
  ///   as the application starts, rather than on the first form submission that happens to use it.
  /// </remarks>
  public sealed class HubSpotMappingRegistry : IHubSpotMappingRegistry {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/

    private readonly            Dictionary<string, HubSpotFormManifest> _manifests;

    private static readonly     JsonSerializerOptions           _jsonOptions                    = new() {
      PropertyNameCaseInsensitive = true
    };

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Establishes a new instance of a <see cref="HubSpotMappingRegistry"/>, loading and validating every manifest under the
    ///   <c>Mappings/</c> folder.
    /// </summary>
    /// <param name="webHostEnvironment">
    ///   The <see cref="IWebHostEnvironment"/> used to resolve the manifest directory relative to the content root.
    /// </param>
    public HubSpotMappingRegistry(IWebHostEnvironment webHostEnvironment) {

      // Validate parameters
      Contract.Requires(webHostEnvironment, nameof(webHostEnvironment));

      // Establish manifest directory
      var mappingsPath          = Path.Combine(webHostEnvironment.ContentRootPath, "Areas", "Forms", "HubSpot", "Mappings");

      // Load and validate each manifest file, if any exist
      var manifests             = new Dictionary<string, HubSpotFormManifest>();

      if (Directory.Exists(mappingsPath)) {
        foreach (var filePath in Directory.EnumerateFiles(mappingsPath, "*.json")) {
          var manifest          = LoadManifest(filePath);
          if (!manifests.TryAdd(manifest.FormIdentifier, manifest)) {
            throw new InvalidOperationException(
              $"The FormIdentifier '{manifest.FormIdentifier}' is defined by more than one manifest under '{mappingsPath}'."
            );
          }
        }
      }

      // Memoize the result
      _manifests                = manifests;

    }

    /*==========================================================================================================================
    | METHOD: TRY GET MANIFEST
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <inheritdoc/>
    public bool TryGetManifest(string formIdentifier, out HubSpotFormManifest manifest) {

      // Validate parameters
      Contract.Requires(formIdentifier, nameof(formIdentifier));

      // Return manifest, if found
      return _manifests.TryGetValue(formIdentifier, out manifest);

    }

    /*==========================================================================================================================
    | METHOD: LOAD MANIFEST (PRIVATE)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Deserializes and validates the <see cref="HubSpotFormManifest"/> at the given <paramref name="filePath"/>.
    /// </summary>
    /// <param name="filePath">The path of the manifest file to load.</param>
    /// <returns>The deserialized and validated <see cref="HubSpotFormManifest"/>.</returns>
    private static HubSpotFormManifest LoadManifest(string filePath) {

      // Deserialize manifest
      var json                  = File.ReadAllText(filePath);
      var manifest              = JsonSerializer.Deserialize<HubSpotFormManifest>(json, _jsonOptions)??
        throw new InvalidOperationException($"The manifest at '{filePath}' deserialized to a null value.");

      // Validate manifest
      ValidateManifest(manifest, filePath);

      // Return manifest
      return manifest;

    }

    /*==========================================================================================================================
    | METHOD: VALIDATE MANIFEST (PRIVATE)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Validates constraints on a <see cref="HubSpotFormManifest"/> that can't be expressed through required members alone.
    /// </summary>
    /// <param name="manifest">The <see cref="HubSpotFormManifest"/> to validate.</param>
    /// <param name="filePath">The path of the manifest file that <paramref name="manifest"/> was loaded from.</param>
    private static void ValidateManifest(HubSpotFormManifest manifest, string filePath) {

      // Ensure exactly one field is marked as the unique key
      Contract.Requires(
        manifest.Fields.Count(f => f.IsUniqueKey) == 1,
        $"The manifest at '{filePath}' must have exactly one field with IsUniqueKey set to true."
      );

      // Ensure each field maps to exactly one of either SourceAttribute or ConstantValue
      foreach (var field in manifest.Fields) {
        Contract.Requires(
          field.SourceAttribute is null != field.ConstantValue is null,
          $"The '{field.HubSpotProperty}' field in the manifest at '{filePath}' must set exactly one of SourceAttribute or " +
          $"ConstantValue."
        );
      }

    }

  } //Class
} //Namespace