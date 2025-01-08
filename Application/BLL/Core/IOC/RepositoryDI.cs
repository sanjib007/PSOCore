using DAL.Repositories;
using DAL.UnitOfWork;
using DLL.Repositories.Cache;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace BLL.Core.IOC
{
    public static class RepositoryDI
    {
        public static void BuidServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region DLL

            services.AddTransient<ICon_MerchantPortalAuthorizationRepository, Con_MerchantPortalAuthorizationRepository>();
            services.AddTransient<IGen_BankInformationRepository, Gen_BankInformationRepository>();
            services.AddTransient<ICon_PaymentAcceptProfileRepository, Con_PaymentAcceptProfileRepository>();
            services.AddTransient<ITran_CheckoutInfoRepository, Tran_CheckoutInfoRepository>();
            services.AddTransient<ITran_TransactionInformationRepository, Tran_TransactionInformationRepository>();
            services.AddTransient<IMerchantKYCRegistrationRepository, MerchantKYCRegistrationRepository>();
            services.AddTransient<IGen_AccountInformationRepository, Gen_AccountInformationRepository>();
            services.AddTransient<ICon_AccountApiAuthorizationRepository, Con_AccountApiAuthorizationRepository>();
            services.AddTransient<IApplicationLogRepository, ApplicationLogRepository>();
            services.AddTransient<ICacheRepository, RedisCacheRepository>();
            services.AddTransient<ICon_ChannelRepository, Con_ChannelRepository>();
            services.AddTransient<ICon_ChannelTypeRepository, Con_ChannelTypeRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            #endregion

            //common
            services.AddScoped<IDatabase>(cfg =>
            {
                IConnectionMultiplexer multiplexer = ConnectionMultiplexer.Connect($"{configuration.GetValue<string>("Redis:Server")}");
                return multiplexer.GetDatabase();
            });

        }



    }
}
