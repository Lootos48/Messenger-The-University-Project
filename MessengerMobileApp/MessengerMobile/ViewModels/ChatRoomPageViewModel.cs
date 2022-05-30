using ChatApp.Mobile.ViewModels;
using Messenger.Services.Interfaces;
using MessengerMobile.Models;
using MessengerMobile.ServerModels;
using MessengerMobile.Services;
using MessengerMobile.Services.Interface;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MessengerMobile.ViewModels
{
    public class ChatRoomPageViewModel : ViewModelBase

    {
        public ChatRoomPageViewModel(INavigationService navigationService, IServerConnect serverConnect) : base(navigationService)
        {
            
            SendMsgCommand = new DelegateCommand(SendMessage);
            AttachImageCommand = new DelegateCommand(AttachImage);
            DisableRefreshCommand = new DelegateCommand(DisableRefresh);
            GetChatInfoCommand = new DelegateCommand(GetChatInfo);
            AddUserToChatCommand = new DelegateCommand(AddUserToChat);
            GoBackCommand = new DelegateCommand(GoBack);

            _navigationService = navigationService;
            _serverConnect = serverConnect;

        }

        INavigationService _navigationService;
        IServerConnect _serverConnect;

        public ICommand AddUserToChatCommand { get; private set; }
        public ICommand GoBackCommand { get; private set; }
        public ICommand GetChatInfoCommand { get; private set; }
        public ICommand SendMsgCommand { get; private set; }
        public ICommand AttachImageCommand { get; private set; }
        public ICommand DisableRefreshCommand { get; private set; }
        public ICommand OnDeleteTapCommand => new Command(OnDeleteTap);
        public ICommand OnEditTapCommand => new Command(OnEditTap);
        public ICommand RefreshPageCommand => new Command(RefreshPage);


        private int ownerId;
        public int OwnerId
        {
            get => ownerId;
            set => SetProperty(ref ownerId, value);
        }

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

        private string chatName;
        public string ChatName
        {
            get => chatName;
            set => SetProperty(ref chatName, value);
        }


        private string message;
        public string Message
        {
            get => message;
            set =>SetProperty(ref message, value);
        }

        private byte[] image;
        public byte[] Image
        {
            get => image;
            set => SetProperty(ref image, value);
        }

        private bool disableRefreshBool;
        public bool DisableRefreshBool
        {
            get => disableRefreshBool;
            set => SetProperty(ref disableRefreshBool, value);
        }

        private bool isRefreshing;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set => SetProperty(ref isRefreshing, value);
        }

        private string messageMode;
        public string MessageMode
        {
            get => messageMode;
            set => SetProperty(ref messageMode, value);
        }


        private MessageEditPost editMessage;
        public MessageEditPost EditMessage
        {
            get => editMessage;
            set => SetProperty(ref editMessage, value);
        }

        private Chat chat;
        public Chat Chat
        {
            get => chat;
            set => SetProperty(ref chat, value);
        }


        private IEnumerable<Message> messageList;
        public IEnumerable<Message> MessageList
        {
            get => messageList;
            set => SetProperty(ref messageList, value);
        }


        public async override void Initialize(INavigationParameters parameters)
        {
            ChatId = parameters.GetValue<int>("ChatId");
            OwnerId = parameters.GetValue<int>("OwnerId");

            Chat chat = await _serverConnect.GetChatById(ChatId);

            ChatId = chat.Id;
            ChatName = chat.Title;

            DisableRefreshBool = false;
            MessageMode = "Add";

            List<Message> messages = await _serverConnect.GetMessages(ChatId, OwnerId);
            MessageList = messages;

            TimerCallback tm = new TimerCallback(UpdateMessageList);
            Timer timer = new Timer(tm, 0, 0, 3000);
        }

        private void SendMessage()
        {
            if (MessageMode == "Add")
            {
                MessagePost message = new MessagePost();
                message.text = Message;
                message.userId = OwnerId;
                message.chatId = ChatId;
                message.imagebytes = Image;
                _serverConnect.CreateMessage(message);

                Message = null;
            }

            else
            {
                EditMessage.text = Message + " (edited)";
                if (Image != null) EditMessage.imageBytes = Image;

                _serverConnect.EditMessage(EditMessage);

                Message = null;
                EditMessage = null;
                MessageMode = "Add";
                DisableRefreshBool = false;
            }
        }

        private async void AttachImage()
        {
            var file = await MediaPicker.PickPhotoAsync();
            if (file == null) return;

            Image = System.IO.File.ReadAllBytes($"{file.FullPath}");

        }

        private async void GoBack()
        {
            var param = new NavigationParameters { { "OwnerId", OwnerId } };

            await NavigationService.NavigateAsync(new System.Uri("http://www.MessengerMobile/NavigationPage/ChatListPage", System.UriKind.Absolute), param);
        }

        private async void GetChatInfo()
        {
            var param = new NavigationParameters();
            param.Add("ChatId", ChatId);
            param.Add("OwnerId", OwnerId);

            await NavigationService.NavigateAsync(new System.Uri("http://www.MessengerMobile/NavigationPage/ChatInfoPage", System.UriKind.Absolute), param);
        }

        private async void AddUserToChat()
        {
            var param = new NavigationParameters();
            param.Add("ChatId", ChatId);
            param.Add("OwnerId", OwnerId);

            await NavigationService.NavigateAsync(new System.Uri("http://www.MessengerMobile/NavigationPage/AddUserPage", System.UriKind.Absolute), param);
        }

        private async void OnDeleteTap( object obj)
        {
            Message message = (Message)obj;
            if(message.UserId == OwnerId)
            {
                await _serverConnect.DeleteMessage(message.Id);
            }
            
            DisableRefreshBool = false;
        }

        private void OnEditTap(object obj)
        {
            Message message = (Message)obj;

            if(message.UserId == ownerId)
            {
                MessageMode = "Edit";

                EditMessage = new MessageEditPost();

                EditMessage.Id = message.Id;
                EditMessage.text = message.Text;
                EditMessage.imageBytes = message.Image;

                Message = EditMessage.text;
            }

            DisableRefreshBool = false;
        }


       
        public async void UpdateMessageList(object obj)
        {
            if(DisableRefreshBool == false)
            {
                List<Message> messages = await _serverConnect.GetMessages(ChatId, OwnerId);
                MessageList = messages;
            } 
        }

        public async void RefreshPage() //Если обновить страницу - список снова обновляется
        {
            DisableRefreshBool = false;
            IsRefreshing = false;
        }

        private void DisableRefresh() //Запрещаю обновление списка, когда сообщение выбрано
        {
            DisableRefreshBool = true;
        }
    }
}
