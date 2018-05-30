<%@ Control Language="C#" ClassName="TouAndNewsletterCheck" %>

<Script RunAt="Server">
/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/

  /*============================================================================================================================
  | VALIDATOR: TERMS OF USE CHECK
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Ensures that the terms of use agreement checkbox is checked.
  /// </summary>
  /// <param name="source" />
  /// <param name="args" />
  void TermsCheckValidator(object source, ServerValidateEventArgs args) {
    if (!TOUCheck.Checked) {
      args.IsValid = false;
    }
  }

</Script>

<div class="cell">
  <div class="checkbox">
    <asp:CheckBox ID="TOUCheck" ClientIDMode="Static" RunAt="Server" />
    <label for="TOUCheck" RunAt="Server">I agree to these terms of use. I also agree to receive the GoldSim newsletter.</label>
  </div>
  <p class="instructions">Note: the GoldSim newsletter is sent a few times a year, and you may unsubscribe at any time.</p>
  <asp:CustomValidator
    OnServerValidate            = "TermsCheckValidator"
    ErrorMessage                = "Please accept the terms of use."
    Display                     = "None"
    RunAt                       = "Server"
    />
</div>
