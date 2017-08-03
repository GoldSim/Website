<%@ Control ClassName="FaxField" %>

<!-- #Include Virtual="/Common/Global/Headers/Form.Headers.inc.aspx" -->

<Script RunAt="Server">
/*===========================================================================================================================
| COMMON FIELD TEMPLATE: FAX
|
| Author:    Katherine Trunkey, Ignia LLC (Jeremy@ignia.com)
| Client     GoldSim
| Project    Site Relaunch | Forms
|
| Purpose :  Template control wrapper for Fax textbox field
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
  public        String          LabelName               = "Fax";
  public        String          AccessKey               = "F";
  public        String          ValidationGroup         = "";

</Script>

<%-- FAX --%>
<Ignia:FormField
  ID                    = "Fax"
  LabelName             = "<%# LabelName %>"
  AccessKey             = "<%# AccessKey %>"
  MaxLength             = "150"
  FieldSize             = "320"
  CssClass              = "TextField"
  SkinId                = "BoxedPairs"
  ValidationGroup       = <%# ValidationGroup %>
  RunAt                 = "Server"
  />