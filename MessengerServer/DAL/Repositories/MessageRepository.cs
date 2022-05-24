using MessengerServer.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessengerServer.DAL.Repositories
{
    public class MessageRepository : GenericRepository<Message>
    {
        public MessageRepository(DbContext context) : base(context)
        {
        }

        public override Task<List<Message>> GetAllAsync()
        {
            return _dbSet
                .Include(x => x.Chat)
                .Include(x => x.Sender)
                    .ThenInclude(x => x.Avatar)
                .Include(x => x.Picture)
                .ToListAsync();
        }

        public override Task<Message> FindByIdAsync(int id)
        {
            return _dbSet
                .Include(x => x.Chat)
                .Include(x => x.Sender)
                    .ThenInclude(x => x.Avatar)
                .Include(x => x.Picture)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
