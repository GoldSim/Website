<%@ Page Language="C#" Title="GoldSim Demo" %>

<%@ MasterType  VirtualPath="/Forms/Common/Templates/Forms.Layout.Master" %>
<%@ Reference   Control="/Common/Global/Controls/FormField.ascx" %>

<Script RunAt="Server">
/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/

  /*============================================================================================================================
  | PAGE LOAD
  \---------------------------------------------------------------------------------------------------------------------------*/
  void Page_Load(Object Src, EventArgs E) {

    /*--------------------------------------------------------------------------------------------------------------------------
    | Set master template properties
    \-------------------------------------------------------------------------------------------------------------------------*/
    // Associated topic for navigation -- sends to Forms.Layout.Master and then on to Page.Layout.master
    // Master.FormTopic         =  Ignia.Topics.TopicRepository.RootTopic.GetTopic(460);

    // Submit button label
    Master.SubmitLabel          = "Request Web Demo";

    // Custom processing event
    Master.ProcessForm         += ProcessForm;

  }

  /*============================================================================================================================
  | VALIDATOR: EVALUATION METHOD
  >=============================================================================================================================
  | Ensures that at least one of the evaluation methods is selected.
  \---------------------------------------------------------------------------------------------------------------------------*/
  void EvaluationMethodValidator(object source, ServerValidateEventArgs args) {
    args.IsValid = !String.IsNullOrEmpty(EvaluationTypeList.SelectedValue);
  }

  /*============================================================================================================================
  | PROCESS FORM
  >=============================================================================================================================
  | Override default form processing to provide conditional email support.  Includes a manual redirect to prevent default
  | processing of the form since the evaluation requires a highly customized experience.
  \---------------------------------------------------------------------------------------------------------------------------*/
  public void ProcessForm(Object sender, CommandEventArgs args) {

    /*--------------------------------------------------------------------------------------------------------------------------
    | Local variables
    \-------------------------------------------------------------------------------------------------------------------------*/
    string      email           = ((IgniaFormField)Email.FindControl("Email")).Value;
    string      demoSubject     = "Demo request WITHOUT Evaluation Request";

    /*--------------------------------------------------------------------------------------------------------------------------
    | Send demo request email
    \-------------------------------------------------------------------------------------------------------------------------*/
    Master.EmailForm(demoSubject, "Software@GoldSim.com", "website@goldsim.com");

    /*--------------------------------------------------------------------------------------------------------------------------
    | Redirect user
    \-------------------------------------------------------------------------------------------------------------------------*/
    Response.Redirect("/Topic/5154/");
    Response.End();

  }

</Script>

<asp:Content ContentPlaceHolderID="Subtitle" runat="server">
  <p class="subtitle">Request Free GoldSim Web Demo</p>
</asp:Content>

<asp:Content ContentPlaceHolderId="Content" runat="Server">
  <p>Qualified prospects can request a live web-based demo. Note that you will be speaking to a technical expert, and not a sales person. During the demonstration, a GoldSim specialist shares his or her desktop with you (using GoToMeeting), explaining the software's key features and benefits, demonstrating GoldSim models, and answering your questions.</p>

  <fieldset style="display: none;">
    <legend>Evaluation Method</legend>

    <%-- EVALUATION METHOD CHECKBOXES --%>
    <div class="FieldContainer Checkboxes">
      <label For="EvaluationTypeList" RunAt="Server">How would you like to evaluate GoldSim?</label>
      (Please select at least one option.)
      <asp:CheckBoxList ID="EvaluationTypeList" RepeatLayout="Flow" RunAt="server">
        <asp:ListItem Value="Trial">GoldSim Trial Version</asp:ListItem>
        <asp:ListItem Value="Demo" Selected="true">Live Demonstration</asp:ListItem>
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
    <div class="grid-x grid-margin-x">

      <%-- NAME BLOCK: FNAME, LNAME --%>
      <GoldSimForm:NameBlock ID="NameBlock" RunAt="Server" />

      <%-- ORGANIZATION --%>
      <!-- Organization -->
      <div class="medium-6 cell">
        <GoldSimForm:Organization ID="Organization" RunAt="Server" />
      </div>
      <!-- Organization -->

      <%-- EMAIL --%>
      <!-- Email -->
      <div class="medium-6 cell">
        <GoldSimForm:Email ID="Email" RunAt="Server" />
      </div>
      <div class="cell">
        <p class="field instructions">Only institutional email domains are accepted. Email addresses of free domains (yahoo.com, gmail.com, etc.) are not accepted nor processed. You can refer to our <a href="/Topic/4222/">privacy policy</a> regarding how we use your email address.</p>
      </div>
      <!-- /Email -->

      <%-- COUNTRY SELECTION --%>
      <!-- Country -->
      <div class="medium-6 cell">
        <GoldSimForm:CountrySelection ID="Country" IsRequired="true" RunAt="Server" />
      </div>
      <!-- /Country -->

      <%-- PHONE --%>
      <!-- Phone -->
      <div class="medium-6 cell">
        <GoldSimForm:Phone ID="Phone" RunAt="Server" />
      </div>
      <!-- /Phone -->

      <%-- AREA OF FOCUS SELECTION --%>
      <GoldSimForm:AreaOfFocusSelection ID="ContactFocus" RunAt="Server" />

      <%-- SIMULATION PROBLEM DESCRIPTION --%>
      <!-- Problem Description -->
      <div class="cell">
        <Ignia:FormField     ID = "ProblemDescription"
          LabelName             = "*What problem are you trying to solve?"
          AccessKey             = "p"
          MaxLength             = "150"
          FieldSize             = "464"
          Required              = "True"
          TextMode              = "MultiLine"
          CssClass              = "BlockLabel TextField"
          SkinId                = "BoxedPairs"
          RunAt                 = "Server"
          />
      </div>
      <!-- /Problem Description -->

      <%-- EXISTING SOFTWARE/TOOLS DESCRIPTION --%>
      <!-- Existing Tools Description -->
      <div class="cell">
        <Ignia:FormField     ID = "ExistingToolsDescription"
          LabelName             = "*What other risk analysis tools do you use, or are evaluating?"
          AccessKey             = "t"
          MaxLength             = "150"
          FieldSize             = "464"
          Required              = "True"
          TextMode              = "MultiLine"
          CssClass              = "BlockLabel TextField"
          SkinId                = "BoxedPairs"
          RunAt                 = "Server"
          />
      </div>
      <!-- /Existing Tools Description -->

      <%-- REFERRAL SOURCE SELECTION --%>
      <GoldSimForm:ReferralSourceSelection ID="ReferralSource" RunAt="Server" />

    </div>

  </fieldset>

</asp:Content>