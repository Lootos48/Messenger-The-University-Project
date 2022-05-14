using MessengerServer.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessengerServer.DAL.Repositories
{
    public class UserChatsRepository
    {
        protected readonly DbContext _context;
        protected readonly DbSet<ChatsUsers> _dbSet;

        public UserChatsRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<ChatsUsers>();
        }

        public Task<List<ChatsUsers>> GetUserChats(string userID)
        {
            return _dbSet
                .Include(x => x.Chat)
                .Where(x => x.UserId == int.Parse(userID))
                .ToListAsync();
        }

        public Task<List<ChatsUsers>> GetChatUsers(string chatID)
        {
            return _dbSet
                .Include(x => x.User)
                .Where(x => x.ChatId == int.Parse(chatID))
                .ToListAsync();
        }
    }
}
