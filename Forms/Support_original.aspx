<%@ Page Language="C#" Title="GoldSim Support" MasterPageFile="/Forms/Common/Templates/Forms.Layout.Master" %>

<%@ MasterType  VirtualPath="/Forms/Common/Templates/Forms.Layout.Master" %>

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
    Master.FormTopic            =  Ignia.Topics.TopicRepository.RootTopic.GetTopic(756);
  //Submit button label
    Master.SubmitLabel          = "Send Request";
  //Submission email subject
    Master.EmailSubject         = "Support Request";
  //Request recipient
    Master.EmailRecipient       = "support@goldsim.com";
  //Redirect URL
    Master.SuccessUrl           = "/Topic/5/";

    }
</Script>

<asp:Content ContentPlaceHolderId="Content" runat="Server">

  <h2 class="Subtitle">Send a Support Request</h2>
  <p>Use this form to report a problem, suggest a feature, or ask a question about GoldSim sotware.</p>

  <fieldset>
    <legend>Inquiry Information</legend>

    <%-- REQUEST TYPE --%>
    <div class="FieldContainer">
      <label for="FeedbackTypeSelection" RunAt="Server">I want to:</label>
      <asp:DropDownList ID="FeedbackTypeSelection" runat="server">
        <asp:ListItem Value="Problem">Report a problem</asp:ListItem>
        <asp:ListItem Value="Feature">Request a feature</asp:ListItem>
        <asp:ListItem Value="Question">Ask a question</asp:ListItem>
      </asp:DropDownList>
    </div>

    <%-- INQUIRY DESCRIPTION --%>
    <Ignia:FormField   ID = "InquiryDescription"
      LabelName           = "Inquiry Description"
      AccessKey           = "I"
      MaxLength           = "150"
      FieldSize           = "320"
      Required            = "True"
      TextMode            = "MultiLine"
      CssClass            = "TextField"
      SkinId              = "BoxedPairs"
      RunAt               = "Server"
      />

  </fieldset>

  <fieldset>

    <legend>Contact Information</legend>

    <%-- NAME BLOCK: FNAME, LNAME --%>
    <GoldSimForm:NameBlock ID="NameBlock" RunAt="Server" />

    <%-- ORGANIZATION --%>
    <GoldSimForm:Organization ID="Organization" RunAt="Server" />

    <%-- EMAIL --%>
    <GoldSimForm:Email ID="Email" RunAt="Server" />

  </fieldset>

  <fieldset>

    <legend>Additional Information</legend>

    <%-- SOFTWARE VERSION --%>
    <Ignia:FormField   ID = "GoldSimVersion"
      LabelName           = "GoldSim Version"
      AccessKey           = "V"
      MaxLength           = "150"
      FieldSize           = "320"
      Required            = "True"
      CssClass            = "TextField"
      SkinId              = "BoxedPairs"
      RunAt               = "Server"
      />

    <%-- INSTALLED MODULES SELECTION --%>
    <div class="FieldContainer Checkboxes">
      <label For="InstalledModulesSelection" Title="To view installed modules, select ModelExtension Modules from the Main menu" RunAt="Server">Installed Modules</label>
      <asp:CheckBoxList ID="InstalledModulesSelection" RepeatLayout="Flow" RepeatDirection="Vertical" RunAt="server">
        <asp:ListItem Value="Academic">Academic</asp:ListItem>
        <asp:ListItem Value="Distributed Processing">Distributed Processing</asp:ListItem>
        <asp:ListItem Value="Distributed Processing Plus">Distributed Processing Plus</asp:ListItem>
        <asp:ListItem Value="Dashboard Authoring">Dashboard Authoring</asp:ListItem>
        <asp:ListItem Value="Radionuclide Transport">Radionuclide Transport</asp:ListItem>
        <asp:ListItem Value="Contaminant Transport">Contaminant Transport</asp:ListItem>
        <asp:ListItem Value="Evaluation">Evaluation</asp:ListItem>
        <asp:ListItem Value="Reliability">Reliability</asp:ListItem>
      </asp:CheckBoxList>
    </div>

    <%-- OPERATING SYSTEM --%>
    <Ignia:FormField   ID = "OperatingSystem"
      LabelName           = "Operating System"
      AccessKey           = "O"
      MaxLength           = "150"
      FieldSize           = "320"
      Required            = "True"
      CssClass            = "TextField"
      SkinId              = "BoxedPairs"
      RunAt               = "Server"
      />

    <%-- PROCESSOR --%>
    <Ignia:FormField   ID = "ProcessorSpeed"
      LabelName           = "Processor (GHz)"
      AccessKey           = "P"
      MaxLength           = "150"
      FieldSize           = "320"
      CssClass            = "TextField"
      SkinId              = "BoxedPairs"
      RunAt               = "Server"
      />

    <%-- MEMORY --%>
    <Ignia:FormField   ID = "Memory"
      LabelName           = "Memory (MB)"
      AccessKey           = "M"
      MaxLength           = "150"
      FieldSize           = "320"
      CssClass            = "TextField"
      SkinId              = "BoxedPairs"
      RunAt               = "Server"
      />

    <%-- ANTI-VIRUS SOFTWARE --%>
    <Ignia:FormField   ID = "AntiVirus"
      LabelName           = "Anti-Virus Software"
      AccessKey           = "V"
      MaxLength           = "150"
      FieldSize           = "320"
      CssClass            = "TextField"
      SkinId              = "BoxedPairs"
      RunAt               = "Server"
      />

  </fieldset>

</asp:Content>