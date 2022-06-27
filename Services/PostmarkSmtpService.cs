/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System.Net.Mail;
using PostmarkDotNet;

namespace GoldSim.Web.Services {

  /*============================================================================================================================
  | CLASS: POSTMARK (SMTP SERVICE)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Given a <see cref="MailMessage", will send through the Postmark SMTP service.
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
    ///   Initializes a new instance of the <see cref="PostmarkSmtpService"/> with necessary dependencies.
    /// </summary>
    /// <returns>A new instance of the <see cref="PostmarkSmtpService"/>.</returns>
    public PostmarkSmtpService(PostmarkClient postmarkClient) {
      _smptClient = postmarkClient?? throw new ArgumentNullException(nameof(postmarkClient));
    }

    /*==========================================================================================================================
    | SEND
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Sends the <see cref="MailMessage"/> via Postmark.
    /// </summary>
    public async Task SendAsync(MailMessage mailMessage) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate input
      \-----------------------------------------------------------------------------------------------------------------------*/
      Contract.Requires(mailMessage, nameof(mailMessage));

      /*------------------------------------------------------------------------------------------------------------------------
      | Assemble email
      \-----------------------------------------------------------------------------------------------------------------------*/
      var message               = new PostmarkMessage {
        To                      = String.Join(", ", mailMessage.To.Select(x => x.Address)),
        From                    = mailMessage.From.Address,
        Subject                 = mailMessage.Subject,
        TrackOpens              = true
      };

      // Conditionally set body
      if (mailMessage.IsBodyHtml) {
        message.HtmlBody        = mailMessage.Body;
      }
      else {
        message.TextBody        = mailMessage.Body;
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Send email
      \-----------------------------------------------------------------------------------------------------------------------*/
      var response              = await _smptClient.SendMessageAsync(message).ConfigureAwait(true);

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate response
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (response.Status is not PostmarkStatus.Success) {
        throw new SmtpException(response.Message);
      }

    }

  } // Class
} // Namespace