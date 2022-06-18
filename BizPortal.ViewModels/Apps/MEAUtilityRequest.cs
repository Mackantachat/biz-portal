using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels.Apps
{
    public class MEAUtilityRequest
    {
        public MEAUtilityRequest()
        {
            NOTI_TYPE = "ไฟใหม่";
            IWERK = 68;
            X = 13.756331;
            Y = 100.501765;
            INSTALL_SIZE = "200A 230/400V 3P 4W";
            ATTACH_FILE = new List<MEAUtilityFile>();
            COMMITTEE_INFORMATION = new List<CommitteeInfo>();
        }

        public string WEBNUM { get; set; }

        public string NOTI_TYPE { get; set; }

        public int IWERK { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        public string NAME { get; set; }

        public string BRANCH { get; set; }

        public string ZCRN { get; set; }

        public string ZTAX20 { get; set; }

        public string TELNO { get; set; }

        public string CONTACT_IDNO { get; set; }

        public string CONTACT_FIRSTNAME { get; set; }

        public string CONTACT_LASTNAME { get; set; }

        public string CONTACT_TEL { get; set; }

        public string CONTACT_EMAIL { get; set; }

        public string INSTALL_SIZE { get; set; }

        public string CO_HOUSEID { get; set; }

        public string CO_HOUSE_NUM1 { get; set; }

        public string CO_FLOOR { get; set; }

        public string CO_VILLAGE { get; set; }

        public string CO_SOI { get; set; }

        public string CO_STREET { get; set; }

        public string CO_SUBDISTRICT { get; set; }

        public string CO_DISTRICT { get; set; }

        public string CO_PROVINCE { get; set; }

        public string CO_POSTCODE { get; set; }

        public string BP_HOUSE_NUM1 { get; set; }

        public string BP_FLOOR { get; set; }

        public string BP_VILLAGE { get; set; }

        public string BP_SOI { get; set; }

        public string BP_STREET { get; set; }

        public string BP_SUBDISTRICT { get; set; }

        public string BP_DISTRICT { get; set; }

        public string BP_PROVINCE { get; set; }

        public string BP_POSTCODE { get; set; }

        public string SERVICE_TYPE { get; set; }

        public List<MEAUtilityFile> ATTACH_FILE { get; set; }
        public Guid? ApplicationRequestID { get; set; }
        public int ApplicationID { get; set; }
        public string TITLE { get; set; }
        public string LASTNAME { get; set; }
        public string BIRTHDATE { get; set; }
        public string ZTHID { get; set; }
        public string TSIC_TYPE { get; set; }

        public List<CommitteeInfo> COMMITTEE_INFORMATION { get; set; }
    }

    public class CommitteeInfo
    {
        public string ID { get; set; }

        public string NAME { get; set; }
    }

    public class MEAUtilityFile
    {
        public string FileName { get; set; }

        public string FileDescription { get; set; }

        public string Base64String { get; set; }

        public string ContentType { get; set; }
    }
}
