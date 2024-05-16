using System.Windows;
using System.Windows.Controls;

namespace MusicApp
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        public void LoginButtonClick(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text;
            string password = PasswordBox.Password;

            if (Authentication.AuthenticationModule.AuthenticateUser(username, password))
            {
                MainWindow window = new MainWindow();
                window.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Error while logging in. Please try again", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void SignInButtonClick(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text;
            string password = PasswordBox.Password;

            if (Authentication.AuthenticationModule.RegisterUser(username, password))
            {
                MessageBox.Show("Sign In successful!", "Sign In", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Error while signing in. Please try again", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
