using DAL.Models.Response;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Net;
using BLL.Services.Merchant;

namespace Endpoint.Middleware;

public class MerchantTokenManagerMiddleware(IMerchantAuthService _authService) : IMiddleware
{

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (context.User.Identity.IsAuthenticated)
        {
            if (await _authService.IsCurrentActiveToken())
            {

                //// role wise access check

                //var role = context.User.Identity.RoleId();
                //var roleName = context.User.Identity.RoleName();
                //var path = context.Request.Path.Value;

                //if (!await _authService.IsCurrentPathAccess(Convert.ToInt64(role), path))
                //{

                //    context.Response.ContentType = "application/json";
                //    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                //    var result = JsonConvert.SerializeObject(
                //      new BaseResponse
                //      {
                //          StatusCode = 403,
                //          Message = "page access denied."
                //      }, new JsonSerializerSettings
                //      {
                //          ContractResolver = new CamelCasePropertyNamesContractResolver()
                //      });

                //    await context.Response.WriteAsync(result);
                //    return;
                //}

                await next(context);
                return;
            }
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            context.Response.ContentType = "application/json";
            var resultU = JsonConvert.SerializeObject(
             new BaseResponse
             {
                 StatusCode = 401,
                 Message = "Unauthorized user."
             }, new JsonSerializerSettings
             {
                 ContractResolver = new CamelCasePropertyNamesContractResolver()
             });

            await context.Response.WriteAsync(resultU);
        }
        else
        {
            await next(context);
            return;
        }

    }
}
