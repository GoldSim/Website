<%@ Page Language="C#" Title="Request Quote" MasterPageFile="/Forms/Common/Templates/Forms.Layout.Master" %>

<%@ MasterType  VirtualPath="/Forms/Common/Templates/Forms.Layout.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<Script RunAt="Server">
/*===========================================================================================================================
| FORM: QUOTE REQUEST
|
| Author        Katherine Trunkey, Ignia LLC (Katie@ignia.com)
| Client        GoldSim
| Project       Site Relaunch
|
| Purpose       Provides form template for GoldSim software quote request.
|
>============================================================================================================================
| Revisions     Date        Author                      Comments
| - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
|               11.24.08    Jeremy Caney                Initial version template.
|               07.27.10    Katherine Trunkey           Adapted for form template.
|               08.02.10    Jeremy Caney                Wired up event-handler for processing conditional email.
|               MM.DD.YY    FName LName                 Description
\--------------------------------------------------------------------------------------------------------------------------*/

/*=========================================================================================================================
| DECLARE PUBLIC PROPERTIES
>==========================================================================================================================
| Declare any public/global variables prior to page initiation; these will be treated as properties in the control
\------------------------------------------------------------------------------------------------------------------------*/

/*===========================================================================================================================
| PAGE LOAD
>============================================================================================================================
| Provide handling for functions that must run prior to page load.  This includes dynamically constructed controls.
\--------------------------------------------------------------------------------------------------------------------------*/
  void Page_Load(Object Src, EventArgs E) {

  /*-------------------------------------------------------------------------------------------------------------------------
  | SET MASTER PROPERTIES
  \------------------------------------------------------------------------------------------------------------------------*/
  //Associated topic for navigation -- sends to Forms.Layout.Master and then on to Page.Layout.master
  //Master.FormTopic            =  Ignia.Topics.TopicRepository.RootTopic.GetTopic(741);
  //Submit button label
    Master.SubmitLabel          = "Send Quote Request";
  //Submission email subject
    Master.EmailSubject         = "GoldSim Quote Form: Product Quote Request";
  //Redirect URL
    Master.SuccessUrl           = "/Topic/755/";

    }
</Script>

<asp:Content ContentPlaceHolderID="Subtitle" runat="server">
  <p class="subtitle">GoldSim Software Quote Request Form</p>
</asp:Content>

<asp:Content ContentPlaceHolderId="Content" runat="Server">

  <p>Please complete the form below so we may better understand your GoldSim software needs. Upon receipt of your request, GoldSim will assemble and contact you with a quote based on your selected product and licensing options.</p>

  <fieldset>
    <legend>Product Selection</legend>
    <div class="grid-x grid-margin-x">

      <%-- SHOW DISCOUNT INFORMATION ON GOLDSIM PRO RESEARCH MULTIPLE LICENSE SELECTION --%>
      <p>Multiple license purchases may be subject to volume discounts.</p>

      <%-- PRODUCT/COMPONENTS SELECTION/CONFIGURATION --%>
      <GoldSimForm:ProductConfiguration ID="ProductConfiguration" ShowQuickStartAddOn="true" RunAt="Server" />

      <%-- ADDITIONAL QUOTE INSTRUCTIONS --%>
      <div class="cell">
        <Ignia:FormField   ID   = "QuoteInstructions"
          LabelName             = "Additional Quote Instructions"
          AccessKey             = "I"
          MaxLength             = "150"
          FieldSize             = "468"
          TextMode              = "MultiLine"
          CssClass              = "BlockLabel TextField"
          SkinId                = "BoxedPairs"
          RunAt                 = "Server"
          />
      </div>

    </div>
  </fieldset>

  <fieldset>
    <legend>Contact Information</legend>
    <div class="grid-x grid-margin-x">

      <%-- NAME BLOCK: FNAME, LNAME --%>
      <GoldSimForm:NameBlock ID="NameBlock" RunAt="Server" />

      <%-- ORGANIZATION --%>
      <div class="cell">
        <GoldSimForm:Organization ID="Organization" RunAt="Server" />
      </div>

      <%-- ADDRESS BLOCK: ADDRESS1, ADDRESS2, CITY, STATE/PROVINCE, ZIP/POSTAL --%>
      <GoldSimForm:AddressBlock ID="AddressBlock" RunAt="Server" />

      <%-- COUNTRY SELECTION --%>
      <div class="cell">
        <GoldSimForm:CountrySelection ID="CountrySelection" RunAt="Server" />
      </div>

      <%-- EMAIL/CONFIRM EMAIL --%>
      <GoldSimForm:Email ID="Email" ShowEmailConfirm="true" RunAt="Server" />

      <%-- PHONE --%>
      <div class="medium-6 cell">
        <GoldSimForm:Phone ID="Phone" RunAt="Server" />
      </div>

      <%-- FAX --%>
      <div class="medium-6 cell">
        <GoldSimForm:Fax ID="Fax" RunAt="Server" />
      </div>

    </div>
  </fieldset>

</asp:Content>