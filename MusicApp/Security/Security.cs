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
        private readonly byte[] key = Encoding.UTF8.GetBytes("HolaEstaEsClave1");
        private readonly byte[] iv = Encoding.UTF8.GetBytes("Vector_init1910*");

        public string EncryptData(string plainText)
        {
            using Aes encryiption = Aes.Create();
            encryiption.Key = key;
            encryiption.IV = iv;

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
        public string DecryptData(string cipherText)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);

            using Aes aesAlgorithm = Aes.Create();
            aesAlgorithm.Key = key;
            aesAlgorithm.IV = iv;

            ICryptoTransform decryptor = aesAlgorithm.CreateDecryptor(aesAlgorithm.Key, aesAlgorithm.IV);

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
