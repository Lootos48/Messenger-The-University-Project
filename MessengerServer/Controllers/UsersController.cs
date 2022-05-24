using AutoMapper;
using MessengerServer.BLL;
using MessengerServer.DAL;
using MessengerServer.DAL.Entities;
using MessengerServer.DAL.Repositories;
using MessengerServer.DTOs;
using MessengerServer.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessengerServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly UsersChatsService _usersChatsService;
        private readonly IMapper _mapper;

        public UsersController(
            UserService userService,
            UsersChatsService usersChatsService,
            IMapper mapper)
        {
            _userService = userService;
            _usersChatsService = usersChatsService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserAuthResponseDTO>> Register(UserRegisterRequestDTO registerUserRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return await _userService.RegisterAsync(registerUserRequest);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserAuthResponseDTO>> Login(UserAuthRequestDTO loginUserRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return await _userService.LoginAsync(loginUserRequest);
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetUserByUsername([FromBody] string username)
        {
            User user = await _userService.GetUserByUsername(username);
            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("{userId:int}")]
        public async Task<IActionResult> GetUserById([FromBody] int userId)
        {
            User user = await _userService.GetUserById(userId);
            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsers();
            return Ok(users);
        }

        [HttpGet("{userId:int}/chats")]
        public async Task<IActionResult> GetUserChats(int userId)
        {
            var chats = await _usersChatsService.GetUserChatsAsync(userId);
            return Ok(chats);
        }

        [HttpPost("edit")]
        public async Task<IActionResult> EditUser(UserEditRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _userService.EditAsync(request);
            }
            catch (ParametersValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteUser([FromBody] int userId)
        {
            try
            {
                await _userService.DeleteAsync(userId);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return Ok();
        }
    }
}
