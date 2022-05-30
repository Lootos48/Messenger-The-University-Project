using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.Unity;
using Prism;
using Prism.Ioc;
using Messenger.ViewModels;
using MessengerMobile;
using Messenger.Services.Interfaces;
using Messenger.Services.Core;
using MessengerMobile.Views;
using MessengerMobile.ViewModels;
using MessengerMobile.Services.Interface;
using MessengerMobile.Services.Core;

namespace Messenger
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }
       

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();

            containerRegistry.RegisterForNavigation<SignInPage, SignInPageViewModel>();
            containerRegistry.RegisterForNavigation<SignUpPage, SignUpPageViewModel>();

            containerRegistry.RegisterForNavigation<ChatRoomPage, ChatRoomPageViewModel>();
            containerRegistry.RegisterForNavigation<ChatListPage, ChatListViewModel>();
            containerRegistry.RegisterForNavigation<AddChatPage, AddChatPageViewModel>();
            containerRegistry.RegisterForNavigation<UserInfoPage, UserInfoPageViewModel>();
            containerRegistry.RegisterForNavigation<ChatInfoPage, ChatInfoPageVIewModel>();
            containerRegistry.RegisterForNavigation<AddChatPage, AddChatPageViewModel>();
            containerRegistry.RegisterForNavigation<AddUserPage, AddUserPageViewModel>();

            containerRegistry.Register<IChatService, ChatService>();
            containerRegistry.Register<IServerConnect, ServerConnect>();

        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/SignInPage");

            //NavigationService.NavigateAsync(new System.Uri("http://www.Messenger/MainPage", System.UriKind.Absolute));
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
