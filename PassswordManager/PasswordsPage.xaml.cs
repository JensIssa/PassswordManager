using Microsoft.Maui.Controls;
using PassswordManager.Models;
using System.Collections.Generic;

namespace PassswordManager
{
    public partial class PasswordsPage : ContentPage
    {
        private List<PasswordItem> _passwords;

        public PasswordsPage(List<PasswordItem> passwords)
        {
            InitializeComponent();
            _passwords = passwords;

            PasswordsListView.ItemsSource = passwords;
        }

        private async void OnAddNewPasswordClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddPasswordPage(_passwords));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            PasswordsListView.ItemsSource = null;
            PasswordsListView.ItemsSource = _passwords;
        }
    }
}
