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
  //Master.FormTopic    =  Ignia.Topics.TopicRepository.RootTopic.GetTopic(754);
  //Submit button label
    Master.SubmitLabel          = "Send Purchase Request";
  //Submission email subject
    Master.EmailSubject         = "GoldSim Buyer Form: Product Purchase Request";
    // Master.EmailSender       = ((IgniaFormField)BuyerEmail.FindControl("Email")).Value;
  //Redirect URL
    Master.SuccessUrl           = "/Topic/755/";
    Master.EmailRecipient       = "katherine.trunkey@ignia.com";
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

<asp:Content ContentPlaceHolderID="Subtitle" runat="server">
  <p class="subtitle">GoldSim Purchase Request Form</p>
</asp:Content>

<asp:Content ContentPlaceHolderId="Content" runat="Server">

  <p>In order to purchase GoldSim, please fill out this form. After we receive the form we will contact you to complete the transaction.</p>
  <p><em>For information regarding how we use your email address and other contact information, you can refer to our <a href="/Topic/4222/">privacy policy</a>.</em></p>

  <fieldset>
    <legend>Product Selection</legend>
    <div class="grid-x grid-margin-x">

      <%-- SHOW DISCOUNT INFORMATION ON GOLDSIM RESEARCH MULTIPLE LICENSE SELECTION --%>
      <div class="cell"><p>Multiple license purchases may be subject to volume discounts. Upon receipt of your purchase request, GoldSim will contact you with a final price quote.</p></div>

      <%-- PRODUCT/COMPONENTS SELECTION/CONFIGURATION --%>
      <GoldSimForm:ProductConfiguration ID="ProductConfiguration" RunAt="Server" />

    </div>
  </fieldset>

  <fieldset>
    <legend>Buyer Contact Information</legend>
    <div class="grid-x grid-margin-x">

      <%-- NAME BLOCK: FNAME, LNAME --%>
      <GoldSimForm:NameBlock ID="BuyerNameBlock" RunAt="Server" />

      <%-- ORGANIZATION --%>
      <div class="cell">
        <GoldSimForm:Organization ID="BuyerOrganization" RunAt="Server" />
      </div>

      <%-- ADDRESS BLOCK: ADDRESS1, ADDRESS2, CITY, STATE/PROVINCE, ZIP/POSTAL --%>
      <GoldSimForm:AddressBlock ID="BuyerAddressBlock" RunAt="Server" />

      <%-- COUNTRY SELECTION --%>
      <div class="cell">
        <GoldSimForm:CountrySelection ID="BuyerCountrySelection" RunAt="Server" />
      </div>

      <%-- EMAIL/CONFIRM EMAIL --%>
      <GoldSimForm:Email ID="BuyerEmail" ShowEmailConfirm="true" RunAt="Server" />

      <%-- PHONE --%>
      <div class="medium-6 cell">
        <GoldSimForm:Phone ID="BuyerPhone" RunAt="Server" />
      </div>

      <%-- FAX --%>
      <div class="medium-6 cell">
        <GoldSimForm:Fax ID="BuyerFax" RunAt="Server" />
      </div>

    </div>
  </fieldset>

  <fieldset>
    <legend>Licensee Information</legend>
    <div class="grid-x grid-margin-x">

      <%-- LICENSEE TYPE --%>
      <div class="cell">
        <asp:RadioButtonList ID="LicenseeTypeSelection" RepeatLayout="Flow" RepeatDirection="Vertical" ClientIDMode="Static" CssClass="radio" RunAt="Server">
          <asp:ListItem Value="Self">I am the primary technical contact for this license purchase.</asp:ListItem>
          <asp:ListItem Value="Third_Party">I am acting as a third party (purchasing agent or distributor) for this purchase.</asp:ListItem>
        </asp:RadioButtonList>
        <asp:RequiredFieldValidator ControlToValidate="LicenseeTypeSelection" RunAt="Server" />
        <asp:CustomValidator
          ControlToValidate     = "LicenseeTypeSelection"
          OnServerValidate      = "LicenseeValidator"
          ErrorMessage          = "If you are a third party purchasing agent or distributor, then the contact information for the licensee is required."
          Display               = "Dynamic"
          RunAt                 = "Server"
          />
      </div>

    </div>
  </fieldset>

  <fieldset id="UserContactInfo">
    <legend>Intended User Contact Information</legend>
    <div class="grid-x grid-margin-x">

      <%-- NAME BLOCK: FNAME, LNAME --%>
      <GoldSimForm:NameBlock ID="UserNameBlock" ValidationGroup="Licensee" RunAt="Server" />

      <%-- ORGANIZATION --%>
      <div class="cell">
        <GoldSimForm:Organization ID="UserOrganization" ValidationGroup="Licensee" RunAt="Server" />
      </div>

      <%-- ADDRESS BLOCK: ADDRESS1, ADDRESS2, CITY, STATE/PROVINCE, ZIP/POSTAL --%>
      <GoldSimForm:AddressBlock ID="UserAddressBlock" ValidationGroup="Licensee" RunAt="Server" />

      <%-- COUNTRY SELECTION --%>
      <div class="medium-6 cell">
        <GoldSimForm:CountrySelection ID="UserCountrySelection" ValidationGroup="Licensee" RunAt="Server" />
      </div>

      <%-- EMAIL --%>
      <GoldSimForm:Email ID="UserEmail" ValidationGroup="Licensee" SplitLayout="true" RunAt="Server" />

      <%-- PHONE --%>
      <div class="medium-6 cell">
        <GoldSimForm:Phone ID="UserPhone" ValidationGroup="Licensee" RunAt="Server" />
      </div>

      <%-- FAX --%>
      <div class="medium-6 cell">
        <GoldSimForm:Fax ID="UserFax" ValidationGroup="Licensee" RunAt="Server" />
      </div>

    </div>
  </fieldset>


  <fieldset>
    <legend>Payment Information</legend>
    <div class="grid-x grid-margin-x">

      <%-- PAYMENT TYPE --%>
      <div class="cell">
        <asp:RadioButtonList ID="PaymentTypeSelection" AppendDataBoundItems="true" RepeatLayout="Flow" RepeatDirection="Vertical" ClientIDMode="Static" CssClass="radio" RunAt="Server">
          <asp:ListItem Value="Credit_Card" onclick="ShowPaymentInstructions('C_C')">Credit Card</asp:ListItem>
          <asp:ListItem Value="Invoice_Self" onclick="ShowPaymentInstructions('I_S')">Invoice Me</asp:ListItem>
          <asp:ListItem Value="Invoice_AP" onclick="ShowPaymentInstructions('I_AP')">Invoice Accounts Payable</asp:ListItem>
        </asp:RadioButtonList>
        <asp:Label ID="PaymentInstructions" CssClass="Payment instructions" style="display: none;" RunAt="Server" />
      </div>

    </div>
  </fieldset>

  <fieldset ID="POInfo">
    <legend>Purchase Order Information</legend>
    <div class="grid-x grid-margin-x">

      <%-- TAX ID --%>
      <div class="medium-6 cell">
        <Ignia:FormField
          ID                    = "TaxID"
          LabelName             = "Purchaser Tax ID"
          AccessKey             = "T"
          MaxLength             = "150"
          FieldSize             = "320"
          CssClass              = "TextField"
          SkinId                = "BoxedPairs"
          RunAt                 = "Server"
          />
      </div>

      <%-- PO NUMBER --%>
      <div class="medium-6 cell">
        <Ignia:FormField
          ID                    = "PONumber"
          LabelName             = "Purchase Order Number"
          AccessKey             = "P"
          MaxLength             = "150"
          FieldSize             = "320"
          CssClass              = "TextField"
          SkinId                = "BoxedPairs"
          RunAt                 = "Server"
          />
      </div>

      <%-- PO NOTES --%>
      <div class="cell">
        <Ignia:FormField
          ID                    = "PurchaseNotes"
          LabelName             = "Other Purchase Notes"
          AccessKey             = "N"
          MaxLength             = "150"
          FieldSize             = "320"
          CssClass              = "TextField"
          SkinId                = "BoxedPairs"
          RunAt                 = "Server"
          />
      </div>

      <%-- PAPER COPY CHECK --%>
      <div ID="PaperCopyField" class="cell">
        <p class="instructions"><em>NOTE</em>: GoldSim's standard method of invoicing and providing receipts is via email.</p>
        <div class="checkbox">
          <asp:CheckBox ID="PaperCopyCheck" ClientIDMode="Static" RunAt="Server" />
          <label for="PaperCopyCheck" RunAt="Server">I would prefer a paper invoice or receipt.</label>
        </div>
      </div>

    </div>
  </fieldset>

  <fieldset ID="APContactInfo">
    <legend>Accounts Payable Contact Information</legend>
    <div class="grid-x grid-margin-x">

      <%-- NAME BLOCK: FNAME, LNAME --%>
      <GoldSimForm:NameBlock ID="APContactNameBlock" ValidationGroup="AccountsPayable" RunAt="Server" />
      <div class="cell">
        <asp:CustomValidator
          ControlToValidate     = "PaymentTypeSelection"
          OnServerValidate      = "AccountsPayableValidator"
          ErrorMessage          = "If 'Invoice Accounts Payable' is selected then the Accounts Payable contact information must be completed."
          Display               = "Dynamic"
          RunAt                 = "Server"
          />
      </div>

      <%-- ORGANIZATION --%>
      <div class="cell">
        <GoldSimForm:Organization ID="APContactOrganization" ValidationGroup="AccountsPayable" RunAt="Server" />
      </div>

      <%-- ADDRESS BLOCK: ADDRESS1, ADDRESS2, CITY, STATE/PROVINCE, ZIP/POSTAL --%>
      <GoldSimForm:AddressBlock ID="APContactAddressBlock" ValidationGroup="AccountsPayable" RunAt="Server" />

      <%-- COUNTRY SELECTION --%>
      <div class="medium-6 cell">
        <GoldSimForm:CountrySelection ID="APContactCountrySelection" ValidationGroup="AccountsPayable" RunAt="Server" />
      </div>

      <%-- EMAIL --%>
      <GoldSimForm:Email ID="APContactEmail" ValidationGroup="AccountsPayable" SplitLayout="true" RunAt="Server" />

      <%-- PHONE --%>
      <div class="medium-6 cell">
        <GoldSimForm:Phone ID="APContactPhone" ValidationGroup="AccountsPayable" RunAt="Server" />
      </div>

      <%-- FAX --%>
      <div class="medium-6 cell">
        <GoldSimForm:Fax ID="APContactFax" ValidationGroup="AccountsPayable" RunAt="Server" />
      </div>

  </fieldset>

</asp:Content>

<asp:Content ContentPlaceHolderID="PageScripts" runat="server">
  <script>
    $(function() {

      /**
       * Sets conditionally required fields to disabled by default
       */
      toggleDisabled('#UserContactInfo input, #UserContactInfo select', true);
      toggleDisabled('#POInfo input, #APContactInfo input, #APContactInfo select', true);

      /**
       * Conditionally enables Intended User Contact Information fields if third-party purchaser is selected
       */
      $('[id^="LicenseeTypeSelection"]').change(function() {
        if ($(this).attr('id') === 'LicenseeTypeSelection_1' && $(this).is(':checked')) {
          setTimeout(function() {
            toggleDisabled('#UserContactInfo input, #UserContactInfo select', false);
          }, 250);
        }
        else {
          toggleDisabled('#UserContactInfo input, #UserContactInfo select', true);
        }
      });

      /**
       * Conditionally enables PO and AP fields if invoice payment choice is selected
       */
      $('[id^="PaymentTypeSelection"]').change(function () {
        if ($(this).attr('id') === 'PaymentTypeSelection_0' && $(this).is(':checked')) {
          toggleDisabled('#POInfo input, #APContactInfo input, #APContactInfo select', true);
        }
        else if ($(this).is(':checked')) {
          setTimeout(function() {
            toggleDisabled('#POInfo input, #APContactInfo input, #APContactInfo select', false);
          }, 250);
        }
      });

    });

    /**
      * Sets disabled state on provided fields (selectors) and provided true/false state
      */
    function toggleDisabled(fields, disabled) {
      $(fields).prop('disabled', disabled);
    };

  </script>
</asp:Content>
