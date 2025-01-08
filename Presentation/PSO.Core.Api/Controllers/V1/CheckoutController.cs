using BLL.Services;
using DAL.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PSOCore.Api.Controllers.V1;
 
public class CheckoutController(ICheckoutService _checkoutService) : BaseV1Controller
{
    #region Merchant Access

    //[ServiceFilter(typeof(DuplicateRequestProcess))]
    [AllowAnonymous]
    [HttpPost("Initiate")]
    public async Task<IActionResult> CheckoutInitiate(CheckoutInitiateRequest request)
    {
        var result = await _checkoutService.InitiateCheckout(request);
        return Ok(result);
    }

    #endregion


    #region Call From Web, Internal

    [AllowAnonymous]
    [HttpGet("GetIdentifier")]
    public async Task<IActionResult> GetIdentifier(string identifier)
    {
        var request = new CheckoutGetIdentifierRequest(Identifier: identifier);
        var result = await _checkoutService.GetIdentifier(request);
        return Ok(result);
    }
    [AllowAnonymous]
    [HttpPost("InitiateCheckoutTransaction")]
    public async Task<IActionResult> InitiateCheckoutTransaction(CheckoutTransactionInitiateRequest request)
    {
        var result = await _checkoutService.InitiateCheckoutTransaction(request);
        return Ok(result);
    }

    //[AllowAnonymous]
    //[HttpPost("CompleteCheckoutTransaction")]
    //public async Task<IActionResult> CompleteCheckoutTransaction(CheckoutTransactionCompleteRequest request)
    //{
    //    var result = await _checkoutService.CompleteCheckoutTransaction(request);
    //    return Ok(result);
    //}

    #endregion
}



