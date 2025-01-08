using PSO.Checkout.Spa.Models.Response;
using PSO.Checkout.Spa.Utility.Extentions;

namespace PSO.Checkout.Spa.Service;

public interface ICheckoutService
{
    Task<GetIdentifierApiResponse> GetIdentifierDetails(string identifier);
    Task<InitiateTransactionApiReponse> InitateTransaction(string identifier, long channelTypeId);
}
public class CheckoutService(HttpClient _httpClient, IConfiguration _configuration) : ICheckoutService
{
    public async Task<GetIdentifierApiResponse> GetIdentifierDetails(string identifier)
    {
        var response = await _httpClient.GetDataAsJsonAsync<GetIdentifierApiResponse>(_configuration.GetSection("AppConfig:CoreBaseUrl").Value + $"api/v1/Checkout/GetIdentifier?Identifier={identifier}");
        return response;
    }
    
    public async Task<InitiateTransactionApiReponse> InitateTransaction(string identifier,long channelTypeId)
    {
        var request = new
        {
            Identifier = identifier,
            ChannelTypeId = channelTypeId
        };
        var response = await _httpClient.PostDataAsJsonAsync<InitiateTransactionApiReponse>($"{_configuration.GetSection("AppConfig:CoreBaseUrl").Value}api/v1/Checkout/InitiateCheckoutTransaction", request);
        return response;
    }
}
