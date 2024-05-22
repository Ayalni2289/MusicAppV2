using Moq;
using NUnit.Framework;
using MusicApp.Authentication;
using MusicApp.Database;
using System.Net;

namespace MusicAppTest
{
    [TestFixture]
    public class AuthenticationModuleTests
    {
        private Mock<DatabaseManager> mockDatabaseManager;
        private AuthenticationModule authenticationModule;

        [SetUp]
        public void SetUp()
        {
            mockDatabaseManager = new Mock<DatabaseManager>();
            authenticationModule = new AuthenticationModule();

            // Utiliza el método GetInstance para devolver el mock
            DatabaseManager.SetInstance(mockDatabaseManager.Object); // Cambia a un método que permita inyectar el mock
        }

        [Test]
        public void RegisterUser_ShouldCallRegisterUserOnDatabaseManager()
        {
            // Arrange
            string username = "testuser";
            string password = "testpassword";
            string salt = "testSalt";
            string hashedPassword = authenticationModule.HashPassword(password, salt);

            mockDatabaseManager.Setup(db => db.RegisterUser(username, hashedPassword, salt))
                               .Returns(true);

            // Act
            bool result = authenticationModule.RegisterUser(username, password);

            // Assert
            mockDatabaseManager.Verify(db => db.RegisterUser(username, hashedPassword, salt), Times.Once);
            Assert.IsTrue(result);
        }

        [Test]
        public void AuthenticateUser_ShouldReturnTrueWhenCredentialsAreValid()
        {
            // Arrange
            string username = "testuser";
            string password = "testpassword";
            string salt = "testSalt";
            string hashedPassword = authenticationModule.HashPassword(password, salt);

            mockDatabaseManager.Setup(db => db.GetCredentials(username))
                               .Returns((hashedPassword, salt));

            // Act
            bool result = authenticationModule.AuthenticateUser(username, password);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void AuthenticateUser_ShouldReturnFalseWhenCredentialsAreInvalid()
        {
            // Arrange
            string username = "testuser";
            string password = "testpassword";
            string salt = "testSalt";
            string wrongHashedPassword = "wrongHashedPassword";

            mockDatabaseManager.Setup(db => db.GetCredentials(username))
                               .Returns((wrongHashedPassword, salt));

            // Act
            bool result = authenticationModule.AuthenticateUser(username, password);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
