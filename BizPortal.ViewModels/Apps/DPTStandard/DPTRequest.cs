using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels.Apps
{
    public class DPTRequestBase
    {
        /// <summary>
        /// R1 = ข1
        /// R6 = ข6
        /// </summary>
        public string RequestType { get; set; }
        /// <summary>
        /// ขอยื่นคำขอรับใบอนุญาต...ต่อเจ้าหน้าที่
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// เขียนที่ Biz Portal
        /// </summary>
        public string WroteAt { get; set; }
        /// <summary>
        /// ApplicationRequestNumber
        /// </summary>
        public string BizId { get; set; }
        /// <summary>
        /// ApplicationRequestID
        /// </summary>
        public string BizGuid { get; set; }
        /// <summary>
        /// วันที่ยื่น
        /// </summary>
        public DateTime SubmitDate { get; set; }
        /// <summary>
        /// ข้อมูลผู้ยื่น
        /// ผู้ยื่นแบบบุคคลธรรมดา PersonApplicantDto
        /// ผู้ยื่นแบบนิติบุคคล JuristicPersonApplicantDto
        /// </summary>
        public IApplicant Applicant { get; set; }
        /// <summary>
        /// ที่อยู่ผู้ยื่น
        /// </summary>
        public DPTAddress Address { get; set; }
        /// <summary>
        /// รายละเอียดสถานที่ก่อสร้าง
        /// </summary>
        public DPTBuilding[] Buildings { get; set; }
        /// <summary>
        /// รายละเอียดที่ดิน
        /// </summary>
        public DPTTitleDeed[] TitleDeeds { get; set; }
        /// <summary>
        /// วันที่คาดว่าจะใช้
        /// </summary>
        public int EstimateDay { get; set; }
        /// <summary>
        /// หน่วยงานพื้นที่ที่รับผิดชอบ
        /// </summary>
        public string IssueByOrgCode { get; set; }
        public string IssueByOrgName { get; set; }
    }

    public interface IApplicant { }

    public class DPTPersonApplicant : DPTPerson, IApplicant { }

    public class DPTJuristicPersonApplicant : DPTJuristicPerson, IApplicant
    {
        /// <summary>
        /// ผู้รับมอบอำนาจ
        /// </summary>
        public DPTPerson DelegatedPerson { get; set; }
    }

    public class DPTPerson {
        /// <summary>
        /// หมายเลขสมาชิก
        /// </summary>
        public string MemberNo { get; set; }

        /// <summary>
        /// คำนำหน้า
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// ชื่อ
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// นามสกุล
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// เลขที่ประชาชน
        /// </summary>
        public string CitizenId { get; set; }
        /// <summary>
        /// ที่อยู่
        /// </summary>
        public DPTAddress Address { get; set; }
        /// <summary>
        /// ที่อยู่ที่ติดต่อได้
        /// </summary>
        public DPTAddress ContactAddress { get; set; }
        /// <summary>
        /// ช่องทางการติดต่อ
        /// </summary>
        public DPTContact[] Contacts { get; set; }
    }

    public class DPTEngineerArchitect : DPTPerson
    {
        /// <summary>
        /// เลขที่ใบอนุญาต
        /// </summary>
        public string LicenseNo { get; set; }
        /// <summary>
        /// 1 วิศวกร
        /// 2 สถาปนิก
        /// </summary>
        public int EAType { get; set; }
    }

    public class DPTJuristicPerson
    {
        /// <summary>
        /// ชื่อนิติบุคคล
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// เลขที่นิติบุคคล
        /// </summary>
        public string JuristicPersonNo { get; set; }
        /// <summary>
        /// ประเภทนิติบุคคล
        /// "ห้างหุ้นส่วนจำกัด = 1
        /// บริษัทจำกัด = 2
        /// ห้างหุ้นส่วนสามัญ = 3
        /// สมาคม = 4
        /// มูลนิธิ = 5
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// วันที่ละทะเบียนนิติบุคคล
        /// </summary>
        public string RegisterDate { get; set; }
        /// <summary>
        /// ที่อยู่
        /// </summary>
        public DPTAddress Address { get; set; }
        public DPTContact[] Contacts { get; set; }
    }

    public class DPTContact
    {
        /// <summary>
        /// Telephone = 1,
        /// Mobile = 2,
        /// Email = 3,
        /// Fax = 4,
        /// Facebook = 5,
        /// Line = 6,
        /// Website = 7,
        /// </summary>
        public int ContactType { get; set; }
        /// <summary>
        /// ข้อมูล
        /// </summary>
        public string Detail { get; set; }
    }

    public class DPTAddress
    {
        public string AddressNo { get; set; }
        public string VillageNo { get; set; }
        public string Soi { get; set; }
        public string Road { get; set; }
        public string SubDistrict { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
        public string PostCode { get; set; }
        public string GeoCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }

    public class DPTBuilding
    {
        /// <summary>
        /// ชนิดของอาคาร
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// ก่อสร้างเพื่อใช้เป็น
        /// </summary>
        public string[] Purpose { get; set; }
        /// <summary>
        /// จำนวนที่จอดรถ
        /// </summary>
        public string ParkingLot { get; set; }
        /// <summary>
        /// จำนวน เช่น 10
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        /// หน่วย เช่น ห้อง
        /// </summary>
        public string UnitType { get; set; }

        /// <summary>
        /// ขนาดพื้นที่
        /// </summary>
        public string Area { get; set; }
        public string ParkingArea { get; set; }
        public string Remark { get; set; }
    }

    public class DPTTitleDeed
    {
        /// <summary>
        /// ประเภทของที่ดิน 1 = โฉนด, 2 = น.ส.3, 3 = ส.ค.1
        /// </summary>
        public int DeedType { get; set; }
        /// <summary>
        /// เลขที่ โฉนดที่ดิน/น.ส.3/ส.ค.1
        /// </summary>
        public string DeedNo { get; set; }
        /// <summary>
        /// ชื่อเจ้าของ
        /// </summary>
        public string OwnerName { get; set; }
    }
}
