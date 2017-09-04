<%@ Control Language="C#" ClassName="NameBlock" %>

<!-- #Include Virtual="/Common/Global/Headers/Form.Headers.inc.aspx" -->

<Script RunAt="Server">
/*===========================================================================================================================
| COMMON FIELDS TEMPLATE: FULL NAME FIELDS
|
| Author:       Katherine Trunkey, Ignia LLC (katie@ignia.com)
| Client        GoldSim
| Project       Site Relaunch | Forms
|
| Purpose       Template control wrapper for commonly grouped address fields
|
>============================================================================================================================
| Revisions     Date            Author                  Comments
|--------------------------------------------------------------------------------------------------------------------------
|               07.28.10        Katherine Trunkey       Initial version template.
\--------------------------------------------------------------------------------------------------------------------------*/

/*===========================================================================================================================
| DECLARE PUBLIC FIELDS
>============================================================================================================================
| Public fields will be exposed as properties to user control
\--------------------------------------------------------------------------------------------------------------------------*/
  public        string          ValidationGroup         = "";

</Script>

<!-- First Name -->
<div class="medium-6 cell">
  <Ignia:FormField
    ID                    = "FirstName"
    LabelName             = "*First Name"
    AccessKey             = "F"
    MaxLength             = "150"
    FieldSize             = "320"
    Required              = "True"
    CssClass              = "TextField"
    SkinId                = "BoxedPairs"
    ValidationGroup       = <%# ValidationGroup %>
    RunAt                 = "Server"
    />
</div>
<!-- /First Name -->

<!-- Last Name -->
<div class="medium-6 cell">
  <Ignia:FormField
    ID                    = "LastName"
    LabelName             = "*Last Name"
    AccessKey             = "L"
    MaxLength             = "150"
    FieldSize             = "320"
    Required              = "True"
    CssClass              = "TextField"
    SkinId                = "BoxedPairs"
    ValidationGroup       = <%# ValidationGroup %>
    RunAt                 = "Server"
    />
</div>
<!-- /Last Name -->