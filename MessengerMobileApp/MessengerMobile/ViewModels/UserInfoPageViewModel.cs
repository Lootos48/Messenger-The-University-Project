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

namespace MessengerMobile.ViewModels
{
    public class UserInfoPageViewModel : ViewModelBase
    {
        public UserInfoPageViewModel(INavigationService navigationService, IServerConnect serverConnect, IPageDialogService dialogService) : base(navigationService)
        {
            _serverConnect = serverConnect;
            EditUserCommand = new DelegateCommand(EditUser);
            EditAvatarCommand = new DelegateCommand(EditAvatar);
            GoBackCommand = new DelegateCommand(GoBack);

            _dialogService = dialogService;
        }

        IPageDialogService _dialogService;
        IServerConnect _serverConnect;

        public ICommand GoBackCommand { get; private set; }
        public ICommand EditUserCommand { get; private set; }
        public ICommand EditAvatarCommand { get; private set; }

        private User ownerUser;
        public User OwnerUser
        {
            get => ownerUser;
            set => SetProperty(ref ownerUser, value);
        }

        private int id;
        public int Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }


        private string imagePath;
        public string ImagePath
        {
            get => imagePath;
            set => SetProperty(ref imagePath, value);
        }

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

        public async override void Initialize(INavigationParameters parameters)
        {
            Id = parameters.GetValue<int>("Id");

            OwnerUser = await _serverConnect.GetUserById(Id);

            ImagePath = OwnerUser.ImagePath;
            Username = OwnerUser.Username;
            Password = OwnerUser.Password;
            ConfirmPassword = OwnerUser.Password;
            Image = OwnerUser.Avatar;
        }

        private async void GoBack()
        {
            var param = new NavigationParameters { { "OwnerId", OwnerUser.Id } };
            await NavigationService.NavigateAsync(new System.Uri("http://www.MessengerMobile/NavigationPage/ChatListPage", System.UriKind.Absolute), param);
        }


        private async void EditAvatar()
        {
            var file = await MediaPicker.PickPhotoAsync();
            if (file == null) return;

            Image = System.IO.File.ReadAllBytes($"{file.FullPath}");
        }

        public async void EditUser()
        {
            if(Username != OwnerUser.Username || Password != OwnerUser.Password || Image!=OwnerUser.Avatar)
            { 
                
                    UserEditPost userEdit = new UserEditPost();
                    userEdit.Id = OwnerUser.Id;
                    userEdit.username = Username;
                    userEdit.password = Password;
                    userEdit.confirmPassword = ConfirmPassword;
                    userEdit.imageBytes = Image;

                    await _serverConnect.EditUser(userEdit);

                    var param = new NavigationParameters { { "OwnerId", userEdit.Id } };
                    await NavigationService.NavigateAsync(new System.Uri("http://www.MessengerMobile/NavigationPage/ChatListPage", System.UriKind.Absolute), param);
            }
        }
    }
}
