using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net;


public class HttpClientRequestHandler : DelegatingHandler
{
    private readonly ILocalStorageService _localStoreService;
    private readonly SpinnerService _spinnerService;
    private readonly NavigationManager _navigationManager;

    public HttpClientRequestHandler(ILocalStorageService localStoreService, SpinnerService spinnerService, NavigationManager navigationManager)
    {
        _localStoreService = localStoreService;
        _spinnerService = spinnerService;
        _navigationManager = navigationManager;
    }
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {

        try
        {
            _spinnerService.Show();

            var accessToken = await _localStoreService.GetItemAsync<string>(Constant.AccessToken);
            if (!request.RequestUri.AbsoluteUri.Contains("Login"))
            {
                if (!string.IsNullOrEmpty(accessToken))
                {
                    request.Headers.Add("Authorization", $"Bearer {accessToken}");
                }
            }


            var requestLog = await request.ToHttpLog();
            var response = await base.SendAsync(request, cancellationToken);
            var responseLog = await request.ToHttpLog();
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized || HttpStatusCode.Forbidden == response.StatusCode)
            {

                _navigationManager.NavigateTo($"/login", true);
            }
            return response;

        }
        catch (Exception ex)
        {
            _navigationManager.NavigateTo($"/result?statusCode={100}&message={ex.Message}", true);
            throw;
        }
        finally
        {
            _spinnerService.Hide();

        }
        return new HttpResponseMessage();
    }
}
