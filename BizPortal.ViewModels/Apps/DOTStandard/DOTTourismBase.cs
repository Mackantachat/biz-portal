using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels.Apps.DOTStandard
{
    public class DOTTourismBase
    {
        public string Token { get; set; }

        public string ApplicationId { get; set; }
        public string ApplicationRequestid { get; set; }
        

        /// <summary>
        /// รหัสเลขที่ใบคำขอของระบบ DOT-TBL
        /// </summary>
        public string RequestFormNo { get; set; }

        /// <summary>
        /// A = เพิ่ม, E = แก้ไข
        /// </summary>
        public string Flag { get; set; }

        
        public string TitleNameTH { get; set; }
        
        public string TitleNameEN { get; set; }
        
        public string FirstNameTH { get; set; }
        
        public string FirstNameEN { get; set; }
        
        public string LastNameTH { get; set; }
        
        public string LastNameEN { get; set; }
        
        public string CardId { get; set; }
        
        /// <summary>
        /// Format YYYY-MM-DD (ค.ส.)
        /// </summary>
        public string CardExpireDate { get; set; }
        
        public string TitleNameTH2 { get; set; }
        
        public string TitleNameEN2 { get; set; }
        
        public string FirstNameTH2 { get; set; }
        
        public string FirstNameEN2 { get; set; }
        
        public string LastNameTH2 { get; set; }
        
        public string LastNameEN2 { get; set; }
        
        public string CardId2 { get; set; }
        
        /// <summary>
        /// Format YYYY-MM-DD (ค.ส.)
        /// </summary>
        public string CardExpireDate2 { get; set; }
        
        /// <summary>
        /// จำนวนสาขา
        /// </summary>
        public string BranchQty { get; set; }
        
        /// <summary>
        /// จดทะเบียนิติบุคคลเลขที่
        /// </summary>
        public string JusRegisterNo { get; set; }
        
        /// <summary>
        /// วันที่จดทะเบียนนิติบุคคล 
        /// Format YYYY-MM-DD (ค.ส.)
        /// </summary>
        public string JusRegisterDate { get; set; }
        
        /// <summary>
        /// ชื่อสำนักงาน ภาษาไทย
        /// </summary>
        public string EstablishmentNameTH { get; set; }
        
        /// <summary>
        /// ชื่อสำนักงาน ภาษาอังกฤษ
        /// </summary>
        public string EstablishmentNameEN { get; set; }
        
        /// <summary>
        /// I = บุคคลธรรมดา, J = นิติบุคคล
        /// </summary>
        public string EstablishmentType { get; set; }
        
        /// <summary>
        /// วันที่ก่อตั้ง
        /// Format YYYY-MM-DD (ค.ส.)
        /// </summary>
        public string EstablishmentDate { get; set; }
        
        /// <summary>
        /// คำอ่านชื่อสำนักงาน
        /// </summary>
        public string EstablishCall { get; set; }
        
        /// <summary>
        /// เบอร์มือถือสำนักงาน
        /// </summary>
        public string EstablishMobile { get; set; }
        
        /// <summary>
        /// เบอร์โทรศัพท์สำนักงาน
        /// </summary>
        public string EstablishTel { get; set; }
        
        /// <summary>
        /// เบอร์โทรสารสำนักงาน
        /// </summary>
        public string EstablishFax { get; set; }
        
        /// <summary>
        /// เว็บไซต์ของสำนักงาน
        /// </summary>
        public string EstablishWebsite { get; set; }
        
        /// <summary>
        /// อีเมลของสำนักงาน
        /// </summary>
        public string EstablishEmail { get; set; }
        
        public string AddressNameTH { get; set; }
        
        public string AddressNameEN { get; set; }
        
        public string BuildingNameTH { get; set; }
        
        public string BuildingNameEN { get; set; }
        
        public string FloorNameTH { get; set; }
        
        public string FloorNameEN { get; set; }
        
        public string MooNameTH { get; set; }
        
        public string MooNameEN { get; set; }
        
        public string SoiNameTH { get; set; }
        
        public string SoiNameEN { get; set; }
        
        public string VillageNameTH { get; set; }
        
        public string VillageNameEN { get; set; }
        
        public string RoadNameTH { get; set; }
        
        public string RoadNameEN { get; set; }
        
        public string SubDistrictNameTH { get; set; }
        
        public string SubDistrictNameEN { get; set; }
        
        public string DistrictNameTH { get; set; }
        
        public string DistrictNameEN { get; set; }
        
        public string ProvinceNameTH { get; set; }
        
        public string ProvinceNameEN { get; set; }
        
        public string ZipCode { get; set; }

        /// <summary>
        /// หลักประกัน (CA = เงินสด, BA = หนังสือค้ำประกันจากธนาคาร)
        /// </summary>
        public string GuaranteeTypeNameTH { get; set; }

        /// <summary>
        /// จำนวนเงินประกัน
        /// </summary>
        public string GaranteeAmount { get; set; }

        /// <summary>
        /// รหัสธนาคาร เช่น SCB, TMB
        /// </summary>
        public string BankNameTH { get; set; }

        /// <summary>
        /// ประเภทธุรกิจ
        /// </summary>
        public DOTBusinessType[] BusinessType { get; set; }

        /// <summary>
        /// เอกสารแนบ
        /// </summary>
        public DOTDocument[] Documents { get; set; }
    }

    public class DOTBusinessType
    {
        /// <summary>
        /// ประเภทธุรกิจ
        /// - Local (ท้องถิ่น)
        /// - Domestic (ภายในประเทศ)
        /// - Inbound (นำเที่ยวจากต่างประเทศ)
        /// - Outbound (ทั่วไป)
        /// </summary>
        public string BusinessType { get; set; }

        /// <summary>
        /// แผนการท่องเที่ยวกี่ครั้ง/ปี
        /// </summary>
        public string Qty { get; set; }
        
        /// <summary>
        /// รายละเอียดเพิ่มเติม
        /// </summary>
        public string Remark { get; set; }
    }

    public class DOTDocument
    {
        public string FileName { get; set; }
        
        public string DocumentType { get; set; }
        
        public string Base64 { get; set; }
        
        public string Remark { get; set; }
    }
}
