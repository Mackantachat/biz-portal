using BizPortal.ViewModels.Apps.DPTStandard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels.Apps
{
    public class DPTG1Request : DPTRequestBase
    {
        /// <summary>
        /// ประเภทของใบอนุญาตที่ต้องการยื่นขอ
        /// 1 ก่อสร้างอาคาร
        /// 2 ดัดแปลงอาคาร
        /// 3 เคลื่อนย้ายอาคาร
        /// 4 รื้อถอนอาคาร
        /// </summary>
        public int PurposeType { get; set; }
        /// <summary>
        /// ไฟล์บัตรประชาชน (กรณีประชาชน)
        /// </summary>
        public DPTFileMetaData[] CitizenIDCards { get; set; }
        /// <summary>
        /// แผนผังบริเวณ แบบแปลน
        /// </summary>
        public DPTFileMetaData[] Plans { get; set; }
        /// <summary>
        /// รายการคำนวณ
        /// </summary>
        public DPTFileMetaData[] Calculations { get; set; }
        /// <summary>
        /// หนังสือแสดงความเป็นตัวแทนของเจ้าของอาคาร
        /// </summary>
        public DPTFileMetaData[] DelegationDocuments { get; set; }
        /// <summary>
        /// หนังสือรับรองการจดทะเบียนนิติบุคคล (กรณีนิติบุคคล)
        /// </summary>
        public DPTFileMetaData[] JuristicPersonRegisterationDocuments { get; set; }
        /// <summary>
        /// หนังสือแสดงว่าเป็นผู้จัดการหรือผู้แทนซึ่งเป็นผู้ดำเนินกิจการของนิติบุคคล (กรณีนิติบุคคล)
        /// </summary>
        public DPTFileMetaData[] DelegationRepresentationDocuments { get; set; }
        /// <summary>
        /// รายละเอียด สถาปนิกที่ออกแบบ
        /// </summary>
        public DPTEADocument[] DesignArchitectDocuments { get; set; }
        /// <summary>
        /// รายละเอียด สถาปนิกควมคุมงาน
        /// </summary>
        public DPTEADocument[] SiteArchitectDocuments { get; set; }
        /// <summary>
        /// รายละเอียด วิศวกรที่ออกแบบและคำนวณ
        /// </summary>
        public DPTEADocument[] DesignEngineerDocuments { get; set; }
        /// <summary>
        /// รายละเอียด วิศวกรควบคุมงาน
        /// </summary>
        public DPTEADocument[] SiteEngineerDocuments { get; set; }
        /// <summary>
        /// โฉนดที่ดิน
        /// </summary>
        public DPTFileMetaData[] TitleDeedDocuments { get; set; }
        /// <summary>
        /// เอกสารอื่น ๆ
        /// </summary>
        public DPTFileMetaData[] OtherDocuments { get; set; }
        /// <summary>
        /// เอกสารเพิ่มเติม
        /// </summary>
        public DPTFileMetaData[] AdditionalDocuments { get; set; }
        /// <summary>
        /// เจ้าของอาคาร
        /// </summary>
        public IApplicant[] Owners { get; set; }
    }
}
