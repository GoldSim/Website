/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/

namespace GoldSim.Web.Areas.Forms.HubSpot {

  /*============================================================================================================================
  | MODEL: HUBSPOT FORM MANIFEST
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly typed representation of a HubSpot form manifest, deserialized from a mapping file, describing how a
  ///   single form's binding model maps to a HubSpot contact.
  /// </summary>
  public sealed record HubSpotFormManifest {

    /*==========================================================================================================================
    | PROPERTY: FORM IDENTIFIER
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets the identifier of the form that this manifest applies to (e.g., <c>TrialForm</c>).
    /// </summary>
    public required string      FormIdentifier                  { get; init; }

    /*==========================================================================================================================
    | PROPERTY: FIELDS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets the collection of field mappings that make up this manifest.
    /// </summary>
    public required IReadOnlyList<HubSpotFieldMapping> Fields   { get; init; }

    /*==========================================================================================================================
    | PROPERTY: UNIQUE KEY FIELD
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets the field mapping used to determine whether a HubSpot contact should be created or updated.
    /// </summary>
    public HubSpotFieldMapping  UniqueKeyField                  => Fields.Single(f => f.IsUniqueKey);

  } //Class
} //Namespace