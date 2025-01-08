//using DAL.Models.Response;
//using Microsoft.AspNetCore.Http;
//using Newtonsoft.Json.Serialization;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Text;
//using System.Threading.Tasks;
//using DAL.Repositories;
//using BLL.Services;

//namespace Endpoint.Middleware;

//public class TokenManagerMiddleware : IMiddleware
//{
//    private readonly IUserInformationService _authService;

//    public TokenManagerMiddleware(IUserInformationService authService)
//    {
//        _authService = authService;
//    }

//    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
//    {
//        if (context.User.Identity.IsAuthenticated)
//        {
//            if (await _authService.IsCurrentActiveToken())
//            {

//                //// role wise access check

//                //var role = context.User.Identity.RoleId();
//                //var roleName = context.User.Identity.RoleName();
//                //var path = context.Request.Path.Value;

//                //if (!await _authService.IsCurrentPathAccess(Convert.ToInt64(role), path))
//                //{

//                //    context.Response.ContentType = "application/json";
//                //    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
//                //    var result = JsonConvert.SerializeObject(
//                //      new BaseResponse
//                //      {
//                //          StatusCode = 403,
//                //          Message = "page access denied."
//                //      }, new JsonSerializerSettings
//                //      {
//                //          ContractResolver = new CamelCasePropertyNamesContractResolver()
//                //      });

//                //    await context.Response.WriteAsync(result);
//                //    return;
//                //}

//                await next(context);
//                return;
//            }
//            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
//            context.Response.ContentType = "application/json";
//            var resultU = JsonConvert.SerializeObject(
//             new BaseResponse
//             {
//                 StatusCode = 401,
//                 Message = "Unauthorized user."
//             }, new JsonSerializerSettings
//             {
//                 ContractResolver = new CamelCasePropertyNamesContractResolver()
//             });

//            await context.Response.WriteAsync(resultU);
//        }
//        else
//        {
//            await next(context);
//            return;
//        }

//    }
//}
