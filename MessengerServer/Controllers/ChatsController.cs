using MessengerServer.DAL;
using MessengerServer.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessengerServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatsController : ControllerBase
    {
        private readonly MessengerDBContext _dbContext;

        public ChatsController(MessengerDBContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
