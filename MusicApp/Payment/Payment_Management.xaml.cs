using System.Windows;
using Stripe;

namespace MusicApp.Payment
{
    /// <summary>
    /// Lógica de interacción para Payment_Management.xaml
    /// </summary>
    public partial class Payment_Management : Window
    {
        public SubscriptionOption CurrentSubscription { get; set; }
        private bool visa = true;

        public Payment_Management()
        {
            InitializeComponent();

            CurrentSubscription = new SubscriptionOption { Description = "$4.95/month" }; // Valor predeterminado
            DataContext = CurrentSubscription; // Enalce de la UI con el programa, se asegura de que la instancia de SubscriptionOption esté vinculada como Payment_Management
        }

        public void UpdateSubscription(string description)
        {
            if (CurrentSubscription != null)
            {
                CurrentSubscription.Description = description;
            }
        }

        private void Visa_RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            visa = false;
        }

        private async void SubmitPayment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Set your Stripe API key
                StripeConfiguration.ApiKey = "sk_test_51P6BUpRvFouX4nrwx398QL6bwPU9iiIkV0hFHw2qv5VMfAsm1GTrMD2nx0MOvWgZ84G6aon3MaJiG944xJ3eM4Yk00hTSBXf1E";

                // Create Token options
                var tokenOptions = new TokenCreateOptions
                {
                    Card = new TokenCardOptions
                    {
                        Number = card_number.Text,
                        ExpMonth = exp_month.Text,
                        ExpYear = exp_year.Text,
                        Cvc = cvc.Text,
                    }
                };

                // Create Token Service
                var tokenService = new TokenService();
                Token stripeToken = await tokenService.CreateAsync(tokenOptions);

                // Now stripeToken contains the token object which you can send to your server
                // Your payment logic here
                MessageBox.Show("Payment Successful!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Payment Failed: " + ex.Message);
            }
        }

        /*
        private async void Pay_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StripeConfiguration.SetApiKey("sk_test_51P6BUpRvFouX4nrwx398QL6bwPU9iiIkV0hFHw2qv5VMfAsm1GTrMD2nx0MOvWgZ84G6aon3MaJiG944xJ3eM4Yk00hTSBXf1E");
                // Creamos el token para tener todos los datos: el cvc, ...
                var paymentMethodOptions = new PaymentMethodCreateOptions
                {
                    Type = "card",
                    Card = new PaymentMethodCardOptions
                    {
                        Number = card_number.Text,
                        ExpMonth = Convert.ToInt32(exp_month.Text),
                        ExpYear = Convert.ToInt32(exp_year.Text),
                        Cvc = cvc.Text,
                    },
                };

                var paymentMethodService = new PaymentMethodService();
                PaymentMethod paymentMethod = paymentMethodService.Create(paymentMethodOptions);

                // Crear un PaymentIntent usando el PaymentMethod
                var paymentIntentOptions = new PaymentIntentCreateOptions
                {
                    Amount = Convert.ToInt32(_price_variable_.Text),  // Asumiendo que este es el monto a cobrar
                    Currency = "usd",
                    PaymentMethod = paymentMethod.Id,
                    ConfirmationMethod = "automatic",
                    Confirm = true,  // Esto confirma el PaymentIntent al crearlo
                };

                var paymentIntentService = new PaymentIntentService();
                PaymentIntent paymentIntent = paymentIntentService.Create(paymentIntentOptions);


                MessageBox.Show("Pago iniciado, ID del PaymentIntent: " + paymentIntent.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed: " + ex.Message);
            }


        }
        */
    }
}
