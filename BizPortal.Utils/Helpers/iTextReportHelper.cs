using BizPortal.Models.Reports;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using iTextSharp.text.pdf.draw;
using BizPortal.Utils.Extensions;

namespace BizPortal.Utils.Helpers
{
    public class iTextReportHelper
    {
        public static BaseFont f_ts = BaseFont.CreateFont(ServerHelper.MapPath("~/fonts/THSarabunNew.ttf"), BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
        public static BaseFont f_tsb = BaseFont.CreateFont(ServerHelper.MapPath("~/fonts/THSarabunBold.ttf"), BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
        public static BaseFont f_wingding = BaseFont.CreateFont(ServerHelper.MapPath("~/fonts/wingding.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

        // Special character
        public static String TEXT_CHECKBOX = "o";    // checkbox
        public static String TEXT_SCISSORS = "\"";    // scissors

        public static byte[] GetConfirmationFormJuristicReport(ConfirmationFormJuristicPDFModel report)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Document document = new Document(PageSize.A4, 30, 30, 20, 20);
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                writer.PageEvent = new ConfirmationFormJuristicHeaderFooter();

                document.Open();
                //document.Open();
                #region GetImageHeader
                PdfPTable imageTable = new PdfPTable(2);
                imageTable.TotalWidth = 530f;
                imageTable.HorizontalAlignment = 0;
                imageTable.SpacingAfter = 10;

                float[] headerTableColWidth = new float[2];
                headerTableColWidth[0] = 220f;
                headerTableColWidth[1] = 310f;

                imageTable.SetWidths(headerTableColWidth);
                imageTable.LockedWidth = true;

                iTextSharp.text.Image png = iTextSharp.text.Image.GetInstance(ServerHelper.MapPath("~/Content/Images/biz-v2/logos/bizportal-logo.png"));
                //iTextSharp.text.Image.GetInstance(this.server.MapPath("~/Content/Images/biz-v2/logos/bizportal-logo.png"));
                //iTextSharp.text.Image.GetInstance(Server.MapPath("~/image/BizIcon.png"));

                png.ScaleAbsolute(81, 52);

                PdfPCell imageTableCell0 = new PdfPCell(png);
                imageTableCell0.HorizontalAlignment = Element.ALIGN_LEFT;
                imageTableCell0.Border = Rectangle.NO_BORDER;
                imageTable.AddCell(imageTableCell0);

                PdfPCell imageTableCell1 = new PdfPCell(new Phrase(""));
                imageTableCell1.Border = Rectangle.NO_BORDER;
                imageTable.AddCell(imageTableCell1);
                document.Add(imageTable);
                #endregion

                #region GetHeader            
                var h1 = new Font(f_tsb, 24);
                PdfPTable headerTable = new PdfPTable(1);
                headerTable.TotalWidth = 530f;
                headerTable.HorizontalAlignment = 0;
                headerTable.SpacingAfter = 20;
                headerTable.LockedWidth = true;


                PdfPCell headerCell = new PdfPCell(new Phrase("ใบรับคำขอผ่านระบบอิเล็กทรอนิกส์", h1));//"บันทึกข้อความ"
                headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                headerCell.VerticalAlignment = Element.ALIGN_BOTTOM;
                headerCell.Border = Rectangle.NO_BORDER;
                headerTable.AddCell(headerCell);
                document.Add(headerTable);
                #endregion


                #region GetHeaderDetail
                var n16 = new Font(f_ts, 16);
                var n16g = new Font(f_ts, 16, 0, BaseColor.GRAY);
                var n14 = new Font(f_ts, 14);
                var b16 = new Font(f_tsb, 16);
                var b14 = new Font(f_tsb, 14);
                PdfPTable table = new PdfPTable(2);
                table.TotalWidth = 530f;
                table.HorizontalAlignment = 0;
                table.SpacingAfter = 10;

                float[] tableWidths = new float[2];
                tableWidths[0] = 305f;
                tableWidths[1] = 225f;

                table.SetWidths(tableWidths);
                table.LockedWidth = true;

                Chunk blank = new Chunk(" ", n16);
                Chunk colon = new Chunk(": ", n16);
                Chunk dot = new Chunk(".", n16);
                Phrase p = new Phrase();

                PdfPCell cell0 = new PdfPCell();
                PdfPCell cell1 = new PdfPCell();

                if (report.IsNormal != true)
                {
                    p.Add(new Chunk("ชื่อนิติบุคคล", b16));
                    p.Add(new Chunk(colon));
                    p.Add(new Chunk(report.CompanyName, n16g));

                    cell0 = new PdfPCell(p);
                    cell0.Border = Rectangle.NO_BORDER;

                    table.AddCell(cell0);

                    p = new Phrase();
                    p.Add(new Chunk("เลขทะเบียนนิติบุคคล", b16));
                    p.Add(new Chunk(colon));
                    p.Add(new Chunk(report.CompanyID, n16g));

                    cell1 = new PdfPCell(p);
                    cell1.Border = Rectangle.NO_BORDER;

                    table.AddCell(cell1);
                }
                else
                {
                    p = new Phrase();
                    p.Add(new Chunk("ชื่อผู้ขออนุญาต", b16));
                    p.Add(new Chunk(colon));
                    p.Add(new Chunk(report.RequestorName, n16g));

                    cell0 = new PdfPCell(p);
                    cell0.Border = Rectangle.NO_BORDER;
                    table.AddCell(cell0);
                }
                

                if (report.IsNormal == true)
                {
                    p = new Phrase();
                    p.Add(new Chunk("เลขประจำตัวผู้เสียภาษีอากร", b16));
                    p.Add(new Chunk(colon));
                    p.Add(new Chunk(report.CompanyVatID, n16g));
                    cell1 = new PdfPCell(p);
                    cell1.Border = Rectangle.NO_BORDER;
                    table.AddCell(cell1);
                }
                

                

                p = new Phrase();
                p.Add(new Chunk("วัน-เวลาที่ยื่นคำขอ", b16));
                p.Add(new Chunk(colon));
                var buffDate = report.RequestDateTime;
                //buffDate = buffDate.AddYears(543);
                var reqDate = buffDate.ToString("dd/MM/yyyy");
                p.Add(new Chunk(reqDate, n16g));

                p.Add(new Chunk(blank));
                p.Add(new Chunk(blank));
                var reqTime = buffDate.ToString("HH:mm");
                p.Add(new Chunk(reqTime, n16g));
                p.Add(new Chunk(blank));
                p.Add(new Chunk("น", n16g));

                cell0 = new PdfPCell(p);
                cell0.Border = Rectangle.NO_BORDER;
                table.AddCell(cell0);


                p = new Phrase();
                p.Add(new Chunk("IP Address", b16));
                p.Add(new Chunk(colon));
                //var reqTime = buffDate.ToString("HH:mm");
                p.Add(new Chunk(report.IPAddress, n16g));

                cell1 = new PdfPCell(p);
                cell1.Border = Rectangle.NO_BORDER;
                table.AddCell(cell1);

                if (report.IsNormal == true)
                {
                    p = new Phrase();
                    p.Add(new Chunk(" ", b16));

                    cell0 = new PdfPCell(p);
                    cell0.Border = Rectangle.NO_BORDER;
                    table.AddCell(cell0);

                    cell1 = new PdfPCell(p);
                    cell1.Border = Rectangle.NO_BORDER;
                    table.AddCell(cell1);
                }


                document.Add(table);
                #endregion

                #region GetDetailTable

                PdfPTable DetailTable = new PdfPTable(5);
                DetailTable.TotalWidth = 530f;
                DetailTable.HorizontalAlignment = 0;
                DetailTable.SpacingAfter = 15;
                DetailTable.SpacingBefore = 45;
                DetailTable.DefaultCell.Border = Rectangle.NO_BORDER;

                float[] colWidths = new float[5];
                colWidths[0] = 53f;
                colWidths[1] = 155f;
                colWidths[2] = 100f;
                colWidths[3] = 80f;
                colWidths[4] = 132f;

                DetailTable.SetWidths(colWidths);
                DetailTable.LockedWidth = true;

                PdfPCell cell;
                var b14g = new Font(f_tsb, 14, 0, BaseColor.GRAY);
                var n14g = new Font(f_ts, 14, 0, BaseColor.GRAY);

                cell = new PdfPCell(new Phrase("ลำดับ", b14g));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_CENTER;
                cell.BorderColor = new iTextSharp.text.BaseColor(211, 211, 211);
                //cell.BackgroundColor = new iTextSharp.text.BaseColor(169, 169, 169);
                //cell.PaddingTop = 10;
                cell.Border = Rectangle.BOTTOM_BORDER;
                cell.PaddingBottom = 10;
                DetailTable.AddCell(cell);

                cell = new PdfPCell(new Phrase("รายการคำขอ", b14g));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_CENTER;
                cell.BorderColor = new iTextSharp.text.BaseColor(211, 211, 211);
                //cell.BackgroundColor = new iTextSharp.text.BaseColor(169, 169, 169);
                //cell.PaddingTop = 10;
                cell.Border = Rectangle.BOTTOM_BORDER;
                cell.PaddingBottom = 10;
                DetailTable.AddCell(cell);

                cell = new PdfPCell(new Phrase("เลขที่คำขอ", b14g));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_CENTER;
                cell.BorderColor = new iTextSharp.text.BaseColor(211, 211, 211);
                //cell.BackgroundColor = new iTextSharp.text.BaseColor(169, 169, 169);
                //cell.PaddingTop = 10;
                cell.Border = Rectangle.BOTTOM_BORDER;
                cell.PaddingBottom = 10;
                DetailTable.AddCell(cell);

                cell = new PdfPCell(new Phrase("หน่วยงาน", b14g));
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_CENTER;
                cell.BorderColor = new iTextSharp.text.BaseColor(211, 211, 211);
                //cell.BackgroundColor = new iTextSharp.text.BaseColor(169, 169, 169);
                //cell.PaddingTop = 10;
                cell.Border = Rectangle.BOTTOM_BORDER;
                cell.PaddingBottom = 10;
                DetailTable.AddCell(cell);

                cell = new PdfPCell(new Phrase("ระยะเวลาที่คาดว่า จะดำเนินการเสร็จสิ้น *", b14g));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.BorderColor = new iTextSharp.text.BaseColor(211, 211, 211);
                //cell.BackgroundColor = new iTextSharp.text.BaseColor(169, 169, 169);
                //cell.PaddingBottom = 10;
                cell.Border = Rectangle.BOTTOM_BORDER;
                cell.PaddingBottom = 10;
                DetailTable.AddCell(cell);
                document.Add(DetailTable);
                int i = 0;
                foreach (var detail in report.Requests)
                {
                    PdfPTable DetailBodyTable = new PdfPTable(5);
                    DetailBodyTable.TotalWidth = 530f;
                    DetailBodyTable.HorizontalAlignment = 0;
                    DetailBodyTable.SpacingAfter = 10;
                    float[] colWidths1 = new float[5];
                    colWidths1[0] = 53f;
                    colWidths1[1] = 155f;
                    colWidths1[2] = 100f;
                    colWidths1[3] = 117f;
                    colWidths1[4] = 95f;


                    DetailBodyTable.SetWidths(colWidths1);
                    DetailBodyTable.LockedWidth = true;

                    cell = new PdfPCell(new Phrase((i + 1).ToString(), n16));
                    cell.FixedHeight = 85f;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.BorderColor = new iTextSharp.text.BaseColor(211, 211, 211);
                    cell.PaddingBottom = 10;
                    cell.DisableBorderSide(Rectangle.RIGHT_BORDER);
                    DetailBodyTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(detail.ApplicationName, n14));//
                    cell.FixedHeight = 85f;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.BorderColor = new iTextSharp.text.BaseColor(211, 211, 211);
                    cell.PaddingLeft = 10;
                    cell.PaddingBottom = 10;
                    cell.DisableBorderSide(Rectangle.RIGHT_BORDER);
                    cell.DisableBorderSide(Rectangle.LEFT_BORDER);
                    cell.PaddingBottom = 10;

                    DetailBodyTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(detail.RequestID, n14g));
                    cell.FixedHeight = 85f;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.BorderColor = new iTextSharp.text.BaseColor(211, 211, 211);

                    cell.DisableBorderSide(Rectangle.RIGHT_BORDER);
                    cell.DisableBorderSide(Rectangle.LEFT_BORDER);
                    cell.PaddingBottom = 10;


                    DetailBodyTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(detail.OrgName, n14));
                    cell.FixedHeight = 85f;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.BorderColor = new iTextSharp.text.BaseColor(211, 211, 211);
                    cell.PaddingBottom = 10;
                    cell.DisableBorderSide(Rectangle.RIGHT_BORDER);
                    cell.DisableBorderSide(Rectangle.LEFT_BORDER);
                    DetailBodyTable.AddCell(cell);

                    p = new Phrase();
                    int addDate = detail.Duration.HasValue ? detail.Duration.Value : 0;
                    var expectDate = report.RequestDateTime.AddDays(addDate).ToString("d MMMM yyyy",
                      CultureInfo.CreateSpecificCulture("th-TH"));
                    p.Add(new Chunk(addDate.ToString() + " วัน", n14));

                    cell = new PdfPCell(p);
                    cell.FixedHeight = 85f;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.BorderColor = new iTextSharp.text.BaseColor(211, 211, 211);
                    cell.PaddingBottom = 10;
                    cell.DisableBorderSide(Rectangle.LEFT_BORDER);
                    DetailBodyTable.AddCell(cell);

                    i += 1;
                    document.Add(DetailBodyTable);
                }

                #endregion

                #region GetFooter
                var n11 = new Font(f_ts, 12, 0, BaseColor.GRAY);
                PdfPTable footerTable = new PdfPTable(1);
                footerTable.TotalWidth = 530f;
                footerTable.HorizontalAlignment = 0;
                footerTable.SpacingAfter = 60;
                footerTable.LockedWidth = true;
                string Footer = "*เป็นระยะเวลาที่จะเริ่มนับ หลังจากที่เจ้าหน้าที่ลงความเห็นว่าคำขอ เอกสาร และเงื่อนไขการขออนุญาตของคุณมีความถูกต้อง ครบถ้วน";
                PdfPCell footerCell = new PdfPCell(new Phrase(Footer, n11));
                footerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                footerCell.VerticalAlignment = Element.ALIGN_TOP;
                footerCell.Border = Rectangle.NO_BORDER;
                footerTable.AddCell(footerCell);


                document.Add(footerTable);
                #endregion

                #region Endpage
                /* Use ConfirmationFormJuristicHeaderFooter instead
                PdfPTable tabFot = new PdfPTable(1);
                tabFot.TotalWidth = 600f;
                cell = new PdfPCell(new Phrase("หากมีข้อสงสัยประการใด กรุณาติดต่อ…", n14));
                cell.Border = Rectangle.NO_BORDER;
                //cell.BackgroundColor = new iTextSharp.text.BaseColor(105, 105, 105);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                tabFot.AddCell(cell);

                cell = new PdfPCell(new Phrase("ศูนย์รับคำขออนุญาต (One Stop Service) สำนักงานคณะกรรมการพัฒนาระบบราชการ\n59/1 ถนนพิษณุโลก แขวงดุสิต เขตดุสิต กรุงเทพฯ 10300  โทร 02 356 9999", b14));
                cell.Border = Rectangle.NO_BORDER;
                //cell.BackgroundColor = new iTextSharp.text.BaseColor(105, 105, 105);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.PaddingBottom = 10;
                tabFot.AddCell(cell);

                tabFot.WriteSelectedRows(0, 2, -40, 80, writer.DirectContent);
                */
                #endregion



                document.Close();
                writer.Close();
                ms.Close();

                return ms.GetBuffer();
            }

        }


        public static byte[] GetBillPaymentReport(BillPaymentFormPDFModel report)
        {
            Func<PdfPCell> getBlankCell = () =>
            {
                PdfPCell c = new PdfPCell();
                c.HorizontalAlignment = Element.ALIGN_LEFT;
                c.VerticalAlignment = Element.ALIGN_TOP;
                c.Border = Rectangle.NO_BORDER;

                return c;
            };

            Func<float[], PdfPTable> getBlankTable = (columnWidths) =>
            {
                PdfPTable tb = new PdfPTable(columnWidths);
                tb.WidthPercentage = 100;
                tb.HorizontalAlignment = Element.ALIGN_LEFT;
                tb.DefaultCell.Border = Rectangle.NO_BORDER;

                return tb;
            };


            using (MemoryStream ms = new MemoryStream())
            {
                PdfPCell cell;
                PdfPTable table;
                iTextSharp.text.Image image;
                LineSeparator line;
                DottedLineSeparator dotline;
                RoundRectangle roundRect = new RoundRectangle();
                var b14blk = new Font(f_tsb, 14, 0, BaseColor.BLACK);
                var n14blk = new Font(f_ts, 14, 0, BaseColor.BLACK);
                var n14grey = new Font(f_ts, 14, 0, BaseColor.GRAY);
                var n11blk = new Font(f_ts, 11, 0, BaseColor.BLACK);
                var n14wing = new Font(f_wingding, 14, 0, BaseColor.BLACK);
                var n11wing = new Font(f_wingding, 11, 0, BaseColor.BLACK);
                var colorWhite = new iTextSharp.text.BaseColor(255, 255, 230);

                Document document = new Document(PageSize.A4, 10, 10, 10, 10);
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                document.Open();

                #region Customer page header
                PdfPTable custPageHeader = new PdfPTable(new float[] { 1, 1, 1 });
                custPageHeader.WidthPercentage = 100;
                custPageHeader.HorizontalAlignment = Element.ALIGN_LEFT;
                custPageHeader.DefaultCell.Border = Rectangle.NO_BORDER;

                custPageHeader.AddCell(new PdfPCell()
                {
                    Phrase = new Phrase("ใบแจ้งการชำระเงิน (Bill Payment Slip)", n14blk),
                    CellEvent = roundRect,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_TOP,
                    PaddingLeft = 20f,
                    Border = PdfPCell.NO_BORDER
                });

                custPageHeader.AddCell("");

                custPageHeader.AddCell(new PdfPCell()
                {
                    Phrase = new Phrase("ส่วนที่ 1 สำหรับลูกค้า", n11blk),
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    Border = PdfPCell.NO_BORDER
                });

                document.Add(custPageHeader);
                #endregion

                #region Customer page header
                PdfPTable bankPageHeader = new PdfPTable(new float[] { 1, 1, 1 });
                bankPageHeader.WidthPercentage = 100;
                bankPageHeader.HorizontalAlignment = Element.ALIGN_LEFT;
                bankPageHeader.DefaultCell.Border = Rectangle.NO_BORDER;

                bankPageHeader.AddCell(new PdfPCell()
                {
                    Phrase = new Phrase("ใบแจ้งการชำระเงิน (Bill Payment Slip)", n14blk),
                    CellEvent = roundRect,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_TOP,
                    PaddingLeft = 20f,
                    Border = PdfPCell.NO_BORDER
                });

                bankPageHeader.AddCell("");

                bankPageHeader.AddCell(new PdfPCell()
                {
                    Phrase = new Phrase("ส่วนที่ 2 สำหรับธนาคาร", n11blk),
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    Border = PdfPCell.NO_BORDER
                });

                #endregion

                #region Additional Note table
                PdfPTable noteTable = new PdfPTable(new float[] { 2, 11 });
                noteTable.WidthPercentage = 100;
                noteTable.HorizontalAlignment = Element.ALIGN_LEFT;
                noteTable.DefaultCell.Border = Rectangle.NO_BORDER;

                cell = new PdfPCell(new Phrase("คำชี้แจงเพิ่มเติม", n11blk));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Border = Rectangle.NO_BORDER;
                noteTable.AddCell(cell);

                string txt = "1. กรุณากรอกข้อมูลให้ครบถ้วนและนำไปชำระเงินที่สาขาของธนาคารกรุงไทยทุกสาขา/ATM/Internet\n";
                txt += "2. จำนวนเงินที่ชำระต้องเท่ากับจำนวนเงินที่กำหนดเท่านั้น\n";
                txt += "3. จำนวนเงินที่ชำระไม่รวมค่าธรรมเนียม 25 บาท/รายการ\n";
                txt += "   จำกัดวงเงิน 1,000,000.- บาท ส่วนเกินคิด 0.01 สูงสุดไม่เกิน 1,000.- บาท/รายการ";

                cell.Phrase = new Phrase(txt, n11blk);
                noteTable.AddCell(cell);
                #endregion

                #region Bill Header
                PdfPTable billHeadTable = getBlankTable(new float[] { 8, 5 });

                // Left side
                PdfPTable billHeadLeft = getBlankTable(new float[] { 1, 4 });
                image = Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/Content/Images/biz-v2/logos/opdc.png"));
                image.ScaleAbsolute(52, 52);

                cell = getBlankCell();
                cell.AddElement(image);
                billHeadLeft.AddCell(cell);

                cell = getBlankCell();
                cell.Phrase = new Phrase("ศูนย์รับคำขออนุญาต (ONE STOP SERVICE)\nสำนักงานคณะกรรมการพัฒนาระบบราชการ", n14blk);
                cell.PaddingTop = 10;
                billHeadLeft.AddCell(cell);

                //image = iTextSharp.text.Image.GetInstance(@"D:\Backup-System\Git\bizportal\BizPortal\Content\Images\biz-v2\logos\ktb.jpg");
                image = Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/Content/Images/biz-v2/logos/ktb.jpg"));
                image.ScaleAbsolute(52, 52);

                cell = getBlankCell();
                cell.AddElement(image);
                billHeadLeft.AddCell(cell);

                cell = getBlankCell();
                cell.Phrase = new Phrase(string.Format("\nธนาคารกรุงไทย (Product Code: {0})", report.ProductCode), n14blk);
                cell.PaddingTop = 0;
                billHeadLeft.AddCell(cell);


                cell = getBlankCell();
                cell.AddElement(billHeadLeft);
                billHeadTable.AddCell(cell);


                // Right side
                PdfPTable billHeadRight = getBlankTable(new float[] { 1 });
                table = getBlankTable(new float[] { 2, 1, 3 });

                cell = getBlankCell();
                cell.PaddingTop = 7;
                cell.Phrase = new Phrase(string.Format("เลขที่อ้างอิง 1"), b14blk);
                table.AddCell(cell);

                cell.Phrase = new Phrase(string.Format("(Ref.1)"), n14grey);
                table.AddCell(cell);

                cell.Phrase = new Phrase(string.Format(": {0}", report.RefCode1), b14blk);
                table.AddCell(cell);

                cell = getBlankCell();
                cell.Phrase = new Phrase(string.Format("เลขที่อ้างอิง 2"), b14blk);
                table.AddCell(cell);

                cell.Phrase = new Phrase(string.Format("(Ref.2)"), n14grey);
                table.AddCell(cell);

                cell.Phrase = new Phrase(string.Format(": {0}", report.RefCode2), b14blk);
                table.AddCell(cell);

                cell = getBlankCell();
                cell.AddElement(table);
                billHeadRight.AddCell(cell);

                table = getBlankTable(new float[] { 1 });
                cell = getBlankCell();
                CultureInfo ci = new CultureInfo("th-TH");
                cell.Phrase = new Phrase(string.Format("กำหนดชำระภายในวันที่: {0}", report.DueDate.HasValue ? report.DueDate.Value.ToString("d/M/yyyy", ci): "-"), b14blk);
                table.AddCell(cell);

                cell = getBlankCell();
                cell.AddElement(table);
                billHeadRight.AddCell(cell);

                cell = getBlankCell();
                cell.AddElement(billHeadRight);
                billHeadTable.AddCell(cell);
                #endregion

                #region Customer section
                PdfPTable custOuterTable = new PdfPTable(new float[] { 1 });
                custOuterTable.SpacingBefore = 10;
                custOuterTable.WidthPercentage = 100;
                custOuterTable.HorizontalAlignment = Element.ALIGN_LEFT;
                custOuterTable.DefaultCell.Border = Rectangle.BOX;

                PdfPTable custBodyTable = new PdfPTable(new float[] { 1 });
                custBodyTable.WidthPercentage = 100;
                custBodyTable.HorizontalAlignment = Element.ALIGN_LEFT;
                custBodyTable.DefaultCell.Border = Rectangle.NO_BORDER;

                // Add bill header for customer
                cell = getBlankCell();
                cell.PaddingTop = -10;
                cell.AddElement(billHeadTable);
                custBodyTable.AddCell(cell);

                table = new PdfPTable(new float[] { 1 });
                table.WidthPercentage = 100;
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                cell = new PdfPCell(new Phrase(string.Format("ผู้ใช้บริการ : {0}", report.CustomerName), b14blk));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(string.Format("รายละเอียดค่าใช้จ่าย/ค่าธรรมเนียม"), b14blk));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 6;
                table.AddCell(cell);

                // Add customer content to body
                cell = new PdfPCell();
                cell.AddElement(table);
                cell.Border = Rectangle.NO_BORDER;
                custBodyTable.AddCell(cell);

                #region Payment item header
                table = new PdfPTable(new float[] { 1, 5, 3 });
                table.WidthPercentage = 100;
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.DefaultCell.Border = Rectangle.NO_BORDER;

                cell = new PdfPCell(new Phrase(string.Format("ลำดับ"), b14blk));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderColor = BaseColor.WHITE;
                cell.BorderWidth = 2;
                cell.BackgroundColor = new iTextSharp.text.BaseColor(231, 230, 230);
                cell.PaddingBottom = 8;
                table.AddCell(cell);

                cell.Phrase = new Phrase(string.Format("รายการ"), b14blk);
                table.AddCell(cell);

                cell.Phrase = new Phrase(string.Format("จำนวนเงิน (บาท)"), b14blk);
                table.AddCell(cell);

                #endregion

                #region Payment item body
                cell.BackgroundColor = BaseColor.WHITE;
                cell.Border = Rectangle.NO_BORDER;
                foreach (var item in report.PaymentItems)
                {
                    cell.Phrase = new Phrase(string.Format("{0}", item.Sequence), n14blk);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell.Phrase = new Phrase(string.Format("{0}", item.Title), n14blk);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cell);

                    cell.Phrase = new Phrase(string.Format("{0}", item.Amount.ToString("#,##0.00")), n14blk);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);
                }

                // Footer Row
                decimal totalAmount = report.PaymentItems.Sum(o => o.Amount);
                string amountText = totalAmount.ToThaiCurrencyText();
                table.AddCell("");

                cell.Phrase = new Phrase(string.Format("รวมเป็นเงินทั้งสิ้น (ตัวอักษร) {0}", amountText), b14blk);
                table.AddCell(cell);

                cell.Phrase = new Phrase(string.Format("{0}", totalAmount.ToString("#,##0.00")), b14blk);
                table.AddCell(cell);
                #endregion

                cell = getBlankCell();
                cell.AddElement(table);
                cell.Border = Rectangle.NO_BORDER;
                custBodyTable.AddCell(cell);


                // Draw line
                line = new LineSeparator(0.0F, 99.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1);
                cell = getBlankCell();
                cell.PaddingLeft = 10;
                cell.AddElement(line);
                custBodyTable.AddCell(cell);

                #region Customer footer

                table = new PdfPTable(new float[] { 10, 5 });
                table.WidthPercentage = 100;
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.DefaultCell.Border = Rectangle.NO_BORDER;

                // Addtional note cell
                cell = getBlankCell();
                cell.AddElement(noteTable);
                table.AddCell(cell);

                // Signature cell
                PdfPTable signatureTable = new PdfPTable(new float[] { 1 });
                signatureTable.WidthPercentage = 100;
                signatureTable.HorizontalAlignment = Element.ALIGN_CENTER;
                signatureTable.DefaultCell.Border = Rectangle.NO_BORDER;

                cell = getBlankCell();
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Phrase = new Phrase("ผู้รับเงิน (เจ้าหน้าที่ลงนามและประทับตรา)", n14blk);
                signatureTable.AddCell(cell);

                dotline = new DottedLineSeparator();
                dotline.LineWidth = 0.0F;
                dotline.Percentage = 80.0F;
                dotline.LineColor = BaseColor.BLACK;
                dotline.Offset = -30;
                dotline.Gap = 3;
                dotline.Alignment = Element.ALIGN_CENTER;

                cell = getBlankCell();
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.AddElement(dotline);
                signatureTable.AddCell(cell);

                cell = getBlankCell();
                cell.AddElement(signatureTable);
                table.AddCell(cell);

                cell = getBlankCell();
                cell.AddElement(table);
                custBodyTable.AddCell(cell);
                #endregion

                // Add customer body to container
                cell = new PdfPCell();
                cell.Padding = 8f;
                cell.AddElement(custBodyTable);
                custOuterTable.AddCell(cell);

                // Add customer container to document
                document.Add(custOuterTable);

                #endregion Customer section

                #region Section dotted line separator
                table = getBlankTable(new float[] { 1 });
                table.SpacingBefore = 5;
                cell = getBlankCell();
                cell.Phrase = new Phrase(TEXT_SCISSORS, n11wing);
                table.AddCell(cell);

                dotline = new DottedLineSeparator();
                dotline.LineWidth = 0.0F;
                dotline.Percentage = 99.0F;
                dotline.LineColor = BaseColor.BLACK;
                dotline.Offset = 3;
                dotline.Gap = 3;
                dotline.Alignment = Element.ALIGN_CENTER;
                cell = getBlankCell();
                cell.AddElement(dotline);
                table.AddCell(cell);
                document.Add(table);
                #endregion


                // Add dummy table
                table = getBlankTable(new float[] { 1 });
                table.SpacingBefore = 5;
                cell = getBlankCell();
                table.AddCell(cell);
                document.Add(table);


                #region Bank section

                document.Add(bankPageHeader);
                PdfPTable bankOuterTable = new PdfPTable(new float[] { 1 });
                bankOuterTable.SpacingBefore = 8;
                bankOuterTable.WidthPercentage = 100;
                bankOuterTable.HorizontalAlignment = Element.ALIGN_LEFT;
                bankOuterTable.DefaultCell.Border = Rectangle.BOX;

                PdfPTable bankBodyTable = new PdfPTable(new float[] { 1 });
                bankBodyTable.WidthPercentage = 100;
                bankBodyTable.HorizontalAlignment = Element.ALIGN_LEFT;
                bankBodyTable.DefaultCell.Border = Rectangle.NO_BORDER;

                #region Bill header for customer
                cell = getBlankCell();
                cell.PaddingTop = -10;
                cell.AddElement(billHeadTable);
                bankBodyTable.AddCell(cell);

                table = getBlankTable(new float[] { 8, 5 });
                cell = getBlankCell();
                cell.Phrase = new Phrase(string.Format("ผู้ใช้บริการ : {0}", report.CustomerName), b14blk);
                table.AddCell(cell);

                var tmp = getBlankTable(new float[] { 1 });

                cell.Phrase = new Phrase(string.Format("วันที่ชำระเงิน       /          /"), n14blk);
                tmp.AddCell(cell);

                dotline = new DottedLineSeparator();
                dotline.LineWidth = 0.0F;
                dotline.Percentage = 50f;
                dotline.LineColor = BaseColor.BLACK;
                dotline.Offset = -1;
                dotline.Gap = 1;
                dotline.Alignment = Element.ALIGN_CENTER;
                cell = getBlankCell();
                cell.AddElement(dotline);
                tmp.AddCell(cell);

                cell = getBlankCell();
                cell.Phrase = new Phrase(string.Format("สาขาธนาคารกรุงไทยที่รับฝาก"), n14blk);
                tmp.AddCell(cell);

                dotline = new DottedLineSeparator();
                dotline.LineWidth = 0.0F;
                dotline.Percentage = 46f;
                dotline.LineColor = BaseColor.BLACK;
                dotline.Offset = -1;
                dotline.Gap = 1;
                dotline.Alignment = Element.ALIGN_RIGHT;
                cell = getBlankCell();
                cell.AddElement(dotline);
                tmp.AddCell(cell);

                cell = getBlankCell();
                cell.AddElement(tmp);
                table.AddCell(cell);

                cell = getBlankCell();
                cell.AddElement(table);
                bankBodyTable.AddCell(cell);

                #endregion

                // line
                table = getBlankTable(new float[] { 1 });
                table.SpacingBefore = 8;

                line = new LineSeparator(0.0F, 99.0F, BaseColor.GRAY, Element.ALIGN_LEFT, -1);
                cell = getBlankCell();
                cell.AddElement(line);
                table.AddCell(cell);

                cell = getBlankCell();
                cell.AddElement(table);
                bankBodyTable.AddCell(cell);

                #region Fee table
                PdfPTable feeTable = getBlankTable(new float[] { 2, 11, 5 });
                feeTable.SpacingBefore = 0;

                // Title
                feeTable.AddCell("");
                cell = getBlankCell();
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Phrase = new Phrase(string.Format("ค่าธรรมเนียม"), b14blk);
                cell.PaddingBottom = 5;
                feeTable.AddCell(cell);
                feeTable.AddCell("");

                // Column 1. Payment method
                table = getBlankTable(new float[] { 1, 2 });

                cell = getBlankCell();
                cell.Phrase = new Phrase(TEXT_CHECKBOX, n14wing);
                table.AddCell(cell);
                table.AddCell(new Phrase(string.Format("เงินสด"), n11blk));

                cell = getBlankCell();
                cell.Phrase = new Phrase(TEXT_CHECKBOX, n14wing);
                table.AddCell(cell);
                table.AddCell(new Phrase(string.Format("เงินโอน"), n11blk));

                cell = new PdfPCell();
                cell.AddElement(table);
                feeTable.AddCell(cell);

                // Column 2. Amount text
                table = getBlankTable(new float[] { 1 });
                cell = getBlankCell();
                cell.Phrase = new Phrase(amountText, b14blk);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);

                cell = getBlankCell();
                cell.Phrase = new Phrase("จำนวนเงินที่ต้องชำระ (ตัวอักษร)", n11blk);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell();
                cell.AddElement(table);
                feeTable.AddCell(cell);

                // Column 3. Amount
                table = getBlankTable(new float[] { 1 });
                cell = getBlankCell();
                cell.Phrase = new Phrase(totalAmount.ToString("#,##0.00"), b14blk);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);

                cell.Phrase = new Phrase("จำนวนเงิน (ตัวเลข)", n11blk);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell();
                cell.AddElement(table);
                feeTable.AddCell(cell);

                // Add Fee table to body
                cell = getBlankCell();
                cell.AddElement(feeTable);
                bankBodyTable.AddCell(cell);

                #endregion

                #region Additional note
                table = new PdfPTable(new float[] { 10, 5 });
                table.WidthPercentage = 100;
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.DefaultCell.Border = Rectangle.NO_BORDER;

                // Addtional note cell
                cell = getBlankCell();
                cell.AddElement(noteTable);
                table.AddCell(cell);
                table.AddCell("");

                cell = getBlankCell();
                cell.AddElement(table);
                bankBodyTable.AddCell(cell);
                #endregion

                #region Signature table
                PdfPTable bankSigTable = getBlankTable(new float[] { 4, 3, 6, 3 });
                bankSigTable.SpacingBefore = 5;

                // Column 1: Depositor
                table = getBlankTable(new float[] { 1 });
                cell = getBlankCell();
                cell.Phrase = new Phrase("ชื่อผู้นำฝาก", n14blk);
                table.AddCell(cell);

                dotline = new DottedLineSeparator();
                dotline.LineWidth = 0.0F;
                dotline.Percentage = 80.0F;
                dotline.LineColor = BaseColor.BLACK;
                dotline.Offset = -25;
                dotline.Gap = 1;
                dotline.Alignment = Element.ALIGN_LEFT;

                cell = getBlankCell();
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.AddElement(dotline);
                table.AddCell(cell);

                cell = getBlankCell();
                cell.AddElement(table);
                bankSigTable.AddCell(cell);

                // Column 2: Tel
                table = getBlankTable(new float[] { 1 });
                cell = getBlankCell();
                cell.Phrase = new Phrase("โทรศัพท์", n14blk);
                table.AddCell(cell);

                dotline = new DottedLineSeparator();
                dotline.LineWidth = 0.0F;
                dotline.Percentage = 80.0F;
                dotline.LineColor = BaseColor.BLACK;
                dotline.Offset = -25;
                dotline.Gap = 1;
                dotline.Alignment = Element.ALIGN_LEFT;

                cell = getBlankCell();
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.AddElement(dotline);
                table.AddCell(cell);

                cell = getBlankCell();
                cell.AddElement(table);
                bankSigTable.AddCell(cell);

                // Column 3: Officer
                table = getBlankTable(new float[] { 1 });
                cell = getBlankCell();
                cell.Phrase = new Phrase("ผู้รับเงิน (เจ้าหน้าที่ลงนามและประทับตรา)", n14blk);
                table.AddCell(cell);

                dotline = new DottedLineSeparator();
                dotline.LineWidth = 0.0F;
                dotline.Percentage = 80.0F;
                dotline.LineColor = BaseColor.BLACK;
                dotline.Offset = -25;
                dotline.Gap = 1;
                dotline.Alignment = Element.ALIGN_LEFT;

                cell = getBlankCell();
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.AddElement(dotline);
                table.AddCell(cell);

                cell = getBlankCell();
                cell.AddElement(table);
                bankSigTable.AddCell(cell);


                // Column 4: dummy column
                bankSigTable.AddCell("");

                // Add bank signature to body
                cell = getBlankCell();
                cell.AddElement(bankSigTable);
                bankBodyTable.AddCell(cell);
                #endregion

                #region Barcode

                PdfPTable barcodeTable = getBlankTable(new float[] { 1 });
                barcodeTable.SpacingBefore = 25;

                var barcodeImg = GetBarcode(report.BarcodeData);
                image = iTextSharp.text.Image.GetInstance(barcodeImg, BaseColor.WHITE);
                image.Alignment = Element.ALIGN_RIGHT;
                image.ScaleAbsoluteHeight(30);
                cell = getBlankCell();
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.AddElement(image);
                barcodeTable.AddCell(cell);

                cell = getBlankCell();
                cell.PaddingRight = 60;
                cell.PaddingTop = -3;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.Phrase = new Phrase(report.BarcodeData, n11blk);
                barcodeTable.AddCell(cell);


                // Add barcode to body
                cell = getBlankCell();
                cell.AddElement(barcodeTable);
                bankBodyTable.AddCell(cell);

                #endregion


                // Add bank body to container
                cell = new PdfPCell();
                cell.Padding = 8f;
                cell.AddElement(bankBodyTable);
                bankOuterTable.AddCell(cell);

                // Add bank container to document
                document.Add(bankOuterTable);

                #endregion


                document.Close();
                writer.Close();
                ms.Close();

                return ms.GetBuffer();
            }
        }

        public static byte[] GetBillPaymentOSSReport(BillPaymentFormOSSPDFModel report)
        {
            Func<PdfPCell> getBlankCell = () =>
            {
                PdfPCell c = new PdfPCell();
                c.HorizontalAlignment = Element.ALIGN_LEFT;
                c.VerticalAlignment = Element.ALIGN_TOP;
                c.Border = Rectangle.NO_BORDER;

                return c;
            };

            Func<float[], PdfPTable> getBlankTable = (columnWidths) =>
            {
                PdfPTable tb = new PdfPTable(columnWidths);
                tb.WidthPercentage = 100;
                tb.HorizontalAlignment = Element.ALIGN_LEFT;
                tb.DefaultCell.Border = Rectangle.NO_BORDER;

                return tb;
            };

            PdfPCell cell = null;
            using (MemoryStream ms = new MemoryStream())
            {
                Document document = new Document(PageSize.A4, 30, 30, 20, 20);
                PdfWriter writer = PdfWriter.GetInstance(document, ms);

                document.Open();


                #region GetImageHeader
                PdfPTable imageTable = new PdfPTable(2);
                imageTable.TotalWidth = 530f;
                imageTable.HorizontalAlignment = 0;
                imageTable.SpacingAfter = 10;

                float[] headerTableColWidth = new float[2];
                headerTableColWidth[0] = 220f;
                headerTableColWidth[1] = 310f;

                imageTable.SetWidths(headerTableColWidth);
                imageTable.LockedWidth = true;

                iTextSharp.text.Image png = iTextSharp.text.Image.GetInstance(ServerHelper.MapPath("~/Content/Images/biz-v2/logos/bizportal-logo.png"));
                png.ScaleAbsolute(81, 52);

                PdfPCell imageTableCell0 = new PdfPCell(png);
                imageTableCell0.HorizontalAlignment = Element.ALIGN_LEFT;
                imageTableCell0.Border = Rectangle.NO_BORDER;
                imageTable.AddCell(imageTableCell0);

                PdfPCell imageTableCell1 = new PdfPCell(new Phrase(""));
                imageTableCell1.Border = Rectangle.NO_BORDER;
                imageTable.AddCell(imageTableCell1);
                document.Add(imageTable);
                #endregion

                #region GetHeader          
                var h1 = new Font(f_tsb, 24);
                PdfPTable headerTable = new PdfPTable(1);
                headerTable.TotalWidth = 530f;
                headerTable.HorizontalAlignment = 0;
                headerTable.SpacingAfter = 20;
                headerTable.LockedWidth = true;


                PdfPCell headerCell = new PdfPCell(new Phrase("ใบแจ้งชำระเงิน", h1));
                headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                headerCell.VerticalAlignment = Element.ALIGN_BOTTOM;
                headerCell.Border = Rectangle.NO_BORDER;
                headerTable.AddCell(headerCell);
                document.Add(headerTable);
                #endregion


                #region GetHeaderDetail
                var n16 = new Font(f_ts, 16);
                var n16g = new Font(f_ts, 16, 0, BaseColor.GRAY);
                var n14 = new Font(f_ts, 14);
                var b16 = new Font(f_tsb, 16);
                var b16u = new Font(f_tsb, 16);
                b16u.SetStyle(Font.UNDERLINE);
                var b14 = new Font(f_tsb, 14);

                headerTable = getBlankTable(new float[] { 12, 10 });

                PdfPTable tbLeft = getBlankTable(new float[] { 1 });

                Chunk blank = new Chunk(" ", n16);
                Chunk colon = new Chunk(": ", n16);
                Chunk dot = new Chunk(".", n16);
                Phrase p = new Phrase();
                PdfPCell cell0 = new PdfPCell();
                PdfPCell cell1 = new PdfPCell();
                if (report.IsJuristic)
                {
                    p.Add(new Chunk("ชื่อนิติบุคคล", b16));
                    p.Add(new Chunk(colon));
                    p.Add(new Chunk(report.CompanyName, n16g));
                    cell0 = new PdfPCell(p);
                    cell0.Border = Rectangle.NO_BORDER;
                    tbLeft.AddCell(cell0);
                }
                else
                {
                    p = new Phrase();
                    p.Add(new Chunk("ชื่อผู้ขออนุญาต", b16));
                    p.Add(new Chunk(colon));
                    p.Add(new Chunk(report.CustomerName, n16g));
                    cell0 = new PdfPCell(p);
                    cell0.Border = Rectangle.NO_BORDER;
                    tbLeft.AddCell(cell0);
                }
                

                
                if (report.IsJuristic)
                {
                    p = new Phrase();
                    p.Add(new Chunk("เลขทะเบียนนิติบุคคล", b16));
                    p.Add(new Chunk(colon));
                    p.Add(new Chunk(report.IdentityID, n16g));
                    cell0 = new PdfPCell(p);
                    cell0.Border = Rectangle.NO_BORDER;
                    tbLeft.AddCell(cell0);
                }
                else
                {
                    p = new Phrase();
                    p.Add(new Chunk("เลขประจำตัวผู้เสียภาษีอากร", b16));
                    p.Add(new Chunk(colon));
                    p.Add(new Chunk(report.TaxID, n16g));
                    cell0 = new PdfPCell(p);
                    cell0.Border = Rectangle.NO_BORDER;
                    tbLeft.AddCell(cell0);
                }    
                
                p = new Phrase();
                p.Add(new Chunk("วัน-เวลาที่ยื่นคำขอ", b16));
                p.Add(new Chunk(colon));
                //if (report.RequestDateTime.Year < 2500) report.RequestDateTime = report.RequestDateTime.AddYears(543);
                var reqDate = report.RequestDateTime.ToString("dd/MM/yyyy");
                p.Add(new Chunk(reqDate, n16g));

                p.Add(new Chunk(blank));
                p.Add(new Chunk(blank));
                var reqTime = report.RequestDateTime.ToString("HH:mm");
                p.Add(new Chunk(reqTime, n16g));
                p.Add(new Chunk(blank));
                p.Add(new Chunk("น", n16g));

                cell0 = new PdfPCell(p);
                cell0.Border = Rectangle.NO_BORDER;
                tbLeft.AddCell(cell0);


                p = new Phrase();
                p.Add(new Chunk("IP Address", b16));
                p.Add(new Chunk(colon));
                p.Add(new Chunk(report.IPAddress, n16g));
                cell0 = new PdfPCell(p);
                cell0.Border = Rectangle.NO_BORDER;
                tbLeft.AddCell(cell0);

                cell = getBlankCell();
                cell.AddElement(tbLeft);
                headerTable.AddCell(cell);



                PdfPTable tbRight = getBlankTable(new float[] { 1 });

                p = new Phrase();
                p.Add(new Chunk("ชื่อหน่วยงาน", b16));
                p.Add(new Chunk(colon));
                p.Add(new Chunk(report.OrganizationName, n16g));
                cell0 = new PdfPCell(p);
                cell0.Border = Rectangle.NO_BORDER;
                tbRight.AddCell(cell0);

                p = new Phrase();
                p.Add(new Chunk("รายการคำขอ", b16));
                p.Add(new Chunk(colon));
                p.Add(new Chunk(report.DocumentTitle, n16g));
                cell0 = new PdfPCell(p);
                cell0.Border = Rectangle.NO_BORDER;
                tbRight.AddCell(cell0);

                p = new Phrase();
                p.Add(new Chunk("เลขที่คำขอ", b16));
                p.Add(new Chunk(colon));
                p.Add(new Chunk(report.ApplicationNumber, n16g));
                cell0 = new PdfPCell(p);
                cell0.Border = Rectangle.NO_BORDER;
                tbRight.AddCell(cell0);

                p = new Phrase();
                p.Add(new Chunk("เบอร์ติดต่อเจ้าหน้าที่", b16));
                p.Add(new Chunk(colon));
                p.Add(new Chunk(report.OrgTel, n16g));
                cell0 = new PdfPCell(p);
                cell0.Border = Rectangle.NO_BORDER;
                tbRight.AddCell(cell0);

                cell = getBlankCell();
                cell.AddElement(tbRight);
                headerTable.AddCell(cell);


                document.Add(headerTable);
                #endregion

                #region Report detail
                PdfPTable table = getBlankTable(new float[] { 1 });
                table.SpacingBefore = 20;
                cell = new PdfPCell(new Phrase(string.Format("รายละเอียดค่าใช้จ่าย/ค่าธรรมเนียม"), b16));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;
                table.AddCell(cell);
                document.Add(table);

                #region Payment item header
                table = getBlankTable(new float[] { 10, 70, 20 });

                cell = new PdfPCell(new Phrase(string.Format("ลำดับ"), b16));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderColor = BaseColor.WHITE;
                cell.BorderWidth = 2;
                cell.BackgroundColor = new iTextSharp.text.BaseColor(231, 230, 230);
                cell.PaddingBottom = 8;
                table.AddCell(cell);

                cell.Phrase = new Phrase(string.Format("รายการ"), b16);
                table.AddCell(cell);

                cell.Phrase = new Phrase(string.Format("จำนวนเงิน (บาท)"), b16);
                table.AddCell(cell);

                #endregion

                #region Payment item body
                cell.BackgroundColor = BaseColor.WHITE;
                cell.Border = Rectangle.NO_BORDER;
                foreach (var item in report.PaymentItems)
                {
                    cell.Phrase = new Phrase(string.Format("{0}", item.Sequence), n16);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell.Phrase = new Phrase(string.Format("{0}", item.Title), n16);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cell);

                    cell.Phrase = new Phrase(string.Format("{0}", item.Amount.ToString("#,##0.00")), n16);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);
                }

                // Footer Row
                decimal totalAmount = report.PaymentItems.Sum(o => o.Amount);
                string amountText = totalAmount.ToThaiCurrencyText();
                table.AddCell("");

                cell.Phrase = new Phrase(string.Format("รวมเป็นเงินทั้งสิ้น (ตัวอักษร) {0}", amountText), b16);
                table.AddCell(cell);

                cell.Phrase = new Phrase(string.Format("{0}", totalAmount.ToString("#,##0.00")), b16u);
                table.AddCell(cell);
                #endregion

                PdfPTable itemTable = getBlankTable(new float[] { 1 });
                itemTable.SpacingBefore = 10;
                cell = new PdfPCell();
                cell.BorderColor = new iTextSharp.text.BaseColor(211, 211, 211);
                cell.BorderWidth = 1.5f;

                cell.AddElement(table);
                itemTable.AddCell(cell);
                document.Add(itemTable);

                #endregion

                tbLeft = getBlankTable(new float[] { 1 });
                tbLeft.SpacingBefore = 10;

                p = new Phrase();
                p.Add(new Chunk("ช่องทางชำระค่าธรรมเนียม", b16));
                p.Add(new Chunk(colon));
                p.Add(new Chunk(report.PaymentChannel, n16g));
                cell0 = new PdfPCell(p);
                cell0.Border = Rectangle.NO_BORDER;
                tbLeft.AddCell(cell0);

                p = new Phrase();
                p.Add(new Chunk("สถานที่ชำระค่าธรรมเนียม", b16));
                p.Add(new Chunk(colon));
                p.Add(new Chunk(report.PaymentAddress, n16g));
                cell0 = new PdfPCell(p);
                cell0.Border = Rectangle.NO_BORDER;
                tbLeft.AddCell(cell0);

                var duedateText = "-";
                if (report.DueDate.HasValue)
                {
                    //if (report.DueDate.Value.Year < 2500) report.DueDate = report.DueDate.Value.AddYears(543);
                    duedateText = report.DueDate.Value.ToString("dd/MM/yyyy");
                }
                
                p = new Phrase();
                p.Add(new Chunk("ชำระเงินภายในวันที่", b16));
                p.Add(new Chunk(colon));
                p.Add(new Chunk(duedateText, n16g));
                cell0 = new PdfPCell(p);
                cell0.Border = Rectangle.NO_BORDER;
                tbLeft.AddCell(cell0);

                document.Add(tbLeft);


                document.Close();
                writer.Close();
                ms.Close();

                return ms.GetBuffer();
            }
        }

        private static System.Drawing.Image GetBarcode(string data)
        {
            iTextSharp.text.pdf.Barcode128 code128 = new iTextSharp.text.pdf.Barcode128(); // barcode type
            code128.Code = data;
            code128.BarHeight = 20f;
            code128.CodeType = iTextSharp.text.pdf.Barcode.CODE128;
            code128.ChecksumText = true;
            code128.GenerateChecksum = true;
            code128.StartStopText = true;
            System.Drawing.Image barCode = code128.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White);

            return barCode;
        }
    }

    public class ConfirmationFormJuristicHeaderFooter : PdfPageEventHelper
    {
        public static BaseFont f_ts = BaseFont.CreateFont(ServerHelper.MapPath("~/fonts/THSarabunNew.ttf"), BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
        public static BaseFont f_tsb = BaseFont.CreateFont(ServerHelper.MapPath("~/fonts/THSarabunBold.ttf"), BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            Font n14g = new Font(f_ts, 14, 0);
            Font n14 = new Font(f_ts, 14);
            Font b14 = new Font(f_tsb, 14);

            PdfPTable tabFot = new PdfPTable(1);
            tabFot.TotalWidth = 600f;
            PdfPCell cell = new PdfPCell(new Phrase("หากมีข้อสงสัยประการใด กรุณาติดต่อ…", n14));
            cell.Border = Rectangle.NO_BORDER;
            //cell.BackgroundColor = new iTextSharp.text.BaseColor(105, 105, 105);
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabFot.AddCell(cell);

            cell = new PdfPCell(new Phrase("ศูนย์รับคำขออนุญาต (One Stop Service) สำนักงานคณะกรรมการพัฒนาระบบราชการ\n59/1 ถนนพิษณุโลก แขวงดุสิต เขตดุสิต กรุงเทพฯ 10300  โทร 02 281 8099", b14));
            cell.Border = Rectangle.NO_BORDER;
            //cell.BackgroundColor = new iTextSharp.text.BaseColor(105, 105, 105);
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.PaddingBottom = 10;
            tabFot.AddCell(cell);

            tabFot.WriteSelectedRows(0, 2, -40, 70, writer.DirectContent);
        }
    }

    public class RoundRectangle : IPdfPCellEvent
    {
        public void CellLayout(
          PdfPCell cell, Rectangle rect, PdfContentByte[] canvas
        )
        {
            PdfContentByte cb = canvas[PdfPTable.LINECANVAS];
            cb.RoundRectangle(
              rect.Left,
              rect.Bottom - 3,
              rect.Width,
              rect.Height,
              4 // change to adjust how "round" corner is displayed
            );
            cb.SetLineWidth(1f);
            cb.SetCMYKColorStrokeF(0f, 0f, 0f, 1f);
            cb.Stroke();
        }
    }
}
