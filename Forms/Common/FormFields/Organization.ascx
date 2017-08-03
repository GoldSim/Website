<%@ Control ClassName="OrganizationField" %>

<Script RunAt="Server">
/*===========================================================================================================================
| COMMON FIELDS TEMPLATE: CATEGORY
|
| Author        Katherine Trunkey, Ignia LLC (katie@ignia.com)
| Client        GoldSim
| Project       Site Relaunch | Forms
|
| Purpose       Template control wrapper for organization field
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
  public        String          LabelName               = "Organization";
  public        String          AccessKey               = "O";
  public        String          ValidationGroup         = "";

</Script>

<%-- ORGANIZATION --%>
<Ignia:FormField
  ID                    = "Organization"
  LabelName             = <%# LabelName %>
  AccessKey             = <%# AccessKey %>
  MaxLength             = "150"
  FieldSize             = "320"
  Required              = "True"
  CssClass              = "TextField"
  SkinId                = "BoxedPairs"
  ValidationGroup       = <%# ValidationGroup %>
  RunAt                 = "Server"
  />
