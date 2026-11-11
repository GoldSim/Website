/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using OnTopic;

namespace GoldSim.Web.Areas.Forms.HubSpot {

  /*============================================================================================================================
  | INTERFACE: HUBSPOT PAYLOAD BUILDER
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Defines a service responsible for translating a <see cref="Topic"/> into a collection of HubSpot contact properties,
  ///   according to the field mappings defined by a <see cref="HubSpotFormManifest"/>.
  /// </summary>
  public interface IHubSpotPayloadBuilder {

    /*==========================================================================================================================
    | METHOD: BUILD
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Builds the collection of HubSpot contact properties for the given <paramref name="topic"/>, according to the field
    ///   mappings defined by <paramref name="manifest"/>.
    /// </summary>
    /// <param name="topic">The <see cref="Topic"/> instance to read source attribute values from.</param>
    /// <param name="manifest">The <see cref="HubSpotFormManifest"/> describing how the topic maps to HubSpot properties.
    /// </param>
    /// <returns>A dictionary of HubSpot internal property names to their corresponding values.</returns>
    IReadOnlyDictionary<string, string> Build(Topic topic, HubSpotFormManifest manifest);

  } //Interface
} //Namespace