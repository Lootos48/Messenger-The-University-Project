using MessengerMobile.Models;
using MessengerMobile.ServerModels;
using MessengerMobile.Services.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms.PlatformConfiguration;

namespace MessengerMobile.Services.Core
{
    public class ServerConnect : IServerConnect
    {
        string url;
        HttpClient httpClient;


        public ServerConnect()
        {
            url = "http://192.168.0.188:4000";
            HttpClientHandler clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            httpClient = new HttpClient(clientHandler);
        }



        /* ------ USERS INTERACTIONS ------ */



        public async Task<Response> Autorization(User user)
        {
            url = "http://192.168.0.188:4000/users/login";

            string jsonData = JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, content);
            string result = await response.Content.ReadAsStringAsync();
            Response responseData = JsonConvert.DeserializeObject<Response>(result);
            return responseData;
        }

        public async Task<Response> Registration(UserPost user)
        {
            url = "http://192.168.0.188:4000/users/register";

            string jsonData = JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, content);
            string result = await response.Content.ReadAsStringAsync();
            Response responseData = JsonConvert.DeserializeObject<Response>(result);
            return responseData;
        }

        public async Task<User> GetUserById(int Id)
        {
            url = $"http://192.168.0.188:4000/users/{Id}";
            var response = await httpClient.GetStringAsync(url);

            User responseUser = JsonConvert.DeserializeObject<User>(response);

            if (responseUser.Avatar != null)
            {
                var images = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                var filepath = Path.Combine(images, $"{Guid.NewGuid()}.jpg");

                File.WriteAllBytes(filepath, responseUser.Avatar);

                responseUser.ImagePath = filepath;
            }

            return responseUser;
        }

        public async Task<int> GetUserByUsername(string username)
        {
            url = $"http://192.168.0.188:4000/users/{username}";

            string response = "";
            int userId = 0;

            try
            {
                response = await httpClient.GetStringAsync(url);
            }
            catch (Exception ex)
            {
                return userId;
            }

            User responseData = JsonConvert.DeserializeObject<User>(response);

            if (responseData.Id != 0) userId = responseData.Id;

            return userId;
        }

        public async Task EditUser(UserEditPost newUser)
        {
            url = "http://192.168.0.188:4000/users/edit";

            string jsonData = JsonConvert.SerializeObject(newUser);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, content);
        }



        /* ------ CHATS INTERACTIONS ------ */



        public async Task<List<Chat>> GetChats(int OwnerId)
        {
            url = $"http://192.168.0.188:4000/users/{OwnerId}/chats";
            var response = await httpClient.GetStringAsync(url);

            List<Chat> responseChatList = JsonConvert.DeserializeObject<List<Chat>>(response);

            for(int i= 0; i<responseChatList.Count; i++)
            {
                if(responseChatList[i].Messages.Count == 0) responseChatList[i].LastMessage = "";
                else responseChatList[i].LastMessage = responseChatList[i].Messages.Last().Username + ": " + responseChatList[i].Messages.Last().Text;
            }

            return responseChatList;
        }

        public async Task<Chat> GetChatById(int chatId)
        {
            url = $"http://192.168.0.188:4000/chats/{chatId}";
            var response = await httpClient.GetStringAsync(url);

            Chat responseChat = JsonConvert.DeserializeObject<Chat>(response);

            return responseChat;
        }

        public async Task<Response> CreateChat(ChatPost chat)
        {
            url = $"http://192.168.0.188:4000/chats/create";

            string jsonData = JsonConvert.SerializeObject(chat);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, content);
            string result = await response.Content.ReadAsStringAsync();
            Response responseData = JsonConvert.DeserializeObject<Response>(result);
            return responseData;
        }

        public async Task DeleteChat(int id) //Не работает, когда удаляется чат с сообщениями
        {
            var url1 = $"http://192.168.0.188:4000/chats/delete?chatId={id}";


            string jsonData = JsonConvert.SerializeObject(id);
            StringContent content = new StringContent(jsonData, Encoding.UTF8);
          
            await httpClient.PostAsync(url1, content);
            
            
        }

        public async Task EditChat(ChatEditPost chat)
        {
            var url1 = $"http://192.168.0.188:4000/chats/edit";

            string jsonData = JsonConvert.SerializeObject(chat);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url1, content);
        }

        public async Task RemoveUserFromChat( UserChatPost userChat)
        {
            var url1 = $"http://192.168.0.188:4000/chats/remove-user";

            string jsonData = JsonConvert.SerializeObject(userChat);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url1, content);
        }

        public async Task<List<User>> GetChatUsers(int chatId)
        {
            url = $"http://192.168.0.188:4000/chats/{chatId}/users";

            var response = await httpClient.GetStringAsync(url);

            List<User> users = JsonConvert.DeserializeObject<List<User>>(response);

            return users;

        }

        public async Task AddUserToChat(UserChatPost userChat)
        {
            var url1 = $"http://192.168.0.188:4000/chats/add-user";

            string jsonData = JsonConvert.SerializeObject(userChat);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url1, content);
        }



        /* ------ MESSAGES INTERACTIONS ------ */



        public async Task<List<Message>> GetMessages(int ChatId, int OwnerId)
        {
            url = $"http://192.168.0.188:4000/chats/{ChatId}/messages";
            var response = await httpClient.GetStringAsync(url);


            List<Message> responseList = JsonConvert.DeserializeObject<List<Message>>(response);

            for (int i = 0; i < responseList.Count; i++)
            {
                if (responseList[i].UserId == OwnerId) responseList[i].IsOwner = true;
                if (responseList[i].Text != null) responseList[i].HasMessage = true;

                if (responseList[i].Image != null)
                {
                    responseList[i].HasPicture = true;

                    var images = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                    var filepath = Path.Combine(images, $"{Guid.NewGuid()}.jpg");

                    File.WriteAllBytes(filepath, responseList[i].Image);

                    responseList[i].ImagePath = filepath;
                }
            }
            return responseList;
        }

        public async Task<Message> GetMessageById(int id)
        {
            var url1 = $"http://192.168.0.188:4000/messages/{id}";

            var response = await httpClient.GetStringAsync(url);

            Message message = JsonConvert.DeserializeObject<Message>(response);

            return message;
        }

        public async Task<Response> CreateMessage(MessagePost message)
        {
            var url1 = "http://192.168.0.188:4000/messages/create";

            string jsonData = JsonConvert.SerializeObject(message);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url1, content);
            string result = await response.Content.ReadAsStringAsync();
            Response responseData = JsonConvert.DeserializeObject<Response>(result);
            return responseData;
        }

        public async Task<Response> DeleteMessage(int messageId)
        {
            url = $"http://192.168.0.188:4000/messages/delete?messageId={messageId}";

            string jsonData = JsonConvert.SerializeObject(messageId);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, content);

            string result = await response.Content.ReadAsStringAsync();
            Response responseData = JsonConvert.DeserializeObject<Response>(result);
            return responseData;
        }

        public async Task<Response> EditMessage(MessageEditPost newMessage)
        {
            url = $"http://192.168.0.188:4000/messages/edit";

            string jsonData = JsonConvert.SerializeObject(newMessage);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, content);
            string result = await response.Content.ReadAsStringAsync();
            Response responseData = JsonConvert.DeserializeObject<Response>(result);
            
            return responseData;
        }
    }
}
