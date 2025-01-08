//using BLL.Services.Communication.Bank.MTB;
//using BLL.Services.Communication.MTB;
//using DAL.Domain;
//using DAL.Models.Request;
//using DAL.Models.Response;
//using DAL.UnitOfWork;
//using System.Diagnostics;
//using Utility;
//using static Utility.Enums.SystemEnum;

//namespace BLL.Services;

//public interface ITransferService
//{
//    Task<BaseResponse> TransferInitiateRequest(TransferInitiateRequest request);
//}
//public class TransferService(
//    IMtbEncryptionService _mtbEncryptionService,
//    IUnitOfWork _unitOfWork,
//    IMtbCommunicationService _mtbCommunicationService
//    ) : ITransferService
//{

//    public async Task<BaseResponse> TransferInitiateRequest(TransferInitiateRequest request)
//    {
//        var account = await _unitOfWork.CustomerBankTokenInfoRepository.FindFirstOrDefault(e => e.CustomerBankTokenInfoId == request.LinkAccountId && e.IsActive == true);
//        if (account == null)
//        {
//            return new BaseResponse { StatusCode = 100, Message = "Invalid bank account" };
//        }

//        var bankInfo = await _unitOfWork.BankInfoRepository.FindFirstOrDefault(e => e.BankCode == account.BankCode);


//        // first call to bank
//        // after confirm call bank 
//        // call core for transaction
//        // also need to update status for understand
//        // need also inquery for resolve processiong transaction


//        // need a transaction track table
//        var transaction = new TransactionInfo();
//        transaction.TransactionId = Utils.GenerateTransactionId();
//        transaction.SenderId = account.UserNumber;
//        transaction.ReceiverId = bankInfo.BankWallet;
//        transaction.BankCode = account.BankCode;
//        transaction.BankAccountNumber = account.BankAccountNoWithMasking;
//        transaction.BankToken = account.BankToken;
//        transaction.TransactionAmount = request.Amount;
//        transaction.TransactionInitiateDate = DateTime.Now;
//        transaction.TransactionType = TransactionType.Transfer.ToString();
//        transaction.TypeCode = account.BankCode;
//        transaction.TransactionStatus = TxnStatus.Initiate.ToString();
//        transaction.Trace = Activity.Current.Id;

//        await _unitOfWork.TransactionInfoRepository.Add(transaction);
//        await _unitOfWork.CompleteAsync();

//        var bankResponse = new BaseResponse();

//        switch (transaction.BankCode)
//        {
//            case "145":  // MTB
//                bankResponse = await _mtbCommunicationService.TransferRequest(account.LinkToken, transaction.TransactionAmount ?? 0, transaction.TransactionId, account.UserNumber);
//                break;
//            default:
//                return new BaseResponse { StatusCode = 100, Message = "Invalid bank account" };
//        }

//        // fail response
//        if (bankResponse.StatusCode == 100)
//        {

//            transaction.TransactionStatus = TxnStatus.Fail.ToString();
//            transaction.TransactionCompleteDate = DateTime.Now;
//            _unitOfWork.TransactionInfoRepository.Update(transaction);
//            await _unitOfWork.CompleteAsync();

//            return new BaseResponse { StatusCode = 100, Message = bankResponse.Message };
//        }
//        else if (bankResponse.StatusCode == 200)
//        {
//            dynamic data = bankResponse.Data;
//            transaction.BankTransactionId = data.BankTransactionId;
//            transaction.TransactionStatus = TxnStatus.Authorized.ToString();
//            transaction.TransactionCompleteDate = DateTime.Now;
//            _unitOfWork.TransactionInfoRepository.Update(transaction);
//            await _unitOfWork.CompleteAsync();

//            return new BaseResponse { StatusCode = 200, Message = bankResponse.Message };
//        }
//        else // processing
//        {
//            return new BaseResponse { StatusCode = 100, Message = bankResponse.Message };
//        }

//    }


//    public async Task CompleteTransaction(TransactionInfo transaction)
//    {
//        // TODO: 
//        // 1. call to core for addmoney transaction and update status
//        // 2. return from core and update transaction



//    }


//}
