using System.Collections.Generic;
using MusicApp;

namespace MusicApp.Profile
{
    public class Profile
    {
        private int id;
        private string biography;
        private List<string> savedSongs;
        private List<string> playlists;

        public Profile(int id)
        {
            this.id = id;
            this.biography = string.Empty;
            this.savedSongs = new List<string>();
            this.playlists = new List<string>();
        }

        public string GetBiography()
        {
            return biography;
        }

        public void SetBiography(string bio)
        {
            biography = bio;
        }

        public List<string> GetSavedSongs()
        {
            return savedSongs;
        }

        public void AddSavedSong(string song)
        {
            savedSongs.Add(song);
        }

        public void RemoveSavedSong(string song)
        {
            savedSongs.Remove(song);
        }

        public List<string> GetPlaylists()
        {
            return playlists;
        }

        public void AddPlaylist(string playlist)
        {
            playlists.Add(playlist);
        }

        public void RemovePlaylist(string playlist)
        {
            playlists.Remove(playlist);
        }
    }
}
