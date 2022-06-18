using ExpressiveAnnotations.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace BizPortal.ViewModels
{


    public class ApplicationTransactionViewModel
    {
        public ApplicationStatusV2Enum Status { get; set; }

        public string StatusOther { get; set; }

        public string Remark { get; set; }

        public bool IsReplyFromOrg { get; set; }

        public DateTime? TranctionDate { get; set; }
    }
}
