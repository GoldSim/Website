/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/

namespace GoldSim.Web.Areas.Forms.HubSpot {

  /*============================================================================================================================
  | MODEL: HUBSPOT SYNC RESULT
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides the outcome of a single HubSpot contact sync attempt.
  /// </summary>
  /// <remarks>
  ///   The HubSpot sync should never throw an exception, since the form will otherwise still e.g., send an email to GoldSim
  ///   and/or write a topic to OnTopic, This type instead conveys success or failure, and any diagnostic information, so
  ///   callers can log or otherwise handle failures without interrupting the form submission.
  /// </remarks>
  public sealed record HubSpotSyncResult {

    /*==========================================================================================================================
    | PROPERTY: IS SUCCESSFUL?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets whether the sync attempt completed successfully.
    /// </summary>
    public required bool        IsSuccessful                    { get; init; }

    /*==========================================================================================================================
    | PROPERTY: HUBSPOT CONTACT ID
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets the identifier of the HubSpot contact that was created or updated. Set only when <see cref="IsSuccessful"/> is
    ///   <c>true</c>.
    /// </summary>
    public string               HubSpotContactId                { get; init; }

    /*==========================================================================================================================
    | PROPERTY: ERROR MESSAGE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets a diagnostic message describing why the sync attempt failed. Set only when <see cref="IsSuccessful"/> is
    ///   <c>false</c>.
    /// </summary>
    public string               ErrorMessage                    { get; init; }

  } //Class
} //Namespace