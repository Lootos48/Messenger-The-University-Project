using MessengerServer.DAL;
using MessengerServer.Hubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace MessengerServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IHubContext<MessengerServerHub> _hubContext;
        private readonly MessengerDBContext _dbContext;

        public UsersController(IHubContext<MessengerServerHub> hubContext, MessengerDBContext context)
        {
            _dbContext = context;
            _hubContext = hubContext;
        }
    }
}
