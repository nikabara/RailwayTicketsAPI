using Infrastructure.ExternalServices.EmailSendingService.Abstractions;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;

namespace Infrastructure.ExternalServices.EmailSendingService.Implementations;

public class SMTPEmailSender : ISMTPEmailSender
{
    #region Properties
    private readonly IConfiguration _configuration;
    #endregion

    #region Constructors
    public SMTPEmailSender(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    #endregion

    #region Methods
    public async Task SendEmail(string toEmail, string subject, string message)
    {
        string fromEmail = _configuration["EmailSettings:From"]!;
        string password = _configuration["EmailSettings:Password"]!;
        string smtpServer = "smtp.gmail.com";

        int port = 587;

        var smtpClient = new SmtpClient(smtpServer, port)
        {
            Credentials = new NetworkCredential(fromEmail, password),
            EnableSsl = true
        };

        var mailMessage = new MailMessage(fromEmail, toEmail)
        {
            Subject = subject,
            Body = message,
            IsBodyHtml = true
        };

        await smtpClient.SendMailAsync(mailMessage);    
    }
    #endregion

}
