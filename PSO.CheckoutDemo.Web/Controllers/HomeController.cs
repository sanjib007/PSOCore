using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PSO.CheckoutDemo.Web.Models;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using Utility;
using Utility.Extentions;

namespace PSO.CheckoutDemo.Web.Controllers
{

    public class HomeController : Controller
    {
        private const string CheckoutRoute = "Payment/Initate";
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(ILogger<HomeController> logger,
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("checkout/Confirm")]
        //[Route("checkout/Confirm")]
        //public IActionResult checkout_Confirm(string statusCode, string message, decimal amount, string transactionId, string referenceId)
        public IActionResult checkout_Confirm([FromQuery] CheckoutConfirmTransactionRequest request)
        {
            // need to validate by referenceId

            ViewBag.status = request.status;
            ViewBag.message = request.message;
            ViewBag.transactionId = request.transactionTrackingNo;
            ViewBag.amount = request.amount;
            ViewBag.transactionDate = request.transactionDate;
            return View();
        }
        #region Finpay


        //[Route("Payment/Finpay")]
        [HttpPost("Payment/Initate")]
        public async Task<IActionResult> PaymentInitate([FromBody] CheckoutInitiateRequestModel requestModel)
        {
            var result = await ETCheckout(requestModel.Amount);
            return Json(result);
        }

        private async Task<object> ETCheckout(decimal amount)
        {
            //var currentDomain = HttpContext.GetCurrentDomain();
            var currentDomain = _configuration.GetSection("CurrentDomain").Value;
            var api_request = new PaymentInitiateRequestModel
            {
                orgCode = _configuration.GetValue<string>("ETConfig:OrgCode"),
                returnUrl = $"{currentDomain + "/checkout/confirm"}",
                description = "Product transaction payment",
                customerId = "Test Biller",
                customerWalletNo = $"01711703573",
                merchantImg = "https://seeklogo.com/images/D/darazlogo-7E01DFC36D-seeklogo.com.png",
                transactionTrackingNo = "REF" + Utility.Utils.GenerateTransactionId(),
                billingAddress = "Gulshin, Dhaka",
                invoiceNo = "INV" + Utils.GenerateTransactionId(),
                amount = amount                                
            };
            var baseUrl = $"{_configuration.GetSection("ETConfig:BaseUrl").Value}/api/v1/payment/init";
           
            //var response = await _httpClient.PostDataAsJsonAsync<PaymentRediractResponseModel>($"{baseUrl}/api/v1/payment/init", api_request);

            var client = _httpClientFactory.CreateClient("Dmoney");
            client.DefaultRequestHeaders.Add("clientId", _configuration.GetValue<string>("ETConfig:ClientId"));
            client.DefaultRequestHeaders.Add("clientSecret", _configuration.GetValue<string>("ETConfig:ClientSecret"));
            var jsonObj = JsonConvert.SerializeObject(api_request);
            var jsonContent = new StringContent(jsonObj, Encoding.UTF8, "application/json");

            // Send the POST request
            var response = await client.PostAsync(baseUrl, jsonContent);
            var response_data = await response.Content.ReadAsStringAsync();
            var api_response = JsonConvert.DeserializeObject<PaymentRediractResponseModel>(response_data);
            return api_response;
            
        }
        #endregion



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
       
    }

   

}
