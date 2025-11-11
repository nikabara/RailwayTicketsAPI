namespace Application.DTOs.UserCreditCardDTOs;

public class GetUserCreditCardsSensitiveDTO
{
    public int CreditCardId { get; set; }
    public int UserId { get; set; }
    public int CreditCardIssuerId { get; set; }
    public string CreditCardIssuerName { get; set; } = string.Empty;
    public DateTime ExpirationDate { get; set; }
    public string CVV { get; set; } = string.Empty;
    public string CreditCardNumber { get; set; } = string.Empty;
}
