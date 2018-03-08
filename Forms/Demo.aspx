<%@ Page Language="C#" Title="GoldSim Demo" %>

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

    // Submit button label
    Master.SubmitLabel          = "Request Web Demo";

    // Custom processing event
    Master.ProcessForm         += ProcessForm;

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
        <p id="EmailInstructions" class="field instructions">Only institutional email domains are accepted. Email addresses of free domains (yahoo.com, gmail.com, etc.) are not accepted nor processed. You can refer to our <a href="/Topic/4222/">privacy policy</a> regarding how we use your email address.</p>
        <p id="EmailErrorInstructions" class="field instructions error" style="display: none;">While we would like to grant your evaluation request, we cannot provide licenses to email addresses that are not associated with an organization (that is, we do not send license information to free webmail or ISP accounts). If you would like to evaluate GoldSim, please use an email address associated with your business or organization. If you are concerned about providing your organizational email address, please view our privacy policy regarding how we use your email address. If you have no other email address to use, please indicate this in an email to <a href="mailto:software@goldsim.com">software@goldsim.com</a>.</p>
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

    });
  </script>
</asp:Content>