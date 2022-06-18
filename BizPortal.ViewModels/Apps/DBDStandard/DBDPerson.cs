using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels.Apps.DBDStandard
{
    public class Owner : BasePerson { }
    public class Manager : BasePerson
    {
        public int seqNo { get; set; }
    }
    public class Partner : BasePerson
    {
        public int seqNo { get; set; }
        public double investAmt { get; set; }
        public string investCode { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }

        public string status { get; set; }
    }

    public class Agent : BasePerson
    {
        public int seqNo { get; set; }
        public string agentName { get; set; }

    }

}
