using DAL.Models.Request;
using DAL.Models.Request.Merchant;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Core.Validators
{
    public static class ValidatorProvider
    {
        public static void BuildValidator(IServiceCollection services)
        {
            //validator
            services.AddTransient<IValidator<ChangePasswordRequest>, ChangePasswordRequestValidator>();
            services.AddTransient<IValidator<TransactionRequest>, TransactionRequestValidator>();
            services.AddTransient<IValidator<MerchantLoginRequest>, MerchantLoginRequestValidator>();
            services.AddTransient<IValidator<AccountApiAuthorizationRegistrationRequest>, AccountApiAuthorizationRegistrationRequestValidator>();
            services.AddTransient<IValidator<UserCreateRequest>, UserCreateRequestValidator>();
            services.AddTransient<IValidator<CheckoutInitiateRequest>, CheckoutInitiateRequestValidator>();
            services.AddTransient<IValidator<CheckoutGetIdentifierRequest>, CheckoutGetIdentifierRequestValidator>();
            services.AddTransient<IValidator<CheckoutTransactionInitiateRequest>, CheckoutTransactionInitiateRequestValidator>();
         

        }
    }
}
