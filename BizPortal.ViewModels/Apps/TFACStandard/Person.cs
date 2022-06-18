using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels.Apps.TFACStandard
{
    public class Person
    {
        public int Person_Type_ID { get; set; }
        public string ID_No { get; set; }
        public string CPA_No { get; set; }
        public string Accounting_No { get; set; }
        public string Title_TH { get; set; }
        public string First_Name_TH { get; set; }
        public string Last_Name_TH { get; set; }
        public int Is_FullTime { get; set; }
        public int Is_Authorize { get; set; }
    }

        public enum PersonType
        {
            [StringValue("Committee")]
             Committee = 1,

            [StringValue("Manager")]
             Manager = 2,

            [StringValue("Accountant")]
             Accountant = 3,

            [StringValue("Asst_Accountant")]
             Asst_Accountant = 4,

            [StringValue("Auditor")]
             Auditor = 5
         }

    }
