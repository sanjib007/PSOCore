using DAL.Domain;
using DAL.UnitOfWork;

namespace DAL.Repositories
{
    public interface IGen_BankInformationRepository : IBaseRepository<Gen_BankInformartion>
    {

    }

    public class Gen_BankInformationRepository(E_PSOContext _context) : BaseRepository<Gen_BankInformartion>(_context), IGen_BankInformationRepository
    {
    
    }
      
    

  

}
