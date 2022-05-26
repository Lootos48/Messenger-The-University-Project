using MessengerServer.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessengerServer.DAL.Repositories
{
    public class ChatRepository : GenericRepository<Chat>
    {
        public ChatRepository(DbContext context) : base(context)
        {
        }

        public override Task<Chat> FindByIdAsync(int id)
        {
            return _dbSet
                .Include(x => x.Messages)
                    .ThenInclude(x => x.Picture)
                .Include(x => x.Messages)
                    .ThenInclude(x => x.Sender)
                        .ThenInclude(x => x.Avatar)
                .Include(x => x.Users)
                    .ThenInclude(x => x.User)
                        .ThenInclude(x => x.Avatar)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<Chat> FindByNameAsync(string chatTitle)
        {
            return _dbSet
                .Include(x => x.Messages)
                    .ThenInclude(x => x.Picture)
                .Include(x => x.Messages)
                    .ThenInclude(x => x.Sender)
                        .ThenInclude(x => x.Avatar)
                .Include(x => x.Users)
                    .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.Title == chatTitle);
        }
    }
}
