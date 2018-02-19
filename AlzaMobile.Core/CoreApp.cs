using System;
using Acr.UserDialogs;
using AlzaMobile.Core.Repositories.Implementation;
using AlzaMobile.Core.Repositories.Interfaces;
using AlzaMobile.Core.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.IoC;
using MvvmCross.Plugins.Messenger;

namespace AlzaMobile.Core
{
    public class CoreApp : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            Mvx.RegisterSingleton<ILoginRepository>(() => new LoginRepository());
            Mvx.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);
            Mvx.LazyConstructAndRegisterSingleton<IMvxMessenger, MvxMessengerHub>();

            RegisterAppStart(new AlzaChatAppStart());
        }
    }

    public class AlzaChatAppStart : MvxNavigatingObject, IMvxAppStart
    {
        public void Start(object hint = null)
        {

            //ShowViewModel<ChatViewModel>();

            if (string.IsNullOrEmpty(Settings.CurrentToken))
                ShowViewModel<AuthorizationViewModel>();
            else
                ShowViewModel<MainViewModel>();
        }
    }
}
