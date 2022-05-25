using AutoMapper;
using MessengerServer.BLL;
using MessengerServer.DAL;
using MessengerServer.DAL.Entities;
using MessengerServer.DTOs;
using MessengerServer.DTOs.Message;
using MessengerServer.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        [HttpGet("{messageId:int}")]
        public async Task<IActionResult> GetMessageById(int messageId)
        {
            try
            {
                Message message = await _messagesService.GetMessageById(messageId);
                MessageDTO messageDTO = _mapper.Map<MessageDTO>(message);
                return Ok(messageDTO);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
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
            try
            {
                Message message = _mapper.Map<Message>(request);
                await _messagesService.EditMessage(message);

                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { error = ex.Message});
            }
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(int messageId)
        {
            try
            {
                await _messagesService.DeleteMessage(messageId);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }
    }
}
