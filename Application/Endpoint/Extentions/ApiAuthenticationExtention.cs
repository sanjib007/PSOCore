using DAL.Models.Response;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Text;
using Utility;

namespace Endpoint.Extentions
{
    public static class ApiAuthenticationExtention
    {
        public static void ApiAuthServiceBuild(this IServiceCollection services)
        {
            #region JWT Api Authentication

            string authSerect = Authenticator.AuthJWTSerect;
            var key = Encoding.ASCII.GetBytes(authSerect);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Events = new JwtBearerEvents
                {
                    OnChallenge = async context =>
                    {
                        // Call this to skip the default logic and avoid using the default response
                        context.HandleResponse();

                        // Write to the response in any way you wish
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        context.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(
                         new BaseResponse
                         {
                             StatusCode = 401,
                             Message = "Unauthorized user."
                         }, new JsonSerializerSettings
                         {
                             ContractResolver = new CamelCasePropertyNamesContractResolver()
                         });

                        await context.Response.WriteAsync(result);
                    }
                };
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            #endregion

            #region Global Authorization
            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });
            #endregion
        }

    }
}
