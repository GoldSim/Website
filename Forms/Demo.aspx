<%@ Page Language="C#" Title="Request Demo or Trial" %>

<%@ MasterType  VirtualPath="/Forms/Common/Templates/Forms.Layout.Master" %>
<%@ Reference   Control="/Common/Global/Controls/FormField.ascx" %>

<Script RunAt="Server">

/*===========================================================================================================================
| FORM: EVALUATION REQUEST
|
| Author        Katherine Trunkey, Ignia LLC (Katie@ignia.com)
| Client        GoldSim
| Project       Site Relaunch
|
| Purpose       Provides form template for GoldSim software evaluation request.
|
>============================================================================================================================
| Revisions     Date        Author                      Comments
| - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
|               11.24.08    Jeremy Caney                Initial version template.
|               07.27.10    Katherine Trunkey           Adapted for form template.
|               08.02.10    Jeremy Caney                Wired up event-handler for processing conditional email.
|               MM.DD.YY    FName LName                 Description
\--------------------------------------------------------------------------------------------------------------------------*/

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
  //Master.FormTopic            =  Ignia.Topics.TopicRepository.RootTopic.GetTopic(460);

  //ContentType for the form Topic
    Master.SaveAsContentType    = "EvaluationRequest";

  //Submit button label
    Master.SubmitLabel          = "Request Evaluation";

  //Custom processing event
    Master.ProcessForm         += ProcessForm;

    }

/*===========================================================================================================================
| VALIDATOR: EVALUATION METHOD
>============================================================================================================================
| Ensures that at least one of the evaluation methods is selected.
\--------------------------------------------------------------------------------------------------------------------------*/
  void EvaluationMethodValidator(object source, ServerValidateEventArgs args) {
    args.IsValid = !String.IsNullOrEmpty(EvaluationTypeList.SelectedValue);
    }

/*===========================================================================================================================
| PROCESS FORM
>============================================================================================================================
| Override default form processing to provide conditional email support.  Includes a manual redirect to prevent default
| processing of the form since the evaluation requires a highly customized experience.
\--------------------------------------------------------------------------------------------------------------------------*/
  public void ProcessForm(Object sender, CommandEventArgs args) {

  /*-------------------------------------------------------------------------------------------------------------------------
  | DEFINE VARIABLES
  \------------------------------------------------------------------------------------------------------------------------*/
    bool        isTrial         = EvaluationTypeList.Items.FindByValue("Trial").Selected;
    bool        isDemo          = EvaluationTypeList.Items.FindByValue("Demo").Selected;
    string      email           = ((IgniaFormField)Email.FindControl("Email")).Value;

  /*-------------------------------------------------------------------------------------------------------------------------
  | SEND TRIAL EMAIL
  \------------------------------------------------------------------------------------------------------------------------*/
    if (isTrial) {
    //Master.EmailForm("Evaluation Download Request", "Software@GoldSim.com", email);
      Master.EmailForm("Evaluation Download Request", "Software@GoldSim.com", "website@goldsim.com");
      }

  /*-------------------------------------------------------------------------------------------------------------------------
  | SAVE FORM AS TOPIC
  \------------------------------------------------------------------------------------------------------------------------*/
    Master.SaveFormAsTopic();

  /*-------------------------------------------------------------------------------------------------------------------------
  | SEND DEMO EMAIL
  \------------------------------------------------------------------------------------------------------------------------*/
    if (isDemo) {
    //Customize the subject line based on whether a trial was also selected
      string demoSubject = "Demo request WITH" + (isTrial? "" : "OUT") + " Evaluation Request";
      //Master.EmailForm(demoSubject, "Software@GoldSim.com", email);
      Master.EmailForm(demoSubject, "Software@GoldSim.com", "website@goldsim.com");
      }

  /*-------------------------------------------------------------------------------------------------------------------------
  | SEND EMAIL RECEIPT
  \------------------------------------------------------------------------------------------------------------------------*/
    Utility.SendWebPage("http://" + Request.Url.Host + "/Forms/Evaluation.Receipt.html", "GoldSim Evaluation Request", "Software@GoldSim.com", email);

  /*-------------------------------------------------------------------------------------------------------------------------
  | REDIRECT USER
  \------------------------------------------------------------------------------------------------------------------------*/
  //Override further processing by default ProcessForm handler by providing a manual redirect
    Response.Redirect("/Topic/431/");
    Response.End();

    }

</script>

<asp:Content ContentPlaceHolderID="Subtitle" runat="server">
  <p class="subtitle">Request Free GoldSim Trial Version and/or Web Demo</p>
</asp:Content>

<asp:Content ContentPlaceHolderId="Content" runat="Server">
  <p>Trial versions are fully functional expiring 30 days after registration. GoldSim is easy to install, register (and uninstall), and, of course, there is no obligation to buy and your privacy is respected. Free technical support is provided throughout your trial. Trial users also have full access to the GoldSim Knowledge Base and Model Library.</p>
  <p>Qualified prospects can also request a live web-based demo. Note you will be speaking to a technical expert, and not a sales person. During the demonstration, a GoldSim specialist shares his or her desktop with you (using GoToMeeting), explaining the software's key features and benefits, demonstrating GoldSim models, and answering your questions.</p>

  <fieldset>
    <legend>Evaluation Method</legend>

    <%-- EVALUATION METHOD CHECKBOXES --%>
    <div class="FieldContainer Checkboxes">
      <label For="EvaluationTypeList" RunAt="Server">How would you like to evaluate GoldSim?</label>
      (Please select at least one option.)
      <asp:CheckBoxList ID="EvaluationTypeList" RepeatLayout="Flow" RunAt="server">
        <asp:ListItem Value="Trial">GoldSim Trial Version</asp:ListItem>
        <asp:ListItem Value="Demo">Live Demonstration</asp:ListItem>
      </asp:CheckBoxList>
      <asp:CustomValidator
        ControlToValidate       = "Email:Email:Field"
        OnServerValidate        = "EvaluationMethodValidator"
        ErrorMessage            = "You must select at least one evaluation method."
        RunAt                   = "Server"
        />
    </div>

  </fieldset>

  <fieldset>
    <legend>Contact Information</legend>

    <%-- NAME BLOCK: FNAME, LNAME --%>
    <GoldSimForm:NameBlock ID="NameBlock" RunAt="Server" />

    <%-- ORGANIZATION --%>
    <GoldSimForm:Organization ID="Organization" RunAt="Server" />

    <%-- EMAIL --%>
    <GoldSimForm:Email ID="Email" RunAt="Server" />
    <em class="Instructions">Only institutional email domains are accepted. Email addresses of free domains (yahoo.com, gmail.com, etc.) are not accepted nor processed. You can refer to our privacy policy regarding how we use your email address.</em>

    <%-- COUNTRY SELECTION --%>
    <GoldSimForm:CountrySelection ID="Country" RunAt="Server" />

    <%-- PHONE --%>
    <GoldSimForm:Phone ID="Phone" RunAt="Server" />

    <%-- GOLDSIM CONTACT CHECK --%>
    <div class="FieldContainer ColSpan Checkbox">
      <label for="GoldSimContactCheck" RunAt="Server">I would like to discuss my application with GoldSim</label>
      <asp:CheckBox ID="GoldSimContactCheck" RunAt="Server" />
    </div>

    <%-- AREA OF FOCUS SELECTION --%>
    <GoldSimForm:AreaOfFocusSelection ID="ContactFocus" RunAt="Server" />

    <%-- SIMULATION PROBLEM DESCRIPTION --%>
    <Ignia:FormField   ID = "ProblemDescription"
      LabelName           = "What problem are you trying to solve?"
      AccessKey           = "p"
      MaxLength           = "150"
      FieldSize           = "464"
      Required            = "True"
      TextMode            = "MultiLine"
      CssClass            = "BlockLabel TextField"
      SkinId              = "BoxedPairs"
      RunAt               = "Server"
      />

    <%-- EXISTING SOFTWARE/TOOLS DESCRIPTION --%>
    <Ignia:FormField   ID = "ExistingToolsDescription"
      LabelName           = "What other risk analysis tools do you use, or are evaluating?"
      AccessKey           = "t"
      MaxLength           = "150"
      FieldSize           = "464"
      Required            = "True"
      TextMode            = "MultiLine"
      CssClass            = "BlockLabel TextField"
      SkinId              = "BoxedPairs"
      RunAt               = "Server"
      />

    <%-- MODULE INTEREST CHECKBOXES --%>
    <GoldSimForm:ModuleInterestSelection ID="ModuleInterest" RunAt="Server" />

    <div class="FieldContainer Referral Select">
      <asp:DropDownList ID="ReferralSelectionList" runat="server">
        <asp:ListItem Value="" Selected="True">How did you learn about GoldSim?</asp:ListItem>
        <asp:ListItem Value="Google">Google</asp:ListItem>
        <asp:ListItem Value="Other Search Engine">Other Search Engine</asp:ListItem>
        <asp:ListItem Value="Wikipedia">Wikipedia</asp:ListItem>
        <asp:ListItem Value="Word of Mouth">Word Of Mouth</asp:ListItem>
        <asp:ListItem Value="From a Colleague">From a Colleague</asp:ListItem>
        <asp:ListItem Value="Trade Show">Trade Show</asp:ListItem>
        <asp:ListItem Value="Journal or Advertisement">Journal or Advertisement</asp:ListItem>
        <asp:ListItem Value="Link from another website">Link from another website</asp:ListItem>
        <asp:ListItem Value="Other">Other</asp:ListItem>
      </asp:DropDownList>
    </div>
    <Ignia:FormField   ID = "ReferralDetails"
      LabelName           = "Referral Details"
      AccessKey           = "D"
      MaxLength           = "150"
      FieldSize           = "320"
      CssClass            = "TextField"
      SkinId              = "BoxedPairs"
      RunAt               = "Server"
      />
    <em class="Instructions">Please provide additional details for other search engines, journal name, specific trade show, etc.</em>

  </fieldset>

</asp:Content>