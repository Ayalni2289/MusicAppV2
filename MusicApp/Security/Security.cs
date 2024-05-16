using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Security
{
    public class Security
    {
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("HolaEstaEsClave1");
        private static readonly byte[] IV = Encoding.UTF8.GetBytes("Vector_init1910*");

        public static string EncryptData(string plainText)
        {
            using Aes encryiption = Aes.Create();
            encryiption.Key = Key;
            encryiption.IV = IV;

            ICryptoTransform encryptor = encryiption.CreateEncryptor(encryiption.Key, encryiption.IV);

            byte[] encryptedBytes;
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                        swEncrypt.Flush(); // Flush the StreamWriter to ensure data is written to the underlying stream
                    }
                }
                encryptedBytes = msEncrypt.ToArray();
            }

            return Convert.ToBase64String(encryptedBytes);
        }
        public static string DecryptData(string cipherText)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);

            using Aes aesAlg = Aes.Create();
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            byte[] decryptedBytes;
            using (MemoryStream msDecrypt = new MemoryStream(cipherBytes))
            {
                using CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                using StreamReader srDecrypt = new StreamReader(csDecrypt);
                decryptedBytes = Encoding.UTF8.GetBytes(srDecrypt.ReadToEnd());
            }

            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }
}
