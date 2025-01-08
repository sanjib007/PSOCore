using DAL.Models.Request;
using DAL.Models.Response;
using DAL.UnitOfWork;
using Utility;
using DAL.Domain;
using static Utility.Enums.SystemEnum;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using Utility.ConstantKeys;
using BLL.Services.Communication.Nagad;
using BLL.Services.Communication.Cybersource;
using BLL.Services.Communication.Mastercard;

namespace BLL.Services
{
    public interface ICheckoutService
    {
        Task<BaseResponse> CompleteCheckoutTransaction(CheckoutTransactionCompleteRequest request);
        Task<BaseResponse> GetIdentifier(CheckoutGetIdentifierRequest request);
        Task<BaseResponse> InitiateCheckout(CheckoutInitiateRequest request);
        Task<BaseResponse> InitiateCheckoutTransaction(CheckoutTransactionInitiateRequest request);
    }
    public class CheckoutService(
        IMastercardCommunicationService _mastercardCommunicationService,
        ICybersourceCommunicationService _cybersourceCommunicationService,
        IUnitOfWork _unitOfWork,
        IConfiguration _configuration,
        INagadComunicationService _nagadComunicationService) : ICheckoutService
    {
        public async Task<BaseResponse> InitiateCheckout(CheckoutInitiateRequest request)
        {
            var checkout = await _unitOfWork.Tran_CheckoutInfoRepository.FindFirstOrDefault(e => e.ReferenceId == request.ReferenceId);
            if (checkout != null) return new BaseResponse { StatusCode = 100, Message = "Reference Id should be unique." };

            // validate username and password
            var user = await _unitOfWork.Con_AccountApiAuthorizationRepository.FindFirstOrDefault(e => e.ClientId == request.ClientId);
            if (user == null) return new BaseResponse { StatusCode = 100, Message = "Invalid Client." };
            if (!user.IsActive.Value)
            {
                return new BaseResponse { StatusCode = 100, Message = "Client Inactive" };
            }
            if (!Authenticator.ValidatePassword(request.ClientSecret, user.ClientSecret)) return new BaseResponse { StatusCode = 100, Message = "Invalid credentials" };

            var info = new Tran_CheckoutInfo();
            info.ReferenceId = request.ReferenceId;
            info.Amount = request.Amount;
            info.OrderId = request.OrderId;
            info.SuccessReturnUrl = request.SuccessUrl;
            info.CancelReturnUrl = request.FailUrl;
            info.FailReturnUrl = request.CancelUrl;
            info.Identifier = Guid.NewGuid().ToString("N");
            info.Trace = Activity.Current?.Id;
            info.Status = TxnStatus.Initiate.ToString();
            info.BillerName = request.BillerName;
            info.AccountId = user.AccountId;
            info.BillingAddress = request.BillingAddress;
            info.BillerPhone = request.BillerPhone;
            info.Currency = request.Currency;
            await _unitOfWork.Tran_CheckoutInfoRepository.Add(info);
            await _unitOfWork.CompleteAsync();

            var gatewayUrl = $"{_configuration.GetSection("AppConfig:CheckoutGatewayUrl").Value}?Identifier={info.Identifier}";
            return new BaseResponse { StatusCode = 200, Message = "Please redirect the Gateway UrZl.", Data = new { GatewayUrl = gatewayUrl } };
        }


        public async Task<BaseResponse> GetIdentifier(CheckoutGetIdentifierRequest request)
        {
            var result_checkout = await _unitOfWork.Tran_CheckoutInfoRepository.FindFirstOrDefault(e => e.Identifier == request.Identifier);
            if (result_checkout == null) return new BaseResponse { StatusCode = 100, Message = "Invalid Identifier." };
            if (result_checkout.Status != TxnStatus.Initiate.ToString()) return new BaseResponse { StatusCode = 100, Message = "Invalid Identifier" };
            if (result_checkout.CreatedAt.Value.AddMinutes(10) < DateTime.Now) return new BaseResponse { StatusCode = 100, Message = "Expired Identifier." };

            var accountInfo = await _unitOfWork.Gen_AccountInformationRepository.FindFirstOrDefault(e => e.AccountId == result_checkout.AccountId);

            var paymentAcceptList = await _unitOfWork.Con_PaymentAcceptProfileRepository.GetPaymentAcceptByProfileId(accountInfo.PaymentAcceptProfileId.Value);

            return new BaseResponse
            {
                StatusCode = 200,
                Message = "Success",
                Data = new
                {
                    CheckoutDetails = new
                    {
                        merchantName = accountInfo.Title,
                        result_checkout.Status,
                        result_checkout.Identifier,
                        result_checkout.ReferenceId,
                        result_checkout.OrderId,
                        Amount = result_checkout.Amount.Value.ToTruncateDecimal(),
                        result_checkout.BillerPhone,
                        result_checkout.BillerName,
                        result_checkout.BillingAddress,
                        logo = accountInfo.CompanyLogo,
                    },
                    PaymentAcceptList = paymentAcceptList
                }
            };
        }

        public async Task<BaseResponse> InitiateCheckoutTransaction(CheckoutTransactionInitiateRequest request)
        {
            var result_checkout = await _unitOfWork.Tran_CheckoutInfoRepository.FindFirstOrDefault(e => e.Identifier == request.Identifier);
            if (result_checkout == null) return new BaseResponse { StatusCode = 100, Message = "Invalid Identifier." };
            if (result_checkout.Status != TxnStatus.Initiate.ToString()) return new BaseResponse { StatusCode = 100, Message = "Invalid Identifier" };
            //if (result_checkout.CreatedAt.Value.AddMinutes(10) < DateTime.Now) return new BaseResponse { StatusCode = 100, Message = "Expired Identifier." };

            var result_transaction = await _unitOfWork.Tran_TransactionInformationRepository.FindByCondition(e => e.Identifier == request.Identifier);
            foreach (var item in result_transaction)
            {
                if (item.TransactionStatus != TxnStatus.Initiate.ToString())
                {
                    return new BaseResponse { StatusCode = 100, Message = "Already transaction completed for this identifier" };
                }
            }
            //TODO !important
            // find the channel and gatewayid and rout this way

            var channelType = await _unitOfWork.Con_ChannelTypeRepository.FindFirstOrDefault(e => e.ChannelTypeId == request.ChannelTypeId);
            var gateway_channel = await _unitOfWork.Con_ChannelRepository.FindFirstOrDefault(
                e=>e.ChannelTypeId == request.ChannelTypeId 
                && e.ActiveStartDate <= DateTime.Now
                && e.ActiveEndDate > DateTime.Now
                && e.IsActive == true );
            if (gateway_channel == null)
            {
                return new BaseResponse { StatusCode = 100, Message = "Available channel not found." };
            }

            // generate transaction
            var transaction = new Tran_TransactionInformation();
            transaction.Identifier = request.Identifier;
            transaction.TransactionId = Utility.Utils.GenerateTransactionId();
            transaction.AccountId = result_checkout.AccountId;
            transaction.TransactionInnitiationDateTime = DateTime.Now;
            transaction.TransactionType = TransactionType.Payment.ToString();
            transaction.TransactionCurrencyCode = result_checkout.Currency;
            transaction.TransactionAmount = result_checkout.Amount;
            transaction.TransactionStatus = TxnStatus.Initiate.ToString();
            transaction.TransactionOrderId = result_checkout.OrderId;
            transaction.ReferenceTransactionId = result_checkout.ReferenceId;
            transaction.Trace = result_checkout.Trace;

            transaction.GatewayRoute = gateway_channel.ChannelName;
            transaction.ChannelId = gateway_channel.ChannelId;

            transaction.BankCode = channelType.BankCode;
            transaction.ChannelTypeId = channelType.ChannelTypeId;
            transaction.ChannelTypeName = channelType.ChannelTypeName;

            await _unitOfWork.Tran_TransactionInformationRepository.Add(transaction);
            await _unitOfWork.CompleteAsync();

            var bankResponse = new BankComAddmoneyResponse();

            switch (gateway_channel.ChannelEnum)
            {

                case ChannelGateway.VisaCybersource:  // visa
                    bankResponse = await _cybersourceCommunicationService.InitatePayment(transaction.TransactionId, transaction.TransactionAmount.Value);
                    break;
                case ChannelGateway.MasterCardEbl:
                    bankResponse = await _mastercardCommunicationService.InitateCheckout(transaction.TransactionId, transaction.TransactionAmount.Value);
                    transaction.DownstremReferenceId = bankResponse.ReferenceId;
                    _unitOfWork.Tran_TransactionInformationRepository.Update(transaction);
                    await _unitOfWork.CompleteAsync();
                    break;
                case ChannelGateway.Nagad:
                    bankResponse = await _nagadComunicationService.PaymentInitiate(transaction.TransactionId, transaction.TransactionAmount.Value);
                    transaction.DownstremReferenceId = bankResponse.ReferenceId;
                    _unitOfWork.Tran_TransactionInformationRepository.Update(transaction);
                    await _unitOfWork.CompleteAsync();
                    break;
                default:
                    return new BaseResponse { StatusCode = 100, Message = "Gateway not found." };
            }

            if (bankResponse.IsOTPrequired)
            {
                return new BaseResponse { StatusCode = 200, Message = "Please submit OTP", Data = new { IsOtpRequire = true } };
            }
            else if (bankResponse.IsRedirect)
            {
                return new BaseResponse
                {
                    StatusCode = 200,
                    Message = "Redirect Url.",
                    Data = new
                    {
                        IsRedirect = true,
                        RedirectUrl = bankResponse.RedirectUrl,
                        MastercardSessionId = bankResponse.MastercardSessionId
                    }
                };
            }
            else if (bankResponse.IsFormRedirect)
            {
                return new BaseResponse
                {
                    StatusCode = 200,
                    Message = "Redirect Url.",
                    Data = new
                    {
                        IsFormRedirect = true,
                        RedirectUrl = bankResponse.RedirectUrl,
                        bankResponse.FormData
                    }
                };
            }
            else
            {
                return new BaseResponse { StatusCode = 200, Message = bankResponse.Message };
            }
        }



        public async Task<BaseResponse> CompleteCheckoutTransaction(CheckoutTransactionCompleteRequest request)
        {

            if (!(request.Status == TxnStatus.Cancel.ToString()
                || request.Status == TxnStatus.Fail.ToString()
                || request.Status == TxnStatus.Authorized.ToString())) return new BaseResponse { StatusCode = 100, Message = "Invalid transaction Status Transaction." };

            var transaction = await _unitOfWork.Tran_TransactionInformationRepository.FindFirstOrDefault(e => e.TransactionId == request.TransactionId);
            if (transaction == null) return new BaseResponse { StatusCode = 100, Message = "Invalid Transaction" };
            if (transaction.TransactionStatus != TxnStatus.Initiate.ToString()) return new BaseResponse { StatusCode = 100, Message = "Invalid transaction status" };

            var checkout = await _unitOfWork.Tran_CheckoutInfoRepository.FindFirstOrDefault(e => e.Identifier == transaction.Identifier);
            if (request.Status == TxnStatus.Fail.ToString())
            {
                transaction.TransactionStatus = TxnStatus.Fail.ToString();
                transaction.TransactionCompletionDateTime = DateTime.Now;
                _unitOfWork.Tran_TransactionInformationRepository.Update(transaction);
                await _unitOfWork.CompleteAsync();
                return new BaseResponse
                {
                    StatusCode = 200,
                    Message = $"{request.Status} Transaction.",
                    Data = new
                    {
                        RedirectUrl = $"{checkout.FailReturnUrl}?statusCode=100&message=transaction fail",
                    }
                };
            }
            if (request.Status == TxnStatus.Cancel.ToString())
            {
                transaction.TransactionStatus = TxnStatus.Cancel.ToString();
                transaction.TransactionCompletionDateTime = DateTime.Now;
                _unitOfWork.Tran_TransactionInformationRepository.Update(transaction);
                await _unitOfWork.CompleteAsync();

                return new BaseResponse
                {
                    StatusCode = 200,
                    Message = $"{request.Status} Transaction.",
                    Data = new
                    {
                        RedirectUrl = $"{checkout.CancelReturnUrl}?statusCode=100&message=transaction cancel",
                    }
                };
            }
            if (request.Status == TxnStatus.Authorized.ToString())
            {
                transaction.TransactionStatus = TxnStatus.Authorized.ToString();
                transaction.ChannelUniqueTransactionId = request.ChannelReferenceId;
                transaction.TransactionCompletionDateTime = DateTime.Now;
                _unitOfWork.Tran_TransactionInformationRepository.Update(transaction);
                await _unitOfWork.CompleteAsync();


                Uri url = new Uri(checkout.SuccessReturnUrl);
                //Uri url = new Uri(checkout.SuccessReturnUrl).
                //            AddQuery("statusCode", "200").
                //            AddQuery("orderId", transaction.TransactionOrderId).
                //            AddQuery("transactionId", transaction.TransactionId).
                //            AddQuery("message", "transaction Successful");

                //var nameValueCollection = new NameValueCollection
                //{
                //    {"StatusCode", "200"},
                //    {"Message","Transaction Authorized."},
                //    {"TransactionId", transaction.TransactionId},
                //    {"TransactionAmount", transaction.TransactionAmount.Value.ToTruncateDecimal().ToString()},
                //    {"TransactionStatus", transaction.TransactionStatus},
                //    {"TransactionOrderId", transaction.TransactionOrderId},
                //    {"ReferenceTransactionId", transaction.ReferenceTransactionId},
                //    {"TransactionCompletionDateTime",transaction.TransactionCompletionDateTime.ToString()},
                //    {"Identifier",transaction.Identifier},
                //    {"TransactionCurrencyCode",transaction.TransactionCurrencyCode},
                //    {"TransactionType",transaction.TransactionType}
                //};
                //Utility.RedirectWithData(nameValueCollection, respose.ReturnUrl);
                return new BaseResponse
                {
                    StatusCode = 200,
                    Message = $"{request.Status} Transaction.",
                    Data = new
                    {
                        RedirectUrl = url.ToString(),
                        TransactionData = new
                        {
                            StatusCode = 200,
                            Message = "Transaction Authorized.",
                            transaction.TransactionId,
                            transaction.TransactionAmount,
                            transaction.TransactionStatus,
                            transaction.TransactionOrderId,
                            transaction.ReferenceTransactionId,
                            transaction.TransactionCompletionDateTime,
                            transaction.Identifier,
                            transaction.TransactionCurrencyCode,
                            transaction.TransactionType

                        }
                    }
                };
            }
            return new BaseResponse
            {
                StatusCode = 100,
                Message = $"Invalid transaction Status Transaction.",
            };
        }



    }
}
