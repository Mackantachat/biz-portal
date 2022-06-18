using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Models.Reports
{
    public class BillPaymentFormPDFModel
    {
        public string CustomerName { get; set; }
        public string ProductCode { get; set; }

        /// <summary>
        /// 1 digit
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// 13 digits
        /// </summary>
        public string TaxID { get; set; }

        /// <summary>
        /// 2 digits code
        /// </summary>
        public string ServiceCode { get; set; }

        /// <summary>
        /// 18 digits
        /// </summary>
        public string RefCode1 { get; set; }

        /// <summary>
        /// 18 digits
        /// </summary>
        public string RefCode2 { get; set; }
        public DateTime? DueDate { get; set; }
        public List<PaymentItem> PaymentItems { get; set; }

        public string BarcodeData
        {
            get
            {
                string barcode = "";
                decimal total = PaymentItems.Sum(o => o.Amount);
                barcode = string.Format("{0}{1}{2}{3}{4}{5}"
                                        , Prefix.Substring(0, 1)
                                        , TaxID
                                        , ServiceCode.PadLeft(2, '0')
                                        , RefCode1.PadLeft(18, '0')
                                        , RefCode2.PadLeft(18, '0')
                                        , total.ToString("0").PadLeft(10, '0'));

                return barcode;
            }
        }

        public BillPaymentFormPDFModel()
        {
            PaymentItems = new List<PaymentItem>();
        }

        public class PaymentItem
        {
            public int Sequence { get; set; }
            public string Title { get; set; }
            public decimal Amount { get; set; }
        }
    }
}
