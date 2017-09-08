<%@ Page Language="C#" Title="GoldSim Newsletter" MasterPageFile="/Forms/Common/Templates/Forms.Layout.Master" %>

<%@ MasterType  VirtualPath="/Forms/Common/Templates/Forms.Layout.Master" %>
<%@ Reference   Control="/Common/Global/Controls/FormField.ascx" %>

<Script RunAt="Server">
/*===========================================================================================================================
| FORM: GOLDSIM TRAINING REGISTRATION
|
| Author        Katherine Trunkey, Ignia LLC (Katie@ignia.com)
| Client        GoldSim
| Project       Site Relaunch
|
| Purpose       Provides form template for GoldSim training session registration.
|
>============================================================================================================================
| Revisions     Date        Author                      Comments
| - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
|               11.24.08    Jeremy Caney                Initial version template.
|               07.27.10    Katherine Trunkey           Adapted for form template.
|				01.12.16	Jason Lillywhite			Changed email domain to software@goldsim.com
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
  //Master.FormTopic    =  Ignia.Topics.TopicRepository.RootTopic.GetTopic(757);
    Master.EmailSender = ((IgniaFormField)Email.FindControl("Email")).Value;
    Master.EmailRecipient       = "Software@GoldSim.com";
    Master.SuccessUrl           = "/Topic/5/";

    }

/*===========================================================================================================================
| SUBSCRIBE REQUEST
>============================================================================================================================
| Subscribe button event handling.
\--------------------------------------------------------------------------------------------------------------------------*/
  public void SubscribeRequest(Object sender, CommandEventArgs args) {
    Master.EmailForm("GoldSim Newsletter: Subscription");
    Response.Redirect(Master.SuccessUrl);
    }

/*===========================================================================================================================
| SUBSCRIPTION REMOVAL REQUEST
>============================================================================================================================
| Remove button event handling.
\--------------------------------------------------------------------------------------------------------------------------*/
  public void RemovalRequest(Object sender, CommandEventArgs args) {
    Master.EmailForm("GoldSim Newsletter: Subscription Removal");
    Response.Redirect(Master.SuccessUrl);
    }

</script>

<asp:Content ContentPlaceHolderID="Subtitle" runat="server">
  <p class="subtitle">Announcements for New or Upcoming Features, Interesting GoldSim Applications, and Application Tips</p>
</asp:Content>

<asp:Content ContentPlaceHolderId="Content" runat="Server">

  <p>Join the thousands of others that enjoy reading about GoldSim!</p>
  <p>The newsletter is sent from the software@goldsim.com email address. To ensure that you receive the newsletter, it is recommended that you add this address to your Safe Senders List.</p>

  <fieldset>
    <legend>Contact Information</legend>
    <div class="grid-x grid-margin-x">

      <%-- NAME BLOCK: FNAME, LNAME --%>
      <GoldSimForm:NameBlock ID="NameBlock" RunAt="Server" />

      <%-- ORGANIZATION --%>
      <div class="medium-6 cell">
        <GoldSimForm:Organization ID="Organization" RunAt="Server" />
      </div>

      <%-- EMAIL --%>
      <GoldSimForm:Email ID="Email" SplitLayout="true" RunAt="Server" />

    </div>
  </fieldset>

</asp:Content>

<asp:Content ContentPlaceHolderID="ButtonArea" RunAt="Server">
  <div class="grid-x grid-margin-x" style="margin-top: 1rem;">
    <div class="medium-6 cell">
      <asp:Button ID="SubscribeButton" Text="Subscribe" OnCommand="SubscribeRequest" CommandName="Subscribe" CssClass="button large primary" style="min-width: 234px;" RunAt="Server" />
    </div>
    <div class="medium-6 cell text-right">
      <asp:Button ID="RemoveButton" Text="Unsubscribe" OnCommand="RemovalRequest" CommandName="Remove" CssClass="button large secondary" style="min-width: 234px;" RunAt="Server" />
    </div>
  </div>
</asp:Content>
