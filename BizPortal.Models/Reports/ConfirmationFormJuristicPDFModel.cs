using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Models.Reports
{
    public class ConfirmationFormJuristicPDFModel
    {
        public string PreferFileName { get; set; }
        public string CompanyName { get; set; }//นิติบุคคล
        public string CompanyID { get; set; }//เลขทะเบียนนิติบุคคล
        public string CompanyVatID { get; set; }  //เลขประจำตัวผู้เสียภาษีอากร
        public string RequestorName { get; set; }//ผู้ขออนุญาต
        public DateTime RequestDateTime { get; set; }//วันที่ยื่นคำขอ + เวลา
        public List<ApplicationRequest> Requests { get; set; } //รายการใบอนุญาตที่ยื่นขอ เรียงลำดับตาม list
        public Nullable<bool> IsNormal { get; set; }//ระบุว่าเป็นบุลคลธรรมดา
        public string IPAddress { get; set; }
        public class ApplicationRequest
        {
            public string RequestID { get; set; }//เลขที่คำขอ
            public string ApplicationName { get; set; }//รายการคำขอ
            public string OrgName { get; set; } //หน่วยงาน
            public int? Duration { get; set; } //ระยะเวลาที่คาดว่าจะดำเนินการจนเสร็จสิ้น (หน่วยเป็น วัน)
            public ApplicationStatusV2Enum RequestStatus { get; set; }
        }

    }
}
