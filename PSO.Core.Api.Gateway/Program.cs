using ApiEndPoint.Middleware;
using Endpoint.Extentions;
using Microsoft.AspNetCore.HttpLogging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using PSO.Core.Api.Gateway.Services;
using Utility.Extentions;

var builder = WebApplication.CreateBuilder(args);
#region Serilog
builder.Host.AddSerilog(projectName: "PSOCore.Api.Gateway");
#endregion
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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
// ocelot configuration file add
#region Ocelot
builder.Configuration.AddJsonFile("configuration.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"configuration.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true);
builder.Services.AddOcelot(builder.Configuration);
builder.Services.ConfigureDownstreamHostAndPortsPlaceholders(builder.Configuration);
#endregion

#region IOC
builder.Services.AddTransient<ICoreGatewayTransactionService, CoreGatewayTransactionService>();
builder.Services.AddHttpClient();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// http logging
app.UseHttpLogging();

// middleware
app.UseMiddleware<RequestMiddleware>();
app.UseRouting();
app.UseHttpsRedirection();
// global cors policy
app.UseCors("CorsPolicy");
app.UseAuthorization();

app.UseEndpoints(e =>
{
    _ = e.MapControllers();
});
//app.MapControllers();
app.UseOcelot().Wait();
app.Run();
