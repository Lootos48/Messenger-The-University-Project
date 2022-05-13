using MessengerServer.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace MessengerServer.DAL.Repositories
{
    public class MessageRepository : GenericRepository<Message>
    {
        public MessageRepository(DbContext context) : base(context)
        {
        }
    }
}
