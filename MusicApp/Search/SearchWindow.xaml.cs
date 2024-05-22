using System.Net.Http.Headers;
using System.Windows;
using System.Windows.Controls;
using MusicApp.Database;

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
        public SearchResultItemControl AddSearchResult(string imagePath, string title, string subTitle1 = "", string subTitle2 = "", string subTitle3 = "")
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

        public void SearchButton(object sender, RoutedEventArgs e)
        {
            // Clear results area for the next search
            searchResultsStackPanel.Children.Clear();

            // Filter search
            int filter = filterComboBox.SelectedIndex;
            List<SearchResultItemControl> searchItems = FilterSearch(filter);
            // Search algorithm
            string keywords = searchInput.Text;
            List<SearchResultItemControl> searchResults = FuzzyMatchingSearch(keywords, searchItems);
            // Sort results
            int sorter = sortComboBox.SelectedIndex;
            searchResults = SortSearchResults(searchResults, sorter);

            // Show results
            DisplaySearchResults(searchResults);
        }

        public List<SearchResultItemControl> FuzzyMatchingSearch(string keywords, List<SearchResultItemControl> searchItems)
        {
            List<SearchResultItemControl> matches = new List<SearchResultItemControl>();
            foreach (SearchResultItemControl item in searchItems)
            {
                string itemTitle = item.title.Text;
                if (new LevenshteinDistance().IsFuzzyMatch(keywords.ToLower(), itemTitle.ToLower(), 2))
                {
                    matches.Add(item);
                }
            }

            return matches;
        }

        public List<SearchResultItemControl> FilterSearch(int filter)
        {
            List<SearchResultItemControl> searchItems = new List<SearchResultItemControl>();
            // Determine which table(s) to search through according to the selected filter
            switch (filter)
            {
                case 0:
                    searchItems = DatabaseManager.GetInstance().LoadSongSearchItems(searchItems);
                    searchItems = DatabaseManager.GetInstance().LoadArtistSearchItems(searchItems);
                    searchItems = DatabaseManager.GetInstance().LoadAlbumSearchItems(searchItems);
                    searchItems = DatabaseManager.GetInstance().LoadUserSearchItems(searchItems);
                    break;
                case 1:
                    searchItems = DatabaseManager.GetInstance().LoadSongSearchItems(searchItems);
                    break;
                case 2:
                    searchItems = DatabaseManager.GetInstance().LoadArtistSearchItems(searchItems);
                    break;
                case 3:
                    searchItems = DatabaseManager.GetInstance().LoadAlbumSearchItems(searchItems);
                    break;
                case 4:
                    string genre = genreInput.Text;
                    searchItems = DatabaseManager.GetInstance().LoadSongSearchItems(searchItems, genre);
                    searchItems = DatabaseManager.GetInstance().LoadAlbumSearchItems(searchItems, genre);
                    break;
                case 5:
                    searchItems = DatabaseManager.GetInstance().LoadUserSearchItems(searchItems);
                    break;
            }

            return searchItems;
        }

        public List<SearchResultItemControl> SortSearchResults(List<SearchResultItemControl> searchResults, int sorter)
        {
            List<SearchResultItemControl> sortedResults = new List<SearchResultItemControl>();
            // Determine the sorting algorithm to use and call it
            switch (sorter)
            {
                case 0:
                    // sortedResults = SortByRelevance(searchResults);
                    sortedResults = searchResults; // PROVISIONAL
                    break;
                case 1:
                    // sortedResults = SortByPopularity(searchResults);
                    sortedResults = searchResults; // PROVISIONAL
                    break;
                case 2:
                    if (filterComboBox.SelectedIndex == 0 || filterComboBox.SelectedIndex == 2 || filterComboBox.SelectedIndex == 5)
                    {
                        MessageBox.Show("Cannot sort artists or users by date. Please select either another sorter or filter", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        sortedResults = searchResults;
                    }
                    else
                    {
                        sortedResults = new Sorters().NumericalQuickSort(searchResults);
                    }
                    break;
                case 3:
                    sortedResults = new Sorters().AlphabeticalQuickSort(searchResults);
                    break;
            }
            return sortedResults;
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
        public void FilterChanged(object sender, SelectionChangedEventArgs e)
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
