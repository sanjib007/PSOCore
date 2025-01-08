using BLL.Services;
using BLL.Services.Merchant;
using DAL.Models.Request;
using DAL.Models.Request.Merchant;
using Endpoint.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utility.Extentions;

namespace PSOCore.Api.Controllers.V1;

public class MerchantReportController(IMerchantReportService _merchantReport, IHttpContextAccessor _httpContext) : BaseV1Controller
{
    [AllowAnonymous]
    [HttpGet("Transactions")]
    public async Task<IActionResult> Transactions(long accountId,
        string? transactionId,
        DateTime? startDate,
        DateTime? endDate,
        string? transactionStatus,
        int pageNumber = 1,
        int pageSize = 10)
    {
        var result = await _merchantReport.GetMerchantTransactions(accountId, transactionId, startDate, endDate, transactionStatus, pageNumber, pageSize);
        return Ok(result);
    }
    [ServiceFilter(typeof(DuplicateRequestProcess))]
    [AllowAnonymous]
    [HttpPost("DoVoid")]
    public async Task<IActionResult> DoVoid(TransactionRequest request)
    {
        var result = await _merchantReport.DoVoid(request);
        return Ok(result);
    }
    [ServiceFilter(typeof(DuplicateRequestProcess))]
    [AllowAnonymous]
    [HttpPost("DoRefund")]
    public async Task<IActionResult> DoRefund(TransactionRequest request)
    {
        var result = await _merchantReport.DoRefund(request);
        return Ok(result);
    }
    [ServiceFilter(typeof(DuplicateRequestProcess))]
    [AllowAnonymous]
    [HttpPost("SettlementRequest")]
    public async Task<IActionResult> SettlementRequest(TransactionRequest request)
    {
        var result = await _merchantReport.SettlementRequest(request);
        return Ok(result);
    }
}




