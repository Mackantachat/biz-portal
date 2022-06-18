using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Models.Reports
{
    public class BillPaymentFormOSSPDFModel
    {
        public BillPaymentFormOSSPDFModel()
        {
            PaymentItems = new List<PaymentItem>();
        }

        public string CompanyName { get; set; }
        public string CustomerName { get; set; }
        public bool IsJuristic { get; set; }
        public string IdentityID { get; set; }
        public string TaxID { get; set; }
        public DateTime RequestDateTime { get; set; }
        public string IPAddress { get; set; }

        public string OrgTel { get; set; }
        public string OrganizationName { get; set; }
        public string DocumentTitle { get; set; }
        public string ApplicationNumber { get; set; }

        public string PaymentChannel { get; set; }
        public string PaymentAddress { get; set; }
        public DateTime? DueDate { get; set; }
        public List<PaymentItem> PaymentItems { get; set; }

        public class PaymentItem
        {
            public int Sequence { get; set; }
            public string Title { get; set; }
            public decimal Amount { get; set; }
        }
    }

}
