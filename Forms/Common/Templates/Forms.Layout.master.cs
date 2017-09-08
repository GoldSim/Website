/*==============================================================================================================================
| Author        Jeremy Caney, Ignia LLC (jeremy.caney@ignia.com)
| Client        GoldSim
| Project       GoldSim Website
\=============================================================================================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ignia.Topics;
using Ignia.Topics.Web;
using Ignia.Web.Tools;
using GoldSim.Web.Controllers;
using GoldSim.Web.Helpers;
using GoldSim.Web.Models;

namespace GoldSim.Forms.Common.Templates {

  /*============================================================================================================================
  | CLASS: LAYOUT MASTER
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides the default overall page layout and common elements for forms. Child of Page.Layout.Master.
  /// </summary>
  public partial class FormsLayoutMaster : System.Web.UI.MasterPage {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private     string[]                        _ignoreControls         = {"Content", "Field"};
    private     bool                            _dataBound              = false;
    private     Topic                           _formTopic              = null;
    private     string                          _saveToTopicPath        = null;
    private     string                          _saveAsContentType      = "";
    private     Dictionary<string, string>      _formValues             = null;

    /*==========================================================================================================================
    | PUBLIC MEMBERS
    \-------------------------------------------------------------------------------------------------------------------------*/
    public      string                          SuccessUrl              = null;
    public      string                          EmailSubject            = "GoldSim.com/Forms: " + HttpContext.Current.Request.Url.Segments[HttpContext.Current.Request.Url.Segments.Length-1];
    public      string                          EmailRecipient          = "Software@GoldSim.com";
    public      string                          EmailSender             = "Website@GoldSim.com";
    public      string                          ReceiptUrl              = null;
    public      string                          ReceiptSubject          = null;
    public      string                          ReceiptRecipient        = null;
    public      string                          ReceiptSender           = "Website@GoldSim.com";
    public      event                           CommandEventHandler     ProcessForm;


    /*==========================================================================================================================
    | PROPERTY: FORM TOPIC
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets a reference to the page topic in the editor associated with the form.
    /// </summary>
    public Topic FormTopic {
      get {
        if (_formTopic == null) {
          _formTopic = TopicRepository.RootTopic.GetTopic(764);
        }
        return _formTopic;
      }
      set {
        _formTopic = value;
      }
    }

    /*==========================================================================================================================
    | PROPERTY: FORM VALUES
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets/sets the key/value pairs of form field names and values.
    /// </summary>
    public Dictionary<string, string> FormValues {
      get {
        if (_formValues == null) {
          _formValues = new Dictionary<string, string>();
          GetFieldValues(Content);
        }
        return _formValues;
      }
      set {
        _formValues = value;
      }
    }

    /*==========================================================================================================================
    | PROPERTY: SAVE TO TOPIC PATH
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets/sets the reference to the Topic path to which to save the form input information.
    /// </summary>
    public string SaveToTopicPath {
      get {
        if (_saveToTopicPath == null) {
          _saveToTopicPath = "CustomerRequests:Licenses";
        }
        return _saveToTopicPath;
      }
      set {
        _saveToTopicPath = value;
      }
    }

    /*==========================================================================================================================
    | PROPERTY: SAVE AS CONTENT TYPE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets/sets the reference to the (Topic) Content Type corresponding to the data model for the form information.
    /// </summary>
    public string SaveAsContentType {
      get {
        return _saveAsContentType;
      }
      set {
        _saveAsContentType = value;
      }
    }

    /*==========================================================================================================================
    | PROPERTY: SUBMIT LABEL
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets/sets the label to be assigned to the submit button.
    /// </summary>
    public string SubmitLabel {
      get {
        return Submit.Text;
      }
      set {
        Submit.Text = value;
      }
    }

    /*==========================================================================================================================
    | PAGE INIT
    \-------------------------------------------------------------------------------------------------------------------------*/
    void Page_Init(Object Src, EventArgs E) {
      PreRender += new EventHandler(Page_PreRender);
      DataBinding += new EventHandler(Page_DataBinding);
    }

    /*===========================================================================================================================
    | PAGE LOAD
    \-------------------------------------------------------------------------------------------------------------------------*/
    void Page_Load(Object sender, EventArgs args) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Set page validation
      \-----------------------------------------------------------------------------------------------------------------------*/
      ((TopicPage)Page).EnableValidation  = false;

      /*------------------------------------------------------------------------------------------------------------------------
      | Send FormTopic to Page.Layout.master as PageTopic
      \-----------------------------------------------------------------------------------------------------------------------*/
      //Master.PageTopic                    = FormTopic;

      /*------------------------------------------------------------------------------------------------------------------------
      | Data bind page
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (!_dataBound) this.DataBind();

    }

    /*==========================================================================================================================
    | DATA BINDING
    \-------------------------------------------------------------------------------------------------------------------------*/
    private void Page_DataBinding(Object sender, EventArgs args) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Flag as data bound
      \-----------------------------------------------------------------------------------------------------------------------*/
      _dataBound = true;

    }

    /*==========================================================================================================================
    | PRE RENDER
    \-------------------------------------------------------------------------------------------------------------------------*/
    private void Page_PreRender(Object sender, EventArgs args) {
      if (!_dataBound) this.DataBind();
    }

    /*==========================================================================================================================
    | METHOD: ON PROCESS FORM
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Performs form submission validation, fires administrator email, and saves the form values as a new Topic.
    /// </summary>
    protected void OnProcessForm(Object sender, CommandEventArgs args) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate form
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (!Page.IsValid) return;

      /*------------------------------------------------------------------------------------------------------------------------
      | Fire event handler
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (ProcessForm != null) {
        ProcessForm(sender, args);
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Re-validate form
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (!Page.IsValid) return;

      /*------------------------------------------------------------------------------------------------------------------------
      | Send email
      \-----------------------------------------------------------------------------------------------------------------------*/
      EmailForm();

      /*------------------------------------------------------------------------------------------------------------------------
      | Save form Topic
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (!String.IsNullOrEmpty(SaveAsContentType)) {
        SaveFormAsTopic();
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Send receipt
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (!String.IsNullOrEmpty(ReceiptUrl) && !String.IsNullOrEmpty(ReceiptRecipient) && !String.IsNullOrEmpty(ReceiptSubject)) {
        Utility.SendWebPage(ReceiptUrl, ReceiptSubject, ReceiptSender, ReceiptRecipient);
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Post followup
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (!String.IsNullOrEmpty(SuccessUrl)) Response.Redirect(SuccessUrl);

      /*------------------------------------------------------------------------------------------------------------------------
      | Set visibility of notification
      \-----------------------------------------------------------------------------------------------------------------------*/
      Notification.Visible      = true;

    }

    /*==========================================================================================================================
    | METHOD: EMAIL FORM
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Loops through all controls on the page and composes an email that is sent to an administrator.
    /// </summary>
    public void EmailForm(string subject = null, string recipient = null, string sender = null) {

      subject                   = subject??     EmailSubject;
      recipient                 = recipient??   EmailRecipient;
      sender                    = sender??      EmailSender;

      /*------------------------------------------------------------------------------------------------------------------------
      | Assemble email
      \-----------------------------------------------------------------------------------------------------------------------*/
      MailMessage mail          = new MailMessage(new MailAddress(sender), new MailAddress(recipient));
      mail.Subject              = subject;
      mail.Body                 = GetFieldValues(Content);
      mail.IsBodyHtml           = true;

      /*------------------------------------------------------------------------------------------------------------------------
      | Send email
      \-----------------------------------------------------------------------------------------------------------------------*/
      new SmtpClient().Send(mail);

    }

    /*==========================================================================================================================
    | METHOD: SAVE FORM AS TOPIC
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Loops through available form fields and their values, setting each to an AttributeValue (Key = field name,
    ///   Value = value), then saving the Attributes to a new Topic.
    /// </summary>
    public void SaveFormAsTopic() {

      string    topicKey        = SaveAsContentType + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
      Topic     topic           = Topic.Create(topicKey, SaveAsContentType);
      Topic     parentTopic     = TopicRepository.RootTopic.GetTopic(SaveToTopicPath);
      string    parentId        = "-1";

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate Topic Parent
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (parentTopic == null) {
        throw new Exception("Request Topics must be saved to an available Parent Topic.");
      }
      parentId                  = parentTopic.Id.ToString();

      /*------------------------------------------------------------------------------------------------------------------------
      | Set Topic values
      \-----------------------------------------------------------------------------------------------------------------------*/
      topic.Parent              = TopicRepository.RootTopic.GetTopic(SaveToTopicPath);
      topic.Attributes.Set("ParentID", parentId);
      topic.LastModified        = DateTime.Now;

      /*------------------------------------------------------------------------------------------------------------------------
      | Set Topic Attributes based on form input values
      \-----------------------------------------------------------------------------------------------------------------------*/
      foreach (string fieldName in FormValues.Keys) {
        topic.Attributes.Set(fieldName, FormValues[fieldName]);
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Save form Topic
      \-----------------------------------------------------------------------------------------------------------------------*/
      TopicRepository.DataProvider.Save(topic);

    }

  /*===========================================================================================================================
  | GET FIELD VALUES
  >============================================================================================================================
  | Loops through all controls on the page and concactenates the values of the controls into HTML formatted name/value pairs.
  \--------------------------------------------------------------------------------------------------------------------------*/
    /*==========================================================================================================================
    | METHOD: GET FIELD VALUES
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Loops through all controls on the page and concactenates the values of the controls into HTML formatted
    ///   name/value pairs.
    /// </summary>
    private string GetFieldValues (Control control, string context = "") {

      string    values          = "";
      string    value           = "";
      bool      crawl           = true;

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish context
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (
        Array.IndexOf(_ignoreControls, control.ID) < 0 &&
        context.IndexOf(control.ID?? "::") < 0 &&
        !String.IsNullOrEmpty(control.ID) &&
        !control.ID.StartsWith("__")
      ) {
        context                += control.ID.Replace("_", " ") + " ";
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Get value based on control type
      \-----------------------------------------------------------------------------------------------------------------------*/

      // Textbox
      if (control.GetType() == typeof(TextBox)) {
        value                   = ((TextBox)control).Text;
      }

      // Checkbox
      else if (control.GetType() == typeof(CheckBox)) {
        value                   = ((CheckBox)control).Checked.ToString();
      }

      // ListControl (CheckBoxList, RadioButtonList)
      else if (control.GetType().IsSubclassOf(typeof(ListControl))) {
        ListControl listControl = ((ListControl)control);
        for (int i=0; i < listControl.Items.Count; i++) {
          if (listControl.Items[i].Selected) {
            value              += ((value == "")? "" : ",") + (listControl.Items[i].Value?? listControl.Items[i].Text);
          }
        }
        crawl                   = false;
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Set value string to return and add name/value pair to FormValues
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (value != "") {

        string  fieldName       = context.Substring(0, context.Length-1);
        values                 += "<b>" + fieldName + "=</b>" + value + "<br />";

        string  shortFieldName  = ( (fieldName.IndexOf(" ") >= 0)? fieldName.Substring(fieldName.IndexOf(" ") + 1) : fieldName);
        if (!FormValues.ContainsKey(shortFieldName)) {
          FormValues.Add(shortFieldName, value);
        }

      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Iterate through child controls
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (crawl) {
        foreach (Control child in control.Controls) {
          values               += GetFieldValues(child, context);
        }
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Return values
      \-----------------------------------------------------------------------------------------------------------------------*/
      return values;

    }

  } // Class
} // Namespace
