using System;
using NUnit.Framework;
using MusicApp.Security;

namespace MusicAppTest
{
    [TestFixture]
    public class SecurityTests
    {
        private Security security;

        [SetUp]
        public void SetUp()
        {
            security = new Security();
        }

        [Test]
        public void EncryptData_ShouldReturnEncryptedString()
        {
            // Arrange
            string plainText = "Hello, this is a test.";

            // Act
            string encryptedText = security.EncryptData(plainText);

            // Assert
            Assert.IsNotNull(encryptedText);
            Assert.IsNotEmpty(encryptedText);
            Assert.AreNotEqual(plainText, encryptedText);
            Console.WriteLine($"Encrypted text: {encryptedText}");
        }

        [Test]
        public void DecryptData_ShouldReturnDecryptedString()
        {
            // Arrange
            string plainText = "Hello, this is a test.";
            string encryptedText = security.EncryptData(plainText);

            // Act
            string decryptedText = security.DecryptData(encryptedText);

            // Assert
            Assert.IsNotNull(decryptedText);
            Assert.IsNotEmpty(decryptedText);
            Assert.AreEqual(plainText, decryptedText);
            Console.WriteLine($"Decrypted text: {decryptedText}");
        }

        [Test]
        public void EncryptAndDecryptData_ShouldReturnOriginalString()
        {
            // Arrange
            string plainText = "Hello, this is a test.";

            // Act
            string encryptedText = security.EncryptData(plainText);
            string decryptedText = security.DecryptData(encryptedText);

            // Assert
            Assert.AreEqual(plainText, decryptedText);
            Console.WriteLine("EncryptAndDecryptData test passed successfully.");
        }
    }
}
