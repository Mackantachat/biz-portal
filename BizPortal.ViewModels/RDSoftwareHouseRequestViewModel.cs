using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels
{
    public class RDSoftwareHouseRequestViewModel
    {
        /// <summary>
        /// เลขที่คำขอของ bizportal
        /// </summary>
        public string APPLICATIONREQUESTNUMBER { get; set; }

        /// <summary>
        /// วันที่ยื่นคำขอ
        /// </summary>
        public string CREATEDDATE { get; set; }

        /// <summary>
        /// แจ้งผู้ประกอบการ(วันที่)
        /// </summary>
        public string TIN { get; set; }

        /// <summary>
        /// ชื่อนำหน้า(เช่น บริษัท)
        /// </summary>
        public string TTITLE { get; set; }

        /// <summary>
        /// ชื่อ
        /// </summary>
        public string FNAME { get; set; }

        /// <summary>
        /// เลขประจำตัวซอฟต์แวร์เฮ้าส์
        /// </summary>
        public string IDRDSW { get; set; }

        /// <summary>
        /// ชื่อซอฟต์แวร์
        /// </summary>
        public string SOFTNAME { get; set; }

        /// <summary>
        /// เวอร์ชั่นซอฟต์แวร์
        /// </summary>
        public string SWVERSION { get; set; }

        /// <summary>
        /// ชนิดของซอฟต์แวร์ ตามมาตรฐานซอฟต์แวร์(ชนิด ก)
        /// </summary>
        public int SOFTTYPE1 { get; set; }

        /// <summary>
        /// ชนิดของซอฟต์แวร์ ตามมาตรฐานซอฟต์แวร์(ชนิด ข)
        /// </summary>
        public int SOFTTYPE2 { get; set; }

        /// <summary>
        /// ชนิดของซอฟต์แวร์ ตามมาตรฐานซอฟต์แวร์(ชนิด ค)
        /// </summary>
        public int SOFTTYPE3 { get; set; }

        /// <summary>
        /// ชนิดของซอฟต์แวร์ ตามมาตรฐานซอฟต์แวร์(ชนิด ง)
        /// </summary>
        public int SOFTTYPE4 { get; set; }

        /// <summary>
        /// คุณสมบัติและข้อมูลของผู้ประกอบการ(1. เป็นผู้เขียนหรือพัฒนาซอฟต์แวร์เพื่อจำหน่าย)
        /// </summary>
        public int TYPETAX1 { get; set; }

        /// <summary>
        /// คุณสมบัติและข้อมูลของผู้ประกอบการ(2. เป็นผู้จำหน่ายซอฟต์แวร์ที่เขียนหรือพัฒนาในประเทศไทย หรือที่เขียนหรือพัฒนาจากต่างประเทศ)
        /// </summary>
        public int TYPETAX2 { get; set; }

        /// <summary>
        /// คุณสมบัติและข้อมูลของผู้ประกอบการ(3. เป็นตัวแทนจำหน่ายซอฟต์แวร์ที่เขียนหรือพัฒนาในประเทศไทย หรือที่เขียนหรือพัฒนาจากต่างประเทศ)
        /// </summary>
        public int TYPETAX3 { get; set; }

        /// <summary>
        /// คุณสมบัติและข้อมูลของผู้ประกอบการ(4. เป็นผู้นำเข้าซอฟต์แวร์จากต่างประเทศ หรือเขียนซอฟต์แวร์หรือพัฒนาซอฟต์แวร์เพื่อใช้ในกิจการของตนเอง และได้นำซอฟต์แวร์ดังกล่าวไปให้บริษัทหรือห้างหุ้นส่วนนิติบุคคลในเครือเดียวกันใช้ด้วย)
        /// </summary>
        public int TYPETAX4 { get; set; }

        /// <summary>
        /// เอกสารแนบบประกอบพิจารณา(ผังการทำงานรวมฯ)
        /// </summary>
        public int DOC1 { get; set; }

        /// <summary>
        /// เอกสารแนบบประกอบพิจารณา(คู่มือการใช้ซอฟต์แวร์ฯ)
        /// </summary>
        public int DOC2 { get; set; }

        /// <summary>
        /// เอกสารแนบบประกอบพิจารณา(สำเนาบัตรประจำตัวประชาชนฯ)
        /// </summary>
        public int DOC3 { get; set; }

        /// <summary>
        /// เอกสารแนบบประกอบพิจารณา(บัตรประจำตัวผู้เสียภาษีอากรฯ)
        /// </summary>
        public int DOC4 { get; set; }

        /// <summary>
        ///  เอกสารแนบบประกอบพิจารณา(หนังสือรับรองการจดทะเบียนนิตบุคคลฯ)
        /// </summary>
        public int DOC5 { get; set; }

        /// <summary>
        /// กิจการประเภท ซื้อ-ขาย
        /// </summary>
        public int FORBUS1 { get; set; }

        /// <summary>
        ///  กิจการประเภท บริการ
        /// </summary>
        public int FORBUS2 { get; set; }

        /// <summary>
        /// กิจการประเภท ผลิต
        /// </summary>
        public int FORBUS3 { get; set; }

        /// <summary>
        /// กิจการประเภท อื่นๆ
        /// </summary>
        public int FORBUS4 { get; set; }

        /// <summary>
        /// ขนาดของกิจการ 1=เล็ก,2=กลาง,3=ใหญ่,4=เล็ก-กลาง,5=กลาง-ใหญ่,6=อิ่นๆ
        /// </summary>
        public int SIZEBUSINESS { get; set; }

        /// <summary>
        /// การขออนุมัติ(เลขที่หนังสือ)
        /// </summary>
        public string DOCRD { get; set; }

        /// <summary>
        /// การขออนุมัติ(ลงวันที่) yyyy-MM-dd
        /// </summary>
        public string DATEDOCRD { get; set; }

        /// <summary>
        /// ผลการดำเนินงาน(ผล) 0=ไม่เห็นชอบ,1=เห็นชอบ
        /// </summary>
        public int RDSTS { get; set; }

        /// <summary>
        /// ผลการดำเนินงาน(วันที่อธิบดีอนุมัติ) yyyy-MM-dd
        /// </summary>
        public string DATERD { get; set; }

        /// <summary>
        /// แจ้งผู้ประกอบการ(เลขที่หนังสือ)
        /// </summary>
        public string RDDOCTAX { get; set; }

        /// <summary>
        /// แจ้งผู้ประกอบการ(วันที่) yyyy-MM-dd
        /// </summary>
        public string RDDATEDOCTAX { get; set; }

        /// <summary>
        /// แจ้งหน่วยปฏิบัติ(เลขที่หนังสือ)
        /// </summary>
        public string RDDOCSN { get; set; }

        /// <summary>
        /// แจ้งหน่วยปฏิบัติ(วันที่) yyyy-MM-dd
        /// </summary>
        public string RDDATEDOCSN { get; set; }

        /// <summary>
        /// จำนวนโมดูล
        /// </summary>
        public string NUMMODULE { get; set; }

        /// <summary>
        /// ชื่อโมดูล กรณีมีหลายโมดูลใช้ ^  เช่น   module1name3^modulename32^modulename3
        /// </summary>
        public string NAMEMODULE { get; set; }

        /// <summary>
        /// อาคาร
        /// </summary>
        public string BLGNAME { get; set; }

        /// <summary>
        /// หมู่บ้าน
        /// </summary>
        public string VILLAGE { get; set; }

        /// <summary>
        /// ห้องที่
        /// </summary>
        public string ROOMNO { get; set; }

        /// <summary>
        /// ชั้นที่
        /// </summary>
        public string FLOORNO { get; set; }

        /// <summary>
        /// เลขที่
        /// </summary>
        public string ADDNO { get; set; }

        /// <summary>
        /// หมู่ที่
        /// </summary>
        public string MOONO { get; set; }

        /// <summary>
        /// ซอย
        /// </summary>
        public string SOINAM { get; set; }

        /// <summary>
        /// แยก
        /// </summary>
        public string YAK { get; set; }
        
        /// <summary>
        /// ถนน
        /// </summary>
        public string THNNAM { get; set; }

        /// <summary>
        /// ตำบล
        /// </summary>
        public string TAMCOD { get; set; }

        /// <summary>
        /// อำเถอ
        /// </summary>
        public string AMPCOD { get; set; }

        /// <summary>
        /// จังหวัด
        /// </summary>
        public string PROVCOD { get; set; }

        /// <summary>
        /// รหัสไปรษณีย์
        /// </summary>
        public string POSCOD { get; set; }

        /// <summary>
        /// รหัสหน่วยงาน
        /// </summary>
        public string OFFCD { get; set; }
    }

    public class RDSoftwareHouseRenewViewModel
    {
        /// <summary>
        /// เลขที่คำขอของ bizportal
        /// </summary>
        public string APPLICATIONREQUESTNUMBER { get; set; }

        /// <summary>
        /// วันที่ยื่นคำขอ
        /// </summary>
        public string CREATEDDATE { get; set; }

        /// <summary>
        /// หมายเหตุ(หน้าบันทึกการซื้อขายซอฟต์แวร์)
        /// </summary>
        public string datesale { get; set; }

        /// <summary>
        /// เลขประจำตัวผู้เสียภาษีอากร(ผู้ขาย)
        /// </summary>
        public string tinsale { get; set; }

        /// <summary>
        /// เลขซอฟร์แวร์เฮ้าส์
        /// </summary>
        public string IDRDSW { get; set; }

        /// <summary>
        /// เลขประจำตัวผู้เสียภาษีอากร(ผู้ซื้อ)
        /// </summary>
        public string tinbuy { get; set; }

        /// <summary>
        /// ชื่อซอฟต์แวร์
        /// </summary>
        public string swname { get; set; }

        /// <summary>
        /// เวอร์ชั่นซอฟต์แวร์
        /// </summary>
        public string swversion { get; set; }

        /// <summary>
        /// เลขลำดับของซอฟต์แวร์
        /// </summary>
        public string Idbuy { get; set; }

        /// <summary>
        /// สถานการณ์เลือกระบบงาน(บางระบบ/ทั้งระบบ)
        /// </summary>
        public string typebuy { get; set; }

        /// <summary>
        /// ปีที่บันทึกแบบ
        /// </summary>
        public string year { get; set; }

        /// <summary>
        /// รหัสหน่วยงานที่บันทึกแบบ
        /// </summary>
        public string offcd { get; set; }

        /// <summary>
        /// Autonumber
        /// </summary>
        public string req { get; set; }

        /// <summary>
        /// Timestamp เก็บตอนบันทึกแบบ
        /// </summary>
        public string rectimestamp { get; set; }

        /// <summary>
        /// หมายเหตุ(หน้าบันทึกการซื้อขายซอฟต์แวร์)
        /// </summary>
        public string remark { get; set; }


        /// <summary>
        /// สาขาของผู้ซื้อ
        /// </summary>
        public string branobuy { get; set; }

        public RDSoftwareHouseRenewDetailViewModel Detail { get; set; }
    }

    public class RDSoftwareHouseRenewDetailViewModel
    {
        /// <summary>
        /// หมายเหตุ(หน้าบันทึกการซื้อขายซอฟต์แวร์)
        /// </summary>
        public string datesale { get; set; }

        /// <summary>
        /// เลขประจำตัวผู้เสียภาษีอากร(ผู้ขาย)
        /// </summary>
        public string tinsale { get; set; }

        /// <summary>
        /// เลขซอฟร์แวร์เฮ้าส์
        /// </summary>
        public string IDRDSW { get; set; }

        /// <summary>
        /// เลขประจำตัวผู้เสียภาษีอากร(ผู้ซื้อ)
        /// </summary>
        public string tinbuy { get; set; }

        /// <summary>
        /// ชื่อซอฟต์แวร์
        /// </summary>
        public string swname { get; set; }

        /// <summary>
        /// เวอร์ชั่นซอฟต์แวร์
        /// </summary>
        public string swversion { get; set; }

        /// <summary>
        /// เลขลำดับของซอฟต์แวร์
        /// </summary>
        public string Idbuy { get; set; }

        /// <summary>
        /// รหัส idmodule เชื่อมกับ Table TmoduleSoftware
        /// </summary>
        public string idmodule { get; set; }


        /// <summary>
        /// ปีที่บันทึกแบบ
        /// </summary>
        public string year { get; set; }

        /// <summary>
        /// รหัสหน่วยงานที่บันทึกแบบ
        /// </summary>
        public string offcd { get; set; }

        /// <summary>
        /// Autonumber
        /// </summary>
        public string req { get; set; }

        /// <summary>
        /// Timestamp เก็บตอนบันทึกแบบ
        /// </summary>
        public string rectimestamp { get; set; }
    }
}
