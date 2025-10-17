using Domain.Entities;

namespace Application.Abstractions;

public interface IVerificationCodeRepository
{
    public Task<int?> AddVerificationCode(VerificationCode verificationCode);
    public Task<bool> RemoveVerificationCode(int id);
    public Task<VerificationCode?> GetVerificationCodeByID(int id);
}
