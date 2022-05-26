using MessengerServer.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessengerServer.DAL.Repositories.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<int> CreateAsync(T item);
        Task<T> FindByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task DeleteAsync(T item);
        Task UpdateAsync(T item);
    }
}
