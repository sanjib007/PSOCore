using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using PemUtils;

namespace Utility.Encryption
{
    public static class RSAManagement
    {

        
        static string ConvertByteToString(byte[] data)
        {
            var str = Convert.ToBase64String(data);
            return str;
        }



        public static string Sign(string data)
        {
            var clientCert_sbl = Get_X509Certificate_Sbl();
            var private_Key_sbl = clientCert_sbl.GetRSAPrivateKey();
            var public_Key_sbl = clientCert_sbl.GetRSAPublicKey();

            using (var privateKey = GetRSACryptoProviderTapPrivateKey())
            {
                var dataByteArray = Encoding.UTF8.GetBytes(data);

                var signatureByteArray = privateKey.SignData(
                data: dataByteArray,
               hashAlgorithm: HashAlgorithmName.SHA256,
               padding: RSASignaturePadding.Pkcs1);
                return Convert.ToBase64String(signatureByteArray);
            }
        }

        private static X509Certificate2 Get_X509Certificate_Sbl()
        {
            var directory = Path.Combine("/Keys/sbl_public_key.cer");
            X509Certificate2 cert = new X509Certificate2(directory);

            return cert;
        }

        public static string GenerateSign(string plaindata)
        {
            try
            {
                //New
                byte[] hash;
                byte[] signature;

                using (HashAlgorithm hasher = SHA256.Create())
                using (RSA rsa = GetRSACryptoProvider())
                {
                    //var privateKey 
                    ////hash = hasher.ComputeHash(Encoding.UTF8.GetBytes(data));
                    ////signature = rsa.SignHash(hash, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

                    signature = rsa.SignData(data: Encoding.UTF8.GetBytes(plaindata), hashAlgorithm: HashAlgorithmName.SHA256, padding: RSASignaturePadding.Pkcs1);
                }
                return ConvertByteToString(signature);
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public static string _GenerateSign(string plaindata)
        {
            try
            {
                //New
                byte[] hash;
                byte[] signature;

                using (HashAlgorithm hasher = SHA256.Create())
                using (RSA rsa = GetRSACryptoProvider())
                {

                    //hash = hasher.ComputeHash(Encoding.UTF8.GetBytes(data));
                    //signature = rsa.SignHash(hash, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

                    signature = rsa.SignData(data: Encoding.UTF8.GetBytes(plaindata), hashAlgorithm: HashAlgorithmName.SHA256, padding: RSASignaturePadding.Pkcs1);
                }
                return ConvertByteToString(signature);
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        private static RSA GetRSACryptoProvider()
        {
            var rsa = RSA.Create();

            try
            {
                var directory = Path.Combine("/Keys/privatekey.pem");
                using (var Key = File.OpenRead(directory))
                {
                    using (var pem = new PemReader(Key))
                    {
                        var rsaParameters = pem.ReadRsaKey();
                        rsa.ImportParameters(rsaParameters);
                    }
                }
                return rsa;
            }
            catch (Exception ex)
            {

                return null;
            }
        } 
        private static RSA GetRSACryptoProviderTapPrivateKey()
        {
            var rsa = RSA.Create();

            try
            {
                var directory = Path.Combine("/Keys/tap_private_key.txt");
                using (var Key = File.OpenRead(directory))
                {
                    using (var pem = new PemReader(Key))
                    {
                        var rsaParameters = pem.ReadRsaKey();
                        rsa.ImportParameters(rsaParameters);
                    }
                }
                return rsa;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

    }
}
