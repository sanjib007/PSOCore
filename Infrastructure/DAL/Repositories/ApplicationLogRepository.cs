using DAL.Domain;
using DAL.UnitOfWork;

namespace DAL.Repositories
{
    public interface IApplicationLogRepository : IBaseRepository<ApplicationLog>
    {

    }
    public class ApplicationLogRepository(E_PSOContext _context) : BaseRepository<ApplicationLog>(_context), IApplicationLogRepository
    {
    
    }
      
   
    

  

}
