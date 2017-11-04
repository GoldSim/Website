<%@ Control Language="C#" ClassName="AreaOfFocusSelection" %>

<!-- #Include Virtual="/Common/Global/Headers/Form.Headers.inc.aspx" -->

<Script RunAt="Server">
/*===========================================================================================================================
| COMMON FIELDS TEMPLATE: FOCUS AREA
|
| Author:    Katherine Trunkey, Ignia LLC (katie@ignia.com)
| Client     GoldSim
| Project    Site Relaunch | Forms
|
| Purpose :  Template control wrapper for commonly grouped address fields
|
>============================================================================================================================
| Revisions     Date            Author                  Comments
| - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
|               07.28.10        Katherine Trunkey       Initial version template.
\--------------------------------------------------------------------------------------------------------------------------*/

/*===========================================================================================================================
| DECLARE PUBLIC FIELDS
>============================================================================================================================
| Public fields will be exposed as properties to user control
\--------------------------------------------------------------------------------------------------------------------------*/
  public        string          LabelName               = "Field Label Name";
  public        string          ValidationGroup         = "";

</Script>

<%-- AREA OF FOCUS --%>
<div class="medium-6 cell">
  <!-- Focus Area -->
  <label for="AreaOfFocusList" accesskey="F" class="form-field label required" RunAt="Server">*Area of Focus</label>
  <asp:DropDownList ID="AreaOfFocusList" ValidationGroup=<%# ValidationGroup %> RunAt="Server">
    <asp:ListItem>Select one...</asp:ListItem>
    <asp:ListItem Value="Design, System Reliability and Throughput">Design, System Reliability and Throughput</asp:ListItem>
    <asp:ListItem Value="Ecological/Biological">Ecological/Biological</asp:ListItem>
    <asp:ListItem Value="Financial Engineering">Financial Engineering &amp; Treasury Risk</asp:ListItem>
    <asp:ListItem Value="Human Health Risk Assessment">Human Health Risk Assessment</asp:ListItem>
    <asp:ListItem Value="Insurance and Risk Management">Insurance and Risk Management</asp:ListItem>
    <asp:ListItem Value="Mine Water Balance/Waste Management">Mine Water Balance, Waste Management &amp; Closure</asp:ListItem>
    <asp:ListItem Value="Oil, Gas and Energy">Oil, Gas and Energy</asp:ListItem>
    <asp:ListItem Value="Radwaste/Hazwaste">Radioactive &amp; Hazardous Waste Management</asp:ListItem>
    <asp:ListItem Value="Risk, Failure & Vulnerability Analysis">Risk, Failure, and Vulnerability Analysis</asp:ListItem>
    <asp:ListItem Value="Strategic Planning/Business Simulation">Strategic Planning/Business Simulation</asp:ListItem>
    <asp:ListItem Value="Supply Chain/Business Process Modeling">Supply Chain/Business Process Modeling</asp:ListItem>
    <asp:ListItem Value="Water Resources">Water Resources</asp:ListItem>
    <asp:ListItem Value="Other">Other (Please Specify)</asp:ListItem>
  </asp:DropDownList>
  <asp:RequiredFieldValidator ControlToValidate="AreaOfFocusList" InitialValue="Select one..." RunAt="Server" />
  <!-- /Focus Area -->
</div>
<div class="medium-6 cell">
  <%-- OTHER FOCUS SPECIFICATION --%>
  <!-- Other Focus Area -->
  <Ignia:FormField
    ID                  = "FocusOther"
    LabelName           = "Other"
    AccessKey           = "O"
    MaxLength           = "150"
    FieldSize           = "320"
    Required            = "False"
    CssClass            = "TextField"
    SkinId              = "BoxedPairs"
    ValidationGroup     = <%# ValidationGroup %>
    RunAt               = "Server"
    />
  <!-- Other Focus Area -->
</div>
