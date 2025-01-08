using DAL.Domain;
using DAL.UnitOfWork;

namespace DAL.Repositories
{
    public interface IGen_AccountInformationRepository : IBaseRepository<Gen_AccountInformation>
    {

    }
    public class Gen_AccountInformationRepository(E_PSOContext _context) : BaseRepository<Gen_AccountInformation>(_context), IGen_AccountInformationRepository
    {
    
    }





}
