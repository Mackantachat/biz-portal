using BizPortal.DAL.MongoDB;
using BizPortal.Models;
using BizPortal.ViewModels;
using BizPortal.ViewModels.SingleForm;
using BizPortal.ViewModels.V2;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BizPortal.AppsHook
{
    public interface IAppsHook
    {
        /// <summary>
        /// ถูกเรียกใช้ตอนกำลังจะส่ง Draft data กลับไปที่หน้า SingleForm เพื่อแสดงผลใน Controls
        /// </summary>
        /// <param name="trid"></param>
        /// <param name="model"></param>
        /// <param name="appHookInfo"></param>
        /// <param name="request"></param>
        /// <param name="currentSectionGroup"></param>
        /// <returns></returns>
        bool InvokeSingleForm(Guid trid, string currentSectionGroup, ref SingleFormRequestViewModel model);

        /// <summary>
        /// เรียกใช้งานสำหรับ PreDoc
        /// </summary>
        /// <returns></returns>
        FileMetadataEntity InvokeFilePreDoc(string IdentityID, string FileTypeCode);

        /// <summary>
        /// ถูกเรียกมาหลักประชาชน/ผู้ประกอบการส่งคำร้อง
        /// </summary>
        /// <param name="stage"></param>
        /// <param name="model"></param>
        /// <param name="appHookInfo"></param>
        /// <returns></returns>
        InvokeResult Invoke(AppsStage stage, ApplicationRequestViewModel model, AppHookInfo appHookInfo, ref ApplicationRequestEntity request);

        /// <summary>
        /// แสดงข้อมูลออกทางหน้าจอติดตามสถานะ/รายละเอียดของเจ้าหน้าที่
        /// </summary>
        /// <param name="stage"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        RenderDataResult RenderData(RenderStage stage, ApplicationRequestViewModel model);

        Dictionary<string, string> TranslateKeyValue(ApplicationRequestViewModel model);
        
        /// <summary>
        /// ใบอนุญาตนี้จะแสดงสรุปค่าธรรมเนียมในหน้าตรวจสอบข้อมูลหรือไม่
        /// </summary>
        bool ShowPermitSummaryInSingleFormConfirmScreen { get; }
        /// <summary>
        /// คำนวณค่าธรรมเนียมการขอใบอนุญาต
        /// </summary>
        /// <param name="sectionData">ข้อมูบที่กรอกใน Single Form</param>
        /// <returns>ค่าธรรมเนียม</returns>
        decimal? CalculateFee(List<ISectionData> sectionData);

        /// <summary>
        /// คำนวณค่าธรรมเนียมการส่งใบอนุญาตทางไปรษณีย์
        /// </summary>
        /// <param name="sectionData">ข้อมูบที่กรอกใน Single Form</param>
        /// <returns>ค่าธรรมเนียมการส่งใบอนุญาตทางไปรษณีย์</returns>
        decimal CalculateEMSFee(List<ISectionData> sectionData);

        /// <summary>
        /// สามารถรับใบอนุญาตในวันที่ชำระเงินได้เลยหรือไม่
        /// </summary>
        bool PermitCanBeDeliveredOnPayment { get; }

        /// <summary>
        /// ขื่อ View สำหรับหน้าเจ้าหน้าที่
        /// </summary>
        string DetailViewName { get; }
        /// <summary>
        /// ชื่อ View สำหรับหน้า ผปปก
        /// </summary>
        string TrackDetailViewName { get; }

        /// <summary>
        /// ต้องพิมพ์รายละเอียดคำร้องลงบนฟอร์ม pdf ของหน่วยงานหรือไม่
        /// </summary>
        bool HasOrgPdfForm { get; }

        /// <summary>
        /// คืนค่า byte[] ของ pdf ฟอร์มของหน่วยงานที่ fill ข้อมูลตามที่กรอกมาในคำร้อง
        /// </summary>
        /// <param name="req">คำร้อง</param>
        /// <param name="serverMapPathFunc">Func สำหรับ Map Path server</param>
        /// <returns>byte[] ของไฟล์ pdf</returns>
        byte[] GetOrgPdfFormContent(ApplicationRequestEntity req, Func<string, string> serverMapPathFunc);

        /// <summary>
        /// Title ที่จะแสดงบนหัวฟอร์มที่พิมพ์ออกมาในหน้าเจ้าหน้าที่ (ปกติจะพิมพ์ชื่อใบอนุญาตให้อยู่แล้ว return null ถ้าไม่ต้องการเปลี่ยน)
        /// </summary>
        string PrintFormTitle { get; }
        /// <summary>
        /// ข้อความตอนพิมพ์หัวกระดาษขวา
        /// </summary>
        string PrintFormHeaderRight { get; }

        bool IsEnabledChat();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool IsEnabledExportData(ApplicationStatusV2Enum status);

        /// <summary>
        /// สร้าง Json Request Data
        /// </summary>
        /// <param name="ApplicationRequestID"></param>
        /// <returns></returns>
        string GenerateRequestData(Guid ApplicationRequestID);

        /// <summary>
        /// สร้าง Json e-license Data
        /// </summary>
        /// <param name="ApplicationRequestID"></param>
        /// <returns></returns>
        JObject GenerateELicenseData(Guid ApplicationRequestID);

        /// <summary>
        /// สร้างใบเสร็จรับเงิน
        /// </summary>
        ApplicationRequestTransactionEntity GenerateBizReceipt(ApplicationRequestEntity request, ApplicationRequestTransactionEntity trans, ApplicationUser user);
    }
}
