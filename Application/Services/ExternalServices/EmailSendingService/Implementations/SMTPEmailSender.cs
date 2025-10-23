using Application.Abstractions;
using Application.Services.ExternalServices.EmailSendingService.Abstractions;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace Application.Services.ExternalServices.EmailSendingService.Implementations;

public class SMTPEmailSender : ISMTPEmailSender
{
    #region Properties
    private readonly IConfiguration _configuration;
    private readonly IEmailTemplateRepository _emailTemplateRepository;
    #endregion

    #region Constructors
    public SMTPEmailSender(IConfiguration configuration, IEmailTemplateRepository emailTemplateRepository)
    {
        _configuration = configuration;
        _emailTemplateRepository = emailTemplateRepository;
    }
    #endregion

    #region Methods
    public async Task SendEmail(string toEmail, string subject, string message, string emailTemplateName)
    {
        string fromEmail = _configuration["EmailSettings:From"]!;
        string password = _configuration["EmailSettings:Password"]!;
        string smtpServer = "smtp.gmail.com";

        int port = 587;

        var template = await GenerateVerificationEmailBody(message, subject, emailTemplateName);

        var smtpClient = new SmtpClient(smtpServer, port)
        {
            Credentials = new NetworkCredential(fromEmail, password),
            EnableSsl = true
        };

        var mailMessage = new MailMessage(fromEmail, toEmail)
        {
            Subject = subject,
            Body = template,
            IsBodyHtml = true
        };

        await smtpClient.SendMailAsync(mailMessage);    
    }

    public async Task<string> GenerateVerificationEmailBody(string message, string subject, string emailTemplateName)
    {
        const string _codePlaceholder = "[[VERIFICATION_CODE]]";
        const string _appNamePlaceholder = "[[APP_NAME]]";

        var template = await _emailTemplateRepository.GetEmailTemplate(e => e.TemplateName == emailTemplateName);

        var sendableTemplate = CleanDatabaseHtml(template.EmailTemplateHTML
            .Replace(_codePlaceholder, message)
            .Replace(_appNamePlaceholder, subject));

        return sendableTemplate;
    }
    public string CleanDatabaseHtml(string escapedHtml)
    {
        if (string.IsNullOrEmpty(escapedHtml))
        {
            return string.Empty;
        }

        // 1. Remove escaped carriage returns and newlines.
        string cleaned = escapedHtml.Replace("\\r\\n", Environment.NewLine);

        // 2. Remove the surrounding quotes (") if they exist (they often wrap the verbatim string).
        if (cleaned.Length > 1 && cleaned.StartsWith("\"") && cleaned.EndsWith("\""))
        {
            cleaned = cleaned.Substring(1, cleaned.Length - 2);
        }

        // 3. Replace all remaining double quotes "" with a single quote ".
        // This reverses the original C# verbatim string escaping.
        cleaned = cleaned.Replace("\"\"", "\"");

        return cleaned;
    }
    
    #endregion
}
