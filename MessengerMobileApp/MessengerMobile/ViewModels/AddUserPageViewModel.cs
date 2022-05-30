using ChatApp.Mobile.ViewModels;
using MessengerMobile.Models;
using MessengerMobile.ServerModels;
using MessengerMobile.Services.Interface;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MessengerMobile.ViewModels
{
    internal class AddUserPageViewModel : ViewModelBase
    {
        public AddUserPageViewModel(INavigationService navigationService, IServerConnect serverConnect, IPageDialogService dialogService) : base(navigationService)
        {
            _navigationService = navigationService;
            _serverConnect = serverConnect;
            _dialogService = dialogService;

            AddUserToChatCommand = new DelegateCommand(AddUserToChat);
            GoBackCommand = new DelegateCommand(GoBack);
        }

        IPageDialogService _dialogService;
        INavigationService _navigationService;
        IServerConnect _serverConnect;

        public ICommand GoBackCommand { get; private set; }
        public ICommand AddUserToChatCommand { get; private set; }

        private string username;
        public string Username
        {
            get => username;
            set => SetProperty(ref username, value);
        }

        private int chatId;
        public int ChatId
        {
            get => chatId;
            set => SetProperty(ref chatId, value);
        }

        private int ownerId;
        public int OwnerId
        {
            get => ownerId;
            set => SetProperty(ref ownerId, value);
        }

        public async override void Initialize(INavigationParameters parameters)
        {
            ChatId = parameters.GetValue<int>("ChatId");
            OwnerId = parameters.GetValue<int>("OwnerId");
        }

        private async void AddUserToChat()
        {
            int userId = await _serverConnect.GetUserByUsername(Username);
            List<User> users = await _serverConnect.GetChatUsers(ChatId);
            

            if (userId == 0) await _dialogService.DisplayAlertAsync($"User with this name doesn't exist", "Enter user's name again", "OК");
            
            else
            {
                User user = await _serverConnect.GetUserById(userId);
                UserChatPost userChat = new UserChatPost();
                userChat.ChatId = ChatId;
                userChat.UserId = userId;

                await _serverConnect.AddUserToChat(userChat);

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
