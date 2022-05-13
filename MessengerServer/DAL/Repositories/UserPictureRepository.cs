using MessengerServer.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace MessengerServer.DAL.Repositories
{
    public class UserPictureRepository : GenericRepository<UserPicture>
    {
        public UserPictureRepository(DbContext context) : base(context)
        {
        }
    }
}
