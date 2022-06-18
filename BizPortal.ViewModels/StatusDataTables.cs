using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Extensions;
using BizPortal.ViewModels.Base;

namespace BizPortal.ViewModels
{
    //public class JuristicApplicationStatusRequestDataTables : DataTables
    //{
    //    public int JuristicApplicationStatusRequestID { get; set; }
    //    public string ApplicationName { get; set; }
    //    public string OrganizationName { get; set; }
    //    public int? ApplicationStatusID { get; set; }
    //    public string ApplicationStatusOther { get; set; }

    //    public string ApplicationStatusTxt { get { return ""; } }

    //    public DateTime SubmitDate { get; set; }
    //    string _SubmitDateTxt = "";
    //    public string SubmitDateTxt { get { return SubmitDate.ToStringFormat(); } set { _SubmitDateTxt = value; } }       
    //}

    public class JuristicApplicationStatusRequestDataTables : DataTables
    {
        public int? ApplicationID { get; set; }

        public string IdentityID { get; set; }

        public string ApplicationStatusID { get; set; }

        public string ApplicationStatusOther { get; set; }

        public string OrgCode { get; set; }

        public int? ProvinceID { get; set; }

        public int? AmphurID { get; set; }

        public bool ShowCompleted { get; set; }
    }

    public class JuristicApplicationStatusLogRequestDataTables : DataTables
    {
        public int JuristicApplicationStatusRequestID { get; set; }
    }

    public class JuristicApplicationStatusDocumentRequestDataTables : DataTables
    {
        public int JuristicApplicationStatusRequestID { get; set; }
    }
}
