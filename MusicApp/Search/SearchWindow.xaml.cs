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

        private List<SearchResultItemControl> FuzzyMatchingSearch(string keywords, List<SearchResultItemControl> searchItems)
        {
            List<SearchResultItemControl> matches = new List<SearchResultItemControl>();
            foreach (SearchResultItemControl item in searchItems)
            {
                string itemTitle = item.title.Text;
                if (LevenshteinDistance.IsFuzzyMatch(keywords.ToLower(), itemTitle.ToLower(), 2))
                {
                    matches.Add(item);
                }
            }

            return matches;
        }

        public List<SearchResultItemControl> FilterSearch(int filter)
        {
            List <SearchResultItemControl> searchItems = new List<SearchResultItemControl>();
            
            // Determine which table(s) to search through according to the selected filter
            switch (filter)
            {
                case 0:
                    searchItems = Database.DatabaseManager.LoadSongSearchItems(searchItems);
                    searchItems = Database.DatabaseManager.LoadArtistSearchItems(searchItems);
                    searchItems = Database.DatabaseManager.LoadAlbumSearchItems(searchItems);
                    searchItems = Database.DatabaseManager.LoadUserSearchItems(searchItems);
                    break;
                case 1:
                    searchItems = Database.DatabaseManager.LoadSongSearchItems(searchItems);
                    break;
                case 2:
                    searchItems = Database.DatabaseManager.LoadArtistSearchItems(searchItems);
                    break;
                case 3:
                    searchItems = Database.DatabaseManager.LoadAlbumSearchItems(searchItems);
                    break;
                case 4:
                    string genre = genreInput.Text;
                    searchItems = Database.DatabaseManager.LoadSongSearchItems(searchItems, genre);
                    searchItems = Database.DatabaseManager.LoadAlbumSearchItems(searchItems, genre);
                    break;
                case 5:
                    searchItems = Database.DatabaseManager.LoadUserSearchItems(searchItems);
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
                    if(filterComboBox.SelectedIndex == 0 || filterComboBox.SelectedIndex == 2 || filterComboBox.SelectedIndex == 5)
                    {
                        MessageBox.Show("Cannot sort artists or users by date. Please select either another sorter or filter", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        sortedResults = searchResults;
                    }
                    else
                    {
                        sortedResults = Sorters.NumericalQuickSort(searchResults);
                    }
                    break;
                case 3:
                    sortedResults = Sorters.AlphabeticalQuickSort(searchResults);
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


        // CODE NOT IN USE BELOW

        // Outdated search algorithm - Exact match search
        /*
        private List<SearchResultItemControl> ExactKeywordSearch(string keywords, List<SearchResultItemControl> searchItems)
        {
            List<SearchResultItemControl> matches = new List<SearchResultItemControl>();

            foreach(SearchResultItemControl item in searchItems)
            {
                string itemTitle = item.title.Text;
                if(itemTitle.ToLower() == keywords.ToLower())
                {
                    matches.Add(item);
                } 
            }

            return matches;
        }
        */

    }
}
