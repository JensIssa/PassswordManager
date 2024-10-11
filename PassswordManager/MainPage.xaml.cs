using Microsoft.Maui.Controls;
using System;
using System.IO;
using PassswordManager.Services;
using PassswordManager.PasswordManagerService.Interfaces;

namespace PassswordManager
{
    public partial class MainPage : ContentPage
    {
        private IPasswordService _service;

        public MainPage()
        {
            InitializeComponent();
        }

        public MainPage(IPasswordService service)
        {
            InitializeComponent();
            _service = service;
        }

        /// <summary>
        /// Unlocks the app and navigates to the PasswordsPage if the master password is correct and the file exists
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

                await Navigation.PushAsync(new PasswordsPage(masterPassword, _service));
            }
            else
            {
                await DisplayAlert("Error", "Please enter the master password.", "OK");
            }
        }
    }
}
