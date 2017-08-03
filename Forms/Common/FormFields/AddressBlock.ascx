<%@ Control ClassName="AddressBlock" %>

<!-- #Include Virtual="/Common/Global/Headers/Form.Headers.inc.aspx" -->

<Script RunAt="Server">
/*===========================================================================================================================
| COMMON FIELDS TEMPLATE: ADDRESS FIELDS
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
>==========================================================================================================================
| Public fields will be exposed as properties to user control
\--------------------------------------------------------------------------------------------------------------------------*/
  public        String          LabelName               = "Field Label Name";
  public        String          ValidationGroup         = "";

</Script>

<%-- ADDRESS1 --%>
<Ignia:FormField
  ID                    = "Address1"
  LabelName             = "Address Line 1"
  AccessKey             = "A"
  MaxLength             = "150"
  FieldSize             = "320"
  Required              = "True"
  CssClass              = "TextField"
  SkinId                = "BoxedPairs"
  ValidationGroup       = <%# ValidationGroup %>
  RunAt                 = "Server"
  />
<%-- ADDRESS2 --%>
<Ignia:FormField
  ID                    = "Address2"
  LabelName             = "Address Line 2"
  AccessKey             = "2"
  MaxLength             = "150"
  FieldSize             = "320"
  CssClass              = "TextField"
  SkinId                = "BoxedPairs"
  ValidationGroup       = <%# ValidationGroup %>
  RunAt                 = "Server"
  />
<%-- CITY --%>
<Ignia:FormField
  ID                    = "City"
  LabelName             = "City"
  AccessKey             = "C"
  MaxLength             = "150"
  FieldSize             = "320"
  Required              = "True"
  CssClass              = "TextField"
  SkinId                = "BoxedPairs"
  ValidationGroup       = <%# ValidationGroup %>
  RunAt                 = "Server"
  />
<%-- STATE/PROVINCE --%>
<Ignia:FormField
  ID                    = "State"
  LabelName             = "State/Province"
  AccessKey             = "S"
  MaxLength             = "150"
  FieldSize             = "320"
  Required              = "True"
  CssClass              = "TextField"
  SkinId                = "BoxedPairs"
  ValidationGroup       = <%# ValidationGroup %>
  RunAt                 = "Server"
  />
<%-- ZIP/POSTAL --%>
<Ignia:FormField
  ID                    = "Postal"
  LabelName             = "Postal Code"
  AccessKey             = "P"
  MaxLength             = "150"
  FieldSize             = "320"
  Required              = "True"
  CssClass              = "TextField"
  SkinId                = "BoxedPairs"
  ValidationGroup       = <%# ValidationGroup %>
  RunAt                 = "Server"
  />