using PassswordManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassswordManager
{
    public partial class AddPasswordPage : ContentPage
    {
        private List<PasswordItem> _passwordList;

        public AddPasswordPage(List<PasswordItem> passwordList)
        {
            InitializeComponent();
            _passwordList = passwordList;
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
                passwordItem.SetPassword(plainPassword); 

                _passwordList.Add(passwordItem);

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

