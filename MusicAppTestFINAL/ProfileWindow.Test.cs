using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using MusicApp;
using MusicApp.Profile;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace MusicAppTest
{
    [TestFixture]
    [Apartment(System.Threading.ApartmentState.STA)]
    public class ProfileWindowTests
    {
        private Mock<Profile> mockProfile;
        private ProfileWindow profileWindow;
        private Mock<IOpenFileDialogService> mockOpenFileDialogService;

        [SetUp]
        public void SetUp()
        {
            // Crea un mock de la clase Profile
            mockProfile = new Mock<Profile>(1) { CallBase = true };
            mockOpenFileDialogService = new Mock<IOpenFileDialogService>();

            // Inicializa ProfileWindow sin modificar su constructor
            profileWindow = new ProfileWindow()
            {
                DataContext = mockProfile.Object
            };

            // Reemplaza el perfil creado internamente con el perfil mockeado
            SetPrivateField(profileWindow, "userProfile", mockProfile.Object);
            // Reemplaza el servicio de diálogo de archivos con el mock
            SetPrivateField(profileWindow, "openFileDialogService", mockOpenFileDialogService.Object);

            // Configurar los controles de UI en ProfileWindow para pruebas unitarias usando reflexión
            SetPrivateField(profileWindow, "txtBiography", new TextBox());
            SetPrivateField(profileWindow, "lstSavedSongs", new ListBox());
            SetPrivateField(profileWindow, "lstPlaylists", new ListBox());
            SetPrivateField(profileWindow, "txtNewSavedSong", new TextBox());
            SetPrivateField(profileWindow, "txtNewPlaylist", new TextBox());
            SetPrivateField(profileWindow, "imgProfilePicture", new Image());
        }

        private void SetPrivateField(object obj, string fieldName, object value)
        {
            FieldInfo field = obj.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            if (field == null)
            {
                throw new ArgumentException($"Field '{fieldName}' not found in '{obj.GetType().FullName}'");
            }
            field.SetValue(obj, value);
        }

        private T GetPrivateField<T>(object obj, string fieldName)
        {
            FieldInfo field = obj.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            if (field == null)
            {
                throw new ArgumentException($"Field '{fieldName}' not found in '{obj.GetType().FullName}'");
            }
            return (T)field.GetValue(obj);
        }

        [Test]
        public void SaveBiography_ShouldCallSetBiographyOnProfile()
        {
            // Arrange
            string newBiography = "New biography";
            GetPrivateField<TextBox>(profileWindow, "txtBiography").Text = newBiography;

            // Act
            profileWindow.SaveBiography_Click(null, null);

            // Assert
            mockProfile.Verify(p => p.SetBiography(newBiography), Times.Once);
            MessageBox.Show("SaveBiography test passed successfully.");
            Console.WriteLine($"Result: {newBiography}");
        }

        [Test]
        public void AddSavedSong_ShouldCallAddSavedSongOnProfile()
        {
            // Arrange
            string newSong = "New Song";
            GetPrivateField<TextBox>(profileWindow, "txtNewSavedSong").Text = newSong;

            // Act
            profileWindow.AddSavedSong_Click(null, null);

            // Assert
            mockProfile.Verify(p => p.AddSavedSong(newSong), Times.Once);
            MessageBox.Show("AddSavedSong test passed successfully.");
            Console.WriteLine($"Result: {newSong}");
        }

        [Test]
        public void RemoveSavedSong_ShouldCallRemoveSavedSongOnProfile()
        {
            // Arrange
            string songToRemove = "Song to Remove";
            mockProfile.Setup(p => p.GetSavedSongs()).Returns(new List<string> { songToRemove });
            var lstSavedSongs = GetPrivateField<ListBox>(profileWindow, "lstSavedSongs");
            lstSavedSongs.ItemsSource = mockProfile.Object.GetSavedSongs();
            lstSavedSongs.SelectedItem = songToRemove;

            // Act
            profileWindow.RemoveSavedSong_Click(null, null);

            // Assert
            mockProfile.Verify(p => p.RemoveSavedSong(songToRemove), Times.Once);
            MessageBox.Show("RemoveSavedSong test passed successfully.");
            Console.WriteLine($"Result: {songToRemove}");
        }

        [Test]
        public void AddPlaylist_ShouldCallAddPlaylistOnProfile()
        {
            // Arrange
            string newPlaylist = "New Playlist";
            GetPrivateField<TextBox>(profileWindow, "txtNewPlaylist").Text = newPlaylist;

            // Act
            profileWindow.AddPlaylist_Click(null, null);

            // Assert
            mockProfile.Verify(p => p.AddPlaylist(newPlaylist), Times.Once);
            MessageBox.Show("AddPlaylist test passed successfully.");
            Console.WriteLine($"Result: {newPlaylist}");
        }

        [Test]
        public void RemovePlaylist_ShouldCallRemovePlaylistOnProfile()
        {
            // Arrange
            string playlistToRemove = "Playlist to Remove";
            mockProfile.Setup(p => p.GetPlaylists()).Returns(new List<string> { playlistToRemove });
            var lstPlaylists = GetPrivateField<ListBox>(profileWindow, "lstPlaylists");
            lstPlaylists.ItemsSource = mockProfile.Object.GetPlaylists();
            lstPlaylists.SelectedItem = playlistToRemove;

            // Act
            profileWindow.RemovePlaylist_Click(null, null);

            // Assert
            mockProfile.Verify(p => p.RemovePlaylist(playlistToRemove), Times.Once);
            MessageBox.Show("RemovePlaylist test passed successfully.");
            Console.WriteLine($"Result: {playlistToRemove}");
        }
    }
}
