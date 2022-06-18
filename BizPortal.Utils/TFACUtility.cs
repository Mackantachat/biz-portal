using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Utils
{

    public static class TFACUtility
    {
        public static Dictionary<int, string> JulisticType = null;
        public static Dictionary<int, string> CorporateServiceType = null;
        public static Dictionary<int, string> RateType = null;
        public static Dictionary<int, string> RequestDateType = null;
        public static Dictionary<string, string> DocumentType = null;

        public static Dictionary<int, string> GetJulisticType()
        {
            JulisticType = new Dictionary<int, string>();
            JulisticType.Add(1, "ห้างหุ้นส่วนสามัญนิติบุคคล");
            JulisticType.Add(2, "ห้างหุ้นส่วนจำกัด");
            JulisticType.Add(3, "บริษัทจำกัด");
            JulisticType.Add(4, "บริษัทมหาชนจำกัด");
            JulisticType.Add(5, "นิติบุคคลต่างประเทศที่ประกอบธุรกิจในประเทศไทย");
            return JulisticType;

        }

        public static Dictionary<int, string> GetCorporateServiceType()
        {
                CorporateServiceType = new Dictionary<int, string>();

                CorporateServiceType.Add(1, "ACCOUNTING");
                CorporateServiceType.Add(2, "AUDITORING");
                CorporateServiceType.Add(3, "BOTH");

                return CorporateServiceType;
            

        }

        public static Dictionary<int, string> GetRateType()
        {
            RateType = new Dictionary<int, string>();

            RateType.Add(1, "คำนวณจากทุน");
            RateType.Add(2, "คำนวณจากรายได้");
          
            return RateType;


        }

        public static Dictionary<int, string> GetRequestDateType()
        {
            RequestDateType = new Dictionary<int, string>();
            RequestDateType.Add(1, "REGISTER_DATE");
            RequestDateType.Add(2, "START_DATE");
            RequestDateType.Add(3, "CHANGE_DATE");
            RequestDateType.Add(4, "OTHER_DATE");


            return RequestDateType;
        }

        //public static Dictionary<int, string> GetDocumentType1()
        //{
        //    DocumentType = new Dictionary<int, string>();

        //    DocumentType.Add(1, "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY");//หนังสือรับรองการจดทะเบียนนิติบุคคล ไม่เกิน 3 เดือน
        //    DocumentType.Add(2, "JURISTIC_COMMITTEE_ID_CARD");//สำเนาบัตรประจำตัวประชาชนของกรรมการ/หุ้นส่วนผู้จัดการผู้มีอำนาจลงนาม
        //    DocumentType.Add(3, "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC");//หนังสือมอบอำนาจให้กระทำการแทนนิติบุคคล (ถ้ามี)
        //   // DocumentType.Add(3, "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD");
        //   // DocumentType.Add(3, "JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD");
        //    DocumentType.Add(4, "ACCOUNTING_SERVICE_STATEMENT");//ข้อมูลในงบกำไรขาดทุน หรือสำเนางบการเงิน ย้อนหลัง 1 ปี กรณีเป็นนิติบุคคลที่จดทะเบียนกับสภาวิชาชีพบัญชีครั้งแรก (ถ้ามี)
        //    DocumentType.Add(5, "ACCOUNTING_SERVICE_PAYMENT_PROOF");//หลักฐานการชำระค่าดำเนินการจดทะเบียนของนิติบุคคล (กรณีชำระผ่านธนาคาร หรือแคชเชียร์เช็ค)
        //    DocumentType.Add(6, "ACCOUNTING_SERVICE_COPY_COLLATERAL");//สำเนาหลักประกัน
        //    DocumentType.Add(7, "ACCOUNTING_SERVICE_COPY_STATEMENT_PREVIOUS");//สำเนางบการเงินของปีก่อน (กรณีที่งบการเงินล่าสุดยังไม่ได้ตรวจสอบและแสดงความเห็น โดยผู้สอบบัญชี)
        //    DocumentType.Add(8, "ACCOUNTING_SERVICE_DETAIL_PROOF");//หลักฐานการชำระค่าดำเนินการแจ้งรายละเอียดเกี่ยวกับหลักประกันฯ (กรณีชำระผ่านธนาคาร หรือแคชเชียร์เช็ค)
        //    DocumentType.Add(9, "FREE_DOC");//เอกสารอื่นๆ



        //    return DocumentType;
        //}

        public static Dictionary<string, string> GetDocumentType()
        {
            DocumentType = new Dictionary<string, string>();

            DocumentType.Add("CERTIFICATION_OF_COMPANY_REGISTRATION_COPY","1");//หนังสือรับรองการจดทะเบียนนิติบุคคล ไม่เกิน 3 เดือน
            DocumentType.Add("JURISTIC_COMMITTEE_ID_CARD","2");//สำเนาบัตรประจำตัวประชาชนของกรรมการ/หุ้นส่วนผู้จัดการผู้มีอำนาจลงนาม
            DocumentType.Add("JURISTIC_AUTHORIZATION_AUTHORIZE_DOC","3");//หนังสือมอบอำนาจให้กระทำการแทนนิติบุคคล (ถ้ามี)
            DocumentType.Add("JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD","2");
            DocumentType.Add("JURISTIC_AUTHORIZATION_AUTHORIZOR_ID_CARD","2");
            DocumentType.Add("ACCOUNTING_SERVICE_STATEMENT","4");//ข้อมูลในงบกำไรขาดทุน หรือสำเนางบการเงิน ย้อนหลัง 1 ปี กรณีเป็นนิติบุคคลที่จดทะเบียนกับสภาวิชาชีพบัญชีครั้งแรก (ถ้ามี)
            DocumentType.Add("ACCOUNTING_SERVICE_PAYMENT_PROOF","5");//หลักฐานการชำระค่าดำเนินการจดทะเบียนของนิติบุคคล (กรณีชำระผ่านธนาคาร หรือแคชเชียร์เช็ค)
            DocumentType.Add("ACCOUNTING_SERVICE_COPY_COLLATERAL","6");//สำเนาหลักประกัน
            DocumentType.Add("ACCOUNTING_SERVICE_COPY_STATEMENT_PREVIOUS","7");//สำเนางบการเงินของปีก่อน (กรณีที่งบการเงินล่าสุดยังไม่ได้ตรวจสอบและแสดงความเห็น โดยผู้สอบบัญชี)
            DocumentType.Add("ACCOUNTING_SERVICE_DETAIL_PROOF","8");//หลักฐานการชำระค่าดำเนินการแจ้งรายละเอียดเกี่ยวกับหลักประกันฯ (กรณีชำระผ่านธนาคาร หรือแคชเชียร์เช็ค)
            DocumentType.Add("FREE_DOC","9");//เอกสารอื่นๆ



            return DocumentType;
        }
        public static string GetDateFormat(string str_Date)
        {
            string result_DateFormatEn = string.Empty;
            if (!String.IsNullOrEmpty(str_Date))
            {

                try
                {
                   // string[] tempsplit = str_Date.Split('/');

                   // string newdateFormat = tempsplit[2] + "/" + tempsplit[1] + "/" + tempsplit[0];

                    var DateFormatEn = DateTime.Parse(str_Date, new CultureInfo("th-TH"));

                    return result_DateFormatEn = DateFormatEn.ToString("yyyy-MM-dd", new CultureInfo("en-US"));


                }
                catch (Exception ex)
                {
                    return "";
                }
            }

            return "";
        }
    }
}
