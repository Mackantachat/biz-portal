using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.DAL.MongoDB
{
    public class ActionApplicationRequestModel
    {
        public ApplicationStatusV2Enum Status { get; set; }
        public string StatusOther { get; set; }
        public bool ReplyFromOrg { get; set; }
        public string PermitDeliveryType { get; set; }
        public string StatusBeforeCancel { get; set; }
        public string PaymentMethod { get; set; }
        public bool NoDocument { get; set; }
        public string PermitDeliveryAddress { get; set; }
        public string EMSTrackingNumber { get; set; }
        public int TotalFee { get; set; } 
    }
}
