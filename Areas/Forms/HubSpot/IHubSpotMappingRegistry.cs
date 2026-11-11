/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/

namespace GoldSim.Web.Areas.Forms.HubSpot {

  /*============================================================================================================================
  | INTERFACE: HUBSPOT MAPPING REGISTRY
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Defines a service responsible for loading and providing access to <see cref="HubSpotFormManifest"/> instances, keyed by
  ///   the form identifier.
  /// </summary>
  public interface IHubSpotMappingRegistry {

    /*==========================================================================================================================
    | METHOD: TRY GET MANIFEST
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Attempts to retrieve the <see cref="HubSpotFormManifest"/> associated with the given <paramref name="formIdentifier"
    ///   />.
    /// </summary>
    /// <param name="formIdentifier">The identifier of the form to retrieve a manifest for (e.g., <c>TrialForm</c>).</param>
    /// <param name="manifest">
    ///   Contains the matching <see cref="HubSpotFormManifest"/>, if one was found; otherwise, <c>null</c>.
    /// </param>
    /// <returns>
    ///   <c>true</c> if a manifest was found for the given <paramref name="formIdentifier"/>; otherwise, <c>false</c>.
    /// </returns>
    bool TryGetManifest(string formIdentifier, out HubSpotFormManifest manifest);

  } //Interface
} //Namespace