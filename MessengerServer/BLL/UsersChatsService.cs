using AutoMapper;
using MessengerServer.DAL.Entities;
using MessengerServer.DAL.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessengerServer.BLL
{
    public class UsersChatsService
    {
        private readonly UserChatsRepository _userChatsRepository;
        private readonly IMapper _mapper;

        public UsersChatsService(UserChatsRepository userChatsRepository, IMapper mapper)
        {
            _userChatsRepository = userChatsRepository;
            _mapper = mapper;
        }

        public async Task<List<Chat>> GetUserChatsAsync(int userId)
        {
            List<ChatsUsers> chatsUsers = await _userChatsRepository.GetUserChats(userId);
            List<Chat> userChats = new List<Chat>();

            chatsUsers.ForEach(x =>
            {
                userChats.Add(x.Chat);
            });

            return userChats;
        }

        public async Task<List<User>> GetChatUsersAsync(int chatId)
        {
            List<ChatsUsers> chatsUsers = await _userChatsRepository.GetChatUsers(chatId);
            List<User> chatUsers = new List<User>();

            chatsUsers.ForEach(x =>
            {
                chatUsers.Add(x.User);
            });

            return chatUsers;
        }

        public Task AddUserToChatAsync(ChatsUsers chatsUsers)
        {
            return _userChatsRepository.AddUserToChat(chatsUsers);
        }

        public Task RemoveUserFromChatAsync(ChatsUsers chatsUsers)
        {
            return _userChatsRepository.RemoveUserFromChat(chatsUsers);
        }
    }
}
