using Application.Abstractions;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CreditCardRepository : ICreditCardRepository
{
    #region Properties
    private readonly ApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public CreditCardRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    #endregion

    #region Methods
    public async Task<int> AddCreditCard(CreditCard CreditCard)
    {
        var result = default(int);

        await _dbContext.CreditCards.AddAsync(CreditCard);

        var rowsAffected = await _dbContext.SaveChangesAsync();

        if (rowsAffected > 0)
        {
            result = CreditCard.CreditCardId;
        }

        return result;
    }

    public async Task<CreditCard?> GetCreditCardByID(int id)
    {
        var result = new CreditCard();

        result = await _dbContext.CreditCards
            .Include(c => c.CreditCardIssuer)
            .Include(u => u.Users)
            .FirstOrDefaultAsync(c => c.CreditCardId == id);

        return result;
    }

    public async Task<bool> RemoveCreditCard(int id)
    {
        var result = default(bool);

        var targetCreditCard = await _dbContext.CreditCards
            .FirstOrDefaultAsync(c => c.CreditCardId == id);

        if (targetCreditCard == null)
        {
            result = false;
        }
        else
        {
            _dbContext.CreditCards.Remove(targetCreditCard);
            
            var rowsAffected = _dbContext.SaveChangesAsync().Result;
            
            result = rowsAffected > 0;
        }

        return result;
    }

    public async Task<CreditCard?> GetCreditCardByInfo(string creditCardNumber, string cvv)
    {
        var result = default(CreditCard);

        result = await _dbContext.CreditCards
            .Include(u => u.Users)
            .Include(c => c.CreditCardIssuer)
            .FirstOrDefaultAsync(c => c.CreditCardNumber == creditCardNumber && c.CVV == cvv);

        return result;
    }

    public async Task<int> UpdateCreditCard(CreditCard card)
    {
        _dbContext.CreditCards.Update(card);

        return await _dbContext.SaveChangesAsync();
    }
    #endregion
}
