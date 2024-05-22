using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Media.Imaging;
using MusicApp.Search;

namespace MusicAppTest
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class SearchWindowTests
    {
        private SearchWindow _searchWindow;

        [SetUp]
        public void SetUp()
        {
            _searchWindow = new SearchWindow();
        }

        [Test]
        public void AddSearchResult_Returns_SearchResultItemControl_With_Correct_Fields()
        {
            // Arrange
            string imagePath = "images/test.jpg";
            string title = "Test Title";
            string subTitle1 = "SubTitle1";
            string subTitle2 = "SubTitle2";
            string subTitle3 = "SubTitle3";

            // Act
            var result = _searchWindow.AddSearchResult(imagePath, title, subTitle1, subTitle2, subTitle3);

            // Assert
            Assert.IsNotNull(result);
            MessageBox.Show("AddSearchResult test passed. The SearchResultItemControl was created successfully.");

            // Convert the actual URI to a relative path
            var actualUri = ((BitmapImage)result.ImageElement.Source).UriSource;
            string actualRelativePath = actualUri.ToString().Replace($"pack://application:,,,/{Assembly.GetExecutingAssembly().GetName().Name};component/", "");

            Assert.AreEqual(imagePath, actualRelativePath);
            Assert.AreEqual(title, result.TitleElement.Text);
            Assert.AreEqual(subTitle1, result.SubTitle1Element.Text);
            Assert.AreEqual(subTitle2, result.SubTitle2Element.Text);
            Assert.AreEqual(subTitle3, result.SubTitle3Element.Text);
            MessageBox.Show("The fields of the SearchResultItemControl are correct.");
            Console.WriteLine($"Result: {result}");
        }

        [Test]
        public void FuzzyMatchingSearch_Returns_Matches_For_Valid_Keywords()
        {
            // Arrange
            var searchItems = new List<SearchResultItemControl>
            {
                new SearchResultItemControl { TitleElement = { Text = "Apple" } },
                new SearchResultItemControl { TitleElement = { Text = "Banana" } },
                new SearchResultItemControl { TitleElement = { Text = "Orange" } }
            };
            string keywords = "apple";

            // Act
            var matches = _searchWindow.FuzzyMatchingSearch(keywords, searchItems);

            // Assert
            Assert.AreEqual(1, matches.Count);
            Assert.AreEqual("Apple", matches.First().TitleElement.Text);
            MessageBox.Show("FuzzyMatchingSearch test passed. The search for 'apple' returned the expected matches.");
            Console.WriteLine($"Result: {matches}");
        }

        // Puedes seguir añadiendo más métodos de prueba para otras funcionalidades de la clase SearchWindow
    }
}
