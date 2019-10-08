/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Threading.Tasks;
using Ignia.Topics;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace GoldSim.Web.Services {

  /*============================================================================================================================
  | CLASS: SEND GRID (SMTP SERVICE)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Given a <see cref="MailMessage", will send through the SendGrid SMTP service.
  /// </summary>
  public class SendGridSmtpService: ISmtpService {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private readonly            SendGridClient                  _smptClient;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="SendGridSmtpService"/> with necessary dependencies.
    /// </summary>
    /// <returns>A new instance of the <see cref="SendGridSmtpService"/>.</returns>
    public SendGridSmtpService(SendGridClient sendGridClient) {
      _smptClient = sendGridClient?? throw new ArgumentNullException(nameof(sendGridClient));
    }

    /*==========================================================================================================================
    | SEND
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Sends the <see cref="MailMessage"/> via SendGrid.
    /// </summary>
    public async Task SendAsync(MailMessage mailMessage) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Assemble email
      \-----------------------------------------------------------------------------------------------------------------------*/
      var message = new SendGridMessage();

      foreach (var recipient in mailMessage.To) {
        message.AddTo(new EmailAddress(recipient.Address, recipient.DisplayName));
      }

      message.SetFrom(new EmailAddress(mailMessage.From.Address, mailMessage.From.DisplayName));
      message.SetSubject(mailMessage.Subject);
      message.AddContent(mailMessage.IsBodyHtml? MimeType.Html : MimeType.Text, mailMessage.Body);

      /*------------------------------------------------------------------------------------------------------------------------
      | Send email
      \-----------------------------------------------------------------------------------------------------------------------*/
      await _smptClient.SendEmailAsync(message);

    }

  } // Interface

} // Namespace
