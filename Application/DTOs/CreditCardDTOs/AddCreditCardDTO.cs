namespace Application.DTOs.CreditCardDTOs;

public class AddCreditCardDTO
{
    public int CreditCardIssuerId { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string CVV { get; set; } = string.Empty;
}
