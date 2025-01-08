using DAL.Domain;
using DAL.UnitOfWork;

namespace DAL.Repositories;

public interface IMerchantKYCRegistrationRepository : IBaseRepository<MerchantKYCRegistration>
{

}
public class MerchantKYCRegistrationRepository(E_PSOContext _context) : BaseRepository<MerchantKYCRegistration>(_context), IMerchantKYCRegistrationRepository
{

}
