using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace client
{
    public class cryptography
    {

        public static byte[] EncryptStringToBytes_Aes(string plainText)
        {
            string key = "MySecret007Key";
            byte[] encryptedBytes;

            // Create an instance of the AES encryption algorithm
            using (Aes aesAlg = Aes.Create())
            {
                //set the encryption key
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.GenerateIV(); // Generate a random Initialization Vector (IV)? idk but its needed

                //create an encryptor using the aes algorithm and the key
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                //create a MemoryStream to store the encrypted bytes
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    //create a CryptoStream to perform the encryption
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

                        //erite the plain text bytes to the CryptoStream, which encrypts them
                        csEncrypt.Write(plainTextBytes, 0, plainTextBytes.Length);
                        csEncrypt.FlushFinalBlock();

                        // Get the encrypted bytes from the MemoryStream
                        encryptedBytes = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes
            return encryptedBytes;
        }



        public static string DecryptBytesToString_Aes(byte[] encryptedBytes)
        {
            string key = "MySecret007Key";
            string decryptedString = null;

            using (Aes aesAlg = Aes.Create())
            {
                //set the encryption key
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.GenerateIV(); // Generate a random Initialization Vector (IV)? idk but its needed

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

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
