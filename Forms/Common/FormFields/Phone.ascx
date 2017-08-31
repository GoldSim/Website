<%@ Control Language="C#" ClassName="PhoneField" %>

<Script RunAt="Server">
/*===========================================================================================================================
| COMMON FIELDS TEMPLATE: PHONE
|
| Author:       Katherine Trunkey, Ignia LLC (katie@ignia.com)
| Client        GoldSim
| Project       Site Relaunch | Forms
|
| Purpose       Template control wrapper for commonly grouped address fields
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
  public        String          LabelName               = "Telephone";
  public        String          ValidationGroup         = "";

</Script>

<%-- PHONE --%>
<Ignia:FormField
  ID                    = "Phone"
  LabelName             = "Telephone"
  AccessKey             = "T"
  MaxLength             = "150"
  FieldSize             = "320"
  Required              = "True"
  CssClass              = "TextField"
  SkinId                = "BoxedPairs"
  ValidationGroup       = <%# ValidationGroup %>
  RunAt                 = "Server"
  />