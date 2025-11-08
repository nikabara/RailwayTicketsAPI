using Domain.Entities;

namespace Application.Abstractions;

public interface ICreditCardIssuerRepository
{
    public Task<CreditCardIssuer?> GetCreditCardIssuer(int id);
}
