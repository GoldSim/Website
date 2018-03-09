<%@ Page Language="C#" Title="GoldSim Academic Application Form" MasterPageFile="/Forms/Common/Templates/Forms.Layout.Master" %>

<%@ Import Namespace="Newtonsoft.Json" %>

<%@ MasterType  VirtualPath="/Forms/Common/Templates/Forms.Layout.Master" %>

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
    | Set master properties
    \-------------------------------------------------------------------------------------------------------------------------*/
    // Associated topic for navigation -- sends to Forms.Layout.Master and then on to Page.Layout.master
    // Master.FormTopic         =  Ignia.Topics.TopicRepository.RootTopic.GetTopic(739);

    // ContentType for form Topic
    Master.SaveAsContentType    = "AcademicRequest";

    // Submit button label
    Master.SubmitLabel          = "Send Request";

    // Submission email subject
    Master.EmailSubject         = "Academic Product Version Application: Instructor";
    Master.EmailSender          = "website@goldsim.com";

    // Redirect URL
    Master.SuccessUrl           = "/Topic/755/";

  }

  /*============================================================================================================================
  | VALIDATOR: TERMS OF USE CHECK
  >=============================================================================================================================
  | Ensures that the terms of use agreement checkbox is checked.
  \---------------------------------------------------------------------------------------------------------------------------*/
  void TermsCheckValidator(object source, ServerValidateEventArgs args) {
    if (!TOUCheck.Checked) {
      args.IsValid = false;
    }
  }

</Script>

<asp:Content ContentPlaceHolderId="Content" runat="Server">

  <p>If you wish to apply for an Academic version of GoldSim, please fill out the application form provided below. The restrictions and terms for Academic licenses are described <a href="../Topic/26/">here</a>.</p>
  <p>Information regarding your Academic license will be sent from the software@goldsim.com email address. To ensure that you receive this communication, it is recommended that you add this address to your Safe Senders List.</p>

  <fieldset>
    <legend>Contact Information</legend>
    <div class="grid-x grid-margin-x">

      <div class="cell">
        <p><em>For information regarding how we use your email address and other contact information, you can refer to our <a href="/Topic/4222/">privacy policy</a>.</em></p>
      </div>

      <%-- NAME BLOCK: FNAME, LNAME --%>
      <GoldSimForm:NameBlock ID="NameBlock" RunAt="Server" />

      <%-- EDUCATIONAL INSTITUTION --%>
      <div class="medium-6 cell">
        <GoldSimForm:Organization ID="Organization" LabelName="*Name of Institution" AccessKey="I" RunAt="Server" />
      </div>

      <%-- EMAIL --%>
      <GoldSimForm:Email ID="Email" SplitLayout="true" RunAt="Server" />
      <div class="cell text-right">
        <p id="EmailInstructions" class="field instructions">Email must be associated with an academic institution</p>
        <p id="EmailErrorInstructions" class="field error instructions" style="display: none;">Unfortunately, we can't issue free academic licenses to generic email addresses like hotmail, gmail and yahoo. If possible, please use an email address that is associated with your university. If you don't have a university email address, please send an email to <a href="mailto:software@goldsim.com">software@goldsim.com</a> with a reference to your credentials to help us verify your status.</p>
      </div>

      <%-- DEPARTMENT --%>
      <div class="medium-6 cell">
        <Ignia:FormField     ID = "Department"
          LabelName             = "*Department"
          AccessKey             = "D"
          MaxLength             = "150"
          FieldSize             = "320"
          Required              = "True"
          CssClass              = "TextField"
          SkinId                = "BoxedPairs"
          RunAt                 = "Server"
          />
      </div>

      <%-- PHONE --%>
      <div class="medium-6 cell">
        <GoldSimForm:Phone ID="Phone" RunAt="Server" />
      </div>

      <%-- ADDRESS BLOCK: ADDRESS1, ADDRESS2, CITY, STATE/PROVINCE, ZIP/POSTAL --%>
      <GoldSimForm:AddressBlock ID="Address" RunAt="Server" />

      <%-- COUNTRY SELECTION --%>
      <div class="cell">
        <GoldSimForm:CountrySelection ID="Country" IsRequired="true" RunAt="Server" />
      </div>

      <%-- AFFILIATION --%>
      <div class="medium-6 cell is-hidden">
        <label for="AffiliationSelection" accesskey="A" class="required">*Affiliation</label>
        <asp:DropDownList ID="AffiliationSelection" runat="server">
          <asp:ListItem Value="Instructor" Selected="True">Instructor</asp:ListItem>
          <asp:ListItem Value="Student">Student</asp:ListItem>
        </asp:DropDownList>
      </div>

      <%-- AREA OF FOCUS SELECTION --%>
      <GoldSimForm:AreaOfFocusSelection ID="ContactFocus" RunAt="Server" />

      <%-- FACULTY URL --%>
      <div class="cell">
        <Ignia:FormField     ID = "FacultyUrl"
          LabelName             = "Faculty Web Page"
          CssClass              = "TextField"
          RunAt                 = "Server"
          />
      </div>

    </div>
  </fieldset>

  <fieldset>
    <legend>Additional Information</legend>
    <div class="grid-x grid-margin-x">

      <%-- MODULE INTEREST CHECKBOXES --%>
      <GoldSimForm:ModuleInterestSelection ID="ModuleInterest" RunAt="Server" />

      <%-- SIMULATION PROBLEM DESCRIPTION --%>
      <div class="cell">
        <Ignia:FormField     ID = "ProblemDescription"
          LabelName             = "*What problem are you trying to solve?"
          AccessKey             = "p"
          MaxLength             = "150"
          FieldSize             = "468"
          Required              = "True"
          TextMode              = "MultiLine"
          CssClass              = "BlockLabel TextField"
          SkinId                = "BoxedPairs"
          RunAt                 = "Server"
          />
      </div>

      <%-- REFERRAL SOURCE SELECTION --%>
      <GoldSimForm:ReferralSourceSelection ID="ReferralSource" RunAt="Server" />

    </div>
  </fieldset>

  <fieldset>
    <legend>Terms of Use</legend>
    <div class="grid-x grid-margin-x">

      <div class="cell">
        <p>By checking the box below I agree to the following:</p>
        <ol>
          <li>I will only use GoldSim Academic for teaching and/or "internally funded" graduate student research (i.e., student research funded at a low-level, typically using departmental funds) at a non-profit academic institution.</li>
          <li>I will not use GoldSim Academic for "externally funded" research (i.e., sponsored research projects) at an academic institution.</li>
          <li>I will not use GoldSim Academic for sponsored research at a private or government research institution.</li>
          <li>I will not use GoldSim Academic for consulting or other commercial purposes.</li>
          <li>If GoldSim Academic is used to generate results that are published, I will notify the GoldSim Technology Group and acknowledge the GoldSim Technology Group in the publication.</li>
          <li>Create a link to https://www.goldsim.com, anchored to the text phrase "simulation software", if actively using GoldSim in your teaching or research.</li>
        </ol>
      </div>
      <div class="cell">
        <div class="checkbox">
          <asp:CheckBox ID="TOUCheck" ClientIDMode="Static" RunAt="Server" />
          <label for="TOUCheck" RunAt="Server">I agree to these terms of use.</label>
        </div>
        <asp:CustomValidator
          OnServerValidate        = "TermsCheckValidator"
          ErrorMessage            = "Please accept the terms of use."
          Display                 = "None"
          RunAt                   = "Server"
          />
      </div>

    </div>
  </fieldset>

</asp:Content>

<asp:Content ContentPlaceHolderID="PageScripts" runat="server">
  <script>
    $(function() {

      /**
       * Establish variables
       */
      var genericEmailDomains   = <%= JsonConvert.SerializeObject(Master.GenericEmailDomains) %>;

      /**
       * Validate user's email domain on blur
       */
      $('#ContentContainer_Content_Email_Email_Field').blur(function () {
        console.log('email field recorded');
        console.log($(this).val());
        var emailValue          = $(this).val().toLowerCase();
        for (var i = 0; i < genericEmailDomains.length; i++) {
          if (emailValue.indexOf(genericEmailDomains[i].toLowerCase()) >= 0) {
            $(this).addClass('Error');
            $('#ContentContainer_Content_Email_Email_Label').addClass('Error');
            $('#EmailInstructions').hide();
            $('#EmailInstructions').parent().removeClass('text-right');
            $('#EmailErrorInstructions').show();
            break;
          }
          else {
            $(this).removeClass('Error');
            $('#ContentContainer_Content_Email_Email_Label').removeClass('Error');
            $('#EmailInstructions').show();
            $('#EmailInstructions').parent().addClass('text-right');
            $('#EmailErrorInstructions').hide();
          }
        }
      });

    });
  </script>
</asp:Content>