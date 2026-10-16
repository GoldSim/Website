/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using OnTopic;

namespace GoldSim.Web.Areas.Forms.HubSpot {

  /*============================================================================================================================
  | INTERFACE: HUBSPOT CONTACT SYNC SERVICE
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Defines a service responsible for synchronizing contact data derived from a <see cref="Topic"/> to HubSpot, creating or
  ///   updating a contact according to the field mappings defined by a <see cref="HubSpotFormManifest"/>.
  /// </summary>
  /// <remarks>
  ///   Implementations must never throw exceptions back to the caller; failures should be reported via the returned <see cref=
  ///   "HubSpotSyncResult"/> instead, so that a HubSpot outage or misconfiguration never blocks a form submission.
  /// </remarks>
  public interface IHubSpotContactSyncService {

    /*==========================================================================================================================
    | METHOD: SYNC (ASYNC)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Creates or updates a HubSpot contact from the given <paramref name="topic"/>, according to the field mappings defined
    ///   by <paramref name="manifest"/>.
    /// </summary>
    /// <param name="topic">The <see cref="Topic"/> instance to read source attribute values from.</param>
    /// <param name="manifest">The <see cref="HubSpotFormManifest"/> describing how the topic maps to HubSpot properties.
    /// </param>
    /// <returns>A <see cref="HubSpotSyncResult"/> describing the outcome of the sync attempt.</returns>
    Task<HubSpotSyncResult> SyncAsync(Topic topic, HubSpotFormManifest manifest);

  } //Interface
} //Namespace