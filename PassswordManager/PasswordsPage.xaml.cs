using PassswordManager.Models;
using PassswordManager.PasswordManagerService.Interfaces;
using System.Linq;

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

            foreach (var passwordItem in passwords)
            {
                passwordItem.IsPasswordVisible = false;
            }

            PasswordsListView.ItemsSource = passwords;
        }

        private async void OnAddNewPasswordClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddPasswordPage(_masterPassword, _service));
        }

        private void OnEyeButtonClicked(object sender, EventArgs e)
        {
            var button = sender as ImageButton;
            var passwordItem = button?.BindingContext as PasswordItem;

            if (passwordItem != null)
            {
                passwordItem.IsPasswordVisible = !passwordItem.IsPasswordVisible;

                var viewCell = GetParent<ViewCell>(button);
                if (viewCell != null)
                {
                    var grid = viewCell.View as Grid;
                    if (grid != null)
                    {
                        var entry = grid.Children.OfType<Entry>().FirstOrDefault();
                        if (entry != null)
                        {
                            if (passwordItem.IsPasswordVisible)
                            {
                                string decryptedPassword;
                                try
                                {
                                    decryptedPassword = passwordItem.GetPassword(_masterPassword);
                                }
                                catch
                                {
                                    decryptedPassword = "Error";
                                }

                                entry.Text = decryptedPassword;
                            }
                            else
                            {
                                // Hide the password
                                entry.Text = "********";
                            }
                        }
                    }
                }
            }
        }

        private T GetParent<T>(Element element) where T : Element
        {
            Element parent = element.Parent;

            while (parent != null)
            {
                if (parent is T)
                {
                    return (T)parent;
                }
                parent = parent.Parent;
            }
            return null;
        }
    }
}
