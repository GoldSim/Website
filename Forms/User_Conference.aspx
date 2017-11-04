<%@ Page Language="C#" Title="GoldSim User Conference Registration" MasterPageFile="/Forms/Common/Templates/Forms.Layout.Master" %>

<%@ MasterType  VirtualPath="/Forms/Common/Templates/Forms.Layout.Master" %>

<Script RunAt="Server">
/*===========================================================================================================================
| FORM: GOLDSIM USER CONFERENCE REGISTRATION
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
|               04.06.15    Rick Kossik                 Changed Training Registration to Conference Registration
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
    Master.FormTopic            =  Ignia.Topics.TopicRepository.RootTopic.GetTopic(2273);
  //Submit button label
    Master.SubmitLabel          = "Submit Registration";
  //Submission email subject
    Master.EmailSubject         = "User Conference Registration Request";
    Master.EmailSender          = ((IgniaFormField)Email.FindControl("Email")).Value;
  //Request recipient
    Master.EmailRecipient       = "support@goldsim.com";
  //Redirect URL
    Master.SuccessUrl           = "/Topic/755/";

    }

/*===========================================================================================================================
| VALIDATOR: ACCOUNTS PAYABLE
>============================================================================================================================
| Ensures that the accounts payable section is only required if the user selected "Invoice Accounts Payable".
\--------------------------------------------------------------------------------------------------------------------------*/
  void AccountsPayableValidator(object source, ServerValidateEventArgs args) {
    if (PaymentTypeSelection.Items.FindByValue("Invoice_AP").Selected) {
      Page.Validate("AccountsPayable");
      }
    else {
      args.IsValid = true;
      }
    }

</script>

<asp:Content ContentPlaceHolderId="PageHead" RunAt="Server">
  <script type="text/javascript">

    function ShowPaymentInstructions(value) {
      var paymentInstructionsBox = document.getElementById('<%= PaymentInstructions.ClientID %>');
      if (value == 'C_C') {
        paymentInstructionsBox.innerHTML        = 'If paying by credit card, call 1-425-295-6985 (-8 hours GMT) or fax 1-425-642-8073 to complete transaction.';
        }
      else {
        paymentInstructionsBox.innerHTML        = 'If paying by invoice, Purchase Order and Accounts Payable information must be filled out below.';
        }
      paymentInstructionsBox.style.display      = 'block';
      }

  </script>
</asp:Content>

<asp:Content ContentPlaceHolderId="Content" runat="Server">

  <h2 class="Subtitle">GoldSim User Conference</h2>
  <h3 >24-25 September 2015</h3>
<p> </p>
  <p>The conference will be preceded (on September 22 and 23) by an optional two day training workshop (focusing on novice users). Hence, novice users can attend the training and then stay for the conference to meet expert users from around the world. Attendees of the training session are expected to bring their own laptop computers. Free 45-day temporary licenses will be provided to any attendees who require a license.</p>
  <p>Costs include breakfast and lunch each day, as well as dinner on the evenings of September 23 and 24 (for conference attendees).</p>

 <p>Note that attendees are responsible for arranging their own lodging (see <a href="/topic/2274" target="_blank">lodging options)</a>.</p>


  <p>We look forward to seeing you in Seattle!</p>

  <fieldset>
    <legend>Contact Information</legend>

    <%-- NAME BLOCK: FNAME, LNAME --%>
    <GoldSimForm:NameBlock ID="NameBlock" RunAt="Server" />

    <%-- ORGANIZATION --%>
    <GoldSimForm:Organization ID="Organization" RunAt="Server" />

    <%-- ADDRESS BLOCK: ADDRESS1, ADDRESS2, CITY, STATE/PROVINCE, ZIP/POSTAL --%>
    <GoldSimForm:AddressBlock ID="AddressBlock" RunAt="Server" />

    <%-- COUNTRY SELECTION --%>
    <GoldSimForm:CountrySelection ID="CountrySelection" IsRequired="true" RunAt="Server" />

    <%-- EMAIL/CONFIRM EMAIL --%>
    <GoldSimForm:Email ID="Email" ShowEmailConfirm="true" RunAt="Server" />

    <%-- PHONE --%>
    <GoldSimForm:Phone ID="Phone" RunAt="Server" />

    <%-- FAX --%>
    <GoldSimForm:Fax ID="Fax" RunAt="Server" />

  </fieldset>

  <fieldset>

    <legend>Poster Session</legend>

    <div id="PosterSessionFieldContainer" class="FieldContainer">
      <asp:CheckBox ID="PosterSessionCheck" RunAt="Server" />
      <label for="PosterSessionCheck" RunAt="Server">I am interested in submitting a poster.</label>
      <p><i>We will follow up with instructions for submitting and presenting your poster at the conference.</i></p>
    </div>

  </fieldset>

  <fieldset>

   <legend>Conference Options</legend>

    <%-- SESSION TYPE --%>
    <asp:Label ID="SessionSelect" CssClass="Session Select" style="display: none;" RunAt="Server" />
    <asp:RadioButtonList ID="SessionTypeSelection" AppendDataBoundItems="true" RepeatLayout="Flow" RepeatDirection="Vertical" RunAt="Server">
      <asp:ListItem Value="User Conference Only" onclick="on 1">User Conference Only ($750)</asp:ListItem>
      <asp:ListItem Value="Training Session Only" onclick="on2">Training Only ($1000)</asp:ListItem>
      <asp:ListItem Value="Training and Conference" onclick="on 3">Training and Conference ($1250)</asp:ListItem>
			<asp:ListItem Value="User Conference Only Student" onclick="on 4">User Conference Only (Student) ($375)</asp:ListItem>
			<asp:ListItem Value="Training Session Only Student" onclick="on5">Training Only (Student) ($250)</asp:ListItem>
			<asp:ListItem Value="Training and Conference Student" onclick="on 6">Training and Conference (Student) ($625)</asp:ListItem>
    </asp:RadioButtonList>

    <p><i>Note (for non-students) that conference fee increases by $750 after July 31, 2015</i></p>

  </fieldset>

  <fieldset>
    <legend>Payment Information</legend>

    <%-- PAYMENT TYPE --%>
    <asp:Label ID="PaymentInstructions" CssClass="Payment Instructions" style="display: none;" RunAt="Server" />
    <asp:RadioButtonList ID="PaymentTypeSelection" AppendDataBoundItems="true" RepeatLayout="Flow" RepeatDirection="Vertical" RunAt="Server">
      <asp:ListItem Value="Credit_Card" onclick="ShowPaymentInstructions('C_C')">Credit Card</asp:ListItem>
      <asp:ListItem Value="Invoice_Self" onclick="ShowPaymentInstructions('I_S')">Invoice Me</asp:ListItem>
      <asp:ListItem Value="Invoice_AP" onclick="ShowPaymentInstructions('I_AP')">Invoice Accounts Payable</asp:ListItem>
    </asp:RadioButtonList>

    <fieldset>

      <%-- PO NUMBER --%>
      <Ignia:FormField   ID = "PONumber"
        LabelName           = "Purchase Order Number"
        AccessKey           = "P"
        MaxLength           = "150"
        FieldSize           = "320"
        CssClass            = "TextField"
        SkinId              = "BoxedPairs"
        RunAt               = "Server"
        />

      <%-- PO NOTES --%>
      <Ignia:FormField   ID = "PurchaseNotes"
        LabelName           = "Additional Instructions"
        AccessKey           = "N"
        MaxLength           = "500"
        FieldSize           = "320"
        CssClass            = "TextField"
        SkinId              = "BoxedPairs"
        RunAt               = "Server"
        />

      <%-- PAPER COPY CHECK --%>
      <div ID="PaperCopyField" class="FieldContainer">
        <em>NOTE</em>: GoldSim's standard method of invoicing and providing receipts is via email.
        <label for="PaperCopyCheck" RunAt="Server">Mark the following box if you would prefer a paper invoice or receipt.</label>
        <asp:CheckBox ID="PaperCopyCheck" RunAt="Server" />
      </div>

    </fieldset>

    <fieldset ID="APContactInfo">
      <legend>Accounts Payable Contact Information</legend>

      <%-- NAME BLOCK: FNAME, LNAME --%>
      <GoldSimForm:NameBlock ID="APContactNameBlock" ValidationGroup="AccountsPayable" RunAt="Server" />

      <asp:CustomValidator
        ControlToValidate       = "PaymentTypeSelection"
        OnServerValidate        = "AccountsPayableValidator"
        ErrorMessage            = "If 'Invoice Accounts Payable' is selected then the Accounts Payable contact information must be completed."
        Display                 = "Dynamic"
        RunAt                   = "Server"
        />

      <%-- ORGANIZATION --%>
      <GoldSimForm:Organization ID="APContactOrganization" ValidationGroup="AccountsPayable" RunAt="Server" />

      <%-- ADDRESS BLOCK: ADDRESS1, ADDRESS2, CITY, STATE/PROVINCE, ZIP/POSTAL --%>
      <GoldSimForm:AddressBlock ID="APContactAddressBlock" ValidationGroup="AccountsPayable" RunAt="Server" />

      <%-- COUNTRY SELECTION --%>
      <GoldSimForm:CountrySelection ID="APContactCountrySelection"  IsRequired="true" ValidationGroup="AccountsPayable" RunAt="Server" />

      <%-- EMAIL --%>
      <GoldSimForm:Email ID="APContactEmail" ValidationGroup="AccountsPayable" RunAt="Server" />

      <%-- PHONE --%>
      <GoldSimForm:Phone ID="APContactPhone" ValidationGroup="AccountsPayable" RunAt="Server" />

      <%-- FAX --%>
      <GoldSimForm:Fax ID="APContactFax" ValidationGroup="AccountsPayable" RunAt="Server" />

    </fieldset>

    <p>Note that you will not be charged when you submit this form. This order is not valid until accepted by GoldSim Technology Group. We will contact you to complete the transaction.</p>

  </fieldset>

</asp:Content>