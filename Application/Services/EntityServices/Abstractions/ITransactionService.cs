using Application.DTOs.TransactionDTO;
using Domain.Common;

namespace Application.Services.EntityServices.Abstractions;

public interface ITransactionService
{
    public Task<ServiceResponse<List<GetTransactionDTO>>> GetTransactionsByUserId(int userId);
}
