using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MusicApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GoToSearch(object sender, RoutedEventArgs e)
        {
            Search.SearchWindow searchWindow = new Search.SearchWindow();
            searchWindow.Show();
            Close();
        }

        private void GoToChat(object sender, RoutedEventArgs e)
        {
            Chat.MVVM.View.ChatWindow chatWindow = new Chat.MVVM.View.ChatWindow();
            chatWindow.Show();
            Close();
        }

        private void GoToProfile(object sender, RoutedEventArgs e)
        {
            Profile.ProfileWindow profileWindow = new Profile.ProfileWindow();
            profileWindow.Show();
            Close();
        }

        private void GoToPayment(object sender, RoutedEventArgs e)
        {
            Payment.PaymentPlatform paymentPlatform = new Payment.PaymentPlatform();
            paymentPlatform.Show();
            Close();
        }
    }
}