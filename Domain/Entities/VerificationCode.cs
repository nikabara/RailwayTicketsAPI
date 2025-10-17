namespace Domain.Entities;

public class VerificationCode
{
    public int VerificationCodeId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public DateTime ExpirationDate { get; set; }
    public bool IsUsed { get; set; } = false;
}
