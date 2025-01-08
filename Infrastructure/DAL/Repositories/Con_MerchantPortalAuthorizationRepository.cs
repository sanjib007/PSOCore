using DAL.Domain;
using DAL.UnitOfWork;

namespace DAL.Repositories
{
    public interface ICon_MerchantPortalAuthorizationRepository : IBaseRepository<Con_MerchantPortalAuthorization>
    {

    }

    public class Con_MerchantPortalAuthorizationRepository(E_PSOContext _context) : BaseRepository<Con_MerchantPortalAuthorization>(_context), ICon_MerchantPortalAuthorizationRepository
    {

    }





}
