using AutoMapper;
using MessengerServer.BLL;
using MessengerServer.DAL.Entities;
using MessengerServer.DTOs;
using MessengerServer.DTOs.Message;
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
    public class MessagesController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly MessagesService _messagesService;
        private readonly ImageService _imageService;
        private readonly IMapper _mapper;

        public MessagesController(
            IWebHostEnvironment environment,
            MessagesService messsagesService,
            ImageService fileService,
            IMapper mapper)
        {
            _webHostEnvironment = environment;
            _messagesService = messsagesService;
            _imageService = fileService;
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

                byte[] imageBytes = await FileService.ConvertFileToByteArray(message.Picture.Path);
                messageDTO.Image = imageBytes;

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
            if (request.ImageBytes != null)
            {
                string path = await FileService.SaveFileInUploadsFolder(_webHostEnvironment, request.ImageBytes);

                Message message = _mapper.Map<Message>(request);

                int createdMessageId = await _messagesService.CreateMessage(message);
                Image imageToCreate = new Image
                {
                    MessageId = createdMessageId,
                    Path = path
                };

                int createdImageId = await _imageService.CreateImage(imageToCreate);

                message.Id = createdMessageId;
                message.ImageId = createdImageId;

                await _messagesService.EditMessage(message);
                return Ok();
            }

            Message messageToCreate = _mapper.Map<Message>(request);
            await _messagesService.CreateMessage(messageToCreate);

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
