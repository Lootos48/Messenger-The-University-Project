using MessengerServer.DAL;
using MessengerServer.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MessengerServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IHubContext<MessengerServerHub> _hubContext;
        private readonly MessengerDBContext _dbContext;

        public MessagesController(IHubContext<MessengerServerHub> hubContext, MessengerDBContext context)
        {
            _hubContext = hubContext;
        }
    }
}
