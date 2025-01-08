using DAL.Models.Response;
using DLL.Repositories.Cache;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Utility.ConstantKeys;
using Utility;
using DAL.UnitOfWork;
using DAL.Models.Request.Merchant;
using Utility.Exceptions;
using Utility.Extentions;
using DAL.Domain;

namespace BLL.Services.Merchant;

public interface IMerchantAuthService
{
    Task<BaseResponse> ChangePassword(ChangePasswordRequest request);
    Task<BaseResponse> DeactivateCurrentAsync();
    Task<BaseResponse> DeactiveClient(string clientId);
    Task<BaseResponse> GetUserInfo();

    //Task<List<RolePageResponse>> GetAllRoleWithPage();
    Task<bool> IsCurrentActiveToken();
    Task<bool> IsCurrentPathAccess(long roleId, string path);
    Task<BaseResponse> Login(MerchantLoginRequest request);
    Task<BaseResponse> RefreshAccessToken(string refreshToken);
    //Task<BaseResponse> Registration(AuthRegistrationRequestModel request);
    //Task ReloadRolePage();

}

public class MerchantAuthService : IMerchantAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheRepository _cache;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;

    public MerchantAuthService(IUnitOfWork unitOfWork, ICacheRepository cache, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _cache = cache;
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
    }

    //public async Task<BaseResponse> Registration(AuthRegistrationRequestModel request)
    //{
    //    var appinfo = await _context.ApplicationInfos.Where(e => e.ClientId == request.ClientId).FirstOrDefaultAsync();
    //    if (appinfo is not null)
    //    {
    //        throw new BaseException("ClientId must be unique.");
    //    }
    //    var applicationInfo = new ApplicationInfo();
    //    applicationInfo.AppName = request.AppName;
    //    applicationInfo.ClientId = request.ClientId;
    //    applicationInfo.ClientSecret = Authenticator.GetHashPassword(request.ClientSecret);
    //    applicationInfo.RoleId = Convert.ToInt64(_configuration.GetSection("AuthConfig:DefultRoleId").Value);
    //    applicationInfo.IsActive = true;
    //    applicationInfo.CreatedBy = "sys";
    //    applicationInfo.CreatedDate = DateTime.Now;
    //    applicationInfo.TypeCode = request.TypeCode;
    //    applicationInfo.UserNumber = request.UserNumber;

    //    var result = await _context.ApplicationInfos.AddAsync(applicationInfo);
    //    var save = await _context.SaveChangesAsync();
    //    if (save > 0)
    //    {
    //        return new BaseResponse
    //        {
    //            StatusCode = 200,
    //            Message = "Registration Successfully.",
    //            Data = new
    //            {
    //                ClientId = result.Entity.ClientId,
    //                AppName = result.Entity.AppName,
    //            }
    //        };
    //    }
    //    else
    //    {
    //        return new BaseResponse
    //        {
    //            StatusCode = 100,
    //            Message = "Registration unsuccessfull."
    //        };
    //    }



    //}



    public async Task<BaseResponse> GetUserInfo()
    {
        var result = await _unitOfWork.MerchantKYCRegistrationRepository.FindFirstOrDefault(e => e.Id == _httpContextAccessor.CurrentUserId());

        var personalInfo = new
        {
            FullName = result.NameOfAuthorizedPerson,
            FatherName = result.NameOfFather,
            MotherName = result.NameOfMother,
            Gender = result.Gender,
            DateOfBirth = result.DateOfBirth,
            PhoneNumber = result.MobileNumber,
            City = result.MerchantCity,
            Email = result.EmailAddress,
            NidNumber = "",
            NidPIN = "",
            Address1 = result.PresentAddress,
            Address2 = result.PermanentAddress,
            ProfileImage = result.Logo,
        };

        //var bankResult = await _unitOfWork.Gen_BankInformationRepository.FindFirstOrDefault(e => e.AccountId == _httpContextAccessor.CurrentUserId() && e.IsActive == true);
        //if (bankResult == null) { return new BaseResponse { StatusCode = 100, Message = "Bank Information Not found" }; };
        var bankInfo = new
        {
            BankName = result.NameOfTheBank,
            BankAccountName = result.BankACName,
            BankAccountNo = result.ACNumber,
            Branch = result.Branch,
            RoutingNo = result.RoutingNumber,
        };

        var businessInfo = new
        {
            Title = result.NameOfOrganization,
            CompanyName = result.NameOfOrganization,
            CompanyPhone = result.MobileNumber,
            CompanyShortName = result.NameOfOrganization,
            CompanyType = result.Industry,
            CompanyWebsite = "",
            CompanyEmail = result.EmailAddress,
            CompanyLogo = result.Logo,
            CompanyAddress1 = result.CompanyOrShopAddress,
            CompanyAddress2 = ""
        };

        return new BaseResponse
        {
            StatusCode = 200,
            Message = "OK",
            Data = new
            {
                personalInfo,
                businessInfo,
                bankInfo,

            }
        };
    }

    public async Task<BaseResponse> Login(MerchantLoginRequest request)
    {
        //var result = await _unitOfWork.Con_AccountApiAuthorizationRepository.FindFirstOrDefault(e => e.PortalClientId == request.ClientId);
        var result = await _unitOfWork.Con_MerchantPortalAuthorizationRepository.FindFirstOrDefault(e => e.ClientId == request.ClientId && e.IsActive == true);
        if (result is null)
        {
            return new BaseResponse { StatusCode = 100, Message = "Client not found." };
        }
        if (!Authenticator.ValidatePassword(request.ClientSecret, result.ClientSecret))
        {
            return new BaseResponse { StatusCode = 100, Message = "Invalid credentials." };
        }
        if (result.IsActive == false)
        {
            return new BaseResponse { StatusCode = 100, Message = "Client inactive" };
        }

        var auth = await GenerateAuth(result);
        var returnResult = new BaseResponse
        {
            StatusCode = 200,
            Message = "success",
            Data = auth
        };

        return returnResult;
    }


    public async Task<BaseResponse> ChangePassword(ChangePasswordRequest request)
    {
        var result = await _unitOfWork.Con_MerchantPortalAuthorizationRepository.FindFirstOrDefault(e => e.AccountId == _httpContextAccessor.CurrentUserId());
        if (result is null)
        {
            return new BaseResponse { StatusCode = 100, Message = "Client not found." };
        }
        if (request.CurrentPassword == request.Password)
        {
            return new BaseResponse { StatusCode = 100, Message = "Current Password and new password should be different." };
        }
        if (!Authenticator.ValidatePassword(request.CurrentPassword, result.ClientSecret))
        {
            return new BaseResponse { StatusCode = 100, Message = "Wrong Current Password." };
        }

        result.ClientSecret = Authenticator.GetHashPassword(request.Password);
        _unitOfWork.Con_MerchantPortalAuthorizationRepository.Update(result);
        await _unitOfWork.CompleteAsync();

        return new BaseResponse { StatusCode = 200, Message = "Password Changed Successfully." };
    }
    public async Task<BaseResponse> DeactiveClient(string clientId)
    {
        var result = await _unitOfWork.Con_MerchantPortalAuthorizationRepository.FindFirstOrDefault(e => e.ClientId == clientId);
        if (result is null)
        {
            throw new BaseException("Client not found.");
        }

        if (result.IsActive == false)
        {
            throw new BaseException("Already client inactive");
        }
        await DeactivateAccessToken(result.AccessToken); // access disable for last token
        result.IsActive = false;
        //result.UpdatedBy = "sys";
        //result.UpdatedDate = DateTime.Now;
        await _unitOfWork.CompleteAsync();
        return new BaseResponse { StatusCode = 200, Message = "Deactivated Successfully." };
    }

    public async Task<BaseResponse> RefreshAccessToken(string refreshToken)
    {
        var result = await _unitOfWork.Con_MerchantPortalAuthorizationRepository.FindFirstOrDefault(e => e.RefreshToken == refreshToken);
        if (result is null)
        {
            throw new BaseException("Refresh token not found.");
        }
        if (result.IsActive == false)
        {
            throw new BaseException("Client inactive");
        }
        if (result.RefreshTokenDate.Value.AddMinutes(Convert.ToDouble(_configuration.GetSection("AuthConfig:RefreshTokenMin").Value)) < DateTime.Now)
        {
            throw new BaseException("Refresh token expired.");
        }

        var auth = await GenerateAuth(result);
        var returnResult = new BaseResponse
        {
            StatusCode = 200,
            Message = "success",
            Data = auth
        };

        return returnResult;
    }

    private async Task<object> GenerateAuth(Con_MerchantPortalAuthorization result)
    {
        // if cache no initiated it should bbe reload again
        #region Cache Check
        //ConfigurationOptions options = ConfigurationOptions.Parse(_configuration.GetSection("Redis:Server").Value);
        //ConnectionMultiplexer connection = ConnectionMultiplexer.Connect(options);
        //IDatabase db = connection.GetDatabase();
        //EndPoint endPoint = connection.GetEndPoints().First();
        //var _pattern = $"{_configuration.GetSection("Redis:Database").Value}{SystemAuthCachKeys.RolePage}*";
        //RedisKey[] keys = connection.GetServer(endPoint).Keys(pattern: _pattern).ToArray();
        //var server = connection.GetServer(endPoint);
        //if (keys.Count() <= 0)
        //{
        //    await ReloadRolePage();
        //}
        #endregion


        //var exp = Convert.ToInt64(_configuration.GetSection("AuthConfig:AccessTokenMin").Value) * 60;
        var exp = Convert.ToInt64(_configuration.GetSection("AuthConfig:AccessTokenMin").Value);
        var auth = new
        {
            ClientId = result.ClientId,
            AccessToken = await GenrateAccessToken(result),
            RefreshToken = GenerateRefreshToken(),
            ExpireInMin = exp, // min
            TokenType = "Bearer",
            FullName = result.FullName
        };
        await DeactivateAccessToken(result.AccessToken);
        result.AccessTokenDate = DateTime.Now;
        result.RefreshTokenDate = DateTime.Now;
        result.AccessToken = auth.AccessToken;
        result.RefreshToken = auth.RefreshToken;

        _unitOfWork.Con_MerchantPortalAuthorizationRepository.Update(result);
        await _unitOfWork.CompleteAsync();

        return auth;
    }


    private string GenerateRefreshToken()
    {
        var guid = Guid.NewGuid().ToString("N");
        return guid;
    }

    private async Task<string> GenrateAccessToken(Con_MerchantPortalAuthorization applicationInfo)
    {
        //var role = await _context.Roles.Where(e => e.Id == applicationInfo.RoleId).FirstOrDefaultAsync();

        // authentication successful so generate jwt token
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Authenticator.AuthJWTSerect);
        if (key.Length < 32)
        {
            throw new InvalidOperationException("JWT secret key length must be at least 32 bytes (256 bits) for HS256 algorithm.");
        }
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            // Claim
            Subject = new ClaimsIdentity(new Claim[]
            {
                    //new Claim(ClaimTypes.Name, applicationInfo.AppName),
                    new Claim(AuthClaimType.ClientId, applicationInfo.ClientId),
                    new Claim(AuthClaimType.RoleId, "1"),
                    new Claim(AuthClaimType.RoleName, "1"),
                    new Claim(AuthClaimType.AccountId, applicationInfo.AccountId.ToString()),
                    new Claim(AuthClaimType.UserId, applicationInfo.AccountId.ToString()),
                    //new Claim(AuthClaimType.BankCode, applicationInfo.BankCode != null? applicationInfo.BankCode.ToString(): ""),
                    //new Claim(AuthClaimType.TypeCode, applicationInfo.TypeCode != null? applicationInfo.TypeCode.ToString(): ""),
                    //new Claim(AuthClaimType.UserNumber, applicationInfo.UserNumber != null? applicationInfo.UserNumber.ToString():"")
            }),
            IssuedAt = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration.GetSection("AuthConfig:AccessTokenMin").Value)),// Access token lifetime 
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var issuer_Token = tokenHandler.WriteToken(token);
        return issuer_Token;
    }


    public async Task<BaseResponse> DeactivateAccessToken(string token)
    {
        var checkCache = await _cache.GetCacheAsync<string>(GetKey(token));
        if (checkCache is not null)
        {
            return new BaseResponse
            {
                StatusCode = 100,
                Message = "Already access token deactivated."
            };
        }
        await _cache.SetCacheAsync(GetKey(token), " ", Convert.ToInt32(_configuration.GetSection("AuthConfig:AccessTokenMin").Value));
        return new BaseResponse
        {
            StatusCode = 200,
            Message = "Access token deactivated successfully."
        };
    }


    public async Task<BaseResponse> DeactivateCurrentAsync()
    {
        return await DeactivateAccessToken(GetCurrentAsync());
    }

    public async Task<bool> IsCurrentActiveToken()
    {
        var token = GetCurrentAsync();
        var result = await _cache.GetCacheAsync<string>(GetKey(token)) == null;
        return result;
    }




    //public async Task ReloadRolePage()
    //{
    //    ConfigurationOptions options = ConfigurationOptions.Parse(_configuration.GetSection("Redis:Server").Value);
    //    ConnectionMultiplexer connection = ConnectionMultiplexer.Connect(options);
    //    IDatabase db = connection.GetDatabase();
    //    EndPoint endPoint = connection.GetEndPoints().First();
    //    var _pattern = $"{_configuration.GetSection("Redis:Database").Value}{SystemAuthCachKeys.RolePage}*";
    //    RedisKey[] keys = connection.GetServer(endPoint).Keys(pattern: _pattern).ToArray();
    //    var server = connection.GetServer(endPoint);
    //    foreach (var key in keys)
    //    {
    //        // Use Keys as per requirement
    //        db.KeyDelete(key);
    //        //await _cache.RemoveCacheAsync(key.ToString());
    //    }

    //    var menus = await _context.Menus.Where(e => e.IsActive == true).ToListAsync();
    //    var roleMenus = await _context.RoleMenus.Where(e => e.IsActive == true).ToListAsync();
    //    foreach (var menu in menus)
    //    {
    //        var roleList = roleMenus.Where(e => e.MenuId == menu.Id).ToList();
    //        var _roles = new List<long>();
    //        foreach (var itemrole in roleList)
    //        {
    //            _roles.Add(itemrole.RoleId.Value);
    //        }
    //        await _cache.SetCacheAsync(GetPageKey(menu.Url), JsonConvert.SerializeObject(_roles), 2147483647);
    //    }
    //    await _cache.SetCacheAsync(SystemAuthCachKeys.RolePage, "  ", 2147483647);
    //}

    //public async Task<List<RolePageResponse>> GetAllRoleWithPage()
    //{
    //    var menus = await _context.Menus.Where(e => e.IsActive == true).ToListAsync();
    //    var roleMenus = await _context.RoleMenus.Where(e => e.IsActive == true).ToListAsync();
    //    var roles = await _context.Roles.Where(e => e.IsActive == true).ToListAsync();
    //    List<RolePageResponse> res = new();
    //    foreach (var itemRole in roles)
    //    {
    //        var obj = new RolePageResponse();
    //        obj.RoleName = itemRole.Name;
    //        obj.RoleId = itemRole.Id;

    //        var roleMenuUser = roleMenus.Where(e => e.RoleId == itemRole.Id).ToList();
    //        foreach (var itemRoleMenuUser in roleMenuUser)
    //        {
    //            var menuUser = menus.Where(e => e.Id == itemRoleMenuUser.MenuId).FirstOrDefault();
    //            if (menuUser is not null)
    //            {
    //                obj.Pages.Add(menuUser.Url);

    //            }
    //        }

    //        res.Add(obj);
    //    }
    //    return res;
    //}

    public string GetPageKey(string url)
    {
        var key = $"{SystemAuthCachKeys.RolePage}{url}";
        return key;
    }

    public async Task<bool> IsCurrentPathAccess(long roleId, string path)
    {
        var pageCache = await _cache.GetCacheAsync<object>(SystemAuthCachKeys.RolePage);
        if (pageCache == null)
        {
            //await ReloadRolePage();
        }
        var cacheRole = await _cache.GetCacheAsync<string>(GetPageKey(path));
        if (cacheRole is not null)
        {
            var roleList = JsonConvert.DeserializeObject<List<long>>(cacheRole);
            if (roleList != null)
            {
                var value = roleList.Where(e => e.Equals(roleId)).FirstOrDefault();
                if (value > 0)
                {
                    return true;
                }
            }

            return false;
        }
        return false;

    }

    private string GetCurrentAsync()
    {
        var authorizationHeader = _httpContextAccessor
            .HttpContext.Request.Headers["authorization"];

        return authorizationHeader == StringValues.Empty
            ? string.Empty
            : authorizationHeader.Single().Split(" ").Last();
    }

    private static string GetKey(string token)
        => $"tokens:{token}:deactivated";


}

