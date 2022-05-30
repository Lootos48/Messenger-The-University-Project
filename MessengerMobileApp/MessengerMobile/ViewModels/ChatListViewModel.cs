using ChatApp.Mobile.ViewModels;
using MessengerMobile.Models;
using MessengerMobile.ServerModels;
using MessengerMobile.Services.Interface;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Input;
using Xamarin.Forms;

namespace MessengerMobile.ViewModels
{
    public class ChatListViewModel: ViewModelBase
    {
        public ChatListViewModel(INavigationService navigationService, IServerConnect serverConnect) : base(navigationService)
        {
            AddChatCommand = new DelegateCommand(AddChat);
            ExitToolBarCommand = new DelegateCommand(ExitToolBar);
            GetUserInfoCommand = new DelegateCommand(GetUserInfo);

            _serverConnect = serverConnect;
        }

        IServerConnect _serverConnect;

        public ICommand ExitToolBarCommand { get; private set; } //done
        public ICommand AddChatCommand { get; private set; }
        public ICommand GetUserInfoCommand { get; private set; }
        
        public ICommand RefreshPageCommand => new Command(RefreshPage);
        public ICommand OnDeleteTapCommand => new Command(OnDeleteTap);
        public ICommand GoToChatPageCommand => new Command(GoToChatPage);


        public IEnumerable<Chat> _chatList;
        public IEnumerable<Chat> ChatList
        {
            get => _chatList;
            set => SetProperty(ref _chatList, value);
        }


        private int id;
        public int Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }

        private string username;
        public string Username
        {
            get => username;
            set => SetProperty(ref username, value);
        }

        private string chatName;
        public string ChatName
        {
            get => chatName;
            set => SetProperty(ref chatName, value);
        }

        private User ownerUser;
        public User OwnerUser
        {
            get => ownerUser;
            set => SetProperty(ref ownerUser, value);
        }

        private bool isRefreshing;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set => SetProperty(ref isRefreshing, value);
        }

        public async override void Initialize(INavigationParameters parameters)
        {
            Id = parameters.GetValue<int>("OwnerId");

            OwnerUser = await _serverConnect.GetUserById(Id);

            Username = OwnerUser.Username;

            List<Chat> chats = await _serverConnect.GetChats(Id);
            ChatList = new List<Chat>(chats);

            //TimerCallback tm = new TimerCallback(RefreshPage);
            //Timer timer = new Timer(tm, 0, 0, 10000);
        }


        public async void GetUserInfo()
        {
            var param = new NavigationParameters { { "Id", OwnerUser.Id } };
            await NavigationService.NavigateAsync(new System.Uri("http://www.MessengerMobile/NavigationPage/UserInfoPage", System.UriKind.Absolute), param);
        }

        public async void RefreshPage()
        {
            List<Chat> chats = await _serverConnect.GetChats(Id);
            ChatList = new List<Chat>(chats);
            IsRefreshing = false;
        }

        private async void AddChat()
        {
            var param = new NavigationParameters { { "CreatorId", OwnerUser.Id } };
            await NavigationService.NavigateAsync("AddChatPage", param);
        }

        private async void GoToChatPage(object obj)
        {
            Chat chat = (Chat)obj;
            var param = new NavigationParameters();
            param.Add("ChatId", chat.Id);
            param.Add("OwnerId", Id);

            await NavigationService.NavigateAsync(new System.Uri("http://www.MessengerMobile/NavigationPage/ChatRoomPage", System.UriKind.Absolute), param);
        }

        private async void ExitToolBar()
        {
            await NavigationService.NavigateAsync(new System.Uri("http://www.MessengerMobile/NavigationPage/SignInPage", System.UriKind.Absolute));
        }

        private async void OnDeleteTap(object obj)
        {
            var chat = (Chat)obj;

            List<User> users = await _serverConnect.GetChatUsers(chat.Id);

            UserChatPost deleteUser = new UserChatPost();
            deleteUser.UserId = OwnerUser.Id;
            deleteUser.ChatId = chat.Id;

            if(users.Count>1) await _serverConnect.RemoveUserFromChat(deleteUser);
            else await _serverConnect.DeleteChat(chat.Id);

        }

    }
}
