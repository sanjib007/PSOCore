using DAL.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;
using Utility;

namespace PSOCore.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class BaseV1Controller : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("TransactionWebResultRedirect")]
    public IActionResult TransactionWebResultRedirect(BaseResponse response)
    {
        var data = (TransactionConfirmResponse)response.Data;
        var nameValueCollection = new NameValueCollection
        {
            {"StatusCode", response.StatusCode.ToString()},
            {"Message",response.Message},
            {"TransactionId", data.TransactionId},
            {"TransactionAmount", data.TransactionAmount.ToTruncateDecimal().ToString()},
            {"TransactionStatus", data.TransactionStatus},
            {"TransactionOrderId", data.TransactionOrderId},
            {"ReferenceTransactionId", data.ReferenceTransactionId},
            {"TransactionCompletionDateTime",data.TransactionCompletionDateTime.ToString()},
            {"Identifier",data.Identifier},
            {"TransactionCurrencyCode",data.TransactionCurrencyCode},
            {"TransactionType",data.TransactionType}
        };
        if (response.StatusCode == 200)
        {
            Utility.Utils.RedirectWithData(nameValueCollection, data.SuccessReturnUrl, HttpContext);
        }
        else
        {
            Utility.Utils.RedirectWithData(nameValueCollection, data.FailReturnUrl, HttpContext);
        }
        return new EmptyResult();
    }
}
