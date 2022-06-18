using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Utils
{
    public class EXCIRSEUtility
    {
        public static Dictionary<int, string> fileTypeRefs = null;
        public static Dictionary<int, string> GetFileTypeRef()
        {
            fileTypeRefs = new Dictionary<int, string>();

            fileTypeRefs.Add(101, "ID_CARD_COPY");//สำเนาบัตรประประชาชน (ผู้ขออนุญาต)
            fileTypeRefs.Add(102, "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE");//สำเนาบัตรประชาชน (ผู้รับมอบอำนาจ)
            fileTypeRefs.Add(103, "INFORMATION_STORE_BUILDING_OWNER_ID_CARD");//สำเนาบัตรประชาชน (ผู้ให้เช่า หรือเจ้าของบ้าน)
            fileTypeRefs.Add(104, "JURISTIC_COMMITTEE_ID_CARD");//สำเนาบัตรประประชาชาชน (กรรมการบริษัท)

            fileTypeRefs.Add(201, "INFORMATION_STORE_HOUSEHOLD_REGISTRATION_COPY");//สำเนาทะเบียนบ้าน (ให้ตรงกับสถานประกอบการค้า)
            //fileTypeRefs.Add(202, "");//สำเนาทะเบียนบ้าน (ผู้ใช้เช่าที่ตรงกับสถานประกอบการค้า)
            //fileTypeRefs.Add(203, "");//สำเนาทะเบียนบ้าน (กรรมการบริษัท)

            fileTypeRefs.Add(301, "CERTIFICATION_OF_COMPANY_REGISTRATION_COPY");//หนังสือรับรองเอกสารอื่นๆของกระทรวงพาณิชย์ และสำเนาทะเบียนพาณิชย์ (ออกไม่เกิน 6 เดือน)
            fileTypeRefs.Add(302, "INFORMATION_STORE_RENTAL_CONTRACT");//หนังสือสัญญาเช่าสถานที่ และหนังสือยินยอมให้ใช้สถานที่
            fileTypeRefs.Add(303, "JURISTIC_AUTHORIZATION_AUTHORIZE_DOC");//หนังสือมอบอำนาจพร้อมติดอากรแสตมป์


            fileTypeRefs.Add(401, "INFORMATION_STORE_MAP");//แผนที่ตั้งของสถานประกอบการ
            fileTypeRefs.Add(501, "FREE_DOC");//เอกสารอื่นๆ

            return fileTypeRefs;
        }
    }
}
