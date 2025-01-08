
using Microsoft.AspNetCore.HttpOverrides;
using Serilog;
using Serilog.Formatting.Compact;
using Endpoint.Extentions;

var builder = WebApplication.CreateBuilder(args);
builder.Host.AddSerilog(projectName: "PSO.Checkout.Web");
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

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient("")
                .ConfigurePrimaryHttpMessageHandler(() =>
                {
                    var handler = new HttpClientHandler();
                    handler.ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                    return handler;
                });
               //.AddHttpMessageHandler<HttpClientRequestHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});
// global cors policy
app.UseCors("CorsPolicy");
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
