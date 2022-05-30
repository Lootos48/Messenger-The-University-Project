using ChatApp.Mobile.ViewModels;
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
    public class AddChatPageViewModel : ViewModelBase
    {
        

        public AddChatPageViewModel(INavigationService navigationService, IServerConnect serverConnect, IPageDialogService dialogService) : base(navigationService)
        {
            SaveChatCommand = new DelegateCommand(SaveChat);

            _serverConnect = serverConnect;
            _dialogService = dialogService;
        }

        IServerConnect _serverConnect;
        IPageDialogService _dialogService;

        private int id;
        public int Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }

        private string chatname;
        public string Chatname
        {
            get => chatname;
            set => SetProperty(ref chatname, value);
        }

        private string secondUserName;
        public string SecondUserName
        {
            get => secondUserName;
            set => SetProperty(ref secondUserName, value);
        }

        public ICommand SaveChatCommand { get; private set; }
        public async override void Initialize(INavigationParameters parameters)
        {
            Id = parameters.GetValue<int>("CreatorId");
        }

        private async void SaveChat()
        {
            ChatPost chatPost = new ChatPost();
            chatPost.title = Chatname;
            chatPost.creatorId = Id;

            if(chatPost.title == "") await _dialogService.DisplayAlertAsync($"Enter chat name", "", "Ok");

            Response response = await _serverConnect.CreateChat(chatPost);

            if(response.chatId != 0)
            {
                await NavigationService.GoBackAsync();
            }

            else await _dialogService.DisplayAlertAsync($"{response.Error}", "Try again", "Ok");
        }
    }
}
