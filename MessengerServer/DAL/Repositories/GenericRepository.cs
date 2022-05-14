using MessengerServer.DAL.Entities;
using MessengerServer.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessengerServer.DAL.Repositories
{
    public abstract class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;

        protected GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public Task CreateAsync(T item)
        {
            _dbSet.Add(item);
            return _context.SaveChangesAsync();
        }

        public Task DeleteAsync(T item)
        {
            _context.Entry(item).State = EntityState.Deleted;
            return _context.SaveChangesAsync();
        }

        public Task<T> FindByIdAsync(int id)
        {
            return _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<T>> GetAllAsync()
        {
            return _dbSet.Select(x => x).ToListAsync();
        }

        public Task UpdateAsync(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }
    }
}
