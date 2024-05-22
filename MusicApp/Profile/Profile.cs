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

        public virtual string GetBiography()
        {
            return biography;
        }

        public virtual void SetBiography(string bio)
        {
            biography = bio;
        }

        public virtual List<string> GetSavedSongs()
        {
            return savedSongs;
        }

        public virtual void AddSavedSong(string song)
        {
            savedSongs.Add(song);
        }

        public virtual void RemoveSavedSong(string song)
        {
            savedSongs.Remove(song);
        }

        public virtual List<string> GetPlaylists()
        {
            return playlists;
        }

        public virtual void AddPlaylist(string playlist)
        {
            playlists.Add(playlist);
        }

        public virtual void RemovePlaylist(string playlist)
        {
            playlists.Remove(playlist);
        }
    }
}
