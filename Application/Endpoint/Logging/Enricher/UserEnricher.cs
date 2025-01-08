using Microsoft.AspNetCore.Http;
using Serilog.Core;
using Serilog.Events;
using Utility.Extentions;

namespace ApiEndPoint.Logging.Enricher
{
    public class UserEnricher : ILogEventEnricher
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserEnricher() : this(new HttpContextAccessor())
        {
        }

        //Dependency injection can be used to retrieve any service required to get a user or any data.
        //Here, I easily get data from HTTPContext
        public UserEnricher(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
                    "Current_UserFullName", _httpContextAccessor.CurrentUserFullName() ?? ""));  
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
                    "Current_UserNumber", _httpContextAccessor.CurrentUserNumber() ?? ""));  
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
                    "Current_UserId", _httpContextAccessor.CurrentUserId().ToString() ?? "")); 

        }
    }

}
