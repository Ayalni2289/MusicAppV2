using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MusicApp.Payment
{
    /// <summary>
    /// Lógica de interacción para PaymentPlatform.xaml
    /// </summary>
    public partial class PaymentPlatform : Window
    {
        public PaymentPlatform()
        {
            InitializeComponent();
        }

        private void ManagePaymentMethods_Click(object sender, RoutedEventArgs e)
        {
            // Instance for showing the window:
            var payment_Management = new Payment_Management();
            // Window show:
            payment_Management.Show();
        }

        private void ManageSubscription_Click(object sender, RoutedEventArgs e)
        {
            var manage_Subscription = new ManageSubscription();
            manage_Subscription.Show();
        }

        private void Cancel_Subscription_Click(object sender, RoutedEventArgs e)
        {
            var cancel_Subscription = new Cancel_Subscription();
            cancel_Subscription.Show();
        }

        private void Download_Click(object sender, RoutedEventArgs e)
        {
        }

        private void ContactSupport_Click(object sender, RoutedEventArgs e)
        {
            var contact_Support = new ContactSupport();
            contact_Support.Show();
        }
    }
}
