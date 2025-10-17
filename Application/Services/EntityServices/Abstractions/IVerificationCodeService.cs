using Domain.Common;
using Domain.Entities;

namespace Application.Services.EntityServices.Abstractions;

public interface IVerificationCodeService
{
    public Task<ServiceResponse<int?>> AddVerificationCode(VerificationCode verificationCode);
}
