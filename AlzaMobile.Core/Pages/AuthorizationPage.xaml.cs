using System;
using System.Collections.Generic;
using AlzaMobile.Core.ViewModels;
using MvvmCross.Forms.Views;
using Xamarin.Forms;

namespace AlzaMobile.Core.Pages
{
    public partial class AuthorizationPage : MvxContentPage<AuthorizationViewModel>
    {
        public AuthorizationPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
