using ChatApp.Mobile.ViewModels;
using Messenger.Services.Interfaces;
using MessengerMobile.Models;
using MessengerMobile.ServerModels;
using MessengerMobile.Services.Interface;
using MessengerMobile.Views;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Messenger.ViewModels
{

    public class SignInPageViewModel : ViewModelBase
    {
        public SignInPageViewModel(INavigationService navigationService, IServerConnect serverConnect, IPageDialogService dialogService) : base(navigationService)
        {
            SignInCommand = new DelegateCommand(SignIn);
            GoToSignUpPage = new DelegateCommand(GoToSignUp);

            _serverConnect = serverConnect;
            _dialogService = dialogService;
        }

        IServerConnect _serverConnect;
        IPageDialogService _dialogService;

        public ICommand SignInCommand { get; private set; }
        public ICommand GoToSignUpPage { get; private set; }


        private string username;
        public string Username {
            get => username;
            set => SetProperty(ref username, value); 
        }

        private string password;
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        
        private async void SignIn() 
        {
            User user = new User();
            user.Username = Username;
            user.Password = Password;

            Response responseData = await _serverConnect.Autorization(user);

            if (responseData.userId != 0)
            {
                var param = new NavigationParameters { { "OwnerId", responseData.userId } };
                await NavigationService.NavigateAsync(new System.Uri("http://www.MessengerMobile/NavigationPage/ChatListPage", System.UriKind.Absolute), param);
            }

            else
            {
                await _dialogService.DisplayAlertAsync("Invalid login or password", "Try again", "Ok");
            }
        }

        private async void GoToSignUp()
        {
            await NavigationService.NavigateAsync("SignUpPage");
        }
    }
}
