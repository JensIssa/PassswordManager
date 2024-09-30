using Microsoft.Maui.Controls;
using System.Collections.Generic;

namespace PassswordManager
{
    public partial class PasswordsPage : ContentPage
    {
        public PasswordsPage()
        {
            InitializeComponent();

            var passwords = new List<PasswordItem>
            {
                new PasswordItem { Name = "Google", Password = "myGooglePassword123" },
                new PasswordItem { Name = "Facebook", Password = "myFacebookPassword456" },
                new PasswordItem { Name = "Twitter", Password = "myTwitterPassword789" }
            };

            PasswordsListView.ItemsSource = passwords;
        }

        private async void OnAddNewPasswordClicked(object sender, EventArgs e)
        {
            await DisplayAlert("New Password", "Add password functionality will go here.", "OK");
        }
    }

    public class PasswordItem
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
