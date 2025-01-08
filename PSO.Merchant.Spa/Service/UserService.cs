using PSO.Merchant.Spa.Models.Request;
using PSO.Merchant.Spa.Models.Response;
using PSO.Merchant.Spa.Pages;
using static PSO.Merchant.Spa.Pages.ChangePassword;

public interface IUserService
{
    Task<BaseResponse> ChangePassword(ChangePasswordRequest request);
    Task<UserInfoResponse> GetUserInfo();
    Task<LoginReponse> Login(LoginRequest request);
}

public class UserService(IConfiguration _configuration, HttpClient _httpClient) : IUserService
{
    public async Task<LoginReponse> Login(LoginRequest request)
    {
        var response = await _httpClient.PostDataAsJsonAsync<LoginReponse>(_configuration.GetSection("AppConfig:MerchantBaseUrl").Value + $"/api/v1/Account/Login", request);
        return response;
    }

    public async Task<BaseResponse> ChangePassword(ChangePasswordRequest request)
    {
        var response = await _httpClient.PostDataAsJsonAsync<BaseResponse>(_configuration.GetSection("AppConfig:MerchantBaseUrl").Value + $"/api/v1/Account/ChangePassword", request);
        return response;
    }
    public async Task<UserInfoResponse> GetUserInfo()
    {
        var response = await _httpClient.GetDataAsJsonAsync<UserInfoResponse>(_configuration.GetSection("AppConfig:MerchantBaseUrl").Value + $"/api/v1/Account/GetUseInfo");
        return response;
    }


}
