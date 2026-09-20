/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/

namespace GoldSim.Web.Areas.Forms.HubSpot {

  /*============================================================================================================================
  | MODEL: HUBSPOT FIELD MAPPING
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly typed representation of a single field mapping, deserialized from a HubSpot form manifest.
  /// </summary>
  public sealed record HubSpotFieldMapping {

    /*==========================================================================================================================
    | CONSTANT: MULTI-VALUE DELIMITER
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets the delimiter used to separate values within a source topic attribute when <see cref="HubSpotType"/> is <c>
    ///   enumerationSet</c>; converted to HubSpot's semicolon delimiter on write.
    /// </summary>
    public const string         MultiValueDelimiter             = ",";

    /*==========================================================================================================================
    | PROPERTY: SOURCE ATTRIBUTE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets the topic attribute key to read the field's value from. Mutually exclusive with <see cref="ConstantValue"/>; null
    ///   when <see cref="ConstantValue"/> is set.
    /// </summary>
    public string               SourceAttribute                 { get; init; }

    /*==========================================================================================================================
    | PROPERTY: CONSTANT VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets a fixed value to be written on every submission, bypassing the topic entirely. Mutually exclusive with <see cref=
    ///   "SourceAttribute"/>; null when <see cref="SourceAttribute"/> is set.
    /// </summary>
    public string               ConstantValue                   { get; init; }

    /*==========================================================================================================================
    | PROPERTY: HUBSPOT PROPERTY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets the internal HubSpot property name that this field maps to.
    /// </summary>
    public required string      HubSpotProperty                 { get; init; }

    /*==========================================================================================================================
    | PROPERTY: HUBSPOT TYPE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets the HubSpot-side field type: <c>string</c>, <c>enumeration</c> (single-select), or <c>enumerationSet</c>
    ///   (multi-checkbox).
    /// </summary>
    public required string      HubSpotType                     { get; init; }

    /*==========================================================================================================================
    | PROPERTY: IS REQUIRED?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets whether the field must be populated. If empty at build time, a required field causes the payload construction to
    ///   throw an exception before any HubSpot API calls are made.
    /// </summary>
    public bool                 IsRequired                      { get; init; }

    /*==========================================================================================================================
    | PROPERTY: IS UNIQUE KEY?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets whether this field is used to determine whether a HubSpot contact should be created or updated. Exactly one field
    ///   per manifest should set this to <c>true</c>.
    /// </summary>
    public bool                 IsUniqueKey                     { get; init; }

    /*==========================================================================================================================
    | PROPERTY: VALUE MAP
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets an optional mapping of source values to HubSpot internal option values. Omitted when the source and HubSpot
    ///   values already match.
    /// </summary>
    public IReadOnlyDictionary<string, string> ValueMap         { get; init; }

  } //Class
} //Namespace