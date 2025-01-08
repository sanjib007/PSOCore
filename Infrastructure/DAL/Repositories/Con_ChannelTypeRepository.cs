using DAL.Domain;
using DAL.UnitOfWork;

namespace DAL.Repositories
{
    public interface ICon_ChannelTypeRepository : IBaseRepository<Con_ChannelType>
    {

    }
    public class Con_ChannelTypeRepository(E_PSOContext _context) : BaseRepository<Con_ChannelType>(_context), ICon_ChannelTypeRepository
    {

   

    }




}
