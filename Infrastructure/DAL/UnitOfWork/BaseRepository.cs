using DAL.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL.UnitOfWork
{

    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetById(long id);
        Task<List<T>> GetAll();
        Task Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        Task<T> FindFirstOrDefault(Expression<Func<T, bool>> expression);
        Task<T> FindLastOrDefault(Expression<Func<T, bool>> expression);
        void Detach(T entity);
        Task<List<T>> FindByCondition(Expression<Func<T, bool>> expression);
        Task AddRange(List<T> entity);
       
    }
    public class BaseRepository<T>(E_PSOContext _context) : IBaseRepository<T> where T : class
    {
        //protected E_PSOContext _context;

        //public BaseRepository(
        //    E_PSOContext context)
        //{
        //    _context = context;
        //}
        public virtual async Task Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }
        public virtual async Task AddRange(List<T> entity)
        {
            await _context.Set<T>().AddRangeAsync(entity);
        }
        public virtual async Task<T> FindLastOrDefault(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().AsNoTracking().LastOrDefaultAsync(expression);
        }

        public virtual async Task<List<T>> FindByCondition(Expression<Func<T, bool>> expression)
        {

            return await _context.Set<T>().Where(expression).AsNoTracking().ToListAsync();

        }

        public virtual async Task<T> FindFirstOrDefault(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(expression);

        }
        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        public void Detach(T entity)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }
        public async Task<T> GetById(long id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}

