using Microsoft.AspNetCore.Mvc;
using PSO.Core.Api.Gateway.Models.Response;
using System.Collections.Specialized;
using Utility;

namespace PSO.Core.Api.Gateway.Controllers;

public class WebBaseController : Controller
{

    public IActionResult TransactionWebResultRedirect(CoreTransactionConfirmResponse response)
    {
        var data = response.data;
        var nameValueCollection = new NameValueCollection
        {
            {"StatusCode", response.statusCode.ToString()},
            {"Message",response.message},
            {"TransactionId", data.transactionId},
            {"TransactionAmount", data.transactionAmount.ToTruncateDecimal().ToString()},
            {"TransactionStatus", data.transactionStatus},
            {"TransactionOrderId", data.transactionOrderId},
            {"ReferenceTransactionId", data.referenceTransactionId},
            {"TransactionCompletionDateTime",data.transactionCompletionDateTime.ToString()},
            {"Identifier",data.identifier},
            {"TransactionCurrencyCode",data.transactionCurrencyCode},
            {"TransactionType",data.transactionType}
        };
        if (response.statusCode == 200)
        {
            Utility.Utils.RedirectWithData(nameValueCollection, data.successReturnUrl, HttpContext);
        }
        else
        {
            Utility.Utils.RedirectWithData(nameValueCollection, data.failReturnUrl, HttpContext);
        }
        return new EmptyResult();
    }

}
