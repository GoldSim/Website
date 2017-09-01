<%@ Page Language="C#" Title="Download Player" MasterPageFile="/Forms/Common/Templates/Forms.Layout.Master" %>

<%@ MasterType  VirtualPath="/Forms/Common/Templates/Forms.Layout.Master" %>

<Script RunAt="Server">
/*===========================================================================================================================
| FORM: PLAYER DOWNLOAD REQUEST
|
| Author        Katherine Trunkey, Ignia LLC (Katie@ignia.com)
| Client        GoldSim
| Project       Site Relaunch
|
| Purpose       Provides form template for GoldSim Academic Version application.
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
  //Master.FormTopic            =  Ignia.Topics.TopicRepository.RootTopic.GetTopic(742);
  //Submit button label
    Master.SubmitLabel          = "Request GoldSim Player";
  //Submission email subject
    Master.EmailSubject         = "Player Download Request";
  //Redirect URL
    Master.SuccessUrl           = "/Topic/442/";

    }
</Script>

<asp:Content ContentPlaceHolderId="Content" runat="Server">

  <h2 class="Subtitle">Download GoldSim Player</h2>
  <p>With GoldSim Player you can view, navigate and run GoldSim models (you need GoldSim Pro to create new models and edit existing models). Fill out this form and we will send you an email with all the information you need to download your free copy of the GoldSim Player. The information you provide below will be used to keep you up to date on GoldSim developments and notify you when new versions of the GoldSim Player have been released. Under no circumstances will this information be released to third parties.</p>
  <p><em><strong>NOTE</strong>: GoldSim uses the file extension .gsm. This file extension is also used by other programs to represent a raw audio stream file. The GoldSim Player only reads GoldSim Player files and cannot read audio files.</em></p>

  <fieldset>

    <%-- NAME BLOCK: FNAME, LNAME --%>
    <GoldSimForm:NameBlock ID="NameBlock" RunAt="Server" />

    <%-- ORGANIZATION --%>
    <GoldSimForm:Organization ID="Organization" RunAt="Server" />

    <%-- EMAIL --%>
    <GoldSimForm:Email ID="Email" RunAt="Server" />
    <em class="Instructions">Please provide an institutional (i.e., organization, company, academic) email address. Requests from generic domains (e.g., hotmail, yahoo, gmail) will not be accepted. If you are concerned with providing this information, please read our Privacy Policy.</em>

    <%-- PLAYER INTEREST DESCRIPTION --%>
    <Ignia:FormField   ID = "PlayerInterestDescription"
      LabelName           = "Why are you interested in the GoldSim Player?"
      AccessKey           = "i"
      MaxLength           = "150"
      FieldSize           = "468"
      Required            = "True"
      TextMode            = "MultiLine"
      CssClass            = "BlockLabel TextField"
      SkinId              = "BoxedPairs"
      RunAt               = "Server"
      />

  </fieldset>

</asp:Content>