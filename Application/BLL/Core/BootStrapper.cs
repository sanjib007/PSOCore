using Microsoft.Extensions.DependencyInjection;
using BLL.Core.Validators;
using BLL.Core.IOC;
using Microsoft.Extensions.Configuration;

namespace BLL.Core
{
    public static class BootStrapper 
    {
        public static void BuildBootStrapper(this IServiceCollection services, IConfiguration configuration)
        {
            // Dependency Injection 
            ServiceDI.BuidServices(services, configuration);
            RepositoryDI.BuidServices(services,configuration);


            // validator
            ValidatorProvider.BuildValidator(services);

        }
    }
}
