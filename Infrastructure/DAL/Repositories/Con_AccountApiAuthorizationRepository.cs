using DAL.Domain;
using DAL.UnitOfWork;

namespace DAL.Repositories
{
    public interface ICon_AccountApiAuthorizationRepository : IBaseRepository<Con_AccountApiAuthorization>
    {

    }
    public class Con_AccountApiAuthorizationRepository(E_PSOContext _context) : BaseRepository<Con_AccountApiAuthorization>(_context), ICon_AccountApiAuthorizationRepository
    {

    }






}
