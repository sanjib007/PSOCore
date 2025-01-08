using BLL.Services.Merchant;
using DAL.Models.Request.Merchant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PSO.Merchant.Api.Controllers.V1;

public class AccountController(IMerchantAuthService _authService) : BaseV1Controller
{
    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<IActionResult> Login(MerchantLoginRequest request)
    {
        var result = await _authService.Login(request);
        return Ok(result);
    }    
    
    [HttpPost("ChangePassword")]
    public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
    {
        var result = await _authService.ChangePassword(request);
        return Ok(result);
    }

    [HttpGet("GetUseInfo")]
    public async Task<IActionResult> GetUseInfo()
    {
        var result = await _authService.GetUserInfo();
        return Ok(result);
    }
}
