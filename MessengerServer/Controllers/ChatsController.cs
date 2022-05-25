using AutoMapper;
using MessengerServer.BLL;
using MessengerServer.DAL;
using MessengerServer.DAL.Entities;
using MessengerServer.DAL.Repositories;
using MessengerServer.DTOs;
using MessengerServer.DTOs.Chat;
using MessengerServer.DTOs.ChatsUsers;
using MessengerServer.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessengerServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ChatsController : ControllerBase
    {
        private readonly ChatsService _chatService;
        private readonly UsersChatsService _usersChatService;
        private readonly MessagesService _messagesService;
        private readonly IMapper _mapper;

        public ChatsController(
            ChatsService chatService,
            UsersChatsService usersChatService,
            MessagesService messagesService,
            IMapper mapper)
        {
            _chatService = chatService;
            _usersChatService = usersChatService;
            _messagesService = messagesService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetChats()
        {
            var chats = await _chatService.GetAllChatsAsync();

            var chatsDTO = _mapper.Map<List<ChatDTO>>(chats);
            return Ok(chatsDTO);
        }

        [HttpGet("{chatId:int}")]
        public async Task<IActionResult> GetChat(int chatId)
        {
            try
            {
                var chat = await _chatService.GetChatByIdAsync(chatId);

                var chatDTO = _mapper.Map<ChatDTO>(chat);
                return Ok(chatDTO);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpGet("{chatId:int}/users")]
        public async Task<IActionResult> GetChatUsers(int chatId)
        {
            var users = await _usersChatService.GetChatUsersAsync(chatId);

            var usersDTO = _mapper.Map<List<UserDTO>>(users);
            return Ok(usersDTO);
        }

        [HttpGet("{chatId:int}/messages")]
        public async Task<IActionResult> GetChatMessages(int chatId)
        {
            var messages = await _messagesService.GetChatMessages(chatId);

            var messagesDTO = _mapper.Map<List<MessageDTO>>(messages);
            return Ok(messagesDTO);
        }

        [HttpPost("add-user")]
        public async Task<IActionResult> AddUserToChat(UserChatInteractRequestDTO request)
        {
            ChatsUsers entity = _mapper.Map<ChatsUsers>(request);
            await _usersChatService.AddUserToChatAsync(entity);

            return Ok();
        }

        [HttpPost("remove-user")]
        public async Task<IActionResult> RemoveUserFromChat(UserChatInteractRequestDTO request)
        {
            ChatsUsers entity = _mapper.Map<ChatsUsers>(request);
            await _usersChatService.RemoveUserFromChatAsync(entity);

            return Ok();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(ChatCreateRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                Chat chatToCreate = _mapper.Map<Chat>(request);
                await _chatService.CreateChatAsync(chatToCreate);

            }
            catch (NotUniqueException ex)
            {
                return BadRequest(new { error = ex.Message });
            }

            Chat chat = await _chatService.GetChatByTitleAsync(request.Title);

            await _usersChatService.AddUserToChatAsync(new ChatsUsers
            {
                ChatId = chat.Id,
                UserId = request.CreatorId
            });

            return Ok(new { chatId = chat.Id });
        }

        [HttpPost("edit")]
        public async Task<IActionResult> Edit(ChatEditRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                Chat chat = _mapper.Map<Chat>(request);
                await _chatService.EditChatAsync(chat);

                return Ok();
            }
            catch (NotUniqueException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { error = ex.Message});
            }
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(int chatId)
        {
            try
            {
                await _chatService.DeleteChatAsync(chatId);

                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }
    }
}
