using Application.Abstractions;
using Domain.Entities;
using Infrastructure.Data;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;


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

    public async Task<VerificationCode?> GetVerificationCode(Expression<Func<VerificationCode, bool>> expression)
    {
        var result = new VerificationCode();

        result = await _dbContext.VerificationCodes
            .Where(expression)
            .OrderByDescending(v => v.ExpirationDate)
            .FirstOrDefaultAsync();

        return result;
    }

    public async Task<bool> MarkVerificationCodeAsUsed(int verificationCodeId)
    {
        var targetVerificationCode = await _dbContext.VerificationCodes
            .FirstOrDefaultAsync(v => v.VerificationCodeId == verificationCodeId);

        if (targetVerificationCode != null)
        {
            targetVerificationCode.IsUsed = true;
        }

        int rowsAffected = await _dbContext.SaveChangesAsync();

        return rowsAffected > 0;
    }

    public Task<bool> RemoveVerificationCode(int id)
    {
        throw new NotImplementedException();
    }
    #endregion
}
