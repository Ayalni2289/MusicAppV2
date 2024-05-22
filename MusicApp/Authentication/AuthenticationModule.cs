using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using MusicApp.Database;

namespace MusicApp.Authentication
{
    public class AuthenticationModule
    {
        private readonly IDatabaseManager databaseManager_;

        // Constructor que acepta la interfaz, facilitando la inyección de dependencias
        public AuthenticationModule(IDatabaseManager databaseManager)
        {
            databaseManager_ = databaseManager;
        }
        public bool RegisterUser(string username, string password)
        {
            string salt = GenerateSalt();
            string hashedPassword = HashPassword(password, salt);
            return databaseManager_.RegisterUser(username, hashedPassword, salt);
        }

        public bool AuthenticateUser(string username, string password)
        {
            (string hashedPassword, string salt) = databaseManager_.GetCredentials(username);
            return hashedPassword == HashPassword(password, salt);
        }

        public virtual string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            RandomNumberGenerator.Fill(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }

        public virtual string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] saltBytes = Convert.FromBase64String(salt);
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] saltedPassword = new byte[saltBytes.Length + passwordBytes.Length];

                Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
                Buffer.BlockCopy(saltBytes, 0, saltedPassword, passwordBytes.Length, saltBytes.Length);

                byte[] hashedBytes = sha256.ComputeHash(saltedPassword);
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
