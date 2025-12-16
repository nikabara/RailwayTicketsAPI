using Application.Abstractions;
using Infrastructure.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    #region Properties
    private readonly ApplicationDbContext _dbContext;
    #endregion

    #region Constructor
    public TransactionRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    #endregion

    #region Methods
    public async Task<int> AddTransaction(Transaction transaction)
    {
        var result = default(int);

        await _dbContext.Transactions.AddAsync(transaction);
        await _dbContext.SaveChangesAsync();

        result = transaction.TransactionId;

        return result;
    }

    public async Task<List<Transaction>> GetAllTransactions(int amount)
    {
        var result = await _dbContext.Transactions.Take(amount).ToListAsync();

        return result;
    }

    public async Task<Transaction?> GetTransactionByID(int transactionID)
    {
        var result = new Transaction();

        result = await _dbContext.Transactions
            .Include(t => t.TrainSchedule)
            .Include(t => t.Seat)
            .Include(t => t.User)
            .Include(t => t.Currency)
            .Include(t => t.TransactionState)
            .Include(t => t.CreditCard)
            .FirstOrDefaultAsync(t => t.TransactionId == transactionID);

        return result;
    }

    public async Task<List<Transaction>> GetTransactionsByuserId(int userId)
    {
        var result = new List<Transaction>();

        result = await _dbContext.Transactions
            .Include(t => t.TrainSchedule)
            .Include(t => t.Seat)
            .Include(t => t.Currency)
            .Include(t => t.TransactionState)
            .Include(t => t.CreditCard)
            .Where(t => t.UserId == userId)
            .ToListAsync();

        return result;
    }

    #endregion
}
