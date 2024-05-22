using System;
using NUnit.Framework;
using MusicApp.Search;
using System.Windows;

namespace MusicAppTest
{
    [TestFixture]
    public class LevenshteinDistanceTests
    {
        private LevenshteinDistance levenshteinDistance;

        [SetUp]
        public void SetUp()
        {
            levenshteinDistance = new LevenshteinDistance();
        }

        [Test]
        public void Compute_SameString_ShouldReturnZero()
        {
            // Arrange
            string s = "test";
            string t = "test";

            // Act
            int result = levenshteinDistance.Compute(s, t);

            // Assert
            Assert.AreEqual(0, result);
            MessageBox.Show("Levenshtein Distance: Compute_SameString test passed. Result: " + result);
            Console.WriteLine($"Result: {result}");
        }

        [Test]
        public void Compute_DifferentStrings_ShouldReturnCorrectDistance()
        {
            // Arrange
            string s = "kitten";
            string t = "sitting";

            // Act
            int result = levenshteinDistance.Compute(s, t);

            // Assert
            Assert.AreEqual(3, result);
            MessageBox.Show("Levenshtein Distance: Compute_DifferentStrings test passed. Result: " + result);
            Console.WriteLine($"Result: {result}");
        }

        [Test]
        public void Compute_EmptyAndNonEmptyString_ShouldReturnLengthOfNonEmptyString()
        {
            // Arrange
            string s = "";
            string t = "nonempty";

            // Act
            int result = levenshteinDistance.Compute(s, t);

            // Assert
            Assert.AreEqual(t.Length, result);
            MessageBox.Show("Levenshtein Distance: Compute_EmptyAndNonEmptyString test passed. Result: " + result);
            Console.WriteLine($"Result: {result}");
        }

        [Test]
        public void IsFuzzyMatch_SameString_ShouldReturnTrue()
        {
            // Arrange
            string s = "fuzzy";
            string t = "fuzzy";
            int threshold = 0;

            // Act
            bool result = levenshteinDistance.IsFuzzyMatch(s, t, threshold);

            // Assert
            Assert.IsTrue(result);
            MessageBox.Show("Levenshtein Distance: IsFuzzyMatch_SameString test passed. Result: " + result);
            Console.WriteLine($"Result: {result}");
        }

        [Test]
        public void IsFuzzyMatch_SmallDifferenceWithinThreshold_ShouldReturnTrue()
        {
            // Arrange
            string s = "kitten";
            string t = "sitting";
            int threshold = 3;

            // Act
            bool result = levenshteinDistance.IsFuzzyMatch(s, t, threshold);

            // Assert
            Assert.IsTrue(result);
            MessageBox.Show("Levenshtein Distance: IsFuzzyMatch_SmallDifferenceWithinThreshold test passed. Result: " + result);
            Console.WriteLine($"Result: {result}");
        }

        [Test]
        public void IsFuzzyMatch_SmallDifferenceOutsideThreshold_ShouldReturnFalse()
        {
            // Arrange
            string s = "kitten";
            string t = "sitting";
            int threshold = 2;

            // Act
            bool result = levenshteinDistance.IsFuzzyMatch(s, t, threshold);

            // Assert
            Assert.IsFalse(result);
            MessageBox.Show("Levenshtein Distance: IsFuzzyMatch_SmallDifferenceOutsideThreshold test passed. Result: " + result);
            Console.WriteLine($"Result: {result}");
        }

        [Test]
        public void IsFuzzyMatch_EmptyAndNonEmptyStringWithinThreshold_ShouldReturnTrue()
        {
            // Arrange
            string s = "";
            string t = "a";
            int threshold = 1;

            // Act
            bool result = levenshteinDistance.IsFuzzyMatch(s, t, threshold);

            // Assert
            Assert.IsTrue(result);
            MessageBox.Show("Levenshtein Distance: IsFuzzyMatch_EmptyAndNonEmptyStringWithinThreshold test passed. Result: " + result);
            Console.WriteLine($"Result: {result}");
        }

        [Test]
        public void IsFuzzyMatch_EmptyAndNonEmptyStringOutsideThreshold_ShouldReturnFalse()
        {
            // Arrange
            string s = "";
            string t = "a";
            int threshold = 0;

            // Act
            bool result = levenshteinDistance.IsFuzzyMatch(s, t, threshold);

            // Assert
            Assert.IsFalse(result);
            MessageBox.Show("Levenshtein Distance: IsFuzzyMatch_EmptyAndNonEmptyStringOutsideThreshold test passed. Result: " + result);
            Console.WriteLine($"Result: {result}");
        }
    }
}
