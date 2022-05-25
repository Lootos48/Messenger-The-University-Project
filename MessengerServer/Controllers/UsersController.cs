using AutoMapper;
using MessengerServer.BLL;
using MessengerServer.DAL;
using MessengerServer.DAL.Entities;
using MessengerServer.DAL.Repositories;
using MessengerServer.DTOs;
using MessengerServer.DTOs.User;
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
        public async Task<IActionResult> Register(UserRegisterRequestDTO registerUserRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var createdUser = await _userService.RegisterAsync(registerUserRequest);
                return Ok(new { userId = createdUser.Id });
            }
            catch (NotUniqueException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserAuthRequestDTO loginUserRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var loginResponse = await _userService.LoginAsync(loginUserRequest);
                return Ok(new { userId = loginResponse.Id });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            User user = await _userService.GetUserByUsername(username);
            if (user is null)
            {
                return NotFound();
            }

            var userDTO = _mapper.Map<UserDTO>(user);
            return Ok(userDTO);
        }

        [HttpGet("{userId:int}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            User user = await _userService.GetUserById(userId);
            if (user is null)
            {
                return NotFound();
            }

            var userDTO = _mapper.Map<UserDTO>(user);
            return Ok(userDTO);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsers();

            var usersDTO = _mapper.Map<List<UserDTO>>(users);
            return Ok(usersDTO);
        }

        [HttpGet("{userId:int}/chats")]
        public async Task<IActionResult> GetUserChats(int userId)
        {
            var chats = await _usersChatsService.GetUserChatsAsync(userId);

            var chatsDTO = _mapper.Map<List<ChatDTO>>(chats);
            return Ok(chatsDTO);
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
                return Ok();
            }
            catch (NotUniqueException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            try
            {
                await _userService.DeleteAsync(userId);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }
    }
}
