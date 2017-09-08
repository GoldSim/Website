<%@ Control Language="C#" ClassName="EmailField" %>

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
  public        bool            ShowEmailConfirm        = false;
  public        bool            SplitLayout             = false;

/*===========================================================================================================================
| DECLARE PRIVATE MEMBER VARIABLES
\--------------------------------------------------------------------------------------------------------------------------*/
  private       string          _emailControlID         = "Email";

/*===========================================================================================================================
| PAGE LOAD
>============================================================================================================================
| Provide handling for functions that must run prior to page load.  This includes dynamically constructed controls.
\--------------------------------------------------------------------------------------------------------------------------*/
  void Page_Load(Object Src, EventArgs E) {

  //_emailControlID     = ((IgniaFormField)Email.FindControl("Email")).ClientID;

    }

</Script>

<%-- EMAIL --%>
<div class="<%= ((ShowEmailConfirm || SplitLayout) ? "medium-6 " : "") %>cell">
  <Ignia:FormField ID   = "Email"
    LabelName           = "*Email"
    AccessKey           = "E"
    TextMode            = "Email"
    MaxLength           = "150"
    FieldSize           = "320"
    Required            = "True"
    ValidateEmail       = "True"
    CssClass            = "TextField"
    SkinId              = "BoxedPairs"
    ValidationGroup     = <%# ValidationGroup %>
    RunAt               = "Server"
    />
</div>
<%-- CONFIRM EMAIL --%>
<div class="<%= ((ShowEmailConfirm || SplitLayout) ? "medium-6 " : "") %>cell"<%= (!ShowEmailConfirm ? " style=\"display: none;\"" : "") %>
  <Ignia:FormField  ID  = "EmailConfirm"
    Visible             = <%# ShowEmailConfirm %>
    LabelName           = "*Confirm Email"
    AccessKey           = "C"
    MaxLength           = "150"
    FieldSize           = "320"
    Required            = "True"
    ValidateEmail       = "True"
    CssClass            = "TextField"
    SkinId              = "BoxedPairs"
    ValidationGroup     = <%# ValidationGroup %>
    RunAt               = "Server"
    />
  <asp:CompareValidator ID= "EmailCompareValidator"
    Visible             = <%# ShowEmailConfirm %>
    ControlToCompare    = "Email:Field"
    ControlToValidate   = "EmailConfirm:Field"
    ErrorMessage        = "The email addresses you entered do not match."
    CssClass            = "error instructions"
    RunAt               = "Server"
    />
</div>