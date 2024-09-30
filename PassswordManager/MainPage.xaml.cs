using Microsoft.Maui.Controls;

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
                await Navigation.PushAsync(new PasswordsPage());
            }
            else
            {
                await DisplayAlert("Error", "Please enter the master password.", "OK");
            }
        }
    }
}
