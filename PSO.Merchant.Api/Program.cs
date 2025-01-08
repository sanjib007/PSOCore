using MassTransit;
using DAL.Domain;
using Microsoft.EntityFrameworkCore;
using BLL.Core;
using Serilog;
using Microsoft.AspNetCore.HttpLogging;
using FluentValidation.AspNetCore;
using Endpoint.Filters;
using Microsoft.AspNetCore.Mvc;
using ApiEndPoint.Middleware;
using Endpoint.Extentions;
using System.Text.Json.Serialization;
using DAL.Domain.Partial;
using Endpoint.Middleware;
using PSO.Merchant.Api.Service;

var builder = WebApplication.CreateBuilder(args);

#region Serilog
builder.Host.AddSerilog(projectName: "PSO.Merchant.Api");
#endregion

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add<FluentValidationCustomResponse>();
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;

}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

}).AddFluentValidation();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// filter
builder.Services.AddScoped<DuplicateRequestProcess>();
builder.Services.SwaggerServiceBuild();
builder.Services.AddTransient<MerchantTokenManagerMiddleware>();
// Database 
var conn = builder.Configuration.GetValue<string>("ConnectionStrings:Connection");
builder.Services.AddDbContext<E_PSOContext>(item =>
{
    item.UseSqlServer(conn, sqlServerOptions => sqlServerOptions.CommandTimeout(60)).EnableSensitiveDataLogging();
});

#region Global Cors Policy
builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
       builder =>
       {
           builder.AllowAnyHeader()
                  .AllowAnyMethod()
                  .SetIsOriginAllowed((host) => true)
                  .AllowCredentials();
       }));
#endregion

#region Http logging service
// http logging service
builder.Services.AddHttpLogging(httpLogging =>
{
    httpLogging.LoggingFields = HttpLoggingFields.All;
    // Add log data need to show from header
    httpLogging.RequestHeaders.Add("DeviceId");
    httpLogging.RequestHeaders.Add("Authorization");
    httpLogging.RequestHeaders.Add("X-API-KEY");
});

#endregion

//bootstraper
builder.Services.BuildBootStrapper(builder.Configuration);
builder.Services.AddTransient<ICoreTransactionService, CoreTransactionService>();
#region ApiAuthentication
builder.Services.ApiAuthServiceBuild();
#endregion



var app = builder.Build();


// Configure the HTTP request pipeline.
app.SwaggerAppBuild();
// http logging
app.UseHttpLogging();
// middleware
app.UseMiddleware<RequestMiddleware>();

app.UseHttpsRedirection();
// global cors policy
app.UseCors("CorsPolicy");
app.UseStaticFiles();

app.UseAuthentication();
app.UseMiddleware<MerchantTokenManagerMiddleware>();
app.UseAuthorization();

app.MapControllers();

// seed
app.UseSeedDatabase();
app.Run();
