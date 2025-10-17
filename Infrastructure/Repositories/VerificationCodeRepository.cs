using Application.Abstractions;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class VerificationCodeRepository : IVerificationCodeRepository
{
    #region Properties
    private readonly ApplicationDbContext _dbContext;
    #endregion

    #region Constructor
    public VerificationCodeRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    #endregion

    #region Methods
    public async Task<int?> AddVerificationCode(VerificationCode verificationCode)
    {
        var result = default(int?);

        await _dbContext.VerificationCodes.AddAsync(verificationCode);

        int rowsAffected = await _dbContext.SaveChangesAsync();

        result = rowsAffected > 0 ? verificationCode.VerificationCodeId : null;

        return result;
    }

    public Task<VerificationCode?> GetVerificationCodeByID(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveVerificationCode(int id)
    {
        throw new NotImplementedException();
    }
    #endregion
}
