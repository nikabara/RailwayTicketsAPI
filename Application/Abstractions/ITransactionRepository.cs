using Domain.Entities;

namespace Application.Abstractions;

public interface ITransactionRepository
{
    public Task<List<Transaction>> GetAllTransactions(int amount);
    public Task<Transaction> GetTransactionByID(int transactionID);
    public Task<int> AddTransaction(Transaction transaction);
}
