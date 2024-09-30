using Microsoft.Maui.Controls;
using PassswordManager.Models;

namespace PassswordManager
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnUnlockClicked(object sender, EventArgs e)
        {
            var masterPassword = MasterPasswordEntry.Text;

            if (!string.IsNullOrEmpty(masterPassword))
            {
                var passwords = new List<PasswordItem>();

                await Navigation.PushAsync(new PasswordsPage(passwords));
            }
            else
            {
                await DisplayAlert("Error", "Please enter the master password.", "OK");
            }
        }

    }
}
