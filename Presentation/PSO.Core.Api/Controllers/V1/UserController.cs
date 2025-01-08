using BLL.Services;
using DAL.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PSOCore.Api.Controllers.V1;

public class UserController(IAccountApiAuthorizationService _authorizationService) : BaseV1Controller
{
    [AllowAnonymous]
    [HttpPost("Registration")]
    public async Task<IActionResult> Registration(AccountApiAuthorizationRegistrationRequest request)
    {
        var result = await _authorizationService.Registration(request);
        return Ok(result);
    }
}




