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
using Xamarin.Essentials;
using static System.Net.Mime.MediaTypeNames;

namespace MessengerMobile.ViewModels
{
    internal class SignUpPageViewModel: ViewModelBase
    {

        public SignUpPageViewModel(INavigationService navigationService, IServerConnect serverConnect, IPageDialogService dialogService) : base(navigationService)
        {
            RegisterCommand = new DelegateCommand(Register);
            PickAvatarCommand = new DelegateCommand(PickAvatar);

            _serverConnect = serverConnect;
            _dialogService = dialogService;
        }

        IServerConnect _serverConnect;
        IPageDialogService _dialogService;

        public ICommand RegisterCommand { get; private set; }
        public ICommand PickAvatarCommand { get; private set; }


        private string username;
        public string Username
        {
            get => username;
            set => SetProperty(ref username, value);
        }

        private string password;
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        private string confirmPassword;
        public string ConfirmPassword
        {
            get => confirmPassword;
            set => SetProperty(ref confirmPassword, value);
        }

        private byte[] image;
        public byte[] Image
        {
            get => image;
            set => SetProperty(ref image, value);
        }

        private async void PickAvatar()
        {
            var file = await MediaPicker.PickPhotoAsync();
            if (file == null) return;

            Image = System.IO.File.ReadAllBytes($"{file.FullPath}");
        }

        private async void Register()
        {
            if(Password == ConfirmPassword)
            {
                UserPost user = new UserPost();
                user.username = Username;
                user.password = Password;
                user.imagebytes = Image;

                Response responseData = await _serverConnect.Registration(user);

                if (responseData.userId != 0)
                {
                    var param = new NavigationParameters { { "OwnerId", responseData.userId } };
                    await _dialogService.DisplayAlertAsync("Registration successful!", "", "Ok");
                    await NavigationService.NavigateAsync(new System.Uri("http://www.MessengerMobile/NavigationPage/ChatListPage", System.UriKind.Absolute), param);
                }

                else
                {
                    await _dialogService.DisplayAlertAsync($"{responseData.Error}", "Try another username", "Ok");
                }
            }

            else await _dialogService.DisplayAlertAsync("Passwords don't match", "Check passwords", "Ok");

        }


    }
}
