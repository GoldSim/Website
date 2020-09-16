/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using PostmarkDotNet;

namespace GoldSim.Web.Services {

  /*============================================================================================================================
  | CLASS: POSTMARK (SMTP SERVICE)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Given a <see cref="MailMessage", will send through the SendGrid SMTP service.
  /// </summary>
  public class PostmarkSmtpService: ISmtpService {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private readonly            PostmarkClient                  _smptClient;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="SendGridSmtpService"/> with necessary dependencies.
    /// </summary>
    /// <returns>A new instance of the <see cref="SendGridSmtpService"/>.</returns>
    public PostmarkSmtpService(PostmarkClient postmarkClient) {
      _smptClient = postmarkClient?? throw new ArgumentNullException(nameof(postmarkClient));
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
      PostmarkMessage message   = new PostmarkMessage {
        To                      = String.Join(", ", mailMessage.To.Select(x => x.Address)),
        From                    = mailMessage.From.Address,
        Subject                 = mailMessage.Subject,
        HtmlBody                = mailMessage.Body,
        TrackOpens              = true
      };

      /*------------------------------------------------------------------------------------------------------------------------
      | Send email
      \-----------------------------------------------------------------------------------------------------------------------*/
      var response              = await _smptClient.SendMessageAsync(message);

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate response
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (response.Status != PostmarkStatus.Success) {
        throw new SmtpException(response.Message);
      }

    }

  } // Class
} // Namespace