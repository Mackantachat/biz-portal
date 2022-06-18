using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Utils
{
    public class FDAUtility
    {
        public static string FDA_DECRYPT_DATA(string encryptedString)
        {
            
            var store = new X509Store(StoreLocation.LocalMachine); // (StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);

            var encPasswordBase64 = Convert.FromBase64String(encryptedString);

            var enveloped = new EnvelopedCms();
            enveloped.Decode(encPasswordBase64);
            enveloped.Decrypt(store.Certificates);

            return Encoding.UTF8.GetString(enveloped.ContentInfo.Content);
        }
        public static string FDA_ENCRYPT_DATA(string message, string PATH_CER)
        {
            X509Certificate2 cer = new X509Certificate2();
            try
            {
                cer = new X509Certificate2(PATH_CER, "");
            }
            catch
            {
                throw new CryptographicException("Unable to open key file.");
            }
            UTF8Encoding encoder = new UTF8Encoding();
            ContentInfo contentInfo = new ContentInfo(encoder.GetBytes(message));
            EnvelopedCms envelop = new EnvelopedCms(contentInfo);
            CmsRecipient recip = new CmsRecipient(cer);
            envelop.Encrypt(recip);
            byte[] encoded = envelop.Encode();
            return Convert.ToBase64String(encoded);
        }




        /* ////X509 Encrypt Decrypt
        ///
        private int GetIntegerSize(BinaryReader binr)
        {
            byte bt = 0;
            byte lowbyte = 0x00;
            byte highbyte = 0x00;
            int count = 0;
            bt = binr.ReadByte();
            if (bt != 0x02)
                return 0;
            bt = binr.ReadByte();

            if (bt == 0x81)
                count = binr.ReadByte();
            else
                if (bt == 0x82)
            {
                highbyte = binr.ReadByte();
                lowbyte = binr.ReadByte();
                byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
                count = BitConverter.ToInt32(modint, 0);
            }
            else
            {
                count = bt;
            }

            while (binr.ReadByte() == 0x00)
            {
                count -= 1;
            }
            binr.BaseStream.Seek(-1, SeekOrigin.Current);
            return count;
        }




        public string DecryptWithRsaPrivateKey(string encryptData, string privatekey, int keysize)
        {
            var privateKeyBits = System.Convert.FromBase64String(privatekey);
            var RSA = new RSACryptoServiceProvider();
            var RSAparams = new RSAParameters();
            using (BinaryReader binr = new BinaryReader(new MemoryStream(privateKeyBits)))
            {
                byte bt = 0;
                ushort twobytes = 0;
                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130)
                    binr.ReadByte();
                else if (twobytes == 0x8230)
                    binr.ReadInt16();
                else
                    throw new Exception("Unexpected value read binr.ReadUInt16()");

                twobytes = binr.ReadUInt16();
                if (twobytes != 0x0102)
                    throw new Exception("Unexpected version");
                bt = binr.ReadByte();
                if (bt != 0x00)
                    throw new Exception("Unexpected value read binr.ReadByte()");
                RSAparams.Modulus = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.Exponent = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.D = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.P = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.Q = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.DP = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.DQ = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.InverseQ = binr.ReadBytes(GetIntegerSize(binr));
            }
            RSA.ImportParameters(RSAparams);
            var csp = new RSACryptoServiceProvider(keysize);
            //how to get the private key
            var privKey = csp.ExportParameters(true);
            //first, get our bytes back from the base64 string ...
            var bytesCypherText = Convert.FromBase64String(encryptData);
            //we want to decrypt, therefore we need a csp and load our private key
            csp = new RSACryptoServiceProvider();
            csp.ImportParameters(RSAparams);
            //decrypt and strip pkcs#1.5 padding
            var bytesPlainTextData = csp.Decrypt(bytesCypherText, false);
            string plainTextData = System.Text.Encoding.UTF8.GetString(bytesPlainTextData);
            return plainTextData;
        }

        public string EncryptWithPublicCer(string data, string CERTIFICATE)
        {
            X509Certificate2 cert = new X509Certificate2();
            cert.Import(Encoding.ASCII.GetBytes(CERTIFICATE));
            RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)cert.PublicKey.Key;
            byte[] plainbytes = System.Text.Encoding.UTF8.GetBytes(data);
            byte[] cipherbytes = rsa.Encrypt(plainbytes, false);
            return Convert.ToBase64String(cipherbytes);
        }
        */
    }
}
