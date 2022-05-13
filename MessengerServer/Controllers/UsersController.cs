using AutoMapper;
using MessengerServer.DAL;
using MessengerServer.DAL.Entities;
using MessengerServer.DAL.Repositories;
using MessengerServer.DTOs;
using MessengerServer.Hubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace MessengerServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IHubContext<MessengerServerHub> _hubContext;
        private readonly UserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(
            IHubContext<MessengerServerHub> hubContext,
            UserRepository userRepository,
            IMapper mapper)
        {
            _hubContext = hubContext;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserAuthResponseDTO>> Register(UserRegisterRequestDTO registerUserRequest)
        {
            User user = _mapper.Map<User>(registerUserRequest);

            await _userRepository.CreateAsync(user);
            user = await _userRepository.FindUserByUserName(registerUserRequest.username);

            UserAuthResponseDTO response = _mapper.Map<UserAuthResponseDTO>(user);
            return response;
        }
    }
}
