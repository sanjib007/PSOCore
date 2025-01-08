using System.Text;
using System.Security.Cryptography;

namespace Utility.Encryption
{
    public static class EncryptionHelper
    {
        private static byte[] keyAndIvBytes;

        static EncryptionHelper()
        {
            // You'll need a more secure way of storing this, I hope this isn't
            // the real key
            keyAndIvBytes = UTF8Encoding.UTF8.GetBytes("Pk9$s@Na7i%$La5T");
        }

        public static string ByteArrayToHexString(byte[] ba)
        {
            return BitConverter.ToString(ba).Replace("-", "");
        }

        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        public static string DecodeAndDecrypt(string cipherText, string key = null)
        {
            string DecodeAndDecrypt = AesDecrypt(StringToByteArray(cipherText));
            return (DecodeAndDecrypt);
        }

        public static string EncryptAndEncode(string plaintext, string key = null)
        {
            return ByteArrayToHexString(AesEncrypt(plaintext));
        }

        public static string AesDecrypt(Byte[] inputBytes)
        {
            Byte[] outputBytes = inputBytes;

            string plaintext = string.Empty;

            using (MemoryStream memoryStream = new MemoryStream(outputBytes))
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, GetCryptoAlgorithm().CreateDecryptor(keyAndIvBytes, keyAndIvBytes), CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(cryptoStream))
                    {
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }

            return plaintext;
        }

        public static byte[] AesEncrypt(string inputText)
        {
            byte[] inputBytes = UTF8Encoding.UTF8.GetBytes(inputText);//AbHLlc5uLone0D1q

            byte[] result = null;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, GetCryptoAlgorithm().CreateEncryptor(keyAndIvBytes, keyAndIvBytes), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(inputBytes, 0, inputBytes.Length);
                    cryptoStream.FlushFinalBlock();

                    result = memoryStream.ToArray();
                }
            }

            return result;
        }


        private static RijndaelManaged GetCryptoAlgorithm()
        {
            RijndaelManaged algorithm = new RijndaelManaged();
            //set the mode, padding and block size
            algorithm.Padding = PaddingMode.PKCS7;
            algorithm.Mode = CipherMode.CBC;
            algorithm.KeySize = 128;
            algorithm.BlockSize = 128;
            return algorithm;
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }



    }

}
