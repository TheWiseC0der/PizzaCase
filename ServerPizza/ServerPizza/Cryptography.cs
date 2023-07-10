using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ServerPizza
{
    internal class Cryptography
    {
        private const string Key = "ThisIsASecretKey123ThisIsASecret";

        public static string EncryptStringToBytes_Aes(string plainText)
        {
            byte[] encrypted;
            // Create a new AesManaged.
            using (AesManaged aes = new AesManaged())
            {
                // Create encryptor
                aes.Key = Encoding.UTF8.GetBytes(Key);
                aes.IV = new byte[aes.BlockSize / 8]; // Use default IV
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                // Create MemoryStream
                using (MemoryStream ms = new MemoryStream())
                {
                    // Create crypto stream using the CryptoStream class. This class is the key to encryption
                    // and encrypts and decrypts data from any given stream. In this case, we will pass a memory stream
                    // to encrypt
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        // Create StreamWriter and write data to a stream
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(plainText);
                        encrypted = ms.ToArray();
                    }
                }
            }
            // Return encrypted data
            return Convert.ToBase64String(encrypted);
        }

        public static string DecryptBytesToString_Aes(string cipherText)
        {
            string plaintext = null;
            // Create AesManaged
            using (AesManaged aes = new AesManaged())
            {
                // Create a decryptor
                aes.Key = Encoding.UTF8.GetBytes(Key);
                aes.IV = new byte[aes.BlockSize / 8]; // Use default IV
                //ICryptoTransform decryptor = aes.CreateDecryptor(Encoding.UTF8.GetBytes(Key), IV);
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                // Create the streams used for decryption.
                byte[] encryptedBytes = Convert.FromBase64String(cipherText);
                using (MemoryStream ms = new MemoryStream(encryptedBytes))
                {
                    // Create crypto stream
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        // Read crypto stream
                        using (StreamReader reader = new StreamReader(cs))
                            plaintext = reader.ReadToEnd();
                    }
                }
            }
            return plaintext;
        }
    }
}
