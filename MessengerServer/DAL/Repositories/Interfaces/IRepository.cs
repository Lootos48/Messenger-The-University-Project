using MessengerServer.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessengerServer.DAL.Repositories.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task CreateAsync(T item);
        Task<T> FindAsync(int id);
        Task<List<T>> GetAllAsync();
        Task DeleteAsync(T item);
        Task UpdateAsync(T item);
    }
}
