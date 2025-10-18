namespace Application.Services.ExternalServices.EmailSendingService.Abstractions;

public interface ISMTPEmailSender
{
    public Task SendEmail(string toEmail, string subject, string message, string emailTemplateName);
    public Task<string> GenerateVerificationEmailBody(string message, string subject, string emailTemplateName);
    public string CleanDatabaseHtml(string escapedHtml);
}
