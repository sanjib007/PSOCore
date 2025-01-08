using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PSO.Checkout.Spa;
using PSO.Checkout.Spa.Service;
using PSO.Checkout.Spa.Utility.Handler;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddTransient<ICheckoutService, CheckoutService>();
builder.Services.AddTransient<HttpClientRequestHandler>();
builder.Services.AddSingleton<SpinnerService>();
builder.Services.AddHttpClient("")
                //.ConfigurePrimaryHttpMessageHandler(() =>
                //{
                //    var handler = new HttpClientHandler();
                //    handler.ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                //    return handler;
                //})
                .AddHttpMessageHandler<HttpClientRequestHandler>();
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddBlazorBootstrap();
await builder.Build().RunAsync();
