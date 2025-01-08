using System.Security.Cryptography;
using System.Text;

namespace Utility.Encryption
{

    public static class EncryptionNagad
    {
        #region Encryptiom

        public static string EncryptData(string plainData, string publicKey)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportFromPem(publicKey);
                byte[] plainBytes = Encoding.UTF8.GetBytes(plainData);
                byte[] encryptedBytes = rsa.Encrypt(plainBytes, RSAEncryptionPadding.Pkcs1);
                return Convert.ToBase64String(encryptedBytes);
            }
        }
        // Method to decrypt data using private key and padding algorithm
        public static string DecryptData(string encryptedData, string privateKey)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportFromPem(privateKey);
                byte[] encryptedBytes = Convert.FromBase64String(encryptedData);
                byte[] decryptedBytes = rsa.Decrypt(encryptedBytes, RSAEncryptionPadding.Pkcs1);
                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }

        // Method to verify signature using public key and signature algorithm
        public static bool VerifySignature(string data, string signature, string publicKey)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportFromPem(publicKey);
                byte[] dataBytes = Encoding.UTF8.GetBytes(data);
                byte[] signatureBytes = Convert.FromBase64String(signature);
                return rsa.VerifyData(dataBytes, signatureBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            }
        }
        public static string SignData(string plainData, string privateKey)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportFromPem(privateKey);
                RSAParameters privateKeyParams = rsa.ExportParameters(true);
                RSAParameters publivKeyParams = rsa.ExportParameters(false);
                byte[] plainBytes = Encoding.UTF8.GetBytes(plainData);
                byte[] signatureBytes = rsa.SignData(data: plainBytes, hashAlgorithm: HashAlgorithmName.SHA256, padding: RSASignaturePadding.Pkcs1);
                return Convert.ToBase64String(signatureBytes);


                //// Get the private key for signing
                //var privateKey = new RSAPrivateKey(rsa.ExportParameters(true));

            }
        }


        public static string GenerateRandomHexString(int length)
        {
            Random random = new Random();
            StringBuilder stringBuilder = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                int randomNumber = random.Next(0, 16); // Generates a random number between 0 and 15
                stringBuilder.Append(randomNumber.ToString("X")); // Convert the number to hexadecimal and append to the string
            }

            return stringBuilder.ToString();
        }

        #endregion
    }

}
