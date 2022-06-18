using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Utils.Helpers
{
    public static class Base64UrlStringHelper
    {
        public static string ByteToBase64urlString(byte[] input)
        {
            StringBuilder result = new StringBuilder(Convert.ToBase64String(input).TrimEnd('='));
            result.Replace('+', '-');
            result.Replace('/', '_');
            return result.ToString();
        }

        public static byte[] Base64urlStringToByte(string base64ForUrlInput)
        {
            int padChars = (base64ForUrlInput.Length % 4) == 0 ? 0 : (4 - (base64ForUrlInput.Length % 4));
            StringBuilder result = new StringBuilder(base64ForUrlInput, base64ForUrlInput.Length + padChars);
            result.Append(String.Empty.PadRight(padChars, '='));
            result.Replace('-', '+');
            result.Replace('_', '/');
            return Convert.FromBase64String(result.ToString());
        }

        public static string Base64StringToBase64urlString(string base64string)
        {
            byte[] convertByte = Convert.FromBase64String(base64string);
            return ByteToBase64urlString(convertByte);
        }

        public static string Base64urlStringToBase64String(string base64urlstring)
        {
            byte[] convertData = Base64urlStringToByte(base64urlstring);
            return Convert.ToBase64String(convertData);
        }
    }
}
