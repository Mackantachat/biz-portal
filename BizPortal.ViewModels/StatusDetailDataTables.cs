using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Extensions;
using BizPortal.ViewModels.Base;

namespace BizPortal.ViewModels
{
    public class JuristicApplicationStatusRequestDetailViewModel
    {
        public int JuristicApplicationStatusRequestLogID { get; set; }
        public string Remark { get; set; }
        public string Status { get; set; }
        public DateTime SubmitDate { get; set; }
        string _SubmitDateTxt = "";
        public string SubmitDateTxt { get { return SubmitDate.ToStringFormat(); } set { _SubmitDateTxt = value; } }
    }
}
