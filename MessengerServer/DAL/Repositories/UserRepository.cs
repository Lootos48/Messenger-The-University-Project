using MessengerServer.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessengerServer.DAL.Repositories
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        public Task<User> FindUserByUserNameAsync(string name)
        {
            return _dbSet.FirstOrDefaultAsync(x => x.Username == name);
        }

        public Task<User> FindUserWithIncludesAsync(string userId)
        {
            return _dbSet
                .Include(x => x.Avatar)
                .Include(x => x.Chats)
                    .ThenInclude(x => x.Chat)
                .Include(x => x.Messages)
                .FirstOrDefaultAsync(x => x.Id == int.Parse(userId));
        }
    }
}
