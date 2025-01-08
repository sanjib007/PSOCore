using DAL.Domain;
using DAL.UnitOfWork;
using static Utility.Enums.SystemEnum;
using System.Transactions;
using Utility;
using Utility.Enums;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using DAL.Models.Response;
namespace DAL.Repositories
{

    public interface ITran_TransactionInformationRepository : IBaseRepository<Tran_TransactionInformation>
    {
        Task<object> GetMerchantTransactions(long accountId,
            string? transactionId,
        DateTime? startDate,
        DateTime? endDate,
        string transactionStatus,
        int pageNumber = 1,
        int pageSize = 10);
    }
    public class Tran_TransactionInformationRepository(E_PSOContext _context) : BaseRepository<Tran_TransactionInformation>(_context), ITran_TransactionInformationRepository
    {

        public async Task<object> GetMerchantTransactions(long accountId,
            string? transactionId,
        DateTime? startDate,
        DateTime? endDate,
        string? transactionStatus,
        int pageNumber = 1,
        int pageSize = 10)
        {
            // date

            pageNumber = (pageNumber <= 0) ? 1 : pageNumber;
            pageSize = (pageSize <= 0) ? 10 : pageSize;
            int count = 0;
            var result = new List<Tran_TransactionInformation>();
            // Apply filters

            if (!string.IsNullOrEmpty(transactionId))
            {
                result = await _context.Tran_TransactionInformations
                   .OrderByDescending(e => e.TransactionInformationId)
                   .Where(e => e.AccountId == accountId
                   && e.TransactionStatus != TxnStatus.Initiate.ToString()
                   && (e.TransactionId.Contains(transactionId) || e.TransactionOrderId.Contains(transactionId) || e.ReferenceTransactionId.Contains(transactionId))
                   )
                   .Skip((pageNumber - 1) * pageSize)
                   .Take(pageSize)
                   .ToListAsync();
                count = result.Count;
            }
            else if (!string.IsNullOrEmpty(transactionStatus))
            {
                result = await _context.Tran_TransactionInformations
                  .OrderByDescending(e => e.TransactionInformationId)
                  .Where(e => e.AccountId == accountId
                  && e.TransactionStatus != TxnStatus.Initiate.ToString()
                  && e.TransactionStatus == transactionStatus
                  )
                  //.Skip((pageNumber - 1) * pageSize)
                  //.Take(pageSize)
                  .ToListAsync();
                count = result.Count;
            }
            else if (startDate.HasValue && endDate.HasValue)
            {
                result = await _context.Tran_TransactionInformations
                  .OrderByDescending(e => e.TransactionInformationId)
                  .Where(e => e.AccountId == accountId
                  && e.TransactionStatus != TxnStatus.Initiate.ToString()
                  && e.TransactionCompletionDateTime >= startDate.Value
                    && e.TransactionCompletionDateTime < endDate.Value.AddDays(1)
                  )
                  //.Skip((pageNumber - 1) * pageSize)
                  //.Take(pageSize)
                  .ToListAsync();
                count = result.Count;
            }

            else // all
            {
                result = await _context.Tran_TransactionInformations
                           .OrderByDescending(e => e.TransactionInformationId)
                           .Where(e => e.AccountId == accountId
                           && e.TransactionStatus != TxnStatus.Initiate.ToString()
                           )
                           .Skip((pageNumber - 1) * pageSize)
                           .Take(pageSize)
                           .ToListAsync();
                count = await _context.Tran_TransactionInformations
                       .Where(e => e.AccountId == accountId && e.TransactionStatus != TxnStatus.Initiate.ToString())
                       .CountAsync();
            }


            var _list = result.Select(o => new MerchantTransactionReportResponse
            {
                TransactionFee = o.FeeAmount ?? 0,
                TransactionAmount = o.TransactionAmount.Value.ToTruncateDecimal(),
                TransactionDate = o.TransactionCompletionDateTime.Value.ToFormatDate(),
                TransactionStatus = o.TransactionStatus,
                TransactionId = o.TransactionId,
                TransactionOrderId = o.TransactionOrderId,
                SettlementDate = o.SettlementDate,
                SettlementStatus = o.SettlementStatus,
                IsRequestedForSettlement = o.IsRequestedForSettlement ?? false,
                TransactionCurrencyCode = o.TransactionCurrencyCode,
                Channel = o.BankCode,
                TransactionType = Enum.Parse<TransactionType>(o.TransactionType).GetEnumDescription()
            });


            //var count = await _context.Tran_TransactionInformations
            //          .Where(e => e.AccountId == accountId && e.TransactionStatus != TxnStatus.Initiate.ToString())
            //          .CountAsync();

            var res = new
            {
                total = count,
                list = _list
            };
            return res;
        }

    }





}
