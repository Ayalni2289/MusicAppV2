using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using MusicApp;

namespace MusicApp.Profile
{
    public partial class ProfileWindow : Window
    {
        private Profile userProfile;
        private IOpenFileDialogService openFileDialogService; // Define el campo aquí

        public ProfileWindow()
        {
            InitializeComponent();
            this.openFileDialogService = new OpenFileDialogService(); // Inicializa el campo aquí

            // Create a new profile with an ID of 1
            userProfile = new Profile(1);
            // Set the biography textbox text
            txtBiography.Text = userProfile.GetBiography();
            // Set the saved songs listbox items source
            lstSavedSongs.ItemsSource = userProfile.GetSavedSongs();
            // Set the playlists listbox items source
            lstPlaylists.ItemsSource = userProfile.GetPlaylists();
        }

        public void SaveBiography_Click(object sender, RoutedEventArgs e)
        {
            // Save the biography entered in the textbox
            userProfile.SetBiography(txtBiography.Text);
            MessageBox.Show("Biography saved!");
        }

        public void AddSavedSong_Click(object sender, RoutedEventArgs e)
        {
            // Add the new saved song entered in the textbox
            userProfile.AddSavedSong(txtNewSavedSong.Text);
            // Refresh the saved songs listbox
            lstSavedSongs.Items.Refresh();
            // Clear the new saved song textbox
            txtNewSavedSong.Clear();
        }

        public void RemoveSavedSong_Click(object sender, RoutedEventArgs e)
        {
            // Remove the selected saved song from the listbox
            string selectedSong = lstSavedSongs.SelectedItem as string;
            if (!string.IsNullOrEmpty(selectedSong))
            {
                userProfile.RemoveSavedSong(selectedSong);
                lstSavedSongs.Items.Refresh();
            }
        }

        public void AddPlaylist_Click(object sender, RoutedEventArgs e)
        {
            // Add the new playlist in the textbox
            userProfile.AddPlaylist(txtNewPlaylist.Text);
            // Refresh the playlists listbox
            lstPlaylists.Items.Refresh();
            // Clear the new playlist textbox
            txtNewPlaylist.Clear();
        }

        public void RemovePlaylist_Click(object sender, RoutedEventArgs e)
        {
            // Remove playlist from the listbox
            string selectedPlaylist = lstPlaylists.SelectedItem as string;
            if (!string.IsNullOrEmpty(selectedPlaylist))
            {
                userProfile.RemovePlaylist(selectedPlaylist);
                lstPlaylists.Items.Refresh();
            }
        }

        public void ChangeProfilePicture_Click(object sender, RoutedEventArgs e)
        {
            // changing profile picture
            if (openFileDialogService.ShowDialog())
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(openFileDialogService.FileName);
                bitmap.EndInit();
                imgProfilePicture.Source = bitmap;
            }
        }
    }
}
