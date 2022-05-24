using AutoMapper;
using MessengerServer.BLL;
using MessengerServer.DAL;
using MessengerServer.DAL.Entities;
using MessengerServer.DTOs.Message;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MessengerServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly MessagesService _messagesService;
        private readonly IMapper _mapper;

        public MessagesController(
            MessagesService messsagesService,
            IMapper mapper)
        {
            _messagesService = messsagesService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetMessages()
        {
            var messages = await _messagesService.GetMessagesAsync();
            List<MessageDTO> messagesDTO = _mapper.Map<List<MessageDTO>>(messages);
            return Ok(messagesDTO);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateMessage(CreateMessageRequestDTO request)
        {
            Message message = _mapper.Map<Message>(request);
            await _messagesService.CreateMessage(message);

            return Ok();
        }

        [HttpPost("edit")]
        public async Task<IActionResult> EditMessage(EditMessageRequestDTO request)
        {
            Message message = _mapper.Map<Message>(request);
            await _messagesService.EditMessage(message);

            return Ok();
        }
    }
}
