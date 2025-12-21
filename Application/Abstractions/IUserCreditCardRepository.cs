using Application.DTOs.CreditCardDTOs;
using Domain.Entities;

namespace Application.Abstractions;

public interface IUserCreditCardRepository
{
    public Task<List<CreditCard>?> GetUserCreditCards(int userId);
    public Task<bool> AddCreditCard(CreditCard creditCard);
}
