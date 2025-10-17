using Application.Abstractions;
using Application.Services.EntityServices.Abstractions;
using Domain.Common;
using Domain.Entities;

namespace Application.Services.EntityServices.Implementations;

public class VerificationCodeService : IVerificationCodeService
{
    #region Properties
    private readonly IVerificationCodeRepository _verificationCodeRepository;
    #endregion

    #region Constructor
    public VerificationCodeService(IVerificationCodeRepository verificationCodeRepository)
    {
        _verificationCodeRepository = verificationCodeRepository;
    }
    #endregion

    #region Methods
    public async Task<ServiceResponse<int?>> AddVerificationCode(VerificationCode verificationCode)
    {
        var response = new ServiceResponse<int?>();

        var addedVerificationCodeId = await _verificationCodeRepository.AddVerificationCode(verificationCode);

        if (addedVerificationCodeId > 0 && addedVerificationCodeId != null)
        {
            response.IsSuccess = true;
            response.Data = addedVerificationCodeId;
        }
        else
        {
            response.IsSuccess = false;
            response.ErrorMessage = "An error occurred while adding the verification code.";
        }

        return response;
    }
    #endregion
}
