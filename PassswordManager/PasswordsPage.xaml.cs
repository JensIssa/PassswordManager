using PassswordManager.Models;
using Microsoft.EntityFrameworkCore;
using PassswordManager.PasswordManagerService.Interfaces;

namespace PassswordManager
{
    public partial class PasswordsPage : ContentPage
    {
        private string _masterPassword;
        private IPasswordService _service;

        public PasswordsPage(string masterPassword, IPasswordService service)
        {
            InitializeComponent();
            _masterPassword = masterPassword;
            _service = service;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadPasswordsAsync();
        }

        private async Task LoadPasswordsAsync()
        {
            var passwords = await _service.GetPasswords();
            PasswordsListView.ItemsSource = passwords;
        }

        private async void OnAddNewPasswordClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddPasswordPage(_masterPassword, _service));
        }

        private void PasswordsListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedPasswordItem = e.Item as PasswordItem;
            if (selectedPasswordItem != null)
            {
                try
                {
                    var decryptedPassword = selectedPasswordItem.GetPassword(_masterPassword);
                    DisplayAlert("Password", $"Site: {selectedPasswordItem.Name}\nPassword: {decryptedPassword}", "OK");
                }
                catch
                {
                    DisplayAlert("Error", "Failed to decrypt the password. Master password may be incorrect.", "OK");
                }
            }
        }
    }
}
