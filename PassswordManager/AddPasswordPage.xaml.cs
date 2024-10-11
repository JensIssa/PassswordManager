using PassswordManager.Models;
using System;
using Microsoft.Maui.Controls;
using PassswordManager.PasswordManagerService.Interfaces;

namespace PassswordManager
{
    public partial class AddPasswordPage : ContentPage
    {
        private string _masterPassword;
        private IPasswordService _service;
        
        public AddPasswordPage(string masterPassword, IPasswordService service)
        {
            InitializeComponent();
            _masterPassword = masterPassword;
            _service = service;
        }

        private async void OnSavePasswordClicked(object sender, EventArgs e)
        {
            var siteName = SiteNameEntry.Text;
            var plainPassword = PasswordEntry.Text;

            if (!string.IsNullOrEmpty(siteName) && !string.IsNullOrEmpty(plainPassword))
            {
                var passwordItem = new PasswordItem
                {
                    Name = siteName
                };

                await _service.AddPassword(siteName, plainPassword, _masterPassword);

                await DisplayAlert("Success", "Password saved!", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Please fill in both fields", "OK");
            }
        }
    }
}
