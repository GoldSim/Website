<%@ Control Language="C#" ClassName="ModuleInterestSelection" %>

<!-- #Include Virtual="/Common/Global/Headers/Form.Headers.inc.aspx" -->

<Script RunAt="Server">
/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/

  /*============================================================================================================================
  | PUBLIC MEMBERS
  \---------------------------------------------------------------------------------------------------------------------------*/
  public        String          LabelName               = "Field Label Name";
  public        String          ValidationGroup         = "";

</Script>

<!-- Module Interest -->
<div class="cell" style="margin-bottom: 1rem;">
  <label For="ModuleInterestList" RunAt="Server">I am also interested in:</label>
  <div class="checkbox">
    <asp:CheckBox ID="CT" ClientIDMode="Static" ValidationGroup=<%# ValidationGroup %> RunAt="Server" />
    <label for="CT">Contaminant Transport Module</label>
  </div>
  <div class="checkbox">
    <asp:CheckBox ID="RL" ClientIDMode="Static" ValidationGroup=<%# ValidationGroup %> RunAt="Server" />
    <label for="RL">Reliability Module</label>
  </div>
</div>
<!-- /Module Interest -->
