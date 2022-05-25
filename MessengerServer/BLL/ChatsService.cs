using AutoMapper;
using MessengerServer.DAL.Entities;
using MessengerServer.DAL.Repositories;
using MessengerServer.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessengerServer.BLL
{
    public class ChatsService
    {
        private readonly ChatRepository _chatRepository;
        private readonly IMapper _mapper;

        public ChatsService(ChatRepository chatRepository, IMapper mapper)
        {
            _chatRepository = chatRepository;
            _mapper = mapper;
        }

        public Task<List<Chat>> GetAllChatsAsync()
        {
            return _chatRepository.GetAllAsync();
        }

        public async Task<Chat> GetChatByTitleAsync(string title)
        {
            var chat = await _chatRepository.FindByNameAsync(title);
            if (chat is null)
            {
                throw new NotFoundException("Chat not found");
            }

            return chat;
        }

        public async Task<Chat> GetChatByIdAsync(int chatId)
        {
            var chat = await _chatRepository.FindByIdAsync(chatId);
            if (chat is null)
            {
                throw new NotFoundException("Chat not found");
            }

            return chat;
        }

        public Task CreateChatAsync(Chat chat)
        {
            return _chatRepository.CreateAsync(chat);
        }

        public async Task EditChatAsync(Chat request)
        {
            Chat chat = await _chatRepository.FindByIdAsync(request.Id);
            if (chat is null)
            {
                throw new NotFoundException("Chat not found");
            }

            chat.Title = request.Title;

            await _chatRepository.UpdateAsync(chat);
        }

        public async Task DeleteChatAsync(int chatId)
        {
            Chat chat = await _chatRepository.FindByIdAsync(chatId);
            if (chat is null)
            {
                throw new NotFoundException("Chat not found");
            }

            await DeleteChatAsync(chat);
        }

        public Task DeleteChatAsync(Chat chat)
        {
            return _chatRepository.DeleteAsync(chat);
        }
    }
}
