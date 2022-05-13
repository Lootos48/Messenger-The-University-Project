using MessengerServer.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace MessengerServer.DAL.Repositories
{
    public class ImageRepository : GenericRepository<Image>
    {
        public ImageRepository(DbContext context) : base(context)
        {
        }
    }
}
