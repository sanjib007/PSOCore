using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PSO.Merchant.Spa;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

#region IOC
// Auth
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddAuthorizationCore();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ITransactionService, TransactionService>();
builder.Services.AddTransient<HttpClientRequestHandler>();
builder.Services.AddSingleton<SpinnerService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddHttpClient("")
                //.ConfigurePrimaryHttpMessageHandler(() =>
                //{
                //    var handler = new HttpClientHandler();
                //    handler.ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                //    return handler;
                //})
                .AddHttpMessageHandler<HttpClientRequestHandler>();

builder.Services.AddBlazorBootstrap();
#endregion

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
