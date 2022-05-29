using MessengerMobile.Models;
using MessengerMobile.ServerModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MessengerMobile.Services.Interface
{
    public interface IServerConnect
    {
        Task<Response> Autorization(User user);
        Task<Response> Registration(UserPost user);
        Task<User> GetUserById(int Id);
        Task<int> GetUserByUsername(string username);
        Task EditUser(UserEditPost newUser);


        Task<List<Chat>> GetChats(int OwnerId);
        Task<Chat> GetChatById(int chatId);
        Task DeleteChat(int id);
        Task EditChat(ChatEditPost chat);
        Task<Response> CreateChat(ChatPost chat);
        Task RemoveUserFromChat(UserChatPost userChat);
        Task<List<User>> GetChatUsers(int chatId);
        Task AddUserToChat(UserChatPost userChat);


        Task<List<Message>> GetMessages(int ChatId, int OwnerId);
        Task<Message> GetMessageById(int id);
        Task<Response> CreateMessage(MessagePost message);
        Task<Response> DeleteMessage(int messageId);
        Task<Response> EditMessage(MessageEditPost newMessage);

    }
}
