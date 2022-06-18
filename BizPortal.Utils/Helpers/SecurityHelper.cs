using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Utils.Helpers
{
    public class SecurityHelper
    {
        public static string MD5EncryptToHex(string text)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] hashedBytes;
            UTF8Encoding encoder = new UTF8Encoding();
            hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(text));
            return BitConverter.ToString(hashedBytes).Replace("-", "");
        }

        public static int RandomNumberGenerator(byte bufferLength)
        {
            int value = 0;
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                // Buffer storage.
                byte[] data = new byte[bufferLength];

                // Fill buffer.
                rng.GetBytes(data);

                // Convert to int 32.
                value = BitConverter.ToInt32(data, 0);
            }
            return value;
        }

        public static string md5encrypt(string phrase)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            MD5CryptoServiceProvider md5hasher = new MD5CryptoServiceProvider();
            byte[] hashedDataBytes = md5hasher.ComputeHash(encoder.GetBytes(phrase));
            return byteArrayToString(hashedDataBytes);
        }

        public static string sha1encrypt(string phrase)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            SHA1CryptoServiceProvider sha1hasher = new SHA1CryptoServiceProvider();
            byte[] hashedDataBytes = sha1hasher.ComputeHash(encoder.GetBytes(phrase));
            return byteArrayToString(hashedDataBytes);
        }

        public static string sha256encrypt(string phrase)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            SHA256Managed sha256hasher = new SHA256Managed();
            byte[] hashedDataBytes = sha256hasher.ComputeHash(encoder.GetBytes(phrase));
            return byteArrayToString(hashedDataBytes);
        }

        public static string sha384encrypt(string phrase)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            SHA384Managed sha384hasher = new SHA384Managed();
            byte[] hashedDataBytes = sha384hasher.ComputeHash(encoder.GetBytes(phrase));
            return byteArrayToString(hashedDataBytes);
        }

        public static string sha512encrypt(string phrase)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            SHA512Managed sha512hasher = new SHA512Managed();
            byte[] hashedDataBytes = sha512hasher.ComputeHash(encoder.GetBytes(phrase));
            return byteArrayToString(hashedDataBytes);
        }

        public static string byteArrayToString(byte[] inputArray)
        {
            StringBuilder output = new StringBuilder("");
            for (int i = 0; i < inputArray.Length; i++)
            {
                output.Append(inputArray[i].ToString("X2"));
            }
            return output.ToString();
        }

        /// <summary>
        /// AES 256 Encryption compatible wite PHP AES 256 Decryption
        /// PHP AES 256 source code: http://blog.djekldevelopments.co.uk/?p=334
        /// </summary>
        /// <param name="Input">String Input</param>
        /// <param name="aes_Key">Key (32 charactors)</param>
        /// <param name="aes_IV">Initial Vector (32 charactors)</param>
        /// <returns></returns>
        public static string AES256_encrypt(string Input, string aes_Key, string aes_IV)
        {
            //if ((aes_Key.Length != 32) || (aes_IV.Length != 32))
            //throw new Exception("Key/InitialVector length must be 32 charactors(256 bit).");

            var aes = new RijndaelManaged();
            aes.Mode = CipherMode.CBC;
            aes.KeySize = 256;
            aes.BlockSize = 256;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = Convert.FromBase64String(aes_Key);
            aes.IV = Convert.FromBase64String(aes_IV);

            var encrypt = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] xBuff = null;
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, encrypt, CryptoStreamMode.Write))
                {
                    byte[] xXml = Encoding.UTF8.GetBytes(Input);
                    cs.Write(xXml, 0, xXml.Length);
                }

                xBuff = ms.ToArray();
            }

            String Output = Convert.ToBase64String(xBuff);
            return Output;
        }

        /// <summary>
        /// AES 256 Decryption compatible wite PHP AES 256 Encryption
        /// PHP AES 256 source code: http://blog.djekldevelopments.co.uk/?p=334
        /// </summary>
        /// <param name="Input">String Input</param>
        /// <param name="aes_Key">Key (32 charactors)</param>
        /// <param name="aes_IV">Initial Vector (32 charactors)</param>
        /// <returns></returns>
        public static string AES256_decrypt(string Input, string aes_Key, string aes_IV)
        {
            //if ((aes_Key.Length != 32) || (aes_IV.Length != 32))
            //    throw new Exception("Key/InitialVector length must be 32 charactors(256 bit).");

            RijndaelManaged aes = new RijndaelManaged();
            aes.KeySize = 256;
            aes.BlockSize = 256;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = Convert.FromBase64String(aes_Key);
            aes.IV = Convert.FromBase64String(aes_IV);

            var decrypt = aes.CreateDecryptor();
            byte[] xBuff = null;
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, decrypt, CryptoStreamMode.Write))
                {
                    byte[] xXml = Convert.FromBase64String(Input);
                    cs.Write(xXml, 0, xXml.Length);
                }

                xBuff = ms.ToArray();
            }

            String Output = Encoding.UTF8.GetString(xBuff);
            return Output;
        }

        public static string GenerateToken(Guid uid, string username, long tick)
        {
            string token = uid.ToString() + tick + username.ToLower();
            for (int i = 0; i < 50; i++)
            {
                token = SecurityHelper.md5encrypt(token);
            }
            return token;
        }


       
    }
}
