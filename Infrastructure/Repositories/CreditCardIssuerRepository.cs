using Application.Abstractions;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CreditCardIssuerRepository : ICreditCardIssuerRepository
{
    #region Properties
    private readonly ApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public CreditCardIssuerRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    #endregion

    #region Methods

    public async Task<CreditCardIssuer?> GetCreditCardIssuer(int id)
    {
        var result = new CreditCardIssuer();

        result = await _dbContext.CreditCardIssuers
            .FirstOrDefaultAsync(cci => cci.CreditCardIssuerId == id);

        return result;
    }
    #endregion
}
