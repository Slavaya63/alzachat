using System;
using System.Collections.Generic;
using AlzaMobile.Core.ViewModels;
using MvvmCross.Forms.Views;
using Xamarin.Forms;

namespace AlzaMobile.Core.Pages
{
    public partial class MainPage : MvxContentPage<MainViewModel>
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);

        }

        void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            (DataContext as MainViewModel).SelectItemCommand.Execute(e.Item.ToString());
        }
    }
}
