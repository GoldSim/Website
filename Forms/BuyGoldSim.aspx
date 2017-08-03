<%@ Page Language="C#" Title="Buy GoldSim" MasterPageFile="/Forms/Common/Templates/Forms.Layout.Master" %>

<%@ MasterType  VirtualPath="/Forms/Common/Templates/Forms.Layout.Master" %>

<Script RunAt="Server">
/*==============================================================================================================================
| FORM: GOLDSIM PRODUCT PURCHASE REQUEST
|
| Author        Katherine Trunkey, Ignia LLC (Katie@ignia.com)
| Client        GoldSim
| Project       Site Relaunch
|
| Purpose       Provides form template for GoldSim software purchase application.
|
>===============================================================================================================================
| Revisions     Date        Author                      Comments
| - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
|               11.24.08    Jeremy Caney                Initial version template.
|               07.27.10    Katherine Trunkey           Adapted for form template.
|               02.12.16    Katherine Trunkey           Removed SelectionTotalDisplay setting from ProductConfiguration.
|               MM.DD.YY    FName LName                 Description
\-----------------------------------------------------------------------------------------------------------------------------*/

/*==============================================================================================================================
| DECLARE PUBLIC PROPERTIES
>===============================================================================================================================
| Declare any public/global variables prior to page initiation; these will be treated as properties in the control
\-----------------------------------------------------------------------------------------------------------------------------*/

/*==============================================================================================================================
| PAGE LOAD
>===============================================================================================================================
| Provide handling for functions that must run prior to page load.  This includes dynamically constructed controls.
\-----------------------------------------------------------------------------------------------------------------------------*/
  void Page_Load(Object Src, EventArgs E) {

  /*----------------------------------------------------------------------------------------------------------------------------
  | SET MASTER PROPERTIES
  \---------------------------------------------------------------------------------------------------------------------------*/
  //Associated topic for navigation -- sends to Forms.Layout.Master and then on to Page.Layout.master
    Master.FormTopic    =  Ignia.Topics.TopicRepository.RootTopic.GetTopic(754);
  //Submit button label
    Master.SubmitLabel          = "Send Purchase Request";
  //Submission email subject
    Master.EmailSubject         = "GoldSim Buyer Form: Product Purchase Request";
    // Master.EmailSender       = ((IgniaFormField)BuyerEmail.FindControl("Email")).Value;
  //Redirect URL
    Master.SuccessUrl           = "/Topic/755/";
  //Wireup form processor
  //Master.ProcessForm          = ProcessForm;

    }

/*==============================================================================================================================
| VALIDATOR: LICENSEE
>===============================================================================================================================
| Ensures that the licensee section is only required if the purchaser is not purchasing the license for themselves.
\-----------------------------------------------------------------------------------------------------------------------------*/
  void LicenseeValidator(object source, ServerValidateEventArgs args) {
    if (LicenseeTypeSelection.Items.FindByValue("Third_Party").Selected) {
      Page.Validate("Licensee");
      }
    else {
      args.IsValid = true;
      }
    }

/*==============================================================================================================================
| VALIDATOR: ACCOUNTS PAYABLE
>===============================================================================================================================
| Ensures that the accounts payable section is only required if the user selected "Invoice Accounts Payable".
\-----------------------------------------------------------------------------------------------------------------------------*/
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

  <h2 class="Subtitle">GoldSim Purchase Request Form</h2>
  <p>In order to purchase GoldSim, please fill out this form. After we receive the form we will contact you to complete the transaction.</p>
  <em class="Instructions">For information regarding how we use your email address and other contact information, you can refer to our <a href="../Topic/613/">privacy policy</a>.</em>

  <fieldset>
    <legend>Product Selection</legend>

    <%-- SHOW DISCOUNT INFORMATION ON GOLDSIM RESEARCH MULTIPLE LICENSE SELECTION --%>
    <p>Multiple license purchases may be subject to volume discounts. Upon receipt of your purchase request, GoldSim will contact you with a final price quote.</p>

    <%-- PRODUCT/COMPONENTS SELECTION/CONFIGURATION --%>
    <GoldSimForm:ProductConfiguration ID="ProductConfiguration" RunAt="Server" />

  </fieldset>

  <fieldset>
    <legend>Buyer Contact Information</legend>

    <%-- NAME BLOCK: FNAME, LNAME --%>
    <GoldSimForm:NameBlock ID="BuyerNameBlock" RunAt="Server" />

    <%-- ORGANIZATION --%>
    <GoldSimForm:Organization ID="BuyerOrganization" RunAt="Server" />

    <%-- ADDRESS BLOCK: ADDRESS1, ADDRESS2, CITY, STATE/PROVINCE, ZIP/POSTAL --%>
    <GoldSimForm:AddressBlock ID="BuyerAddressBlock" RunAt="Server" />

    <%-- COUNTRY SELECTION --%>
    <GoldSimForm:CountrySelection ID="BuyerCountrySelection" RunAt="Server" />

    <%-- EMAIL/CONFIRM EMAIL --%>
    <GoldSimForm:Email ID="BuyerEmail" ShowEmailConfirm="true" RunAt="Server" />

    <%-- PHONE --%>
    <GoldSimForm:Phone ID="BuyerPhone" RunAt="Server" />

    <%-- FAX --%>
    <GoldSimForm:Fax ID="BuyerFax" RunAt="Server" />

  </fieldset>

  <fieldset>
    <legend>Licensee Information</legend>

    <%-- LICENSEE TYPE --%>
    <asp:RadioButtonList ID="LicenseeTypeSelection" RepeatLayout="Flow" RepeatDirection="Vertical" RunAt="Server">
      <asp:ListItem Value="Self">I am the primary technical contact for this license purchase.</asp:ListItem>
      <asp:ListItem Value="Third_Party">I am acting as a third party (purchasing agent or distributor) for this purchase.</asp:ListItem>
    </asp:RadioButtonList>
    <asp:RequiredFieldValidator ControlToValidate="LicenseeTypeSelection" RunAt="Server" />

    <asp:CustomValidator
      ControlToValidate         = "LicenseeTypeSelection"
      OnServerValidate          = "LicenseeValidator"
      ErrorMessage              = "If you are a third party purchasing agent or distributor, then the contact informaiton for the licensee is required."
      Display                   = "Dynamic"
      RunAt                     = "Server"
      />

    <fieldset ID="UserContactInfo">
      <legend>Intended User Contact Information</legend>

      <%-- NAME BLOCK: FNAME, LNAME --%>
      <GoldSimForm:NameBlock ID="UserNameBlock" ValidationGroup="Licensee" RunAt="Server" />

      <%-- ORGANIZATION --%>
      <GoldSimForm:Organization ID="UserOrganization" ValidationGroup="Licensee" RunAt="Server" />

      <%-- ADDRESS BLOCK: ADDRESS1, ADDRESS2, CITY, STATE/PROVINCE, ZIP/POSTAL --%>
      <GoldSimForm:AddressBlock ID="UserAddressBlock" ValidationGroup="Licensee" RunAt="Server" />

      <%-- COUNTRY SELECTION --%>
      <GoldSimForm:CountrySelection ID="UserCountrySelection" ValidationGroup="Licensee" RunAt="Server" />

      <%-- EMAIL --%>
      <GoldSimForm:Email ID="UserEmail" ValidationGroup="Licensee" RunAt="Server" />

      <%-- PHONE --%>
      <GoldSimForm:Phone ID="UserPhone" ValidationGroup="Licensee" RunAt="Server" />

      <%-- FAX --%>
      <GoldSimForm:Fax ID="UserFax" ValidationGroup="Licensee" RunAt="Server" />

    </fieldset>

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

    <fieldset ID="POInfo">
      <legend>Purchase Order Information</legend>

      <%-- TAX ID --%>
      <Ignia:FormField
        ID                      = "TaxID"
        LabelName               = "Purchaser Tax ID"
        AccessKey               = "T"
        MaxLength               = "150"
        FieldSize               = "320"
        CssClass                = "TextField"
        SkinId                  = "BoxedPairs"
        RunAt                   = "Server"
        />

      <%-- PO NUMBER --%>
      <Ignia:FormField
        ID                      = "PONumber"
        LabelName               = "Purchase Order Number"
        AccessKey               = "P"
        MaxLength               = "150"
        FieldSize               = "320"
        CssClass                = "TextField"
        SkinId                  = "BoxedPairs"
        RunAt                   = "Server"
        />

      <%-- PO NOTES --%>
      <Ignia:FormField
        ID                      = "PurchaseNotes"
        LabelName               = "Other Purchase Notes"
        AccessKey               = "N"
        MaxLength               = "150"
        FieldSize               = "320"
        CssClass                = "TextField"
        SkinId                  = "BoxedPairs"
        RunAt                   = "Server"
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
      <GoldSimForm:CountrySelection ID="APContactCountrySelection" ValidationGroup="AccountsPayable" RunAt="Server" />

      <%-- EMAIL --%>
      <GoldSimForm:Email ID="APContactEmail" ValidationGroup="AccountsPayable" RunAt="Server" />

      <%-- PHONE --%>
      <GoldSimForm:Phone ID="APContactPhone" ValidationGroup="AccountsPayable" RunAt="Server" />

      <%-- FAX --%>
      <GoldSimForm:Fax ID="APContactFax" ValidationGroup="AccountsPayable" RunAt="Server" />

    </fieldset>

  </fieldset>


</asp:Content>