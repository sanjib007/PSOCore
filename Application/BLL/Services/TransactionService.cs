using DAL.Models.Request;
using DAL.Models.Response;
using DAL.UnitOfWork;
using System.Diagnostics;
using static Utility.Enums.SystemEnum;

namespace BLL.Services;

public interface ITransactionService
{
    Task<BaseResponse> Mastercard_ProcessRedirectAddMoney(string resultIndicator);
    Task<BaseResponse> Nagad_ProcessRedirect(string transactionId, string status, string referenceId);
    Task<BaseResponse> Visa_ProcessRedirectAddMoney(VisaPaymentApproveRequest request);
}
public class TransactionService(IUnitOfWork _unitOfWork) : ITransactionService
{
    #region NAGAD
    public async Task<BaseResponse> Nagad_ProcessRedirect(string transactionId, string status, string referenceId)
    {

        if (status == "Success")
        {
            return await CompleteRedirectSuccessTransaction(transactionId, referenceId);
        }
        else
        {
            return await CompleteRedirectFailTransaction(transactionId);
        }

    }

    #endregion
    #region MASTER CARD
    public async Task<BaseResponse> Mastercard_ProcessRedirectAddMoney(string resultIndicator)
    {
        // they just giving decision
        var master_transaction = await _unitOfWork.Tran_TransactionInformationRepository.FindFirstOrDefault(e => e.DownstremReferenceId == resultIndicator);
        if (master_transaction == null) return new BaseResponse { StatusCode = 100, Message = "Invalid resultIndicator" };

        return await CompleteRedirectSuccessTransaction(master_transaction.TransactionId, resultIndicator);

    }

    #endregion

    #region VISA- cybersource Redirect
    public async Task<BaseResponse> Visa_ProcessRedirectAddMoney(VisaPaymentApproveRequest request)
    {
        // they just giving decision
        if (request.decision == "ACCEPT")
        {
            return await CompleteRedirectSuccessTransaction(request.req_reference_number, request.transaction_id);
        }
        else
        {
            return await CompleteRedirectFailTransaction(request.req_reference_number);
        }
    }

    #endregion
    private async Task<BaseResponse> CompleteRedirectSuccessTransaction(string transactionId, string bankTransactionId)
    {
        // call database call
        var transaction = await _unitOfWork.Tran_TransactionInformationRepository.FindFirstOrDefault(e => e.TransactionId == transactionId);
        if (transaction == null)
        {
            return new BaseResponse { StatusCode = 100, Message = "Invalid transaction." };
        }
        if (transaction.TransactionStatus != TxnStatus.Initiate.ToString())
        {
            return new BaseResponse { StatusCode = 100, Message = "Invalid transaction staus." };
        }
        var activity = new Activity(nameof(CompleteRedirectSuccessTransaction));
        activity.SetParentId(transaction.Trace);
        activity.Start();

        transaction.TransactionStatus = TxnStatus.Authorized.ToString();
        transaction.TransactionCompletionDateTime = DateTime.Now;
        transaction.ChannelUniqueTransactionId = bankTransactionId;
        _unitOfWork.Tran_TransactionInformationRepository.Update(transaction);
        await _unitOfWork.CompleteAsync();

        var checkout = await _unitOfWork.Tran_CheckoutInfoRepository.FindFirstOrDefault(e=>e.Identifier == transaction.Identifier);

        var res = new TransactionConfirmResponse
        {
            TransactionId = transaction.TransactionId,
            TransactionAmount = transaction.TransactionAmount.Value,
            TransactionStatus = transaction.TransactionStatus,
            TransactionCompletionDateTime = transaction.TransactionCompletionDateTime.Value,
            SuccessReturnUrl = checkout.SuccessReturnUrl,
            FailReturnUrl = checkout.FailReturnUrl,
            CancelReturnUrl = checkout.CancelReturnUrl,
            TransactionCurrencyCode = transaction.TransactionCurrencyCode,
            Identifier = transaction.Identifier,
            TransactionOrderId = transaction.TransactionOrderId,
            ReferenceTransactionId = transaction.ReferenceTransactionId
        };

        return new BaseResponse { StatusCode = 200, Message = "Transaction Successfull" ,Data = res };
    }

    private async Task<BaseResponse> CompleteRedirectFailTransaction(string transactionId)
    {
        var transaction = await _unitOfWork.Tran_TransactionInformationRepository.FindFirstOrDefault(e => e.TransactionId == transactionId);
        if (transaction == null)
        {
            return new BaseResponse { StatusCode = 100, Message = "Invalid transaction." };
        }
        if (transaction.TransactionStatus != TxnStatus.Initiate.ToString())
        {
            return new BaseResponse { StatusCode = 100, Message = "Invalid transaction status." };
        }

        transaction.TransactionStatus = TxnStatus.Fail.ToString();
        transaction.TransactionCompletionDateTime = DateTime.Now;
        _unitOfWork.Tran_TransactionInformationRepository.Update(transaction);
        await _unitOfWork.CompleteAsync();

        var checkout = await _unitOfWork.Tran_CheckoutInfoRepository.FindFirstOrDefault(e => e.Identifier == transaction.Identifier);

        var res = new TransactionConfirmResponse
        {
            TransactionId = transaction.TransactionId,
            TransactionAmount = transaction.TransactionAmount.Value,
            TransactionStatus = transaction.TransactionStatus,
            TransactionCompletionDateTime = transaction.TransactionCompletionDateTime.Value,
            SuccessReturnUrl = checkout.SuccessReturnUrl,
            FailReturnUrl = checkout.FailReturnUrl,
            CancelReturnUrl = checkout.CancelReturnUrl,
            TransactionCurrencyCode = transaction.TransactionCurrencyCode,
            Identifier = transaction.Identifier,
            TransactionOrderId = transaction.TransactionOrderId,
            ReferenceTransactionId = transaction.ReferenceTransactionId
        };

        return new BaseResponse { StatusCode = 100, Message = "Transaction fail.", Data = res };
    }

}
