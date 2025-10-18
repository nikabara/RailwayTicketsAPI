using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Abstractions;

public interface IVerificationCodeRepository
{
    public Task<bool> RemoveVerificationCode(int id);
    public Task<int?> AddVerificationCode(VerificationCode verificationCode);
    public Task<bool> MarkVerificationCodeAsUsed(int verificationCodeId);
    public Task<VerificationCode?> GetVerificationCode(Expression<Func<VerificationCode, bool>> expression);
}
