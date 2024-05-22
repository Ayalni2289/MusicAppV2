using Moq;
using NUnit.Framework;
using MusicApp.Database;
using System;
using System.Data;

namespace MusicApp.Database.Tests
{
    [TestFixture]
    public class DatabaseManagerTests
    {
        private Mock<IDbConnection> mockConnection;
        private Mock<IDbCommand> mockCommand;
        private Mock<IDataReader> mockDataReader;

        private DatabaseManager databaseManager;

        [SetUp]
        public void SetUp()
        {
            mockConnection = new Mock<IDbConnection>();
            mockCommand = new Mock<IDbCommand>();
            mockDataReader = new Mock<IDataReader>();

            mockConnection.Setup(conn => conn.CreateCommand()).Returns(mockCommand.Object);

            // Asegúrate de que el constructor de DatabaseManager es accesible
            databaseManager = DatabaseManager.GetInstance();
            DatabaseManager.SetInstance(databaseManager);
        }

        [Test]
        public void RegisterUser_ShouldReturnTrue_WhenUserIsRegisteredSuccessfully()
        {
            // Arrange
            string username = "testuser";
            string password = "testpassword";
            string salt = "testsalt";

            mockCommand.Setup(cmd => cmd.ExecuteNonQuery()).Returns(1);

            // Act
            bool result = databaseManager.RegisterUser(username, password, salt);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void RegisterUser_ShouldReturnFalse_WhenExceptionIsThrown()
        {
            // Arrange
            string username = "testuser";
            string password = "testpassword";
            string salt = "testsalt";

            mockCommand.Setup(cmd => cmd.ExecuteNonQuery()).Throws<Exception>();

            // Act
            bool result = databaseManager.RegisterUser(username, password, salt);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void GetCredentials_ShouldReturnCredentials_WhenUserExists()
        {
            // Arrange
            string username = "testuser";
            string expectedHashedPassword = "hashedPassword";
            string expectedSalt = "testsalt";

            mockDataReader.Setup(reader => reader.Read()).Returns(true);
            mockDataReader.Setup(reader => reader.GetString(0)).Returns(expectedHashedPassword);
            mockDataReader.Setup(reader => reader.GetString(1)).Returns(expectedSalt);
            mockCommand.Setup(cmd => cmd.ExecuteReader()).Returns(mockDataReader.Object);

            // Act
            var (hashedPassword, salt) = databaseManager.GetCredentials(username);

            // Assert
            Assert.AreEqual(expectedHashedPassword, hashedPassword);
            Assert.AreEqual(expectedSalt, salt);
        }

        [Test]
        public void GetCredentials_ShouldReturnEmptyStrings_WhenUserDoesNotExist()
        {
            // Arrange
            string username = "testuser";

            mockDataReader.Setup(reader => reader.Read()).Returns(false);
            mockCommand.Setup(cmd => cmd.ExecuteReader()).Returns(mockDataReader.Object);

            // Act
            var (hashedPassword, salt) = databaseManager.GetCredentials(username);

            // Assert
            Assert.AreEqual(" ", hashedPassword);
            Assert.AreEqual(" ", salt);
        }
    }
}
