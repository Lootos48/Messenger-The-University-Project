using ChatApp.Mobile.ViewModels;
using MessengerMobile.Models;
using MessengerMobile.ServerModels;
using MessengerMobile.Services.Interface;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MessengerMobile.ViewModels
{
    public class ChatInfoPageVIewModel : ViewModelBase
    {
        public ChatInfoPageVIewModel(INavigationService navigationService, IServerConnect serverConnect) : base(navigationService)
        {
            _navigationService = navigationService;
            _serverConnect = serverConnect;

            GoBackCommand = new DelegateCommand(GoBack);
            EditChatCommand = new DelegateCommand(EditChat);
        }

        INavigationService _navigationService; 
        IServerConnect _serverConnect;

        public ICommand GoBackCommand { get; private set; }
        public ICommand EditChatCommand { get; private set; }

        private int ownerId;
        public int OwnerId
        {
            get => ownerId;
            set => SetProperty(ref ownerId, value);
        }

        private int chatId;
        public int ChatId
        {
            get => chatId;
            set => SetProperty(ref chatId, value);
        }

        private string chatName;
        public string ChatName
        {
            get => chatName;
            set => SetProperty(ref chatName, value);
        }

        private string chatList;
        public string ChatList
        {
            get => chatList;
            set => SetProperty(ref chatList, value);
        }

        private Chat chat;
        public Chat Chat
        {
            get => chat;
            set => SetProperty(ref chat, value);
        }


        public async override void Initialize(INavigationParameters parameters)
        {
            ChatId = parameters.GetValue<int>("ChatId");
            OwnerId = parameters.GetValue<int>("OwnerId");

            Chat = await _serverConnect.GetChatById(ChatId);

            ChatName = Chat.Title;
            ChatList = "";

            for(int i = 0; i< Chat.Users.Count; i++)
            {
                string name = Chat.Users[i].Username;
                ChatList = ChatList + "\n" + name;
            }
        }

        private async void EditChat()
        {
            if(ChatName != Chat.Title)
            {
                ChatEditPost chat = new ChatEditPost();
                chat.Id = ChatId;
                chat.Title = ChatName;

                await _serverConnect.EditChat(chat);


                var param = new NavigationParameters();
                param.Add("ChatId", ChatId);
                param.Add("OwnerId", OwnerId);

                await NavigationService.NavigateAsync(new System.Uri("http://www.MessengerMobile/NavigationPage/ChatRoomPage", System.UriKind.Absolute), param);


            }
        }
        private async void GoBack()
        {
            var param = new NavigationParameters();
            param.Add("ChatId", ChatId);
            param.Add("OwnerId", OwnerId);

            await NavigationService.NavigateAsync(new System.Uri("http://www.MessengerMobile/NavigationPage/ChatRoomPage", System.UriKind.Absolute), param);
        }


    }
}
