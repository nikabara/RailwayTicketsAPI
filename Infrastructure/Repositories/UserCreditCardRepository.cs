using Application.Abstractions;
using Application.DTOs.CreditCardDTOs;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserCreditCardRepository : IUserCreditCardRepository
{
    #region Properties
    private readonly ApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public UserCreditCardRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    #endregion

    #region Methods
    public async Task<bool> AddCreditCard(CreditCard creditCard)
    {
        await _dbContext.CreditCards.AddAsync(creditCard);

        int rowsChanged = await _dbContext.SaveChangesAsync();

        return rowsChanged > 0;
    }

    public async Task<List<CreditCard>?> GetUserCreditCards(int userId)
    {
        var result = new List<CreditCard>();

        var targetUser = await _dbContext.Users
            .Include(c => c.CreditCards)
            .FirstOrDefaultAsync(u => u.UserId == userId);

        if (targetUser != null)
            result = targetUser.CreditCards;
        
        return result;
    }


    #endregion
}
