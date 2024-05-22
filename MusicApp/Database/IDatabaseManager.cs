using System.Collections.Generic;
using MusicApp.Search;

namespace MusicApp.Database
{
    public interface IDatabaseManager
    {
        bool RegisterUser(string username, string password, string salt);
        (string, string) GetCredentials(string username);
        List<SearchResultItemControl> LoadUserSearchItems(List<SearchResultItemControl> searchItems);
        List<SearchResultItemControl> LoadArtistSearchItems(List<SearchResultItemControl> searchItems);
        List<SearchResultItemControl> LoadSongSearchItems(List<SearchResultItemControl> searchItems, string genreFilter = "");
        List<SearchResultItemControl> LoadAlbumSearchItems(List<SearchResultItemControl> searchItems, string genreFilter = "");
    }
}
