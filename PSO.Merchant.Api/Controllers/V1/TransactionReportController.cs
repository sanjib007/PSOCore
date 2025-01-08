using BLL.Services.Merchant;
using DAL.Models.Request.Merchant;
using Microsoft.AspNetCore.Mvc;
using PSO.Merchant.Api.Service;
using System.Drawing.Printing;
using Utility.Extentions;

namespace PSO.Merchant.Api.Controllers.V1;

public class TransactionReportController(IMerchantReportService merchantReportService, ICoreTransactionService _coreTransactionService,IHttpContextAccessor _httpContextAccessor) : BaseV1Controller
{
    [HttpGet("GetTransactions")]
    public async Task<IActionResult> GetTransactions(string? transactionId,
        DateTime? startDate,
        DateTime? endDate,
        string? transactionStatus,
        int pageNumber = 1,
        int pageSize = 10)
    {
        var result = await _coreTransactionService.GetTransactionReport(_httpContextAccessor.CurrentUserId(), transactionId, startDate, endDate, transactionStatus, pageNumber, pageSize);
        return Ok(result);
    }

    [HttpPost("DoVoid")]
    public async Task<IActionResult> DoVoid(TransactionRequest request)
    {
        var result = await _coreTransactionService.DoVoid(request);
        return Ok(result);
    }
   
    [HttpPost("DoRefund")]
    public async Task<IActionResult> DoRefund(TransactionRequest request)
    {
        var result = await _coreTransactionService.DoRefund(request);
        return Ok(result);
    }
    [HttpPost("SettlementRequest")]
    public async Task<IActionResult> SettlementRequest(TransactionRequest request)
    {
        var result = await _coreTransactionService.SettlementRequest(request);
        return Ok(result);
    }


    [HttpGet("DashbordReport")]
    public async Task<IActionResult> DashbordReport()
    {
        var result = await merchantReportService.GetDashboardReport(_httpContextAccessor.CurrentUserId());
        return Ok(result);
    }
}
