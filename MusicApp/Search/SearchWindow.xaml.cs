using System.Net.Http.Headers;
using System.Windows;
using System.Windows.Controls;

namespace MusicApp.Search
{
    /// <summary>
    /// Lógica de interacción para SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        public SearchWindow()
        {
            InitializeComponent();
        }

        // This is here since it correponds to the search function and not the interactions with the database,
        // but it is actually used in the database manager
        public static SearchResultItemControl AddSearchResult(string imagePath, string title, string subTitle1 = "", string subTitle2 = "", string subTitle3 = "")
        {
            // Create a result item control
            SearchResultItemControl resultItem = new SearchResultItemControl();

            // Set fields
            resultItem.SetImage(imagePath);
            resultItem.SetTitle(title);
            resultItem.SetSubTitle1(subTitle1);
            resultItem.SetSubTitle2(subTitle2);
            resultItem.SetSubTitle3(subTitle3);

            return resultItem;
        }

        private void SearchButton(object sender, RoutedEventArgs e)
        {
            // Clear results area for the next search
            searchResultsStackPanel.Children.Clear();

            // Get search results (given the inputted values in the GUI)
            int filter = filterComboBox.SelectedIndex;
            string genre = genreInput.Text;
            string keywords = searchInput.Text;
            int sorter = sortComboBox.SelectedIndex;
            List<SearchResultItemControl> searchResults = SearchLogic.GetSearchResults(filter, genre, keywords, sorter);

            // Show results
            DisplaySearchResults(searchResults);
        }

        public void DisplaySearchResults(List<SearchResultItemControl> searchResults)
        {
            // Add SearchResultItemControl items to the searchResultsPanel
            foreach (SearchResultItemControl item in searchResults)
            {
                searchResultsStackPanel.Children.Add(item);
            }
            // Trigger layout update
            searchResultsStackPanel.UpdateLayout();
        }

        // Small event handler for hiding the genre input bar as long as the genre filter is not selected
        private void FilterChanged(object sender, SelectionChangedEventArgs e)
        {
            if (genreInput != null)
            {
                if (filterComboBox.SelectedIndex == 4)
                {
                    genreInput.Visibility = Visibility.Visible;
                }
                else
                {
                    genreInput.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}
