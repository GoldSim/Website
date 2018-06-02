<%@ Page Language="C#" Title="GoldSim User Conference Registration" MasterPageFile="/Forms/Common/Templates/Forms.Layout.Master" %>

<%@ MasterType  VirtualPath="/Forms/Common/Templates/Forms.Layout.Master" %>

<Script RunAt="Server">
/*==============================================================================================================================
| FORM: GOLDSIM USER CONFERENCE REGISTRATION
|
| Author        Katherine Trunkey, Ignia LLC (Katie@ignia.com)
| Client        GoldSim
| Project       Site Relaunch
|
| Purpose       Provides form template for GoldSim training session registration.
|
>===============================================================================================================================
| Revisions     Date        Author                      Comments
| - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
|               11.24.08    Jeremy Caney                Initial version template.
|               07.27.10    Katherine Trunkey           Adapted for form template.
|               04.06.15    Rick Kossik                 Changed Training Registration to Conference Registration
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
  Master.SubmitLabel            = "Submit Registration";
  Master.EmailSubject           = "User Conference Registration Request";
  Master.EmailSender            = ((IgniaFormField)Email.FindControl("Email")).Value;
  Master.EmailRecipient         = "conference@goldsim.com";
  Master.SuccessUrl             = "/Topic/755/";

}

</Script>

<asp:Content ContentPlaceHolderId="PageHead" RunAt="Server">
  <script type="text/javascript">

    function ShowPaymentInstructions(value) {
      var paymentInstructionsBox = document.getElementById('<%= PaymentInstructions.ClientID %>');
      if (value == 'C_C') {
        paymentInstructionsBox.innerHTML        = 'If paying by credit card, call 1-425-295-6985 (-8 hours GMT) or fax 1-425-642-8073 to complete transaction.';
      }
      else {
        paymentInstructionsBox.innerHTML        = ''; //'If paying by invoice, Purchase Order information must be filled out below.';
      }
      paymentInstructionsBox.style.display      = 'block';
    }

  </script>
</asp:Content>

<asp:Content ContentPlaceHolderID="Subtitle" runat="server">
  <p class="subtitle">Seattle, Washington: September 10-12, 2018</p>
</asp:Content>

<asp:Content ContentPlaceHolderId="Content" runat="Server">

  <p>The conference itself will be held September 11 and 12.</p>
  <p>The conference will be preceded (on September 10) by an optional one-day Basic Training workshop focusing on novice users (and could also serve as a refresher course for more experienced users who have not used GoldSim recently). Attendees of this training session are expected to bring their own laptop computers. Free 45-day temporary licenses will be provided to any attendees who require a license.</p>
  <p>Attendees are responsible for arranging their own lodging. <strong>A limited number of discounted hotel rooms will be available at the Washington Athletic Club (the conference venue).</strong> Other accommodation options are also available nearby (see <a href="/topic/2274" target="_blank">lodging options</a>).</p>
  <p>We look forward to seeing you in Seattle!</p>

  <fieldset>
    <legend>Contact Information</legend>
    <div class="grid-x grid-margin-x">

      <%-- NAME BLOCK: FNAME, LNAME --%>
      <GoldSimForm:NameBlock ID="NameBlock" RunAt="Server" />

      <%-- ORGANIZATION --%>
      <div class="cell">
        <GoldSimForm:Organization ID="Organization" RunAt="Server" />
      </div>

      <%-- ADDRESS BLOCK: ADDRESS1, ADDRESS2, CITY, STATE/PROVINCE, ZIP/POSTAL --%>
      <GoldSimForm:AddressBlock ID="AddressBlock" RunAt="Server" />

      <%-- COUNTRY SELECTION --%>
      <div class="cell">
        <GoldSimForm:CountrySelection ID="CountrySelection" IsRequired="true" RunAt="Server" />
      </div>

      <%-- EMAIL/CONFIRM EMAIL --%>
      <GoldSimForm:Email ID="Email" ShowEmailConfirm="true" RunAt="Server" />

      <%-- PHONE --%>
      <div class="medium-6 cell">
        <GoldSimForm:Phone ID="Phone" RunAt="Server" />
      </div>

      <%-- FAX --%>
      <div class="medium-6 cell">
        <GoldSimForm:Fax ID="Fax" RunAt="Server" />
      </div>

    </div>
  </fieldset>

  <fieldset>
    <legend>Poster Session</legend>
    <div class="grid-x grid-margin-x">
      <div class="cell">
        <div id="PosterSessionFieldContainer" class="FieldContainer checkbox">
          <asp:CheckBox ID="PosterSessionCheck" ClientIDMode="Static" RunAt="Server" />
          <label for="PosterSessionCheck" RunAt="Server">I am interested in submitting a poster.</label>
        </div>
        <p style="margin-top: 1rem;"><em>We will follow up with instructions for submitting and presenting your poster at the conference.</em> <strong>Poster presenters will be entered into a drawing for a $500 Amazon gift certificate. Attendees will also vote on a Best Poster, with the winner receiving a $500 Amazon gift certificate.</strong></p>
      </div>
    </div>
  </fieldset>

  <fieldset>
    <legend>Advanced Training Topics</legend>
    <div class="grid-x grid-margin-x">

      <div class="cell">
        <p>The first day of the conference (September 11) will consist of two parallel sessions on advanced GoldSim applications and modeling techniques. Attendees will be able to move between the two sessions (each topic will last approximately 1 hour) in order to select the topics that best align with their interests. Topics will be based on attendee requests specified below:</p>
      </div>

      <div class="medium-6 cell">
        <div class="checkbox">
          <asp:CheckBox ID="TopicScriptsDllsCheck" ClientIDMode="Static" value="ScriptsAndDLLs" RunAt="Server" />
          <label for="TopicScriptsDllsCheck" RunAt="Server">Scripts and DLLs</label>
        </div>
        <div class="checkbox">
          <asp:CheckBox ID="TopicDiscreteEventModelingCheck" ClientIDMode="Static" value="DiscreteEventModeling" RunAt="Server" />
          <label for="TopicDiscreteEventModelingCheck" RunAt="Server">Discrete Event Modeling</label>
        </div>
        <div class="checkbox">
          <asp:CheckBox ID="TopicModelingScenariosCheck" ClientIDMode="Static" value="ModelingScenarios" RunAt="Server" />
          <label for="TopicModelingScenariosCheck" RunAt="Server">Modeling Scenarios</label>
        </div>
        <div class="checkbox">
          <asp:CheckBox ID="TopicAdvancedTimesteppingTechniquesCheck" ClientIDMode="Static" value="AdvancedTimesteppingTechniques" RunAt="Server" />
          <label for="TopicAdvancedTimesteppingTechniquesCheck" RunAt="Server">Advanced Timestepping Techniques</label>
        </div>
        <div class="checkbox">
          <asp:CheckBox ID="TopicCalibratingModelCheck" ClientIDMode="Static" value="CalibratingAModel" RunAt="Server" />
          <label for="TopicCalibratingModelCheck" RunAt="Server">Calibrating a Model</label>
        </div>
        <div class="checkbox">
          <asp:CheckBox ID="TopicBuildingEffectiveDashboardsCheck" ClientIDMode="Static" value="BuildingEffectiveDashboards" RunAt="Server" />
          <label for="TopicBuildingEffectiveDashboardsCheck" RunAt="Server">Building Effective Dashboards</label>
        </div>
        <div class="checkbox" style="margin-top: 6px;">
          <asp:CheckBox ID="TopicUnderstandingControllingCausalityCheck" ClientIDMode="Static" value="UnderstandingControllingCausality" RunAt="Server" />
          <label for="TopicUnderstandingControllingCausalityCheck" RunAt="Server">Understanding and Controlling the Causality Sequence</label>
        </div>
        <div class="checkbox">
          <asp:CheckBox ID="TopicIntroductionReliabilityModelingCheck" ClientIDMode="Static" value="IntroductionReliabilityModeling" RunAt="Server" />
          <label for="TopicIntroductionReliabilityModelingCheck" RunAt="Server">Introduction to Reliability Modeling</label>
        </div>
        <div class="checkbox" style="margin-top: 6px;">
          <asp:CheckBox ID="TopicLinkingGoldSimPhreeqcCheck" ClientIDMode="Static" value="LinkingGoldSimPHREEQC" RunAt="Server" />
          <label for="TopicLinkingGoldSimPhreeqcCheck" RunAt="Server">Linking GoldSim to PHREEQC for Geochemical Calculations</label>
        </div>
      </div>
      <div class="medium-6 cell">
        <div class="checkbox" style="margin-top: 6px;">
          <asp:CheckBox ID="TopicModelingPumpsEnergyUseCheck" ClientIDMode="Static" value="ModelingPumpsEnergyUse" RunAt="Server" />
          <label for="TopicModelingPumpsEnergyUseCheck" RunAt="Server">Modeling Pumps and Energy Use in a Water Management Model</label>
        </div>
        <div class="checkbox" style="margin-top: 6px;">
          <asp:CheckBox ID="TopicRepresentingReservoirDamOperationsCheck" ClientIDMode="Static" value="RepresentingReservoirDamOperations" RunAt="Server" />
          <label for="TopicRepresentingReservoirDamOperationsCheck" RunAt="Server">Representing Reservoir and Dam Operations</label>
        </div>
        <div class="checkbox">
          <asp:CheckBox ID="TopicModelingRunoffCheck" ClientIDMode="Static" value="ModelingRunoff" RunAt="Server" />
          <label for="TopicModelingRunoffCheck" RunAt="Server">Modeling Runoff</label>
        </div>
        <div class="checkbox">
          <asp:CheckBox ID="TopicStochasticWeatherGenerationCheck" ClientIDMode="Static" value="StochasticWeatherGeneration" RunAt="Server" />
          <label for="TopicStochasticWeatherGenerationCheck" RunAt="Server">Stochastic Weather Generation</label>
        </div>
        <div class="checkbox">
          <asp:CheckBox ID="TopicRiverRoutingCheck" ClientIDMode="Static" value="RiverRouting" RunAt="Server" />
          <label for="TopicRiverRoutingCheck" RunAt="Server">River Routing</label>
        </div>
        <div class="checkbox">
          <asp:CheckBox ID="TopicRepresentingFlowNetworksCheck" ClientIDMode="Static" value="Representing Flow Networks" RunAt="Server" />
          <label for="TopicRepresentingFlowNetworksCheck" RunAt="Server">Representing Flow Networks</label>
        </div>
        <div class="checkbox">
          <asp:CheckBox ID="TopicModelingPopulationGrowthCheck" ClientIDMode="Static" value="ModelingPopulationGrowth" RunAt="Server" />
          <label for="TopicModelingPopulationGrowthCheck" RunAt="Server">Modeling Population Growth</label>
        </div>
        <div class="checkbox">
          <asp:CheckBox ID="TopicOtherCheck" ClientIDMode="Static" value="Other" RunAt="Server" />
          <label for="TopicOtherCheck" RunAt="Server">Other (Please Specify)</label>
        </div>
      </div>

      <div class="cell" style="margin-top: 1rem;">
        <Ignia:FormField
          ID                    = "TopicOther"
          LabelName             = "Other Topic(s) of Interest"
          AccessKey             = "O"
          MaxLength             = "150"
          FieldSize             = "320"
          Required              = "False"
          CssClass              = "TextField"
          RunAt                 = "Server"
        />
      </div>

    </div>
  </fieldset>

  <fieldset>

   <legend>Conference Options</legend>

    <!-- Academic Discount -->
    <div class="checkbox" style="margin-bottom: 1rem;">
      <asp:CheckBox ID="AcademicDiscountCheck" ClientIDMode="Static" RunAt="Server" />
      <label for="AcademicDiscountCheck" RunAt="Server">Apply student discount</label>
    </div>
    <!-- /Academic Discount -->

    <%-- SESSION TYPE --%>
    <asp:Label ID="SessionSelect" CssClass="Session Select" style="display: none;" RunAt="Server" />
    <asp:RadioButtonList ID="SessionTypeSelection" AppendDataBoundItems="true" RepeatLayout="Flow" RepeatDirection="Vertical" CssClass="radio" RunAt="Server">
      <asp:ListItem Value="User Conference Only">User Conference Only (September 11-12): <span id="ConferenceOnlyPrice">$1,250</span></asp:ListItem>
      <asp:ListItem Value="Basic Training and Conference">Basic Training and Conference (September 10-12): <span id="TrainingAndConferencePrice">$1,500</span></asp:ListItem>
    </asp:RadioButtonList>

    <p style="margin-top: 1rem;">Costs include breakfast and lunch each day, as well as dinner during the social events on the evenings of September 10 and 11. Spouses/partners can also attend the two dinners for a modest fee.</p>

  </fieldset>

  <fieldset id="PaymentInformation">
    <legend>Payment Information</legend>
    <div class="grid-x grid-margin-x">

      <%-- PAYMENT TYPE --%>
      <div class="cell">
        <asp:RadioButtonList ID="PaymentTypeSelection" AppendDataBoundItems="true" RepeatLayout="Flow" RepeatDirection="Vertical" ClientIDMode="Static" CssClass="radio" RunAt="Server">
          <asp:ListItem Value="Credit_Card" onclick="ShowPaymentInstructions('C_C')">Credit Card</asp:ListItem>
          <asp:ListItem Value="Invoice_Self" onclick="ShowPaymentInstructions('I_S')">Invoice Me</asp:ListItem>
          <%-- <asp:ListItem Value="Invoice_AP" onclick="ShowPaymentInstructions('I_AP')">Invoice Accounts Payable</asp:ListItem> --%>
        </asp:RadioButtonList>
        <asp:Label ID="PaymentInstructions" CssClass="Payment instructions" style="display: none;" RunAt="Server" />
      </div>

      <%-- TAX ID
      <div class="medium-6 cell">
        <Ignia:FormField
          ID                    = "TaxID"
          LabelName             = "*Purchaser Tax ID"
          AccessKey             = "T"
          MaxLength             = "150"
          FieldSize             = "320"
          CssClass              = "TextField"
          SkinId                = "BoxedPairs"
          RunAt                 = "Server"
          />
      </div>
      --%>

      <%-- PO NUMBER --%>
      <div class="cell" style="margin-top: 1rem;">
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
          LabelName             = "Additional Instructions (e.g., interest in spouse/partner attending dinners)"
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

  <div class="grid grid-margin-x" style="margin-top: 1rem;">
    <div class="cell">
      <p>Note that you will not be charged when you submit this form. This order is not valid until accepted by GoldSim Technology Group. We will contact you to complete the transaction.</p>
    </div>
  </div>

</asp:Content>

<asp:Content ContentPlaceHolderID="PageScripts" runat="server">
  <script>
    $(function () {

      /**
       * Sets conditionally required fields to disabled and not required by default
       */
      toggleDisabled('input[id$="PONumber_Field"]', true);

      /**
       * Conditionally enables PO and AP fields if invoice payment choice is selected
       */
      $('[id^="PaymentTypeSelection"]').change(function () {
        if ($(this).attr('id') === 'PaymentTypeSelection_1' && $(this).is(':checked')) {
          setTimeout(function() {
            toggleDisabled('input[id$="PONumber_Field"]', false);
          }, 250);
        }
      });

      /**
       * Update registration fees and price increase amount if the academic discount checkbox is selected
       */
      var
        conferenceOnlyPriceLabel                = '#ConferenceOnlyPrice',
        conferenceOnlyRate                      = '$850',
        conferenceOnlyDiscountRate              = '$425',
        trainingAndConferencePriceLabel         = '#TrainingAndConferencePrice',
        trainingAndConferenceRate               = '$1,100',
        trainingAndConferenceDiscountRate       = '$550',
        priceIncreaseLabel                      = '#PriceIncrease',
        priceIncreaseRate                       = '$400',
        priceIncreaseDiscountRate               = '$200';
      $('#AcademicDiscountCheck').change(function () {
        if ($(this).is(':checked')) {
          $(conferenceOnlyPriceLabel).text(conferenceOnlyDiscountRate);
          $(trainingAndConferencePriceLabel).text(trainingAndConferenceDiscountRate);
          $(priceIncreaseLabel).text(priceIncreaseDiscountRate);
        }
        else {
          $(conferenceOnlyPriceLabel).text(conferenceOnlyRate);
          $(trainingAndConferencePriceLabel).text(trainingAndConferenceRate);
          $(priceIncreaseLabel).text(priceIncreaseRate);
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
