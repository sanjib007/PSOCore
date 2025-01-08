using DAL.Domain;
using DAL.Models.Request;
using DAL.Models.Response;
using DAL.UnitOfWork;
using Utility;

namespace BLL.Services;

public interface IAccountApiAuthorizationService
{
    Task<BaseResponse> Registration(AccountApiAuthorizationRegistrationRequest request);
}
public class AccountApiAuthorizationService(IUnitOfWork _unitOfWork) : IAccountApiAuthorizationService
{
    public async Task<BaseResponse> Registration(AccountApiAuthorizationRegistrationRequest request)
    {
        var result_client = await _unitOfWork.Con_AccountApiAuthorizationRepository.FindFirstOrDefault(e => e.ClientId == request.ClientId);
        if (result_client is not null)
        {
            return new BaseResponse { StatusCode = 100, Message = "Already used this client Id." };
        }
        var result_account = await _unitOfWork.Con_AccountApiAuthorizationRepository.FindFirstOrDefault(e => e.AccountId == request.AccountId);
        if (result_account is not null)
        {
            return new BaseResponse { StatusCode = 100, Message = "Already Created for your requested account." };
        }
        var applicationInfo = new Con_AccountApiAuthorization();
        applicationInfo.AccountId = request.AccountId;
        applicationInfo.ClientId = request.ClientId;
        applicationInfo.ClientSecret = Authenticator.GetHashPassword(request.ClientSecret);

        await _unitOfWork.Con_AccountApiAuthorizationRepository.Add(applicationInfo);
        var cresult = await _unitOfWork.CompleteAsync();

        if (cresult > 0)
        {
            return new BaseResponse
            {
                StatusCode = 200,
                Message = "Registration Successfully.",
            };
        }
        else
        {
            return new BaseResponse
            {
                StatusCode = 100,
                Message = "Registration unsuccessful."
            };
        }
    }

}
