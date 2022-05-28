using AutoMapper;
using MessengerServer.BLL;
using MessengerServer.DAL.Entities;
using MessengerServer.DTOs;
using MessengerServer.DTOs.User;
using MessengerServer.Exceptions;
using MessengerServer.Util;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessengerServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserService _userService;
        private readonly UsersChatsService _usersChatsService;
        private readonly UserPictureService _pictureService;
        private readonly IMapper _mapper;

        public UsersController(
            IWebHostEnvironment webHostEnvironment,
            UserService userService,
            UsersChatsService usersChatsService,
            UserPictureService pictureService,
            IMapper mapper)
        {
            _webHostEnvironment = webHostEnvironment;
            _userService = userService;
            _usersChatsService = usersChatsService;
            _pictureService = pictureService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                if (request.ImageBytes != null)
                {
                    string path = await FileService.SaveFileInAvatarsFolder(_webHostEnvironment, request.ImageBytes);

                    User user = _mapper.Map<User>(request);

                    user = await _userService.RegisterAsync(user);
                    UserPicture picture = new UserPicture
                    {
                        UserId = user.Id,
                        Path = path
                    };

                    int createdImageId = await _pictureService.CreatePicture(picture);
                    user.UserPictureId = createdImageId;

                    await _userService.EditAsync(user);
                    return Ok(new { userId = user.Id });
                }

                User userToCreate = _mapper.Map<User>(request);
                var createdUser = await _userService.RegisterAsync(userToCreate);

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
            try
            {
                User user = await _userService.GetUserByUsername(username);

                var userDTO = _mapper.Map<UserDTO>(user);
                return Ok(userDTO);
            }
            catch (NotFoundException ex)
            {
                return NotFound( new { error = ex.Message });
            }
        }

        [HttpGet("{userId:int}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            try
            {
                User user = await _userService.GetUserById(userId);

                var userDTO = _mapper.Map<UserDTO>(user);
                return Ok(userDTO);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
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
                await _userService.EditAsync(_webHostEnvironment, request);
                return Ok();
            }
            catch (NotUniqueException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
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
