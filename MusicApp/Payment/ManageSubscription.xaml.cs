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
    /// Lógica de interacción para ManageSubscription.xaml
    /// </summary>
    public partial class ManageSubscription : Window
    {
        private string subscription_type = string.Empty;

        public ManageSubscription()
        {
            InitializeComponent();
            DataContext = new SubscriptionOption();
        }

        // Conectar las ventanas de ManageSubscription y Payment_Management
        private void OpenPaymentManagement(string description)
        {
            var manage_Payment = new Payment_Management();
            manage_Payment.CurrentSubscription.Description = description; // In Payment_Management tenemos getters y setters de la clase CurrentSubs, simplemente accedemos a ellos
                                                                          // ojo! a los de Payment_Management, no a la clase directamente CurrentSubs
            manage_Payment.Show();
        }

        private void MonthlyPlan_Click(object sender, RoutedEventArgs e)
        {
            subscription_type = "Monthly Plan";
            var subscriptionOption = DataContext as SubscriptionOption;
            if (subscriptionOption != null)
            {
                subscriptionOption.Description = "$4.95/month";
                OpenPaymentManagement(subscriptionOption.Description);
            }
        }

        private void ThreeMonthPlan_Click(object sender, RoutedEventArgs e)
        {
            subscription_type = "3-Month Plan";
            var subscriptionOption = DataContext as SubscriptionOption;
            if (subscriptionOption != null)
            {
                subscriptionOption.Description = "$12.99 for 3 months";
                OpenPaymentManagement(subscriptionOption.Description);
            }
        }

        private void SixMonthPlan_Click(object sender, RoutedEventArgs e)
        {
            subscription_type = "6-Month Plann";
            var subscriptionOption = DataContext as SubscriptionOption;
            if (subscriptionOption != null)
            {
                subscriptionOption.Description = "$22.90 for 6 months";
                OpenPaymentManagement(subscriptionOption.Description);
            }
        }

        private void YearlyPlan_Click(object sender, RoutedEventArgs e)
        {
            subscription_type = "Yearly Plan";
            var subscriptionOption = DataContext as SubscriptionOption;
            if (subscriptionOption != null)
            {
                subscriptionOption.Description = "$39.95/year";
                OpenPaymentManagement(subscriptionOption.Description);
            }
        }
    }
}
