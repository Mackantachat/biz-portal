using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace BizPortal.ViewModels.Apps.DOAStandard
{
    public class DOAPlants
    {
        //สมัครใหม่ แก้ไข
        public class OrganicDetail
        {
            public string ConsumerKey { get; set; }
            public string certificateID { get; set; }
            public string certificateNumber { get; set; }
            public string farmer_group_id { get; set; }
            public string idcard { get; set; }
            public string idcardType { get; set; }
            public string prefixname { get; set; }
            public string name { get; set; }
            public string surname { get; set; }
            public string contact_name { get; set; }
            public string contact_mobile { get; set; }
            public string contact_tel { get; set; }
            public string contact_fax { get; set; }
            public string contact_email { get; set; }
            public string village_no { get; set; }
            public string lane { get; set; }
            public string moo { get; set; }
            public string road { get; set; }
            public string district_id { get; set; }
            public string district_name { get; set; }
            public string amphur_id { get; set; }
            public string amphur_name { get; set; }
            public string province_id { get; set; }
            public string province_name { get; set; }
            public string postcode { get; set; }
            public string tel { get; set; }
            public string mobile { get; set; }
            public string fax { get; set; }
            public string email { get; set; }
            public string document_url { get; set; }
            public GardenInfo garden_info { get; set; }
            public List<GardenItem> garden_item { get; set; }
            public List<DOAFileMetaData> garden_document { get; set; }
            public List<DOAFileMetaData> garden_document_request { get; set; }
            public OrganicSectionModified garden_modified { get; set; }
            public BizPortalInfo bizportal_info { get; set; }

           
        }


        public class OrganicSectionModified
        {
            public bool isLocation { get; set; } //แก้ไขข้อมูลที่ตั้งแหล่งผลิต/ที่ตั้งแปลง
            public string isLocationTitle { get; set; }
            public bool isOrganic{ get; set; } //แก้ไขข้อมูลแผนการผลิตพืชอินทรีย์
            public string isOrganicTitle { get; set; }
            public bool isOrganicAddArea { get; set; } //แก้ไขข้อมูลแผนการผลิตพืชอินทรีย์เพิ่มพื้นที่ ลดพื้นที่ เพิ่มชนิดพืช ลดชนิดพืช
            public string isOrganicAddAreaTitle { get; set; }
            public bool isOrganicReduceArea { get; set; } //แก้ไขข้อมูลแผนการผลิตพืชอินทรีย์เพิ่มพื้นที่ ลดพื้นที่ เพิ่มชนิดพืช ลดชนิดพืช
            public string isOrganicReduceAreaTitle { get; set; }
            public bool isOrganicAddOrgnic { get; set; } //แก้ไขข้อมูลแผนการผลิตพืชอินทรีย์เพิ่มพื้นที่ ลดพื้นที่ เพิ่มชนิดพืช ลดชนิดพืช
            public string isOrganicAddOrgnicTitle { get; set; }
            public bool isOrganicReduceOrgnic { get; set; } //แก้ไขข้อมูลแผนการผลิตพืชอินทรีย์เพิ่มพื้นที่ ลดพื้นที่ เพิ่มชนิดพืช ลดชนิดพืช
            public string isOrganicReduceOrgnicTitle { get; set; }

            public bool isLicence { get; set; } //แก้ไขข้อมูลการขอใบรับรอง
            public string isLicenceTitle { get; set; }
            public bool isContact { get; set; } //แก้ไขข้อมูลบุคคลที่สามารถติดต่อได้สะดวก
            public string isContactTitle { get; set; }
        }

        public enum DOAModifySection
        {
            [StringValue("ที่ตั้งแหล่งผลิต/ที่ตั้งแปลง")]
            Location = 0,
            [StringValue("แผนการผลิตพืชอินทรีย์")]
            Organic = 1,
            [StringValue("ข้อมูลการขอใบรับรอง")]
            Licence = 1,
            [StringValue("ข้อมูลบุคคลที่สามารถติดต่อได้สะดวก")]
            Contact = 1,

        }

        public class OrganicSectionCancel
        {
            public string plantAndProduct { get; set; }//ชนิดพืช/พันธุ์/ผลิตภัณฑ์ * :
            public string framCode { get; set; }//รหัสแปลง * :
            public string licenceCode { get; set; }//รหัสรับรอง * :
            public string framAddressNo { get; set; }//เลขที่ : *
            public string framMooNo { get; set; }//หมู่ที่ :
            public string framSoi { get; set; }//ตรอก/ซอย :
            public string framRoad { get; set; }//ถนน
            public string framBuilding { get; set; }//อาคาร :
            public string framRoomNo { get; set; }//ห้องเลขที่ :
            public string framFloorNo { get; set; }//ชั้น :

            public string framProvinceCode { get; set; }
            public string framProvinceName { get; set; }
            public string framAmphurCode { get; set; }
            public string framAmphurName { get; set; }
            public string framTambonCode { get; set; }
            public string framTambonName { get; set; }
            public string framPostCode { get; set; }
            public string framTel { get; set; }
            public string framTelExt { get; set; }
            public string framFax { get; set; }


        }
        //ต่อายุ ยกเลิก
        public class OrganicDetailRenew
        {
            public string ConsumerKey { get; set; }
            public string idcard { get; set; }
            public string certificateID { get; set; }
            public GardenInfo garden_info { get; set; }
            public List<GardenItem> garden_item { get; set; }
            public List<DOAFileMetaData> garden_document { get; set; }
            public List<DOAFileMetaData> garden_document_request { get; set; }
            public BizPortalInfo bizportal_info { get; set; }
        }

        public class OrganicDetailCancel
        {
            public string ConsumerKey { get; set; }
            public string idcard { get; set; }
            public string certificateID { get; set; }
            public string Remark { get; set; } //เหตุผลในการขอยกเลิก * :
            public List<OrganicSectionCancel> cancelDetail { get; set; }
            public GardenInfo garden_info { get; set; }
            public List<GardenItem> garden_item { get; set; }
            public List<DOAFileMetaData> garden_document { get; set; }
            public List<DOAFileMetaData> garden_document_request { get; set; }
            public BizPortalInfo bizportal_info { get; set; }
        }

        public class Plants
        {
            [JsonProperty("id")]
            public string ID { get; set; }
            [JsonProperty("name")]
            public string NAME { get; set; }
            [JsonProperty("typeid")]
            public string TYPEID { get; set; }
        }
        public class TypePlants
        {
            [JsonProperty("id")]
            public string ID { get; set; }
            [JsonProperty("name")]
            public string NAME { get; set; }
        }
        public class GardenInfo
        {            
            public string address_type { get; set; } //1 ที่อยู่สำนักงาน/ตามบัตรประชาชน  2 ที่ตั้งแปลง
            public string area_size { get; set; }
            public string plant_type_id { get; set; }
            public string plant_type_name { get; set; }            
            public string village_no { get; set; }
            public string lane { get; set; }
            public string moo { get; set; }
            public string road { get; set; }
            public string district_id { get; set; }
            public string amphur_id { get; set; }
            public string province_id { get; set; }
            public string postcode { get; set; }
            public string tel { get; set; }
            public string mobile { get; set; }
            public string fax { get; set; }
            public string email { get; set; }
            public string certificate_type { get; set; }
            public string th_name { get; set; }
            //public string th_address { get; set; }
            public doa_address th_address { get; set; }
            public string eng_name { get; set; }
            //public string eng_address { get; set; }
            public doa_address eng_address { get; set; }
            public bool request_hallmark { get; set; }
            /*ประเภทการขอข้อมูลใบรับรอง 
                1 ใช้ข้อมูลเกษตรกร
                2 ชื่ออื่นภาษาไทย
                3 ภาษาอังกฤษ
                4 ชื่ออื่นภาษาไทย-ภาษาอังกฤษ
            */
            public bool request_certificate_type_1 { get; set; }//ใช้ข้อมูลเกษตรกร
            public bool request_certificate_type_2 { get; set; }//ชื่ออื่นภาษาไทย
            public bool request_certificate_type_3 { get; set; }//ภาษาอังกฤษ
            public bool request_certificate_type_4 { get; set; }//null



            public document_detail doc_type_1 { get; set; }//โฉนดที่ดิน
            public document_detail doc_type_2 { get; set; }//น.ส.2
            public document_detail doc_type_3 { get; set; }//น.ส.3
            public document_detail doc_type_4 { get; set; }//น.ส.3.ก
            public document_detail doc_type_5 { get; set; }//ปส.23
            public document_detail doc_type_6 { get; set; }//ส.ป.ก
            public document_detail doc_type_7 { get; set; }//ก.ส.น.5
            public document_detail doc_type_8 { get; set; }//น.ค.3
            public document_detail doc_type_9 { get; set; }//ส.ค.1
            public document_detail doc_type_10 { get; set; }//เอกสารรับรองจากหน่วยงานที่เกี่ยวข้อง
            public string doc_type_description { get; set; }//เลขที่เอกสารสิทธิการใช้ประโยชน์ที่ดิน * :
        }
        public enum DoaTypeDocument
        {
            [StringValue("โฉนดที่ดิน")]
            chk1,
            [StringValue("น.ส.2")]
            chk2,
            [StringValue("น.ส.3")]
            chk3,
            [StringValue("น.ส.3.ก")]
            chk4,
            [StringValue("ปส.23")]
            chk5,
            [StringValue("ส.ป.ก")]
            chk6,
            [StringValue("ก.ส.น.5")]
            chk7,
            [StringValue("น.ค.3")]
            chk8,
            [StringValue("ส.ค.1")]
            chk9,
            [StringValue("เอกสารรับรองจากหน่วยงานที่เกี่ยวข้อง")]
            chk10      
        }
        public class document_detail
        {
            public bool is_check { get; set; }
            public string  description { get; set; }//เลขที่เอกสารสิทธิการใช้ประโยชน์ที่ดิน * :

        }
        public class doa_address
        {
            public string address_no { get; set; }
            public string moo { get; set; }
            public string village { get; set; }
            public string soi { get; set; }
            public string building { get; set; }
            public string room { get; set; }
            public string floor { get; set; }
            public string road { get; set; }
            public string province_code { get; set; }
            public string province_name { get; set; }
            public string amphur_code { get; set; }
            public string amphur_name { get; set; }
            public string tambon_code { get; set; }
            public string tambon_name { get; set; }
            public string post_code { get; set; }           
        }
        public class GardenItem
        {
            public int plant_id { get; set; }
            public string plant_name { get; set; }
            public double area_size { get; set; }
            public double plant_qty { get; set; }
            public int cycle_qty { get; set; }
            public int start_harvest { get; set; }
            public int end_harvest { get; set; }
            public string date_harvest { get; set; }
            public double qty { get; set; }
        }
        public class DOAFileMetaData
        {
            public string Name { get; set; }
            public string Content { get; set; }
            public string FileName { get; set; }
            public string ContentType { get; set; }
            public string Description { get; set; }
        }
        public enum DOAidcardTyep
        {
            [StringValue("personal")]
            personal = 0,
            [StringValue("company")]
            company = 1,
         
        }
        public class BizPortalInfo
        {
            public string ApplicationRequestID { get; set; }
            public string ApplicationRequestNumber { get; set; }
            public int ApplicationID { get; set; }           
            public string IdentityID { get; set; }
            public string IdentityType { get; set; }
            public string IdentityName { get; set; }
            public string RequsetDateTime { get; set; }
            public string RequestAt { get; set; }
            public string RequsetTypeValue { get; set; }
            public string RequsetTypeName { get; set; }
           
        }
        public class GetCertificate
        {
            public string ConsumerKey { get; set; }
            public string idcard { get; set; }
            public string certificateNumber { get; set; }
        }
        public class ListCertificate
        {
            public string certificateNumber { get; set; }
            public string idcard { get; set; }
        }

       


    }
}
