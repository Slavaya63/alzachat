using System;
using System.Collections.Generic;
using System.Net.Http;
using Acr.UserDialogs;
using AlzaMobile.Core.Repositories.Interfaces;
using MvvmCross.Core.ViewModels;

namespace AlzaMobile.Core.ViewModels
{
    public class AuthorizationViewModel : BaseViewModel
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IUserDialogs _userDialogs;
        private string _login;
        private string _password;

        public string Login
        {
            get { return _login; }
            set { SetProperty(ref _login, value);}
        }

        public string Password
        {
            get { return _password; }
            set {SetProperty(ref _password, value);}
        }

        public AuthorizationViewModel(ILoginRepository loginRepository, IUserDialogs userDialogs)
        {
            _loginRepository = loginRepository;
            _userDialogs = userDialogs;
        }



        #region commands

        public MvxCommand LoginCommand => new MvxCommand(DoLogin);

        private async void DoLogin()
        {
            HttpResponseMessage result = await _loginRepository.Login(Login, Password);

            if (result != null && result.IsSuccessStatusCode)
            {
                ShowViewModel<MainViewModel>();
            }
            else
                _userDialogs.Toast("Ошибка");
        }

        #endregion  
    }
}
