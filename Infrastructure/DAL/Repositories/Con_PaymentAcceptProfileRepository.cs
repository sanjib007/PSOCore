using DAL.Domain;
using DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public interface ICon_PaymentAcceptProfileRepository : IBaseRepository<Con_PaymentAcceptProfile>
{
    Task<object> GetPaymentAcceptByProfileId(long acceptProfileId);
}

public class Con_PaymentAcceptProfileRepository(E_PSOContext _context) : BaseRepository<Con_PaymentAcceptProfile>(_context), ICon_PaymentAcceptProfileRepository
{

    public async Task<object> GetPaymentAcceptByProfileId(long acceptProfileId)
    {
        var result = await (from p in _context.Con_PaymentAcceptProfileDetails
                            join c in _context.Con_ChannelTypes on p.ChannelTypeId equals c.ChannelTypeId
                            where
                            p.IsActive == true
                            && c.IsActive == true
                            && p.PaymentAcceptProfileId == acceptProfileId
                            orderby c.Serial ascending
                            select new
                            {
                                ChannelTypeId = c.ChannelTypeId,
                                ChannelTypeName = c.ChannelTypeName,
                                BankCode = c.BankCode,
                                AccountType = c.AccountType,
                                Logo = c.Logo
                            }).ToListAsync();
        return result;
    }

}
