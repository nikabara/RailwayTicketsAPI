namespace Application.DTOs.CreditCardDTOs;

public class AddCreditCardDTO
{
    public int CreditCardIssuerId { get; set; }
    public int UserId { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string CreditCardNumber { get; set; } = string.Empty;
    public string CVV { get; set; } = string.Empty;
}
