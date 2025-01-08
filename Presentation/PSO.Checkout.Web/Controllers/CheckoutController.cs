using FinPayCore.Web.Models;
using Microsoft.AspNetCore.Mvc;
using PSO.Checkout.Web.Models.Request;
using PSO.Checkout.Web.Models.Response;
using System.Security.Cryptography;
using System.Text;
using TapCore.Web.Models;
using Utility.Extentions;
using Utility;
using static System.Net.WebRequestMethods;
using static Utility.Enums.SystemEnum;
using System.Collections.Specialized;


namespace PSO.Checkout.Web.Controllers;

public class CheckoutController(IConfiguration _configuration, HttpClient _httpClient) : Controller
{
    public async Task<IActionResult> Index(string identifier)
    {
        var result_core = await _httpClient.GetAsJsonAsync<GetIdentifierResponse>($"{_configuration.GetSection("CoreApi:Url").Value}/api/v1/Checkout/GetIdentifier?Identifier={identifier}");

        @ViewBag.MerchantName = result_core.data.merchantName;
        @ViewBag.RedirectURL = "https://localhost:7045/checkout";
        @ViewBag.OrderId = result_core.data.orderId;
        @ViewBag.Amount = result_core.data.amount + " BDT";
        @ViewBag.Identifier = identifier;

        return View();
    }

    [HttpPost("Checkout/MasterCard")]
    public async Task<IActionResult> MasterCard()
    {
        var request = new
        {
            apiOperation = "INITIATE_CHECKOUT",
            order = new
            {
                id = Utility.Utils.GenerateTransactionId(),
                currency = "BDT",
                amount = "121",
                description = "TAKA"
            },
            interaction = new
            {
                operation = "PURCHASE",
                merchant = new
                {
                    name = "Pso Merchant"

                }
            }
        };

        _httpClient.DefaultRequestHeaders.Add("Authorization", "Basic bWVyY2hhbnQuVEVTVDQwOTkwMDAyOmJhMTE2ZjliNzdjMDg4Nzk3NDRhYzk0YjNhMTk5MzA2");
        var response = await _httpClient.PostAsJsonAsync<MasterInitiateCheckoutResponse>
            ("https://easternbank.test.gateway.mastercard.com/api/rest/version/78/merchant/TEST40990002/session", request);

        var result = new
        {
            StatusCode = 200,
            Message = "Ok",
            Data = new
            {
                MerchantId = response.merchant,
                OrderId = request.order.id,
                SessionId = response.session.id,
                SessionVersion = response.session.version,
                SuccessIndicator = response.successIndicator,
                Currency = request.order.currency
            }
        };
        return Json(result);
    }


    #region Cybersource - visa

    [HttpPost("Checkout/Visa")]
    public async Task<IActionResult> Visa([FromBody] InitiateTransactionRequest request)
    {

        var api_request = new
        {
            Identifier = request.Identifier,
            ChannelTypeId = request.ChannelTypeId,
        };

        var result_core = await _httpClient.PostAsJsonAsync<InitiateCheckoutTransactionResponse>($"{_configuration.GetSection("CoreApi:Url").Value}/api/v1/Checkout/InitiateCheckoutTransaction", api_request);
        var parameters = new Dictionary<string, string>
        {
            { "access_key", Security.ACCESS_KEY },
            { "profile_id", Security.PROFILE_ID },
            { "transaction_uuid", result_core.data.TransactionId },
            //{ "transaction_uuid", Guid.NewGuid().ToString() },
            { "consumer_id", result_core.data.merchantName },
            { "signed_field_names", "access_key,profile_id,transaction_uuid,consumer_id,signed_field_names,unsigned_field_names,signed_date_time,locale,transaction_type,reference_number,auth_trans_ref_no,amount,currency,bill_to_forename,bill_to_surname,bill_to_email,bill_to_phone,bill_to_address_line1,bill_to_address_city,bill_to_address_country,bill_to_postal_code" },
            { "transaction_type", "sale" },
            { "currency", result_core.data.TransactionCurrencyCode },
            { "unsigned_field_names", "" },
            { "signed_date_time", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ") },
            { "locale", "en" },
            { "amount", result_core.data.TransactionAmount.ToTruncateDecimal().ToString() },
            { "bill_to_forename", "Mr" },
            { "bill_to_surname", "Ovi" },
            { "bill_to_email", "null@cybersource.com" },
            { "bill_to_phone", "00000000000" },
            { "bill_to_address_line1", "Gulshan" },
            { "bill_to_address_city", "Dhaka" },
            { "bill_to_address_country", "BD" },
            { "bill_to_postal_code", "0000" }

            //{ "payment_method", "card" },
            //{ "card_type", "001" },
            //{ "card_type_selection", "001" } // 001 for Visa, you may need to check with Cybersource for the correct code
        };

        //var referenceNumber = "PWHC" + DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(1000, 9999) + new Random().Next(100, 999);
        parameters.Add("reference_number", result_core.data.TransactionId);
        parameters.Add("auth_trans_ref_no", result_core.data.TransactionId);

        // Sign the parameters
        var signature = Security.Sign(parameters);
        parameters.Add("signature", signature);

        // Now you can proceed with your logic using the signature
        return Json(new
        {
            StatusCode = 200,
            Data = new
            {
                payUrl = "https://testsecureacceptance.cybersource.com/pay",
                parameters

            }
        });


    }

    //[HttpPost("Checkout/Visa/Approve")]
    [HttpPost("payment-aggregator-web/cs/approve")]
    public async Task<IActionResult> VisaApprove([FromForm] VisaPaymentApproveRequest paymentRequest)
    {

        if (paymentRequest.decision == "ACCEPT")
        {
            var api_request = new
            {
                TransactionId = paymentRequest.bill_trans_ref_no,
                ChannelReferenceId = paymentRequest.transaction_id,
                Status = TxnStatus.Authorized.ToString(),
            };
            var result_core = await _httpClient.PostAsJsonAsync<CompleteCheckoutTransactionResponse>($"{_configuration.GetSection("CoreApi:Url").Value}/api/v1/Checkout/CompleteCheckoutTransaction", api_request);
            if (result_core.statusCode == 200)
            {

                var nameValueCollection = new NameValueCollection
                {
                    {"StatusCode", "200"},
                    {"Message","Transaction Authorized."},
                    {"TransactionId", result_core.data.transactionData.TransactionId},
                    {"TransactionAmount", result_core.data.transactionData.TransactionAmount.ToTruncateDecimal().ToString()},
                    {"TransactionStatus", result_core.data.transactionData.TransactionStatus},
                    {"TransactionOrderId", result_core.data.transactionData.TransactionOrderId},
                    {"ReferenceTransactionId", result_core.data.transactionData.ReferenceTransactionId},
                    {"TransactionCompletionDateTime",result_core.data.transactionData.TransactionCompletionDateTime.ToString()},
                    {"Identifier",result_core.data.transactionData.Identifier},
                    {"TransactionCurrencyCode",result_core.data.transactionData.TransactionCurrencyCode},
                    {"TransactionType",result_core.data.transactionData.TransactionType}
                };
                Utility.Utils.RedirectWithData(nameValueCollection, result_core.data.redirectUrl, HttpContext);
                return new EmptyResult();
            }
            else
            {
                // TODO: Return fail page to gateway

                //Utility.Utils.RedirectWithData(result_core.data.transactionDataCollection, result_core.data.redirectUrl, HttpContext);
            }
        }

        return View("PaymentSuccess");
        ////return null;
    }

    [HttpPost("payment-aggregator-web/cancel")]
    //[HttpPost("Checkout/Visa/cancel"),]
    public async Task<IActionResult> VisaCancel([FromForm] VisaCancelRequest paymentRequest)
    {
        return View("Index");
    }


    public static class Security
    {
        public const string HMAC_SHA256 = "sha256";
        public const string SECRET_KEY = "0e032e0371ca45b59fe1bced7640760024db5cc3734a434e99e7077f00080ff2617f8e6830b54f0e9d08e81d9492ea09afccc74304b34262b3c1f380f80f0a548360c8527ce34865af36c6b71c2f47a78eef7b61a6694077aaa060edab6b4ce71dee8583e303499284d912afcd9e58a82462f158d5f841e8b3c72d2bd8107c94";
        public const string ACCESS_KEY = "caf9892420663fd699cc09c00193651d";
        public const string PROFILE_ID = "D38680D2-42B3-4924-9BB6-07C3ADA9D82D";

        public static string Sign(Dictionary<string, string> parameters)
        {
            return SignData(BuildDataToSign(parameters), SECRET_KEY);
        }

        private static string SignData(string data, string secretKey)
        {
            var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
            return Convert.ToBase64String(hash);
        }

        private static string BuildDataToSign(Dictionary<string, string> parameters)
        {
            var signedFieldNames = parameters["signed_field_names"].Split(',');
            var dataToSign = new List<string>();
            foreach (var field in signedFieldNames)
            {
                dataToSign.Add(field + "=" + parameters[field]);
            }
            return CommaSeparate(dataToSign);
        }

        private static string CommaSeparate(List<string> dataToSign)
        {
            return string.Join(",", dataToSign);
        }
    }

    #endregion

}
