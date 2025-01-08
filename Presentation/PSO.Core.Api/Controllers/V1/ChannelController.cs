using BLL.Services;
using DAL.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utility;

namespace PSOCore.Api.Controllers.V1;

public class ChannelController(ICheckoutService _checkoutService, ITransactionService _transactionService) : BaseV1Controller
{
    #region Nagad
    [AllowAnonymous]
    [HttpGet("~/api/v1/nagad-confirm-return")]
    public async Task<IActionResult> NagadPaymentConfirmReturn(
      string merchant,
      string order_id,
      string payment_ref_id,
      string status,
      string status_code,
      string message
      )
    {
        var result = await _transactionService.Nagad_ProcessRedirect(order_id, status, payment_ref_id);
        return TransactionWebResultRedirect(result);
    }

    #endregion

    #region Mastercard

    [AllowAnonymous]
    [HttpGet("~/api/v1/mastercard-return")]
    public async Task<IActionResult> Addmoney_mastercard_return(string resultIndicator, string sessionVersion, string checkoutVersion)
    {
        var result = await _transactionService.Mastercard_ProcessRedirectAddMoney(resultIndicator);
        return TransactionWebResultRedirect(result);
    }

    #endregion

    #region VISA
    [AllowAnonymous]
    [HttpPost("~/payment-aggregator-web/cs/approve")]
    [Consumes("application/x-www-form-urlencoded")]
    public async Task<IActionResult> VisaApprove([FromForm] IFormCollection request)
    {
        var paymentRequest = request.AsObject<VisaPaymentApproveRequest>();
        var result = await _transactionService.Visa_ProcessRedirectAddMoney(paymentRequest);
        return TransactionWebResultRedirect(result);
    }
    [AllowAnonymous]
    [HttpPost("~/payment-aggregator-web/cancel")]
    public async Task<IActionResult> VisaCancel([FromForm] VisaCancelRequest paymentRequest)
    {

        var request = new VisaPaymentApproveRequest
        {
            decision = paymentRequest.decision,
            req_reference_number = paymentRequest.req_reference_number
        };
        var result = await _transactionService.Visa_ProcessRedirectAddMoney(request);
        return TransactionWebResultRedirect(result);
    }
    #endregion
}