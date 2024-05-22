using Moq;
using NUnit.Framework;
using MusicApp.Authentication;
using MusicApp.Database;
using System.Net;
using System.Windows;

namespace MusicAppTest
{
    [TestFixture]
    public class AuthenticationModuleTests
    {
        private Mock<IDatabaseManager> mockDatabaseManager;
        private Mock<AuthenticationModule> mockAuthModule; // Mock de AuthenticationModule

        [SetUp]
        public void SetUp()
        {
            mockDatabaseManager = new Mock<IDatabaseManager>();
            // Crea un mock de AuthenticationModule, permitiendo llamadas base
            mockAuthModule = new Mock<AuthenticationModule>(mockDatabaseManager.Object) { CallBase = true };

            // Configura los métodos para devolver valores fijos
            mockAuthModule.Setup(m => m.GenerateSalt()).Returns("testSalt");
            mockAuthModule.Setup(m => m.HashPassword(It.IsAny<string>(), "testSalt")).Returns("hashedPassword");
        }

        [Test]
        public void RegisterUser_ShouldCallRegisterUserOnDatabaseManager()
        {
            // Arrange
            string username = "testuser";
            string password = "testpassword";

            mockDatabaseManager.Setup(db => db.RegisterUser(username, "hashedPassword", "testSalt"))
                               .Returns(true);

            // Act
            bool result = mockAuthModule.Object.RegisterUser(username, password);

            // Assert
            mockDatabaseManager.Verify(db => db.RegisterUser(username, "hashedPassword", "testSalt"), Times.Once);
            Assert.IsTrue(result);
            Console.WriteLine($"Result: {result}");
            MessageBox.Show("User registered correctly.");
        }

        [Test]
        public void AuthenticateUser_ShouldReturnTrueWhenCredentialsAreValid()
        {
            // Arrange
            string username = "testuser";
            string password = "testpassword";

            mockDatabaseManager.Setup(db => db.GetCredentials(username))
                               .Returns(("hashedPassword", "testSalt")); // Asegúrate de que coincida con los valores mockeados

            // Act
            bool result = mockAuthModule.Object.AuthenticateUser(username, password);

            // Assert
            Assert.IsTrue(result);
            Console.WriteLine($"Result: {result}");
            MessageBox.Show("User authenticated successfully.");
        }

        [Test]
        public void AuthenticateUser_ShouldReturnFalseWhenCredentialsAreInvalid()
        {
            // Arrange
            string username = "testuser";
            string password = "testpassword";

            mockDatabaseManager.Setup(db => db.GetCredentials(username))
                               .Returns(("wrongHashedPassword", "testSalt"));

            // Act
            bool result = mockAuthModule.Object.AuthenticateUser(username, password);

            // Assert
            Assert.IsFalse(result);
            Console.WriteLine($"Result: {result}");
            MessageBox.Show("Authentication failed. Invalid credentials.");
        }
    }
}
