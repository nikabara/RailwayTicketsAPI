namespace Application.DTOs.EmailDTOs;

public class SendEmailDTO
{
    public string ToEmail { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}
