using DAL.Domain;
using DAL.Models.Request.Merchant;
using DAL.Models.Response;
using DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;
using Utility.Extentions;
using static Utility.Enums.SystemEnum;

namespace BLL.Services.Merchant;

public interface IMerchantReportService
{
    Task<BaseResponse> DoRefund(TransactionRequest request);
    Task<BaseResponse> DoVoid(TransactionRequest request);
    Task<BaseResponse> GetDashboardReport(long accountId);
    Task<BaseResponse> GetMerchantTransactions(long accountId,
        string? transactionId,
        DateTime? startDate,
        DateTime? endDate,
        string? transactionStatus,
        int pageNumber = 1,
        int pageSize = 10);
    Task<BaseResponse> SettlementRequest(TransactionRequest request);
}
public class MerchantReportService(IUnitOfWork _unitOfWork) : IMerchantReportService
{
    public async Task<BaseResponse> GetMerchantTransactions(long accountId,
        string? transactionId,
        DateTime? startDate,
        DateTime? endDate,
        string? transactionStatus,
        int pageNumber = 1,
        int pageSize = 10)
    {
        var result = await _unitOfWork.Tran_TransactionInformationRepository.GetMerchantTransactions(accountId, transactionId, startDate, endDate, transactionStatus, pageNumber, pageSize);

        return new BaseResponse
        {
            StatusCode = 200,
            Message = "OK",
            Data = result
        };
    }


    public async Task<BaseResponse> DoVoid(TransactionRequest request)
    {
        var transaction = await _unitOfWork.Tran_TransactionInformationRepository.FindFirstOrDefault(e => e.TransactionId == request.TransactionId);
        if (transaction == null)
        {
            return new BaseResponse { StatusCode = 100, Message = "Transaction not found." };
        }
        if (transaction.TransactionStatus != TxnStatus.Authorized.ToString())
        {
            return new BaseResponse { StatusCode = 100, Message = "Invalid void request." };
        }

        // create new transction
        var void_transction = new Tran_TransactionInformation();
        void_transction.CopyObjectFrom(transaction);
        void_transction.TransactionInformationId = 0;
        void_transction.ReferenceTransactionId = transaction.TransactionId;
        void_transction.TransactionType = TransactionType.Void.ToString();
        void_transction.TransactionId = Utils.GenerateTransactionId();
        void_transction.TransactionInnitiationDateTime = DateTime.Now;
        void_transction.TransactionCompletionDateTime = DateTime.Now;
        void_transction.TransactionStatus = TxnStatus.Authorized.ToString();
        void_transction.SettlementDate = DateTime.Now;
        await _unitOfWork.Tran_TransactionInformationRepository.Add(void_transction);

        transaction.TransactionStatus = TxnStatus.Voided.ToString();
        _unitOfWork.Tran_TransactionInformationRepository.Update(transaction);
        await _unitOfWork.CompleteAsync();

        return new BaseResponse
        {
            StatusCode = 200,
            Message = "Transaction void successfully.",
        };
    }

    public async Task<BaseResponse> DoRefund(TransactionRequest request)
    {
        var transaction = await _unitOfWork.Tran_TransactionInformationRepository.FindFirstOrDefault(e => e.TransactionId == request.TransactionId);
        if (transaction == null)
        {
            return new BaseResponse { StatusCode = 100, Message = "Transaction not found." };
        }
        if (transaction.TransactionStatus != TxnStatus.Settled.ToString())
        {
            return new BaseResponse { StatusCode = 100, Message = "Invalid Refund request." };
        }

        // create new refund transaction
        var refund_transction = new Tran_TransactionInformation();
        refund_transction.CopyObjectFrom(transaction);
        refund_transction.TransactionInformationId = 0;
        refund_transction.ReferenceTransactionId = transaction.TransactionId;
        refund_transction.TransactionType = TransactionType.Refund.ToString();
        refund_transction.TransactionId = Utils.GenerateTransactionId();
        refund_transction.TransactionInnitiationDateTime = DateTime.Now;
        refund_transction.TransactionCompletionDateTime = DateTime.Now;
        refund_transction.TransactionStatus = TxnStatus.Authorized.ToString();
        refund_transction.SettlementStatus = null;
        refund_transction.SettlementDate = null;
        await _unitOfWork.Tran_TransactionInformationRepository.Add(refund_transction);

        transaction.TransactionStatus = TxnStatus.Refunded.ToString();
        _unitOfWork.Tran_TransactionInformationRepository.Update(transaction);
        await _unitOfWork.CompleteAsync();

        return new BaseResponse
        {
            StatusCode = 200,
            Message = "Transaction refund successfully.",
        };
    }

    public async Task<BaseResponse> SettlementRequest(TransactionRequest request)
    {
        var transaction = await _unitOfWork.Tran_TransactionInformationRepository.FindFirstOrDefault(e => e.TransactionId == request.TransactionId);
        if (transaction == null)
        {
            return new BaseResponse { StatusCode = 100, Message = "Transaction not found." };
        }
        if (transaction.TransactionStatus != TxnStatus.Authorized.ToString())
        {
            return new BaseResponse { StatusCode = 100, Message = "Invalid Settlement request." };
        }
        if (transaction.IsRequestedForSettlement == true)
        {
            return new BaseResponse { StatusCode = 100, Message = "Already Settlement request received." };
        }
        transaction.IsRequestedForSettlement = true;
        transaction.RequestedForSettlementDate = DateTime.Now;
        _unitOfWork.Tran_TransactionInformationRepository.Update(transaction);
        await _unitOfWork.CompleteAsync();

        return new BaseResponse
        {
            StatusCode = 200,
            Message = "Settlment Request Send successfully.",
        };
    }

    public async Task<BaseResponse> GetDashboardReport(long accountId)
    {
        var todayTransaction = await _unitOfWork.Tran_TransactionInformationRepository.FindByCondition(e => e.AccountId == accountId
        && e.TransactionType == TransactionType.Payment.ToString()
        && e.TransactionStatus == TxnStatus.Authorized.ToString()
        && e.TransactionCompletionDateTime > DateTime.Now.Date.AddDays(-1)
        && e.TransactionCompletionDateTime < DateTime.Now.Date.AddDays(1)
        ); 

        var todayRefundTransaction = await _unitOfWork.Tran_TransactionInformationRepository.FindByCondition(e => e.AccountId == accountId
        && e.TransactionType == TransactionType.Refund.ToString()
        && e.TransactionStatus == TxnStatus.Authorized.ToString()
        && e.TransactionCompletionDateTime > DateTime.Now.Date.AddDays(-1)
        && e.TransactionCompletionDateTime < DateTime.Now.Date.AddDays(1)
        );

        return new BaseResponse
        {
            StatusCode = 200,
            Message = "OK",
            Data = new
            {
                todayCount = todayTransaction.Count(),
                todayAmount = todayTransaction.Sum(e=>e.TransactionAmount),
                refundCount = todayRefundTransaction.Count(),
            }
        };

    }
}
