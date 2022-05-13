using MessengerServer.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MessengerServer.DAL.Repositories
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        public Task<User> FindUserByUserName(string name)
        {
            return _dbSet.FirstOrDefaultAsync(x => x.Username == name);
        }
    }
}
