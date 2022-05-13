using MessengerServer.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace MessengerServer.DAL.Repositories
{
    public class ChatRepository : GenericRepository<Chat>
    {
        public ChatRepository(DbContext context) : base(context)
        {
        }
    }
}
