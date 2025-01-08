using BLL.Services;
using BLL.Services.Communication.Cybersource;
using BLL.Services.Communication.Mastercard;
using BLL.Services.Communication.Nagad;
using BLL.Services.Merchant;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Core.IOC
{
    public static class ServiceDI
    {
        public static void BuidServices(IServiceCollection services, IConfiguration configuration)
        {

            #region Bll

            services.AddTransient<CybersourceSecurity>();
            services.AddTransient<IMerchantReportService, MerchantReportService>();
            services.AddTransient<INagadComunicationService, NagadCommunicationService>();
            services.AddTransient<ICybersourceCommunicationService, CybersourceCommunicationService>();
            services.AddTransient<IMastercardCommunicationService, MastercardCommunicationService>();
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<ICheckoutService, CheckoutService>();
            services.AddTransient<IAccountApiAuthorizationService, AccountApiAuthorizationService>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IMerchantAuthService, MerchantAuthService>();
            services.AddHttpContextAccessor();

            services.AddHttpClient("")
             .ConfigurePrimaryHttpMessageHandler(() =>
             {
                 var handler = new HttpClientHandler();
                 handler.ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                 return handler;
             });
            //.AddHttpMessageHandler<HttpClientRequestHandler>();
          

            #endregion


            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetValue<string>("Redis:Server");
                options.InstanceName = configuration.GetValue<string>("Redis:Database");
            });
        }

    }
}
