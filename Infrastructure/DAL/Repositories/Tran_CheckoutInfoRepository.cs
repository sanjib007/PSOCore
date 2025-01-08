using DAL.Domain;
using DAL.UnitOfWork;

namespace DAL.Repositories
{
    public interface ITran_CheckoutInfoRepository : IBaseRepository<Tran_CheckoutInfo>
    {

    }

    public class Tran_CheckoutInfoRepository(E_PSOContext _context) : BaseRepository<Tran_CheckoutInfo>(_context), ITran_CheckoutInfoRepository
    {
    
    }   


    
  

  

}
