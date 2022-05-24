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

        public Task<List<ChatsUsers>> GetUserChats(int userID)
        {
            return _dbSet
                .Include(x => x.Chat)
                    .ThenInclude(x => x.Messages)
                .Where(x => x.UserId == userID)
                .ToListAsync();
        }

        public Task<List<ChatsUsers>> GetChatUsers(int chatID)
        {
            return _dbSet
                .Include(x => x.User)
                .Where(x => x.ChatId == chatID)
                .ToListAsync();
        }

        public Task AddUserToChat(ChatsUsers chatsUsers)
        {
            _dbSet.Add(chatsUsers);

            return _context.SaveChangesAsync();
        }

        public Task RemoveUserFromChat(ChatsUsers chatsUsers)
        {
            _dbSet.Remove(chatsUsers);

            return _context.SaveChangesAsync();
        }
    }
}
