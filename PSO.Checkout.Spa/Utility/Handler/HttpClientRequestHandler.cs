
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PSO.Checkout.Spa.Service;

namespace PSO.Checkout.Spa.Utility.Handler
{
    public class HttpClientRequestHandler : DelegatingHandler
    {
        private readonly SpinnerService _spinnerService;
        private readonly NavigationManager _navigationManager;
        private string? sessionToken;
        public string? hardwareSignature;

        public HttpClientRequestHandler(IJSRuntime JSRuntime, SpinnerService spinnerService, NavigationManager navigationManager)
        {
            this.JSRuntime = JSRuntime;
            _spinnerService = spinnerService;
            _navigationManager = navigationManager;
        }

        public IJSRuntime JSRuntime { get; }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {

            try
            {
                _spinnerService.Show();

                //var requestLog = await request.ToHttpLog();
                var response = await base.SendAsync(request, cancellationToken);
                //var responseLog = await request.ToHttpLog();
                //if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized || HttpStatusCode.Forbidden == response.StatusCode)
                //{
                //    // call to refresh tokon for again authentication 
                //    // redirect to login page ;
                //}
                return response;

            }
            catch (Exception ex)
            {
                //_navigationManager.NavigateTo($"/result?statusCode={100}&message={ex.Message}", true);
                throw;
                //return new HttpResponseMessage();
            }
            finally
            {
                _spinnerService.Hide();

            }
            return new HttpResponseMessage();
        }
    }

}
