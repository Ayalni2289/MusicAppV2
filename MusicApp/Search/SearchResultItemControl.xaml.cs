using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MusicApp.Search
{
    /// <summary>
    /// Lógica de interacción para SearchResultItemControl.xaml
    /// </summary>
    public partial class SearchResultItemControl : UserControl
    {
        public SearchResultItemControl()
        {
            InitializeComponent();
        }

        // Propiedades públicas para acceder a los elementos desde fuera de la clase
        public Image ImageElement => image;
        public TextBlock TitleElement => title;
        public TextBlock SubTitle1Element => subTitle1;
        public TextBlock SubTitle2Element => subTitle2;
        public TextBlock SubTitle3Element => subTitle3;

        public void SetImage(string imagePath)
        {
            // Check if imagePath is empty or null
            if (string.IsNullOrEmpty(imagePath))
            {
                // Set default image
                var uri = new Uri("images/default.jpg", UriKind.Relative);
                image.Source = new BitmapImage(uri);
            }
            else
            {
                try
                {
                    // Set the image source
                    var uri = new Uri(imagePath, UriKind.Relative);
                    image.Source = new BitmapImage(uri);
                }
                catch (UriFormatException ex)
                {
                    // Handle invalid URI format
                    Console.WriteLine("Error loading image: " + ex.Message);
                    // Set default image
                    var uri = new Uri("images/default.jpg", UriKind.Relative);
                    image.Source = new BitmapImage(uri);
                }
            }
        }

        public void SetTitle(string title)
        {
            // Set the title text
            this.title.Text = title;
        }

        public void SetSubTitle1(string subTitle1)
        {
            // Set the first subtitle text
            this.subTitle1.Text = subTitle1;
        }

        public void SetSubTitle2(string subTitle2)
        {
            // Set the second subtitle text
            this.subTitle2.Text = subTitle2;
        }

        public void SetSubTitle3(string subTitle3)
        {
            // Set the third subtitle text
            this.subTitle3.Text = subTitle3;
        }
    }
}
