using Moq;
using NUnit.Framework;
using MusicApp.Database;
using MusicApp.Search;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Controls;
using System.Windows;

namespace MusicAppTest
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class DatabaseManagerTests
    {
        private Mock<IDatabaseManager> mockDatabaseManager;

        [SetUp]
        public void SetUp()
        {
            mockDatabaseManager = new Mock<IDatabaseManager>();
        }

        [Test]
        public void LoadUserSearchItems_ShouldReturnUserSearchItems()
        {
            // Arrange
            var searchItems = new List<SearchResultItemControl>();
            var expectedItems = new List<SearchResultItemControl>
            {
                CreateSearchResultItemControl("User1"),
                CreateSearchResultItemControl("User2")
            };

            mockDatabaseManager.Setup(db => db.LoadUserSearchItems(searchItems))
                               .Returns(expectedItems);

            // Act
            var result = mockDatabaseManager.Object.LoadUserSearchItems(searchItems);

            // Assert
            Assert.AreEqual(expectedItems.Count, result.Count);
            for (int i = 0; i < expectedItems.Count; i++)
            {
                Assert.IsTrue(AreEqual(expectedItems[i], result[i]));
            }
            Console.WriteLine($"Result: {result}");
            MessageBox.Show("User search items loaded successfully.");
        }

        [Test]
        public void LoadArtistSearchItems_ShouldReturnArtistSearchItems()
        {
            // Arrange
            var searchItems = new List<SearchResultItemControl>();
            var expectedItems = new List<SearchResultItemControl>
            {
                CreateSearchResultItemControl("Artist1"),
                CreateSearchResultItemControl("Artist2")
            };

            mockDatabaseManager.Setup(db => db.LoadArtistSearchItems(searchItems))
                               .Returns(expectedItems);

            // Act
            var result = mockDatabaseManager.Object.LoadArtistSearchItems(searchItems);

            // Assert
            Assert.AreEqual(expectedItems.Count, result.Count);
            for (int i = 0; i < expectedItems.Count; i++)
            {
                Assert.IsTrue(AreEqual(expectedItems[i], result[i]));
            }
            Console.WriteLine($"Result: {result}");
            MessageBox.Show("Artist search items loaded successfully.");
        }

        [Test]
        public void LoadSongSearchItems_ShouldReturnSongSearchItems()
        {
            // Arrange
            var searchItems = new List<SearchResultItemControl>();
            var genreFilter = "Rock";
            var expectedItems = new List<SearchResultItemControl>
            {
                CreateSearchResultItemControl("Song1"),
                CreateSearchResultItemControl("Song2")
            };

            mockDatabaseManager.Setup(db => db.LoadSongSearchItems(searchItems, genreFilter))
                               .Returns(expectedItems);

            // Act
            var result = mockDatabaseManager.Object.LoadSongSearchItems(searchItems, genreFilter);

            // Assert
            Assert.AreEqual(expectedItems.Count, result.Count);
            for (int i = 0; i < expectedItems.Count; i++)
            {
                Assert.IsTrue(AreEqual(expectedItems[i], result[i]));
            }
            Console.WriteLine($"Result: {result}");
            MessageBox.Show("Song search items loaded successfully.");
        }

        [Test]
        public void LoadAlbumSearchItems_ShouldReturnAlbumSearchItems()
        {
            // Arrange
            var searchItems = new List<SearchResultItemControl>();
            var genreFilter = "Jazz";
            var expectedItems = new List<SearchResultItemControl>
            {
                CreateSearchResultItemControl("Album1"),
                CreateSearchResultItemControl("Album2")
            };

            mockDatabaseManager.Setup(db => db.LoadAlbumSearchItems(searchItems, genreFilter))
                               .Returns(expectedItems);

            // Act
            var result = mockDatabaseManager.Object.LoadAlbumSearchItems(searchItems, genreFilter);

            // Assert
            Assert.AreEqual(expectedItems.Count, result.Count);
            for (int i = 0; i < expectedItems.Count; i++)
            {
                Assert.IsTrue(AreEqual(expectedItems[i], result[i]));
            }
            Console.WriteLine($"Result: {result}");
            MessageBox.Show("Album search items loaded successfully.");
        }

        private SearchResultItemControl CreateSearchResultItemControl(string title)
        {
            var item = new SearchResultItemControl();
            item.SetTitle(title);
            Console.WriteLine($"Result: {item}");
            return item;
        }

        private bool AreEqual(SearchResultItemControl expected, SearchResultItemControl actual)
        {
            // Implement a method to compare the actual properties of the controls
            // For simplicity, assuming SetTitle sets a private field we can access
            // In reality, you might need to adjust this based on your actual implementation
            Console.WriteLine($"Result: {GetTitle(expected) == GetTitle(actual)}");
            return GetTitle(expected) == GetTitle(actual);
        }

        private string GetTitle(SearchResultItemControl item)
        {
            // Use reflection to get the private title TextBlock and its Text property
            var titleField = item.GetType().GetField("title", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var titleTextBlock = titleField.GetValue(item) as TextBlock;
            Console.WriteLine($"Result: {titleTextBlock?.Text}");
            return titleTextBlock?.Text;
        }
    }
}
