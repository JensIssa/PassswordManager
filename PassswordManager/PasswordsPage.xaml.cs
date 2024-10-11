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

        /// <summary>
        /// Loads the passwords from the database and decrypts them with the master password
        /// </summary>
        /// <returns></returns>
        private async Task LoadPasswordsAsync()
        {
            var passwords = await _service.GetPasswords();

            foreach (var passwordItem in passwords)
            {
                passwordItem.IsPasswordVisible = false;
            }

            PasswordsListView.ItemsSource = passwords;
        }

        /// <summary>
        /// Navigates to the AddPasswordPage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnAddNewPasswordClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddPasswordPage(_masterPassword, _service));
        }

        /// <summary>
        /// On the eye button clicked, show the password if it's not visible, otherwise hide it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                                entry.Text = "********";
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets the parent of the given type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element"></param>
        /// <returns></returns>
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
