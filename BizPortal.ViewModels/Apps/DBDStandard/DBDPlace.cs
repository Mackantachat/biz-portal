using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels.Apps.DBDStandard
{
    public class HeadOffice : BaseAddress
    {
        public string buildingTH { get; set; }
        public string buildingEN { get; set; }
        public string villageTH { get; set; }
        public string villageEN { get; set; }
        public string soiTH { get; set; }
        public string soiEN { get; set; }
        public string roadTH { get; set; }
        public string roadEN { get; set; }
    }

    public class Branch : BaseAddress
    {
        public int seqNo { get; set; }
        public string information { get; set; }
    }

    public class Warehouse : BaseAddress
    {
        public int seqNo { get; set; }
        public string information { get; set; }
    }

    public class Office
    {
        public int officeCode { get; set; }
        public string officeNameTH { get; set; }
        public string officeNameEN { get; set; }
        public string tumbonCode { get; set; }
        public string amphurCode { get; set; }
        public string provinceCode { get; set; }
        public string activeFlag { get; set; }
    }

}
