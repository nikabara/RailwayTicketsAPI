namespace Application.DTOs.EmailDTOs;

public class SendEmailDTO
{
    public int UserId { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}
