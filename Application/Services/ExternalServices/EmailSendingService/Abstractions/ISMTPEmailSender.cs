namespace Application.Services.ExternalServices.EmailSendingService.Abstractions;

public interface ISMTPEmailSender
{
    public Task SendEmail(string toEmail, string subject, string message);
}
