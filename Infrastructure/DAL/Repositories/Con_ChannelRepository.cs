using DAL.Domain;
using DAL.UnitOfWork;

namespace DAL.Repositories;

public interface ICon_ChannelRepository : IBaseRepository<Con_Channel>
{

}

public class Con_ChannelRepository(E_PSOContext _context) : BaseRepository<Con_Channel>(_context), ICon_ChannelRepository
{

}


