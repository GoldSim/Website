<%@ Page Language="C#" Title="GoldSim Webinar Signup" MasterPageFile="/Forms/Common/Templates/Forms.Layout.Master" %>

<%@ MasterType  VirtualPath="/Forms/Common/Templates/Forms.Layout.Master" %>

<Script RunAt="Server">
/*===========================================================================================================================
| FORM: GOLDSIM WEBINAR REGISTRATION
|
| Author        Katherine Trunkey, Ignia LLC (Katie@ignia.com)
| Client        GoldSim
| Project       Site Relaunch
|
| Purpose       Provides form template for GoldSim webinar registration.
|
>============================================================================================================================
| Revisions     Date        Author                      Comments
| - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
|               11.24.08    Jeremy Caney                Initial version template.
|               07.27.10    Katherine Trunkey           Adapted for form template.
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
    Master.FormTopic            =  Ignia.Topics.TopicRepository.RootTopic.GetTopic(777);
  //Submit button label
    Master.SubmitLabel          = "Submit Registration";
  //Submission email subject
    Master.EmailSubject         = "Webinar(s) Registration Request";
    Master.EmailSender = ((IgniaFormField)Email.FindControl("Email")).Value;
  //Request recipient
    Master.EmailRecipient       = "support@goldsim.com";
  //Redirect URL
    Master.SuccessUrl           = "/Topic/846/";
    
    }
</script>

<asp:Content ContentPlaceHolderId="Content" runat="Server">

  <h2 class="Subtitle">Webinar Registration</h2>
  <p>Register for the webinar by filling out the form below.</p>
  <ul>
    <li>
      <h4><a href="http://www.goldsim.com/Web/Resources/Webinars/#water"><strong>Applications in Probabilistic Simulation</strong></a></h4>
      <p><b>Date</b>: Thursday, May 26th, 2011  <br/>
         <b>Times</b>: 8:00 a.m. and 4:00 p.m. PST (-8 hours GMT)</p>
      <p>This webinar will introduce Monte Carlo simulation and showcase examples in GoldSim.</p>
    </li>
  </ul>

  <br/>

  <fieldset>

    <%-- NAME BLOCK: FNAME, LNAME --%>
    <GoldSimForm:NameBlock ID="NameBlock" RunAt="Server" />

    <%-- ORGANIZATION --%>
    <GoldSimForm:Organization ID="Organization" RunAt="Server" />
      
    <%-- EMAIL --%>
    <GoldSimForm:Email ID="Email" RunAt="Server" />
	
    <div class="FieldContainer Checkboxes">
	
      <%-- EVENT SELECTION --%>
      <label For="WebinarEventSelection" RunAt="Server">Which webinar(s) would you like to attend?</label>
      <asp:CheckBoxList ID="WebinarEventSelection" RepeatLayout="Flow" RepeatDirection="Vertical" RunAt="server">
		<asp:ListItem Value=" Water-5.26.2011-8am"><strong>Applications in Probabilistic Simulation</strong>: May 26, 2011, 8:00 a.m. PST </asp:ListItem>
      </asp:CheckBoxList>
    </div>

  </fieldset>  


</asp:Content>