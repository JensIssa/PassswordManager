using Microsoft.Maui.Controls;
using System;
using System.IO;
using PassswordManager.Services;

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
                if (!File.Exists(MasterPasswordService.MasterPasswordFilePath))
                {
                    MasterPasswordService.SetMasterPassword(masterPassword);
                    await DisplayAlert("Success", "Master password set.", "OK");
                }
                else if (!MasterPasswordService.VerifyMasterPassword(masterPassword))
                {
                    await DisplayAlert("Error", "Incorrect master password.", "OK");
                    return;
                }

                await Navigation.PushAsync(new PasswordsPage(masterPassword));
            }
            else
            {
                await DisplayAlert("Error", "Please enter the master password.", "OK");
            }
        }
    }
}
