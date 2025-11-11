namespace Application.DTOs.UserCreditCardDTOs;

public class GetUserCreditCardsNonSensitiveDTO
{
    public int CreditCardId { get; set; }
    public int UserId { get; set; }
    public int CreditCardIssuerId { get; set; }
    public string CreditCardIssuerName { get; set; } = string.Empty;
    public string LastFourDigits { get; set; } = string.Empty;
}
