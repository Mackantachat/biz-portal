using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels
{
    public class MemberDataTables : DataTables
    {
        public string Role { get; set; }

        public string OrgCode { get; set; }

        public string UserType { get; set; }
    }
}
