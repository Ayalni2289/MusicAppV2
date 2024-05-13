using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MusicApp.Search
{
    public static class SearchLogic
    {
        public static List<SearchResultItemControl> GetSearchResults(int filter, string genre, string keywords, int sorter)
        {
            // Filter search
            List<SearchResultItemControl> searchItems = FilterSearch(filter, genre);

            // Search algorithm
            List<SearchResultItemControl> searchResults = FuzzyMatchingSearch(keywords, searchItems);

            // Sort results
            searchResults = SortSearchResults(searchResults, sorter, filter);

            return searchResults;
        }

        private static List<SearchResultItemControl> FilterSearch(int filter, string genre)
        {
            List<SearchResultItemControl> searchItems = new List<SearchResultItemControl>();

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
                    searchItems = Database.DatabaseManager.LoadSongSearchItems(searchItems, genre);
                    searchItems = Database.DatabaseManager.LoadAlbumSearchItems(searchItems, genre);
                    break;
                case 5:
                    searchItems = Database.DatabaseManager.LoadUserSearchItems(searchItems);
                    break;
            }

            return searchItems;
        }

        private static List<SearchResultItemControl> FuzzyMatchingSearch(string keywords, List<SearchResultItemControl> searchItems)
        {
            List<SearchResultItemControl> matches = new List<SearchResultItemControl>();
            foreach (SearchResultItemControl item in searchItems)
            {
                string itemTitle = item.title.Text;

                // Allow for an edit distance of 2, without case sensitivity
                if (LevenshteinDistance.IsFuzzyMatch(keywords.ToLower(), itemTitle.ToLower(), 2))
                {
                    matches.Add(item);
                }
            }

            return matches;
        }

        private static List<SearchResultItemControl> SortSearchResults(List<SearchResultItemControl> searchResults, int sorter, int filter)
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
                    if (filter == 0 || filter == 2 || filter == 5)
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
    }
}
