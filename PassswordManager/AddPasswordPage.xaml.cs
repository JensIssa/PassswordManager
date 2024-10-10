using PassswordManager.Models;
using PassswordManager.Data;
using System;
using Microsoft.Maui.Controls;

namespace PassswordManager
{
    public partial class AddPasswordPage : ContentPage
    {
        private string _masterPassword;

        public AddPasswordPage(string masterPassword)
        {
            InitializeComponent();
            _masterPassword = masterPassword;
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
                passwordItem.SetPassword(plainPassword, _masterPassword);

                using (var db = new PasswordsContext())
                {
                    db.PasswordItems.Add(passwordItem);
                    await db.SaveChangesAsync();
                }

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
