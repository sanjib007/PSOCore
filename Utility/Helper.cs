using System.Drawing;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Utility.Helper
{



    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
    public class Helper
    {
        static byte[] tdesKey = Encoding.ASCII.GetBytes("JDUEOF#UDNCMKCK#MDJEHGWH");
        static byte[] tdesIV = Encoding.ASCII.GetBytes("12345678");

        public static JsonSerializerSettings GetJsonSettings()
        {
            var _jsonSettings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto,
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                ObjectCreationHandling = ObjectCreationHandling.Auto
            };
            return _jsonSettings;
        }
        // This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
        // 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.
        private const string initVector = "pemgail9uzpgzl88";
        // This constant is used to determine the keysize of the encryption algorithm
        private const int keysize = 256;
        //Encrypt
        public static string EncryptString(string plainText, string passPhrase)
        {
            byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            return Convert.ToBase64String(cipherTextBytes);
        }
        //Decrypt
        public static string DecryptString(string cipherText, string passPhrase)
        {
            byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }
        public static string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_' || c==' ' ||c==',')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        public enum ImageFormat
        {

            jpeg,
            png,
            PDF,
            unknown
        }

        public enum UserStatus
        {
            New,
            Approved,
            Banned,
            Dormant,
            Unapproved
        }

        public static ImageFormat GetImageFormat(byte[] bytes)
        {
            var bmp = Encoding.ASCII.GetBytes("BM");     // BMP
            var gif = Encoding.ASCII.GetBytes("GIF");    // GIF
            //var pdf = Encoding.ASCII.GetBytes("PDF");    // PDF
            var png = new byte[] { 137, 80, 78, 71 };              // PNG
            var tiff = new byte[] { 73, 73, 42 };                  // TIFF
            var tiff2 = new byte[] { 77, 77, 42 };                 // TIFF
            var jpeg = new byte[] { 255, 216, 255, 224 };          // jpeg
            var jpeg2 = new byte[] { 255, 216, 255, 225 };         // jpeg canon
            var pdf = new byte[] { 37, 80, 68, 70 };


            if (png.SequenceEqual(bytes.Take(png.Length)))
                return ImageFormat.png;

            if (jpeg.SequenceEqual(bytes.Take(jpeg.Length)))
                return ImageFormat.jpeg;

            if (jpeg2.SequenceEqual(bytes.Take(jpeg2.Length)))
                return ImageFormat.jpeg;

            if (pdf.SequenceEqual(bytes.Take(pdf.Length)))
                return ImageFormat.PDF;

            return ImageFormat.unknown;
        }


        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };

        public static void QCashSetToken(string TdesKey, string TdesIV)
        {
            tdesKey = Encoding.ASCII.GetBytes(TdesKey);
            tdesIV = Encoding.ASCII.GetBytes(TdesIV);
        }
        public static string QCashEncrypt(string textToEncrypt, string TdesKey, string TdesIV)
        {
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = Encoding.ASCII.GetBytes(TdesKey);
            tdes.IV = Encoding.ASCII.GetBytes(TdesIV);
            //tdes.Key = tdesKey;
            //tdes.IV = tdesIV;
            byte[] buffer = Encoding.ASCII.GetBytes(textToEncrypt);
            return Convert.ToBase64String(tdes.CreateEncryptor().TransformFinalBlock(buffer, 0, buffer.Length));
        }
        public static string QCashDecrypt(string textToDecrypt)
        {
            byte[] buffer = Convert.FromBase64String(textToDecrypt);
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            des.Key = tdesKey; des.IV = tdesIV;
            return Encoding.ASCII.GetString(des.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length));
        }

        public static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        public static bool AllSameCharacterCheck(string data)
        {
            int n = data.Length;
            for (int i = 1; i < n; i++)
                if (data[i] != data[0])
                    return false;

            return true;
        }
        public static bool CharacterSequenceCheck(char[] data)
        {
            int l = data.Length;
            Array.Sort(data);
            for (int i = 1; i < l; i++)
            {
                if (data[i] - data[i - 1] != 1)
                    return false;
            }

            return true;
        }

        public static string LoadImage(string imageByteData)
        {
            //data:image/gif;base64,
            //this image is a single pixel (black)
            byte[] bytes = Convert.FromBase64String(imageByteData);

            string imageName = null;
            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);

            }

            return imageName;
        }
    }
}
 