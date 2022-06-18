using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Extensions;
namespace BizPortal.ViewModels.Base
{
    public abstract class BaseDatatable
    {
        public string ToolTemplate { get; set; }
        public DateTime UpdatedDate { get; set; }
        string _UpdatedDateTxt = "";
        public string UpdatedDateTxt { get { return UpdatedDate.ToStringFormat(); } set { _UpdatedDateTxt = value; } }
    }
}
