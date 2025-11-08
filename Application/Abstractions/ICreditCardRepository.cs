using Domain.Entities;

namespace Application.Abstractions;

public interface ICreditCardRepository
{
    public Task<int> AddCreditCard(CreditCard CreditCard);
    public Task<bool> RemoveCreditCard(int id);
    public Task<CreditCard?> GetCreditCardByID(int id);
    public Task<CreditCard?> GetCreditCardByInfo(string creditCardNumber, string cvv);
    public Task<int> UpdateCreditCard(CreditCard card);
}
