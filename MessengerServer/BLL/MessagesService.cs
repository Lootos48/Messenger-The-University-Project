using AutoMapper;
using MessengerServer.DAL.Entities;
using MessengerServer.DAL.Repositories;
using MessengerServer.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessengerServer.BLL
{
    public class MessagesService
    {
        private readonly MessageRepository _messageRepository;
        private readonly ChatRepository _chatRepository;
        private readonly UserChatsRepository _userChatsRepository;

        private readonly IMapper _mapper;

        public MessagesService(
            MessageRepository messageRepository,
            ChatRepository chatRepository,
            UserChatsRepository userChatsRepository,
            IMapper mapper)
        {
            _messageRepository = messageRepository;
            _chatRepository = chatRepository;
            _userChatsRepository = userChatsRepository;
            _mapper = mapper;
        }

        public Task<List<Message>> GetMessagesAsync()
        {
            return _messageRepository.GetAllAsync();
        }

        public async Task<List<Message>> GetChatMessages(int chatId)
        {
            Chat chat = await _chatRepository.FindByIdAsync(chatId);
            if (chat is null)
            {
                throw new NotFoundException("Chat not found");
            }

            return chat.Messages.OrderBy(x => x.SendTime).ToList();
        }

        public Task<Message> GetMessageById(int messageId)
        {
            return _messageRepository.FindByIdAsync(messageId);
        }

        public Task CreateMessage(Message message)
        {
            message.SendTime = DateTime.Now;
            return _messageRepository.CreateAsync(message);
        }

        public Task EditMessage(Message message)
        {
            return _messageRepository.UpdateAsync(message);
        }

        public async Task DeleteMessage(int messageId)
        {
            Message message = await _messageRepository.FindByIdAsync(messageId);
            if (message is null)
            {
                throw new NotFoundException("Message not found exception");
            }

            await _messageRepository.DeleteAsync(message);
        }
    }
}
