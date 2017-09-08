<%@ Control Language="C#" ClassName="ProductConfiguration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<!-- #Include Virtual="/Common/Global/Headers/Form.Headers.inc.aspx" -->

<Script RunAt="Server">
/*===========================================================================================================================
| COMMON FIELD TEMPLATE: PRODUCT CONFIGURATION
|
| Author:    Katherine Trunkey, Ignia LLC (Jeremy@ignia.com)
| Client     GoldSim
| Project    Site Relaunch | Forms
|
| Purpose :  Template control wrapper for product and license selection/configuration.
|
>============================================================================================================================
| Revisions     Date            Author                  Comments
| - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
|               08.04.10        Katherine Trunkey       Initial version template.
|               02.12.16        Katherine Trunkey       Updated license types and disabled total calculation
\--------------------------------------------------------------------------------------------------------------------------*/

/*===========================================================================================================================
| DECLARE PUBLIC FIELDS
>============================================================================================================================
| Public fields will be exposed as properties to user control
\--------------------------------------------------------------------------------------------------------------------------*/
  public        String          ValidationGroup         = "";
  public        bool            ShowQuickStartAddOn     = false;
  public        string          SelectionTotalDisplay   = "none";

/*===========================================================================================================================
| PAGE LOAD
>============================================================================================================================
| Provide handling for functions that must run prior to page load.  This includes dynamically constructed controls.
\--------------------------------------------------------------------------------------------------------------------------*/
  void Page_Load(Object Src, EventArgs E) {

    LicenseTypeSelection.Attributes.Add("onChange", "RecalculateTotal();");

    }


</Script>

<%-- PRODUCT VERSION --%>
<div class="medium-6 cell">
  <label for="ProductVersionSelection" class="required">*Product</label>
  <asp:DropDownList ID="ProductVersionSelection" onchange="RecalculateTotal()" runat="server">
    <asp:ListItem Value="Pro" Selected="True" onclick="RecalculateTotal()">GoldSim</asp:ListItem>
    <asp:ListItem Value="Research" onclick="RecalculateTotal()">GoldSim Research</asp:ListItem>
  </asp:DropDownList>
</div>

<%-- LICENSE TYPE --%>
<div class="medium-6 cell">
  <label for="LicenseTypeSelection" class="required">*License Type</label>
  <asp:DropDownList ID="LicenseTypeSelection" runat="server">
    <%--
    <asp:ListItem Value="Standalone" Selected="True" onclick="RecalculateTotal()">Standalone</asp:ListItem>
    <asp:ListItem Value="Floating" onclick="RecalculateTotal()">Floating (Network)</asp:ListItem>
    --%>
    <asp:ListItem Value="Desktop-Standalone" Selected="True" onclick="RecalculateTotal()">Desktop Standalone</asp:ListItem>
    <asp:ListItem Value="Enterprise-Standalone" onclick="RecalculateTotal()">Enterprise Standalone</asp:ListItem>
    <asp:ListItem Value="Concurrent-Network" onclick="RecalculateTotal()">Concurrent Network</asp:ListItem>
    <asp:ListItem Value="Leased-Standalone" onclick="RecalculateTotal()">Leased Standalone</asp:ListItem>
  </asp:DropDownList>
</div>

<%-- LICENSE QUANITY SELECTION --%>
<ajaxToolkit:ToolkitScriptManager EnablePartialRendering="true" LoadScriptsBeforeUI="false" ScriptMode="Release" ID="ToolkitScriptManagerControl" CombineScripts="false" runat="server" />
<div class="cell Product Spinner">
  <label for="LicenseQuantityBox">License Quantity</label>
  <asp:TextBox id="LicenseQuantityBox" type="number" value="1" onchange="RecalculateTotal()" runat="server" />
  <%--
  <input type="button" id="ProductDownButton" class="Down Button"  />
  <input type="button" id="ProductUpButton" class="Up Button"  />
  <ajaxToolkit:NumericUpDownExtender ID="LicenseQuanityUpDown1" runat="server"
    TargetControlID="LicenseQuantityBox"
    Width="50"
    Minimum="1"
    Maximum="50"
    TargetButtonUpID="ProductUpButton"
    TargetButtonDownID="ProductDownButton"
    />
  --%>
</div>

<%-- ADDITIONAL COMPONENTS SELECTION --%>
<div class="cell Checkboxes">
  <label>Add-on Modules:</label>
  <div class="checkbox">
    <asp:CheckBox ID="ReliabilityAddOnCheck" ClientIDMode="Static" value="Reliability" onclick="RecalculateTotal()" ValidationGroup=<%# ValidationGroup %> RunAt="Server" />
    <label for="ReliabilityAddOnCheck" RunAt="Server">Reliability Module</label>
  </div>
  <div class="checkbox">
    <asp:CheckBox ID="CTAddOnCheck" ClientIDMode="Static" value="CT" onclick="RecalculateTotal()" ValidationGroup=<%# ValidationGroup %> RunAt="Server" />
    <label for="CTAddOnCheck" RunAt="Server">Radionuclide Transport (RT) Module</label>
  </div>
  <div class="checkbox">
    <asp:CheckBox ID="RTAddOnCheck" ClientIDMode="Static" value="RT" onclick="RecalculateTotal()" ValidationGroup=<%# ValidationGroup %> RunAt="Server" />
    <label for="RTAddOnCheck" RunAt="Server">Contaminant Transport (CT) Module</label>
  </div>
  <div class="checkbox">
    <asp:CheckBox ID="DPPlusAddOnCheck" ClientIDMode="Static" value="DP-Plus" onclick="RecalculateTotal()" ValidationGroup=<%# ValidationGroup %> RunAt="Server" />
    <label for="DPPlusAddOnCheck" RunAt="Server">Distributed Processing (DP-Plus) Module</label>
  </div>
</div>

<div class="cell Total" style="display: <%= SelectionTotalDisplay %>">
  <%-- <label for="">Current Total:</label> --%><%-- = ProductConfigTotal.ClientID --%>
  <%-- asp:TextBox id="ProductConfigTotal" runat="server" / --%>
</div>

<script type="text/javascript">
<!--
  function RecalculateTotal() {

    var productPrice            = 0;
    if (document.getElementById("<%= ProductVersionSelection.ClientID %>").value == "Pro") {
      productPrice              = 4450;
      }
    else {
      productPrice              = 950;
      }

    var licenseQuantity         = document.getElementById("<%= LicenseQuantityBox.ClientID %>").value;

    var productTotalCost        = licenseQuantity * productPrice;

    var licenseType             = document.getElementById("<%= LicenseTypeSelection.ClientID %>").value;
    var licenseTypeAdjustment   = 1;
    if (licenseType == "Floating") {
      licenseTypeAdjustment     = 1.5;
      }

    var addonReliabilityCost    = 0;
    var addonCTCost             = 0;
    var addonRTCost             = 0;
    var addonDPPlusCost         = 0;

  //Reliability Module Add-on
    if (document.getElementById("<%= ReliabilityAddOnCheck.ClientID %>").checked == true) {
      addonReliabilityCost      = licenseQuantity * 2000;
      }
  //CT Module Add-on
    if (document.getElementById("<%= CTAddOnCheck.ClientID %>").checked == true) {
      addonCTCost               = licenseQuantity * 2000;
      }
  //RT Module Add-on
    if (document.getElementById("<%= RTAddOnCheck.ClientID %>").checked == true) {
      addonRTCost               = licenseQuantity * 9000;
      }
  //DP-Plus Module Add-on
    if (document.getElementById("<%= DPPlusAddOnCheck.ClientID %>").checked == true) {
      addonDPPlusCost           = licenseQuantity * 2000;
      }
    var addonsTotalCost         = addonReliabilityCost + addonCTCost + addonRTCost + addonDPPlusCost;
  //Calculate add-ons discount
    if (document.getElementById("<%= ProductVersionSelection.ClientID %>").value == "Research") {
      addonsTotalCost           = (addonsTotalCost*0.25);
      }

    // document.getElementById("").value = "USD$" + ((productTotalCost + addonsTotalCost) * licenseTypeAdjustment);
    <%-- = ProductConfigTotal.ClientID --%>
    }
-->
</script>