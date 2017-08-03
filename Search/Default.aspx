<%@ Page Language="C#" Title="Search" MasterPageFile="/Common/Templates/Page.Layout.Master" %>

<%@ Import Namespace="Ignia.Topics" %>

<Script RunAt="Server">

/*=========================================================================================================================
| PAGE LAYOUT MASTER TEMPLATE FILE
|
| Author     Jeremy Caney, Ignia LLC (Jeremy@ignia.com)
| Client     Ignia, LLC
| Project    Common Library
|
| Purpose    Provides the default overall page layout for the site as well as methods and properties to aid in the creation
|            of standardized inner page design.  Intended to be inherited directly from a page or by a more granular
|            content layout template.
|
>=========================================================================================================================
| Revisions  Date        Author          Comments
| - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
|            11.24.08    Jeremy Caney    Initial version template.
|            MM.DD.YY    FName LName     Description
\------------------------------------------------------------------------------------------------------------------------*/

/*=========================================================================================================================
| DECLARE PUBLIC PROPERTIES
>==========================================================================================================================
| Declare any public/global variables prior to page initiation; these will be treated as properties in the control
\------------------------------------------------------------------------------------------------------------------------*/
  public   string       SearchTerm      = "GoldSim";

/*===========================================================================================================================
| PAGE LOAD
>============================================================================================================================
| Provide handling for functions that must run prior to page load.  This includes dynamically constructed controls.
\--------------------------------------------------------------------------------------------------------------------------*/
  void Page_Load(Object Src, EventArgs E) {

  /*-------------------------------------------------------------------------------------------------------------------------
  | GET SEARCH TERM
  \------------------------------------------------------------------------------------------------------------------------*/
    if (!String.IsNullOrEmpty(Request.QueryString["SearchText"])) {
      if (Request.QueryString.ToString().ToLower().IndexOf("search+goldsim") >= 0) {
        SearchTerm      = "GoldSim";
        }
      else {
        SearchTerm      = Server.UrlEncode(Request.QueryString["SearchText"]);
        }
      }

  /*-------------------------------------------------------------------------------------------------------------------------
  | DATA BIND PAGE
  \------------------------------------------------------------------------------------------------------------------------*/
    this.DataBind();

    }

</script>

<asp:Content ContentPlaceHolderID="PageHead" RunAt="Server">

  <style type="text/css">
    .Search.Results table { border: none 0 transparent !important; }
    .Search.Results table td {
      border: none 0 transparent !important;
      border-width: 0 !important;
      border-spacing: 0 !important;
      padding: 0 !important;
      margin: 0 !important;
      }
    .Search.Results div.gsc-input-box {
      height: auto !important;
      margin-top: -4px;
      }
    .Search.Results .gsc-search-box table td {
      font-size: 11px;
      line-height: 19px;
      }
    table.gstl_50 td, .Search.Results div.gsc-input-box input[type="text"] { font: 11px/17px Tahoma,Arial,sans-serif !important; }
    .Search.Results .cse .gsc-control-cse, .Search.Results .gsc-control-cse { padding: 0 !important; }
    .Search.Results div.gsc-input-box input[type="text"] {
      height: 17px !important;
      padding: 0 5px !important;
      }
    .Search.Results .gscb_a { font: 15px/13px Tahoma,arial,sans-serif; }
    .Search.Results .gsst_a {
      padding-top: 0;
      padding-bottom: 2px;
      }
    .Search.Results .gsst_a .gscb_a { color: #C0BDAA; }
    .Search.Results .gsst_a:hover .gscb_a, .Search.Results .gsst_a:focus .gscb_a { color: #736955; }
    .Search.Results .cse .gsc-search-button input.gsc-search-button-v2, .Search.Results input.gsc-search-button-v2 { padding: 4px 12px !important; }
    .Search.Results .gsc-tabHeader.gsc-tabhActive, .Search.Results .gsc-tabHeader.gsc-tabhInactive { border-radius: 2px; }
    .Search.Results .gsc-tabHeader.gsc-tabhInactive {
      margin-top: -1px;
      margin-bottom: 1px;
      }
    .gsc-refinementsArea, .gsc-above-wrapper-area { border-bottom: 1px solid #E5E4DA !important; }
    .gsc-above-wrapper-area { padding: 0 0 8px !important; }
    .gsc-result-info { padding: 0 !important; }
    .gs-webResult.gs-result a.gs-title:link, .gs-webResult.gs-result a.gs-title:link b, .gs-imageResult a.gs-title:link, .gs-imageResult a.gs-title:link b {
      font-family: 'Times New Roman',Times,serif !important;
      font-size: 17px !important;
      line-height: 24px;
      }
    .gsc-result .gs-title { height: 1.5em !important; }
    .gs-result .gs-title, .gs-result .gs-title * { text-decoration: none !important; }
    .gs-result .gs-title:link, .gs-result .gs-title:visited, a.gs-title:link, a.gs-title:visited { text-decoration: none !important; }
    .gs-result .gs-title:hover, .gs-result .gs-title:active, .gs-result .gs-title:focus, a.gs-title:hover, a.gs-title:active, a.gs-title:focus { text-decoration: underline !important; }
    .gs-imageResult .gs-snippet { line-height: 1.35em; }
    .gsc-results .gsc-cursor-box .gsc-cursor-page { padding: 2px 8px 2px 0; }
    .gsc-results .gsc-cursor-box .gsc-cursor-current-page { color: #AA8138 !important; }
    .gs-no-results-result .gs-snippet, .gs-error-result .gs-snippet {
      background-color: #FFFBE9 !important;
      border-color: #C0BDAA !important;
      }
  </style>


</asp:Content>

<asp:Content ContentPlaceHolderId="ContentArea" runat="Server">

  <h2 class="Subtitle"><%= Page.Title %></h2>

  <div class="Search Results"><gcse:search queryParameterName="SearchText"></gcse:search></div>

</asp:Content>