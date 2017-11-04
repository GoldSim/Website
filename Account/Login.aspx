<%@ Page Language="C#" Title="Login" %>

<!-- #Include Virtual="/Common/Global/Headers/Form.Headers.inc.aspx" -->

<Script Language="C#" RunAt="Server">
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
    | Ensure caching is disabled
    \-------------------------------------------------------------------------------------------------------------------------*/
    Response.Cache.SetCacheability(HttpCacheability.NoCache);

    /*--------------------------------------------------------------------------------------------------------------------------
    | Entry
    |---------------------------------------------------------------------------------------------------------------------------
    | ### TODO JJC120308: Update to use default button method instead of Page_Load intercept.
    \-------------------------------------------------------------------------------------------------------------------------*/
    if (!IsPostBack) {
      if (Request.Cookies["MembershipAuthentication"] != null && Request.QueryString["ReturnUrl"] != null) {
        Response.Redirect(Request.QueryString["ReturnUrl"]);
      }
      lblInstructions.Text      = "The resource you have requested requires authentication. Please login.";
    }

    /*--------------------------------------------------------------------------------------------------------------------------
    | Success
    \-------------------------------------------------------------------------------------------------------------------------*/
    else if (ProcessPage()) {

    }

    /*--------------------------------------------------------------------------------------------------------------------------
    | Error
    \-------------------------------------------------------------------------------------------------------------------------*/
    else {

    }

  }

  /*============================================================================================================================
  | STATE: FORM PROCESSOR
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Handles valid page results and processes accordingly.
  /// </summary>
  /// <remarks>
  ///   ### NOTE: JJC030207: With ASP.NET 2.0 support for default buttons, this can be moved to a button click event handler.
  /// </remarks>
  /// <returns>Valid page state check results.</returns>
  bool ProcessPage() {

    /*--------------------------------------------------------------------------------------------------------------------------
    | Validate page
    \-------------------------------------------------------------------------------------------------------------------------*/
    Page.Validate();

    /*--------------------------------------------------------------------------------------------------------------------------
    | Return page state
    \-------------------------------------------------------------------------------------------------------------------------*/
    return (IsPostBack && Page.IsValid);

  }

  /*============================================================================================================================
  | VALIDATOR: AUTHENTICATEUSER
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Checks to determine if the user is valid; if they are, logs the user in.
  /// </summary>
  void AuthenticateUser(object source, ServerValidateEventArgs args) {

    /*--------------------------------------------------------------------------------------------------------------------------
    | Ensure form is valid before proceeding
    \-------------------------------------------------------------------------------------------------------------------------*/
    if (!Page.IsValid) return;

    /*--------------------------------------------------------------------------------------------------------------------------
    | Attempt to locate user in membership services
    \-------------------------------------------------------------------------------------------------------------------------*/
    MembershipUser objUser      = Membership.GetUser(Username.Value, false);

    /*--------------------------------------------------------------------------------------------------------------------------
    | Authenticate user and return to originally requested page
    \-------------------------------------------------------------------------------------------------------------------------*/
    if (Membership.ValidateUser(Username.Value, Password.Value)) {
      FormsAuthentication.SetAuthCookie(Username.Value, RememberPassword.Checked);
      Response.Redirect(Request.QueryString["ReturnUrl"] ?? "/"); //FormsAuthentication.DefaultUrl
      args.IsValid              = true;
      return;
    }

    /*-------------------------------------------------------------------------------------------------------------------------
    | Provide instructions
    \------------------------------------------------------------------------------------------------------------------------*/
    lblInstructions.Text        = "Login failed. Please check your user name and password and try again.";

    /*--------------------------------------------------------------------------------------------------------------------------
    | Success
    \-------------------------------------------------------------------------------------------------------------------------*/
    args.IsValid                = false;

  }
</Script>
<!DOCTYPE html>
<html xmlns="https://www.w3.org/1999/xhtml" lang="en">
  <head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, height=device-height, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <title>Admin Login - GoldSim</title>
    <link rel="stylesheet" type="text/css" href="/Shared/Styles/Vendor/FontAwesome/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css" href="/Shared/Styles/Vendor/RedCastor/awesome-foundation6-checkbox.css" />
    <link rel="stylesheet" type="text/css" href="/Shared/Styles/Style.min.css" />
    <style type="text/css">
      .logo {
        margin: 0 auto;
      }
      article {
        padding: 3rem 0;
      }
      .HtmlSeparator {
        padding: 0;
        height: 0;
        background-color: rgb(255, 255, 255);
      }
      #SiteHeader {
        -webkit-box-shadow: 0 0 9px 2px rgba(0, 0, 0, 0.07);
        box-shadow: 0 0 9px 2px rgba(0, 0, 0, 0.07);
        margin-bottom: 15px;
      }
      table.login.form {
        width: 65%;
      }
      tbody,
      tbody tr,
      table td {
        background-color: transparent;
      }
      table td {
        vertical-align: middle;
      }
      table td input {
        margin-bottom: 0;
      }
      td.form-field.label {
        padding-top: 14px;
        width: 100%;
        text-align: right;
      }
    </style>
  </head>
  <body>
    <form runat="server">

      <!-- Site Header Area -->
      <header id="SiteHeader" class="site header title-bar" role="banner" vocab="http://schema.org" typeof="WPHeader">
        <div class="logo centered">
          <!-- Logo -->
          <a href="/"><img src="/Images/Logo.png" alt="GoldSim Technology Group" class="logo" /></a>
        </div>
      </header>
      <!-- /Site Header Area -->

      <!-- Main Site Content Area -->
      <main id="MainContentArea" class="page content" role="main">
        <article itemscope itemtype="http://schema.org/WebPageElement" itemprop="mainContentOfPage" class="grid-container">

          <div class="grid-x grid-margin-x">

            <!-- Instructions -->
            <div class="cell text-center">
              <div class="callout primary" style="float: none; margin-left: 0;"><asp:Label ID="lblInstructions" RunAt="Server"></asp:Label></div>
            </div>

            <div class="cell">
              <section class="panel body">

                <!-- Client Validation -->
                <Ignia:ClientValidation  ID = "objClientValidation"
                  SummaryIntro              = "The following errors have been found:<br>"
                  SummaryExit               = "<br />"
                  SummaryColor              = "Red"
                  SummaryClassName          = "Validation"
                  RunAt                     = "Server"
                />

                <table class="login form">
                  <Ignia:FormField       ID = "Username"
                    LabelName               = "Username:"
                    AccessKey               = ""
                    MaxLength               = "50"
                    FieldSize               = "214"
                    Required                = "True"
                    RunAt                   = "Server"
                  />
                  <Ignia:FormField       ID = "Password"
                    LabelName               = "Password:"
                    AccessKey               = ""
                    MaxLength               = "50"
                    FieldSize               = "214"
                    Required                = "True"
                    TextMode                = "Password"
                    RunAt                   = "Server"
                  />
                </table>

                <asp:Checkbox            ID = "RememberPassword"
                  Text                      = "Remember password on this machine."
                  AccessKey                 = "R"
                  CssClass                  = "checkbox"
                  Checked                   = "False"
                  TextAlign                 = "Right"
                  RunAt                     = "Server"
                />

                <asp:CustomValidator
                  ErrorMessage              = "The password you typed was incorrect."
                  ControlToValidate         = "Password:Field"
                  OnServerValidate          = "AuthenticateUser"
                  RunAt                     = "Server"
                >
                </asp:CustomValidator>

                <br /><br />
                <asp:Button              ID = "SubmitButton"
                  Text                      = "Login"
                  CssClass                  = "button"
                  CausesValidation          = "true"
                  RunAt                     = "Server"
                />

              </section>

            </div>

          </div>

        </article>
      </main>

    </form>
  </body>
</html>
