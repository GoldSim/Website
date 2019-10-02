<%@ Page Language="C#" Title="GoldSim Trial" %>

<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="Newtonsoft.Json" %>

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

    // ContentType for the form Topic
    Master.SaveAsContentType    = "EvaluationRequest";

    // Submit button label
    Master.SubmitLabel          = "Request Trial";

    // Custom processing event
    Master.ProcessForm         += ProcessForm;

  }

  /*============================================================================================================================
  | PROCESS FORM
  >=============================================================================================================================
  | Override default form processing to provide conditional email support.  Includes a manual redirect to prevent default
  | processing of the form since the trial requires a highly customized experience.
  \---------------------------------------------------------------------------------------------------------------------------*/
  public void ProcessForm(Object sender, CommandEventArgs args) {

    /*--------------------------------------------------------------------------------------------------------------------------
    | Local variables
    \-------------------------------------------------------------------------------------------------------------------------*/
    string      email           = ((IgniaFormField)Email.FindControl("Email")).Value;

    /*--------------------------------------------------------------------------------------------------------------------------
    | Send trial request email
    \-------------------------------------------------------------------------------------------------------------------------*/
    Master.EmailForm("Trial Download Request", "Software@GoldSim.com", "website@goldsim.com");

    /*--------------------------------------------------------------------------------------------------------------------------
    | Save form as Topic
    \-------------------------------------------------------------------------------------------------------------------------*/
    Master.SaveFormAsTopic();

    /*--------------------------------------------------------------------------------------------------------------------------
    | Send email receipt (to user)
    \-------------------------------------------------------------------------------------------------------------------------*/
    Utility.SendWebPage(
      "https://" + Request.Url.Host + "/Forms/Evaluation.Receipt.html",
      "GoldSim Trial Request",
      "Software@GoldSim.com",
      email
    );

    /*--------------------------------------------------------------------------------------------------------------------------
    | Redirect user
    \-------------------------------------------------------------------------------------------------------------------------*/
    Response.Redirect("/Topic/431/");
    Response.End();

  }

  /*============================================================================================================================
  | VALIDATOR: EMAIL DOMAIN
  >=============================================================================================================================
  | Ensures that the email address entered is not from a generic / free email domain.
  \---------------------------------------------------------------------------------------------------------------------------*/
  void EmailDomainValidator(object source, ServerValidateEventArgs args) {
    string      email           = ((IgniaFormField)Email.FindControl("Email")).Value;

    foreach (string emailDomain in Master.GenericEmailDomains) {
      if (email.IndexOf(emailDomain, StringComparison.InvariantCultureIgnoreCase) >= 0) {
        EmailInstructions.Style.Add("display", "none");
        EmailErrorInstructions.Style.Add("display", "block");
        args.IsValid            = false;
        break;
      }
    }

  }

</Script>

<asp:Content ContentPlaceHolderID="Subtitle" runat="server">
  <p class="subtitle">Request Free GoldSim Trial Version</p>
</asp:Content>

<asp:Content ContentPlaceHolderId="Content" runat="Server">
  <p>Trial versions are fully functional, but limit the size of models that can be built or edited (to 50 elements). The license expires 30 days from the date it was issued. GoldSim is easy to install, activate (and uninstall), and, of course, there is no obligation to buy and your privacy is respected. Free technical support is provided throughout your trial.</p>

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
        <div class="hidden">
          <asp:CustomValidator
            ControlToValidate   = "Email:Email:Field"
            OnServerValidate    = "EmailDomainValidator"
            ErrorMessage        = "Please use an email address with an institutional domain."
            RunAt               = "Server"
          />
        </div>
      </div>
      <div class="cell">
        <p id="EmailInstructions" class="field instructions" ClientIDMode="Static" runat="server">Only institutional email domains are accepted. Email addresses of free domains (yahoo.com, gmail.com, etc.) are not accepted nor processed. You can refer to our <a href="/Topic/4222/">privacy policy</a> regarding how we use your email address.</p>
        <p id="EmailErrorInstructions" class="field instructions error" style="display: none;" ClientIDMode="Static" runat="server">While we would like to grant your trial request, we cannot provide licenses to email addresses that are not associated with an organization (that is, we do not send license information to free webmail or ISP accounts). If you would like to evaluate GoldSim, please use an email address associated with your business or organization. If you are concerned about providing your organizational email address, please view our privacy policy regarding how we use your email address. If you have no other email address to use, please indicate this in an email to <a href="mailto:software@goldsim.com">software@goldsim.com</a>.</p>
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

      <%-- MODULE INTEREST CHECKBOXES --%>
      <div class="cell">
        <label>GoldSim Trials include the Financial, Reliability, Dashboard Authoring and Distributed Processing Modules.</label>
	<label>I am also interested in:</label>
      </div>
      <div class="medium-6 cell" style="margin-bottom: 1rem;">
        <div class="checkbox">
          <asp:CheckBox ID="RT" ClientIDMode="Static" RunAt="Server" />
          <label for="RT">Radionuclide Transport Module</label>
        </div>
      </div>
      <div class="medium-6 cell" style="margin-bottom: 1rem;">
        <div class="checkbox">
          <asp:CheckBox ID="CT" ClientIDMode="Static" RunAt="Server" />
          <label for="CT">Contaminant Transport Module</label>
        </div>
      </div>

      <%-- REFERRAL SOURCE SELECTION --%>
      <GoldSimForm:ReferralSourceSelection ID="ReferralSource" RunAt="Server" />

      <!-- Training Course Check -->
      <div id="TrainingCourseCheckField" class="cell" style="margin-top: 1rem;">
        <div class="checkbox">
          <asp:CheckBox ID="TrainingCourseCheck" ClientIDMode="Static" RunAt="Server" />
          <label for="TrainingCourseCheck" RunAt="Server">Are you using this Trial Version for a training course?</label>
        </div>
      </div>

    </div>

  </fieldset>

  <fieldset>
    <legend>Trainer Contact Information</legend>
    <div class="grid-x grid-margin-x">

      <div class="medium-6 cell">
        <Ignia:FormField
          ID                    = "TrainerName"
          LabelName             = "*Name"
          MaxLength             = "150"
          FieldSize             = "320"
          CssClass              = "TextField"
          SkinId                = "BoxedPairs"
          RunAt                 = "Server"
          />
      </div>
      <div class="medium-6 cell">
        <Ignia:FormField
          ID                    = "TrainerEmail"
          LabelName             = "*Email"
          MaxLength             = "150"
          FieldSize             = "320"
          CssClass              = "TextField"
          SkinId                = "BoxedPairs"
          RunAt                 = "Server"
          />
      </div>
      <div class="cell">
        <Ignia:FormField
          ID                    = "TrainerOrganization"
          LabelName             = "*Organization"
          MaxLength             = "150"
          FieldSize             = "320"
          CssClass              = "TextField"
          SkinId                = "BoxedPairs"
          RunAt                 = "Server"
          />
      </div>

    </div>
  <fieldset>

  <fieldset>
    <legend>Terms of Use</legend>
    <div class="grid-x grid-margin-x">

      <div class="cell">
        <p>By checking the box below I agree to the following:</p>
        <ol>
          <li>I will only use this free Trial Version for the purpose of evaluating and/or testing the software or taking a GoldSim training course.</li>
          <li>I will <em>not</em> use this free Trial Version for consulting or other commercial applications. If I require a temporary license for such purposes, I will <a href="mailto:support@goldsim.com">contact the GoldSim Technology Group</a>.</li>
        </ol>
      </div>
      <GoldSimForm:TouAndNewsletterCheck ID="TouAndNewsletterCheck" RunAt="Server" />

    </div>
  </fieldset>

</asp:Content>

<asp:Content ContentPlaceHolderID="PageScripts" runat="server">
  <script>
    $(function() {

      /**
       * Establish variables
       */
      var
        genericEmailDomains     = <%= JsonConvert.SerializeObject(Master.GenericEmailDomains) %>,
        trainerInfoFields       = 'input[id$="TrainerName_Field"], input[id$="TrainerEmail_Field"], input[id$="TrainerOrganization_Field"]',
        trainerInfoLabels       = 'label[id$="TrainerName_Label"], label[id$="TrainerEmail_Label"], label[id$="TrainerOrganization_Label"]';

      /**
       * Sets conditionally required fields to disabled and not required by default
       */
      toggleDisabled(trainerInfoFields, true);
      toggleRequired(trainerInfoFields, false);

      /**
       * Sets label class on conditionally required fields
       */
      $(trainerInfoLabels).addClass('required');

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
            $('#EmailErrorInstructions').show();
            break;
          }
          else {
            $(this).removeClass('Error');
            $('#ContentContainer_Content_Email_Email_Label').removeClass('Error');
            $('#EmailInstructions').show();
            $('#EmailErrorInstructions').hide();
          }
        }
      });

      /**
       * Conditionally enables Trainer fields if invoice payment choice is selected
       */
      $('#TrainingCourseCheck').change(function () {
        if ($(this).is(':checked')) {
          toggleRequired(trainerInfoFields, true);
          toggleDisabled(trainerInfoFields, false);
        }
        else {
          setTimeout(function() {
            toggleRequired(trainerInfoFields, false);
            toggleDisabled(trainerInfoFields, true);
          }, 250);
        }
      });

      /**
        * Monitor Radionuclide and Contaminant Transport checkboxes to ensure only RT is checked if the user selects both
        */
      $('#RT').change(function() {
        if ($('#CT').is(':checked')) {
          $('#CT').prop('checked', false);
        }
      });
      $('#CT').change(function() {
        if ($('#RT').is(':checked')) {
          $('#RT').prop('checked', false);
        }
      });

    });

    /**
      * Removes or adds required attribute on fields depending on field selection
      */
    function toggleRequired(fields, required) {
      $(fields).prop('required', required);
    };

    /**
      * Sets disabled state on provided fields (selectors) and provided true/false state
      */
    function toggleDisabled(fields, disabled) {
      $(fields).prop('disabled', disabled);
    };

  </script>
</asp:Content>