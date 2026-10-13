/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using OnTopic;

namespace GoldSim.Web.Areas.Forms.HubSpot {

  /*============================================================================================================================
  | CLASS: HUBSPOT PAYLOAD BUILDER
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Default implementation of <see cref="IHubSpotPayloadBuilder"/>, which translates a <see cref="Topic"/> into HubSpot
  ///   contact properties according to a <see cref="HubSpotFormManifest"/>.
  /// </summary>
  /// <remarks>
  ///   A manifest that references a missing required value, an unrecognized <see cref="HubSpotFieldMapping.HubSpotType"/>, or
  ///   a value with no corresponding entry in <see cref="HubSpotFieldMapping.ValueMap"/> represents a design-time configuration
  ///   error, not a transient runtime failure; <see cref="Build(Topic, HubSpotFormManifest)"/> throws immediately in those
  ///   cases so the error is apparent while the mapping is being authored.
  /// </remarks>
  public sealed class HubSpotPayloadBuilder : IHubSpotPayloadBuilder {

    /*==========================================================================================================================
    | METHOD: BUILD
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <inheritdoc/>
    public IReadOnlyDictionary<string, string> Build(Topic topic, HubSpotFormManifest manifest) {

      // Validate parameters
      Contract.Requires(topic, nameof(topic));
      Contract.Requires(manifest, nameof(manifest));

      // Establish output payload
      var payload               = new Dictionary<string, string>();

      // Map each field defined by the manifest
      foreach (var field in manifest.Fields) {

        // Resolve source value from the field's constant value, flag attributes, or source attribute
        var rawValue            = field.ConstantValue?? (
          field.SourceFlagMap is not null?
            GetFlagValues(topic, field) :
            topic.Attributes.GetValue(field.SourceAttribute)
          );

        // Skip empty optional fields; throw for empty required fields
        if (string.IsNullOrEmpty(rawValue)) {
          if (field.IsRequired) {
            throw new InvalidOperationException(
              $"The '{field.HubSpotProperty}' HubSpot field is required, but no value was found for the topic attribute " +
              $"'{field.SourceAttribute}'."
            );
          }
          continue;
        }

        // Convert the value according to the field's HubSpot type
        var value               = field.HubSpotType switch {
          "string"              => rawValue,
          "enumeration"         => MapValue(field, rawValue),
          "enumerationSet" when field.SourceFlagMap is not null => rawValue,
          "enumerationSet"      => MapValueSet(field, rawValue),
          _                     => throw new InvalidOperationException(
                                     $"The '{field.HubSpotProperty}' HubSpot field specifies an unrecognized HubSpotType of " +
                                     $"'{field.HubSpotType}'."
                                   )
        };

      }

      // Return payload
      return payload;

    }

    /*==========================================================================================================================
    | METHOD: MAP VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Translates a single source value into its corresponding HubSpot option value, according to the given <paramref name=
    ///   "field"/>'s <see cref="HubSpotFieldMapping.ValueMap"/>.
    /// </summary>
    /// <param name="field">The <see cref="HubSpotFieldMapping"/> that <paramref name="value"/> was sourced from.</param>
    /// <param name="value">The source value to translate.</param>
    /// <returns>
    ///   The mapped HubSpot option value, or <paramref name="value"/> unchanged if <paramref name="field"/> doesn't define a
    ///   <see cref="HubSpotFieldMapping.ValueMap"/>.
    /// </returns>
    private static string MapValue(HubSpotFieldMapping field, string value) {

      // Return the value unchanged if the field doesn't define a value map
      if (field.ValueMap is null) {
        return value;
      }

      // Return mapped value, if found
      if (field.ValueMap.TryGetValue(value, out var mappedValue)) {
        return mappedValue;
      }

      // Report an unmappable value as a manifest configuration error
      throw new InvalidOperationException(
        $"The '{field.HubSpotProperty}' HubSpot field's ValueMap has no entry for source value '{value}'."
      );

    }

    /*==========================================================================================================================
    | METHOD: MAP VALUE SET
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Translates a delimited source value into HubSpot's semicolon-delimited multi-checkbox format, mapping each token
    ///   individually via <see cref="MapValue(HubSpotFieldMapping, string)"/>.
    /// </summary>
    /// <param name="field">The <see cref="HubSpotFieldMapping"/> that <paramref name="value"/> was sourced from.</param>
    /// <param name="value">
    ///   The source value to translate, with individual tokens separated by <see cref="HubSpotFieldMapping.MultiValueDelimiter"
    ///   />.
    /// </param>
    /// <returns>A semicolon-delimited string of mapped HubSpot option values.</returns>
    private static string MapValueSet(HubSpotFieldMapping field, string value) {

      // Split source value into individual tokens
      var tokens                = value.Split(
        HubSpotFieldMapping.MultiValueDelimiter,
        StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries
      );

      // Map each token and join using HubSpot's semicolon delimiter
      return string.Join(";", tokens.Select(token => MapValue(field, token)));

    }

    /*==========================================================================================================================
    | METHOD: GET FLAG VALUES
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Collects the HubSpot option value for each boolean topic attribute in the given <paramref name="field"/>'s <see cref=
    ///   "HubSpotFieldMapping.SourceFlagMap"/> that's set on the <paramref name="topic"/>.
    /// </summary>
    /// <remarks>
    ///   Option values are joined directly with HubSpot's delimiter, instead of being routed through <see cref=
    ///   "MapValueSet(HubSpotFieldMapping, string)"/>, since an option value may itself contain a <see cref=
    ///   "HubSpotFieldMapping.MultiValueDelimiter"/>.
    /// </remarks>
    /// <param name="topic">The <see cref="Topic"/> to read the boolean attributes from.</param>
    /// <param name="field">The <see cref="HubSpotFieldMapping"/> defining the flag attributes.</param>
    /// <returns>
    ///   A semicolon-delimited string of HubSpot option values, or an empty string if none of the attributes are set.
    /// </returns>
    private static string GetFlagValues(Topic topic, HubSpotFieldMapping field) {

      // Select the option value for each attribute that's set
      var optionValues          = field.SourceFlagMap
        .Where(flag => topic.Attributes.GetBoolean(flag.Key))
        .Select(flag => flag.Value);

      // Join using HubSpot's semicolon delimiter
      return string.Join(";", optionValues);

    }

  } //Class
} //Namespace