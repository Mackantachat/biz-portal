using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing.Common;

namespace BizPortal.Utils.Helpers
{
    public class QRAndBarCodeHelper
    {
        public static string GenerateBarCodeBase64(string data)
        {
            var writer = new ZXing.BarcodeWriter
            {
                Format = ZXing.BarcodeFormat.CODE_128,
                Options = new EncodingOptions
                {
                    PureBarcode = true,
                    Height = 120,
                    Width = 900,
                    Margin = 0
                }
            };
            var barcode = writer.Write(data);
            MemoryStream stream = new MemoryStream();
            barcode.Save(stream, ImageFormat.Bmp);
            byte[] barcodeBytes = stream.ToArray();

            string encode = ByteToBase64urlString(barcodeBytes);
            return encode;
        }

        public static string GenerateQRCodeBase64(string data, bool isEncodeToBase64UrlString = true, ImageFormat format = null, int height = 120, int width = 120, int margin = 0)
        {
            var writer = new ZXing.BarcodeWriter
            {
                Format = ZXing.BarcodeFormat.QR_CODE,
                Options = new EncodingOptions
                {
                    PureBarcode = true,
                    Height = height,
                    Width = width,
                    Margin = margin
                }
            };
            var barcode = writer.Write(data);
            MemoryStream stream = new MemoryStream();
            barcode.Save(stream, (format == null ? ImageFormat .Bmp: format));
            byte[] barcodeBytes = stream.ToArray();

            return isEncodeToBase64UrlString ? ByteToBase64urlString(barcodeBytes) : Base64urlStringToBase64String(ByteToBase64urlString(barcodeBytes));
        }

        public static string EmptyString = "-";

        public static string FormatOutput(object s)
        {
            string output = Convert.ToString(s);
            if (string.IsNullOrEmpty(output))
            {
                return EmptyString;
            }
            else
            {
                output = output.Trim();
                if (string.IsNullOrEmpty(output))
                    return EmptyString;
                return output;
            }
        }

        public static string ByteToBase64urlString(byte[] input)
        {
            StringBuilder result = new StringBuilder(Convert.ToBase64String(input).TrimEnd('='));
            result.Replace('+', '-');
            result.Replace('/', '_');
            return result.ToString();
        }

        public static string Base64urlStringToBase64String(string base64urlstring)
        {
            byte[] convertData = Base64urlStringToByte(base64urlstring);
            return Convert.ToBase64String(convertData);
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
    }
}
