using DAL.Models.Response;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using System.Net;
using Utility.Exceptions;

namespace ApiEndPoint.Middleware
{
    public class RequestMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext, IWebHostEnvironment env)
        {

            try
            {
                httpContext.Request.EnableBuffering();
                await _next(httpContext);
            }
            catch (BaseException ex)
            {
                await HandleApplicationExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, env, ex);
            }
        }


        private async Task HandleApplicationExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            var result = JsonConvert.SerializeObject(
                new BaseResponse
                {
                    StatusCode = 100,
                    Message = exception.Message
                }, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });

            await context.Response.WriteAsync(result);
        }

        private async Task HandleExceptionAsync(HttpContext context, IWebHostEnvironment env, Exception exception)
        {
            if (env.IsDevelopment())
            {
                // Logger
                Log.Error(exception, "Global Exception");
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                await context.Response.WriteAsync(new ErrorDetails()
                {
                    StatusCode = 100,
                    Message = exception.Message,
                    Data = exception

                }.ToString());
            }
            else
            {
                // Logger
                Log.Error(exception, "Global Exception");
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                await context.Response.WriteAsync(new ErrorDetails()
                {
                    StatusCode = 100,
                    Message = "Something went wrong. Please contact to administrator."

                }.ToString());
            }

        }
    }

    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }
    }


}
