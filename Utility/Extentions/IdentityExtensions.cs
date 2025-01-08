using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Security.Principal;

namespace Utility.Extentions;

public static class IdentityExtensions
{
    //public static long CurrentUserId(this HttpContext httpContext)
    public static long CurrentUserId(this IHttpContextAccessor httpContext)
    {
        ClaimsIdentity? claimsIdentity = httpContext.HttpContext?.User.Identity as ClaimsIdentity;
        Claim? claim = claimsIdentity?.FindFirst(AuthClaimType.UserId);
        var result = claim?.Value ?? string.Empty;
        if (string.IsNullOrEmpty(result)) return 0;
        else return Convert.ToInt64(result);

    }  
    
    public static long CurrentAccountId(this IHttpContextAccessor httpContext)
    {
        ClaimsIdentity? claimsIdentity = httpContext.HttpContext?.User.Identity as ClaimsIdentity;
        Claim? claim = claimsIdentity?.FindFirst(AuthClaimType.AccountId);
        var result = claim?.Value ?? string.Empty;
        if (string.IsNullOrEmpty(result)) return 0;
        else return Convert.ToInt64(result);

    }
    public static string CurrentUserNumber(this IHttpContextAccessor httpContext)
    {
        ClaimsIdentity? claimsIdentity = httpContext.HttpContext?.User.Identity as ClaimsIdentity;
        Claim? claim = claimsIdentity?.FindFirst(AuthClaimType.UserNumber);
        return claim?.Value ?? string.Empty;
    }
    public static string CurrentUserFullName(this IHttpContextAccessor httpContext)
    {
        ClaimsIdentity? claimsIdentity = httpContext.HttpContext?.User.Identity as ClaimsIdentity;
        Claim? claim = claimsIdentity?.FindFirst(AuthClaimType.FullName);
        return claim?.Value ?? string.Empty;
    }


    public static long CurrentUserId(this HttpContext httpContext)
    {
        ClaimsIdentity? claimsIdentity = httpContext?.User.Identity as ClaimsIdentity;
        Claim? claim = claimsIdentity?.FindFirst(AuthClaimType.UserId);
        var result = claim?.Value ?? string.Empty;
        if (string.IsNullOrEmpty(result)) return 0;
        else return Convert.ToInt64(result);

    }
    public static string AppName(this IIdentity identity)
    {
        ClaimsIdentity? claimsIdentity = identity as ClaimsIdentity;
        Claim? claim = claimsIdentity?.FindFirst(AuthClaimType.AppName);
        return claim?.Value ?? string.Empty;
    }
    public static string UserName(this IIdentity identity)
    {
        ClaimsIdentity? claimsIdentity = identity as ClaimsIdentity;
        Claim? claim = claimsIdentity?.FindFirst(AuthClaimType.UserName);
        return claim?.Value ?? string.Empty;
    }
    public static string FullName(this IIdentity identity)
    {
        ClaimsIdentity? claimsIdentity = identity as ClaimsIdentity;
        Claim? claim = claimsIdentity?.FindFirst(AuthClaimType.FullName);
        return claim?.Value ?? string.Empty;
    }

    public static string ClientId(this IIdentity identity)
    {
        ClaimsIdentity? claimsIdentity = identity as ClaimsIdentity;
        Claim? claim = claimsIdentity?.FindFirst(AuthClaimType.ClientId);
        return claim?.Value ?? string.Empty;
    }
    public static string RoleId(this IIdentity identity)
    {
        ClaimsIdentity? claimsIdentity = identity as ClaimsIdentity;
        Claim? claim = claimsIdentity?.FindFirst(AuthClaimType.RoleId);
        return claim?.Value ?? string.Empty;
    }
    public static string UserNumber(this IIdentity identity)
    {
        ClaimsIdentity? claimsIdentity = identity as ClaimsIdentity;
        Claim? claim = claimsIdentity?.FindFirst(AuthClaimType.UserNumber);
        return claim?.Value ?? string.Empty;
    }
    public static string BankCode(this IIdentity identity)
    {
        ClaimsIdentity? claimsIdentity = identity as ClaimsIdentity;
        Claim? claim = claimsIdentity?.FindFirst(AuthClaimType.BankCode);
        return claim?.Value ?? string.Empty;
    }
    public static string RoleName(this IIdentity identity)
    {
        ClaimsIdentity? claimsIdentity = identity as ClaimsIdentity;
        Claim? claim = claimsIdentity?.FindFirst(AuthClaimType.RoleName);
        return claim?.Value ?? string.Empty;
    }
}
