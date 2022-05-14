using AutoMapper;
using MessengerServer.BLL;
using MessengerServer.DAL;
using MessengerServer.DAL.Entities;
using MessengerServer.DAL.Repositories;
using MessengerServer.DTOs;
using MessengerServer.Hubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessengerServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IHubContext<MessengerServerHub> _hubContext;
        private readonly UserService _userService;
        private readonly IMapper _mapper;

        public UsersController(
            IHubContext<MessengerServerHub> hubContext,
            UserService userService,
            IMapper mapper)
        {
            _hubContext = hubContext;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserAuthResponseDTO>> Register(UserRegisterRequestDTO registerUserRequest)
        {
            return await _userService.RegisterAsync(registerUserRequest);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserAuthResponseDTO>> Login(UserAuthRequestDTO loginUserRequest)
        {
            return await _userService.LoginAsync(loginUserRequest);
        }

        [HttpPost("userChats")]
        public async Task<ActionResult<List<Chat>>> GetUserChats([FromBody] string userID)
        {
            return await _userService.GetChatsAsync(userID);
        }
    }
}
