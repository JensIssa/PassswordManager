using PassswordManager.Models;
using PassswordManager.Data;
using Microsoft.EntityFrameworkCore;

namespace PassswordManager
{
    public partial class PasswordsPage : ContentPage
    {
        private string _masterPassword;

        public PasswordsPage(string masterPassword)
        {
            InitializeComponent();
            _masterPassword = masterPassword;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadPasswordsAsync();
        }

        private async Task LoadPasswordsAsync()
        {
            using (var db = new PasswordsContext())
            {
                var passwords = await db.PasswordItems.ToListAsync();
                PasswordsListView.ItemsSource = passwords;
            }
        }

        private async void OnAddNewPasswordClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddPasswordPage(_masterPassword));
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
