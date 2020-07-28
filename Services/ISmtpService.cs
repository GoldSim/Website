/*==============================================================================================================================
| Author        Ignia, LLC
| Client        GoldSim
| Project       Website
\=============================================================================================================================*/
using System.Net.Mail;
using System.Threading.Tasks;

namespace GoldSim.Web.Services {

  /*============================================================================================================================
  | INTERFACE: SMTP SERVICE
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Given a <see cref="MailMessage", will send through an SMTP service configured as part of a concrete implementation.
  /// </summary>
  /// <remarks>
  ///   For simplicity and familiarity, the <see cref="ISmtpService"/> uses ASP.NET Core's out-of-the-box <see
  ///   cref="MailMessage"/> class as a well-known data transfer object, so that implementation-specific versions needn't be
  ///   known to implementers, and to prevent the need to reinvent a model that is already built into the framework.
  /// </remarks>
  public interface ISmtpService {

    /*==========================================================================================================================
    | SEND
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Sends the <see cref="MailMessage"/>.
    /// </summary>
    Task SendAsync(MailMessage mailMessage);

  } // Interface
} // Namespace