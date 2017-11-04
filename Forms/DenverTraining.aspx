<%@ Page Language="C#" Title="GoldSim Training Sessions" MasterPageFile="/Forms/Common/Templates/Forms.Layout.Master" %>

<%@ MasterType  VirtualPath="/Forms/Common/Templates/Forms.Layout.Master" %>

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
    Master.FormTopic            =  Ignia.Topics.TopicRepository.RootTopic.GetTopic(776);
  //Submit button label
    Master.SubmitLabel          = "Submit Registration";
  //Submission email subject
    Master.EmailSubject         = "Denver Training Session Registration Request";
    Master.EmailSender = ((IgniaFormField)Email.FindControl("Email")).Value;
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

 <h2 class="Subtitle">GoldSim Training Session Registration: Denver, Colorado</h2>
  <h4 >October 1-2, 2012</h4>
  <p>We will be holding a 2-day GoldSim training course in Denver, Colorado on October 1 and 2, 2012. The training will be held at a hotel conference room in the <a href="http://www.marriott.com/hotels/travel/denrk-residence-inn-denver-southwest-lakewood/" target="_blank">Residence Inn Denver Southwest/Lakewood</a> (7050 West Hampden Avenue, Lakewood).  The two-day workshop will provide a general introduction to the use of GoldSim, tailored to the needs of the group. It will be of value to new users, and will also serve as an excellent refresher course (and introduction to new features) for existing users. A brief agenda can be found <a href="http://www.goldsim.com/downloads/documents/Denver_2012.pdf" target="_blank">here</a>.</p>
  <p>The cost will be $1000 per attendee for the 2-day session. The price includes breakfast and lunch each day.</p>
 
  <p>Attendees are expected to bring their own laptop computers. Free 45-day temporary licenses will be provided to any attendees who require a license.</p>
  <p>To register for the training session, please fill out and submit this form. We look forward to seeing you in Denver!</p>

  <fieldset>
    <legend>Contact Information</legend>

    <%-- NAME BLOCK: FNAME, LNAME --%>
    <GoldSimForm:NameBlock ID="NameBlock" RunAt="Server" />

    <%-- ORGANIZATION --%>
    <GoldSimForm:Organization ID="Organization" RunAt="Server" />

    <%-- ADDRESS BLOCK: ADDRESS1, ADDRESS2, CITY, STATE/PROVINCE, ZIP/POSTAL --%>
    <GoldSimForm:AddressBlock ID="AddressBlock" RunAt="Server" />

    <%-- COUNTRY SELECTION --%>
    <GoldSimForm:CountrySelection ID="CountrySelection" SelectedValue="United States of America" RunAt="Server" />

    <%-- EMAIL/CONFIRM EMAIL --%>
    <GoldSimForm:Email ID="Email" ShowEmailConfirm="true" RunAt="Server" />

    <%-- PHONE --%>
    <GoldSimForm:Phone ID="Phone" RunAt="Server" />

    <%-- FAX --%>
    <GoldSimForm:Fax ID="Fax" RunAt="Server" />

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
      <GoldSimForm:CountrySelection ID="APContactCountrySelection" SelectedValue="United States of America" ValidationGroup="AccountsPayable" RunAt="Server" />

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