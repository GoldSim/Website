<%@ Control Language="C#" ClassName="ReferralSourceSelection" %>

<!-- #Include Virtual="/Common/Global/Headers/Form.Headers.inc.aspx" -->

<Script RunAt="Server">
/*==============================================================================================================================
| COMMON FIELDS TEMPLATE: REFERRAL SOURCE
|
| Author:       Katherine Trunkey, Ignia LLC (katie@ignia.com)
| Client:       GoldSim
| Project:      GoldSim.com Forms
|
| Purpose:      Template control wrapper for commonly grouped referral source
|
>===============================================================================================================================
| Revisions     Date            Author                  Comments
| - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
|               01.29.18        Katherine Trunkey       Initial version template.
\-----------------------------------------------------------------------------------------------------------------------------*/

/*==============================================================================================================================
| DECLARE PUBLIC FIELDS
>===============================================================================================================================
| Public fields will be exposed as properties to user control
\-----------------------------------------------------------------------------------------------------------------------------*/
public          string          LabelName               = "Field Label Name";
public          string          ValidationGroup         = "";
public          bool            IsSelectionValid        = true;

/*==============================================================================================================================
| VALIDATOR: REFERRAL SOURCE
\-----------------------------------------------------------------------------------------------------------------------------*/
/// <summary>
///   Ensures that the referral source selection, if available, is valid (not set to "Select one...")
/// </summary>
void ReferralSourceValidator(object source, ServerValidateEventArgs args) {
  args.IsValid = (ReferralSelectionList.SelectedIndex > 0);
  if (!args.IsValid) {
    IsSelectionValid                    = false;
    ReferralSelectionList.CssClass      = "form-field required is-invalid-input";
  }
}

</Script>

<!-- Referral Source -->
<div class="medium-6 cell Referral Select">
  <label id="ReferralSourceLabel" for="ReferralSelectionList" accesskey="R" class="form-field label required<%= (!IsSelectionValid? " is-invalid-label" : "") %>">*How did you learn about GoldSim?</label>
  <asp:DropDownList ID="ReferralSelectionList" CssClass="form-field required" RunAt="Server">
    <asp:ListItem Value="">Select one...</asp:ListItem>
    <asp:ListItem Value="Google">Google</asp:ListItem>
    <asp:ListItem Value="Other Search Engine">Other Search Engine</asp:ListItem>
    <asp:ListItem Value="Wikipedia">Wikipedia</asp:ListItem>
    <asp:ListItem Value="Word of Mouth">Word Of Mouth</asp:ListItem>
    <asp:ListItem Value="From a Colleague">From a Colleague</asp:ListItem>
    <asp:ListItem Value="Trade Show">Trade Show</asp:ListItem>
    <asp:ListItem Value="Journal or Advertisement">Journal or Advertisement</asp:ListItem>
    <asp:ListItem Value="Link from another website">Link from another website</asp:ListItem>
    <asp:ListItem Value="Other">Other</asp:ListItem>
  </asp:DropDownList>
  <asp:RequiredFieldValidator
    ControlToValidate   = "ReferralSelectionList"
    RunAt               = "Server"
  />
  <asp:CustomValidator
    OnServerValidate    = "ReferralSourceValidator"
    ErrorMessage        = "Please indicate how you learned about GoldSim."
    Display             = "None"
    RunAt               = "Server"
  />
</div>
<div class="medium-6 cell">
  <Ignia:FormField     ID = "ReferralDetails"
    LabelName             = "Referral Details"
    AccessKey             = "D"
    MaxLength             = "150"
    FieldSize             = "320"
    CssClass              = "TextField"
    SkinId                = "BoxedPairs"
    RunAt                 = "Server"
    />
</div>
<div class="cell">
  <p class="instructions">Please provide additional details for other search engines, journal name, specific trade show, etc.</p>
</div>
<!-- /Referral Source -->
