using System.Security.Cryptography;
using System.Text;

namespace ServerPizza
{
    public static class Cryptography
    {
        private const string Key = "ThisIsASecretKey123ThisIsASecret";

        public static string EncryptStringToBytes_Aes(string plainText)
        {
            byte[] encryptedBytes;

            // Create an instance of the AES encryption algorithm
            using (Aes aesAlg = Aes.Create())
            {
                // Set the encryption key
                aesAlg.Key = Encoding.UTF8.GetBytes(Key);
                aesAlg.GenerateIV(); // Generate a random Initialization Vector (IV)

                // Create an encryptor using the AES algorithm and the key
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create a MemoryStream to store the encrypted bytes
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    // Create a CryptoStream to perform the encryption
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

                        // Write the plain text bytes to the CryptoStream, which encrypts them
                        csEncrypt.Write(plainTextBytes, 0, plainTextBytes.Length);
                        csEncrypt.FlushFinalBlock();

                        // Get the encrypted bytes from the MemoryStream
                        encryptedBytes = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes as a Base64-encoded string
            return Convert.ToBase64String(encryptedBytes);
        }

        public static string DecryptBytesToString_Aes(string encryptedString)
        {
            string decryptedString;

            using (Aes aesAlg = Aes.Create())
            {
                // Set the encryption key
                aesAlg.Key = Encoding.UTF8.GetBytes(Key);
                aesAlg.GenerateIV(); // Generate a random Initialization Vector (IV)

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Convert the Base64-encoded string to encrypted bytes
                byte[] encryptedBytes = Convert.FromBase64String(encryptedString);

                using (MemoryStream msDecrypt = new MemoryStream(encryptedBytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes and convert them to a string
                            decryptedString = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return decryptedString;
        }
    }
}