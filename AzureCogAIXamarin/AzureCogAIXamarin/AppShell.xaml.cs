using AzureCogAIXamarin.ViewModels;
using AzureCogAIXamarin.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace AzureCogAIXamarin
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
