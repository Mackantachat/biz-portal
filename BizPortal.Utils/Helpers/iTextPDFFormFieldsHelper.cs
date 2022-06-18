using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Utils.Helpers
{
    public class iTextPDFFormFieldsHelper
    {
        public static BaseFont bf = BaseFont.CreateFont(ServerHelper.MapPath("~/fonts/THSarabunNew.ttf"), BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
        public static string PDF_FIELD_TEXTFONT = "textfont";
        public static string PDF_FIELD_TEXTSIZE = "textsize";

        private static BaseFont GetFont(PDFConfig config)
        {
            if (config != null  && !string.IsNullOrEmpty(config.FontName))
            {
                string path = ServerHelper.MapPath("~/fonts/" + config.FontName);
                if (File.Exists(path)) return BaseFont.CreateFont(path, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            }

            return bf;
        }

        public static byte[] ApplyPDFFieldValues(string templateFile, List<PDFFieldValue> fieldValues, PDFConfig config = null)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                PdfReader reader = new PdfReader(templateFile);
                PdfStamper stamper = new PdfStamper(reader, ms, '\0', false);

                BaseFont font = GetFont(config);
                
                foreach (PDFFieldValue field in fieldValues)
                {
                    float fontSize = (config != null && config.FontSize > 0) ? config.FontSize : 0;
                    if (field.FieldType == PDFFieldValue.eFieldType.Textbox)
                    {
                        fontSize = (field.FontSize > 0) ? field.FontSize : fontSize; //(fontSize > 0) ? fontSize : field.FontSize; // Try to use font-size from field first
                        stamper.AcroFields.SetFieldProperty(field.FieldName, PDF_FIELD_TEXTFONT, font, null);
                        stamper.AcroFields.SetFieldProperty(field.FieldName, PDF_FIELD_TEXTSIZE, fontSize, null);
                        stamper.AcroFields.SetField(field.FieldName, field.Value);
                    }
                    else if (field.FieldType == PDFFieldValue.eFieldType.Image)
                    {
                        string imgFieldname = field.FieldName + "_af_image";
                        Rectangle rect = stamper.AcroFields.GetFieldPositions(imgFieldname)[0].position;
                        int page = stamper.AcroFields.GetFieldPositions(imgFieldname)[0].page;
                        Image img = Image.GetInstance(Convert.FromBase64String(field.Value));
                        //Scale it
                        img.ScaleAbsolute(rect.Width, rect.Height);
                        //Position it
                        img.SetAbsolutePosition(rect.Left, rect.Bottom);
                        stamper.GetOverContent(page).AddImage(img);
                        stamper.AcroFields.SetField(imgFieldname, field.Value);
                    }                   
                    else if (field.FieldType == PDFFieldValue.eFieldType.Checkbox)
                    {
                        stamper.AcroFields.SetField(field.FieldName, field.Value, true);
                    }

                    //stamper.AcroFields.SetField(field.FieldName, field.Value);
                }

                stamper.FormFlattening = true;
                stamper.Close();

                reader.Close();

                return ms.GetBuffer();
            }
        }

        /// <summary>
        /// Merge multiple pdf files into a single file.
        /// </summary>
        /// <param name="pdfStreams"></param>
        /// <returns></returns>
        public static byte[] MergePDFFiles(List<byte[]> pdfStreams)
        {
            byte[] result = null;

            //Perform the conversion in memory first
            using (MemoryStream ms = new MemoryStream())
            {
                //Using the ItextSharp Document
                using (Document document = new Document())
                {
                    //Using the ITextSharp PdfCopy to create a PDF document in the memory stream
                    using (PdfCopy copy = new PdfCopy(document, ms))
                    {
                        //Open the document before any changes can be made.
                        document.Open();

                        //Loop through each file
                        foreach (var stream in pdfStreams)
                        {
                            // Convert the BinaryStream of the file to a ITexSharp PdfReader
                            PdfReader reader = new PdfReader(stream);
                            int n = reader.NumberOfPages;
                            //Loop through each page in the current PDF file
                            for (int page = 0; page < n;)
                            {
                                //Import the page to the PDF document in the memory stream.
                                copy.AddPage(copy.GetImportedPage(reader, ++page));
                            }
                        }
                    }
                }

                //Convert the final memory stream into a byte array.
                result = ms.ToArray();
            }

            return result;
        }

        #region Utility functions
        public static void FillIdentityDigits(List<PDFFieldValue> fields, string id, string prefix, int maxDigit, bool allowAlphabet = false)
        {
            if (id == null) id = "";

            int digit = 0;
            for (int i = 0; i < id.Length && digit <= maxDigit; i++)
            {
                if (!allowAlphabet && !StringHelper.IsNumeric(id.Substring(i, 1))) continue;

                digit++;
                fields.Add(new PDFFieldValue() { FieldName = prefix + digit, Value = id.Substring(i, 1), FontSize = 13 });
            }
        }
        #endregion

        public class PDFConfig
        {
            public string FontName { get; set; }
            public float FontSize { get; set; }

            public PDFConfig()
            {
                FontSize = 14f;
                FontName = "Angsima.ttf";
            }
        }

        public class PDFFieldValue
        {
            public string FieldName { get; set; }
            public string Value { get; set; }
            public Single FontSize { get; set; }
            public eFieldType FieldType { get; set; }
            public enum eFieldType
            {
                Textbox,
                Checkbox,
                Image
            }

            public PDFFieldValue()
            {
                FontSize = 0;
                FieldType = eFieldType.Textbox;
            }
        }

    }
}
