<%@ Control Language="C#" ClassName="ModuleInterestSelection" %>

<!-- #Include Virtual="/Common/Global/Headers/Form.Headers.inc.aspx" -->

<Script RunAt="Server">
/*===========================================================================================================================
| COMMON FIELDS TEMPLATE: EMAIL
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
  public        String          LabelName               = "Field Label Name";
  public        String          ValidationGroup         = "";

</Script>

<%-- MODULE INTEREST CHECKBOXES --%>
<div class="FieldContainer Checkboxes">
  <label For="ModuleInterestList" RunAt="Server">I am also interested in:</label>
  <asp:CheckBox ID="CT" Value="Ignore" ValidationGroup=<%# ValidationGroup %> RunAt="Server" /> Contaminant Transport Module
  <asp:CheckBox ID="RL" Value="Ignore" ValidationGroup=<%# ValidationGroup %> RunAt="Server" /> Reliability Module

  <asp:CheckBoxList ID="ModuleInterestList" RepeatLayout="Flow" ValidationGroup=<%# ValidationGroup %> Visible="False" RunAt="server">
    <asp:ListItem Value="Contaminant">Contaminant Transport Module</asp:ListItem>
    <asp:ListItem Value="Reliability">Reliability Module</asp:ListItem>
  </asp:CheckBoxList>
</div>
