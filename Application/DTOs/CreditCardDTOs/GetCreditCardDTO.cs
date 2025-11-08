using Domain.Entities;

namespace Application.DTOs.CreditCardDTOs;

public class GetCreditCardDTO
{
    public int CreditCardId { get; set; }
    public int CreditCardIssuerId { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string CVV { get; set; } = string.Empty;
    //public CreditCardIssuer CreditCardIssuer { get; set; } = new();
}
