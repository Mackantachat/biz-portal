using BizPortal.Areas.WebApi.Controllers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.SingleForm;
using BizPortal.Utils.Extensions;
using Mapster;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;
using BizPortal.AppsHook;
using System.Globalization;

namespace BizPortal.Areas.WebApiV2.Controllers
{
    [Authorize]
    public class SingleFormRequestsController : ApiControllerBase
    {
        private static CultureInfo _cultureTH = new CultureInfo("th-TH");

        [HttpGet]
        [Route("Api/V2/SingleForm/Draft")]
        public SingleFormRequestViewModel GetDraft(Guid? trid = null, string sectionGroup = null)
        {
            var model = SingleFormRequestEntity.Get(IdentityID, IdentityType, new ApplicationStatusV2Enum[] { ApplicationStatusV2Enum.DRAFT });

            if (model == null || (model != null && model.Status != ApplicationStatusV2Enum.DRAFT))
            {
                return null;
            }

            var result = model.Adapt<SingleFormRequestEntity, SingleFormRequestViewModel>();

            // Frontis: Set request date as current time instead of value from previous request. The value may differ from display text in seconds.
            if (sectionGroup == "INFORMATION")
            {
                var generalInfo = result.SectionData.FirstOrDefault(o => o.SectionName == "GENERAL_INFORMATION");
                if (generalInfo != null)
                    generalInfo.FormData.AddOrUpdate("INFORMATION_HEADER__REQUEST_DATE", DateTime.Now.ToString("d/M/yyyy HH:mm:ss"));

                // ไม่ให้เรียกข้อมูลของนิติบุคคล เนื่องจากมีการเรียกใช้ข้อมูลอัพเดทล่าสุดจาก DBD Juristic v3 โดยไม่ดึงข้อมูลจาก draft
                if (IdentityType == UserTypeEnum.Juristic)
                {
                    var generalSection = result.SectionData.Where(o => o.SectionName == "GENERAL_INFORMATION").SingleOrDefault();
                    if (generalSection != null && generalSection.FormData.Count > 0)
                    {
                        result.SectionData.Remove(generalSection);

                        var fields = new string[]
                        {
                            "COMPANY_NAME_TH",
                            "COMPANY_NAME_EN",
                            "IDENTITY_ID",
                            "REGISTER_CAPITAL",
                            "REGISTER_DATE"
                        };
                        foreach (var field in fields)
                        {
                            if (generalSection.FormData.ContainsKey(field))
                                generalSection.FormData.Remove(field);
                        }
                        result.SectionData.Add(generalSection);
                    }

                    var addrSection = result.SectionData.Where(o => o.SectionName == "JURISTIC_ADDRESS_INFORMATION").SingleOrDefault();
                    if (addrSection != null && addrSection.FormData.Count > 0)
                    {
                        result.SectionData.Remove(addrSection);

                        var fields = new string[]
                        {
                            "ADDRESS_JURISTIC_HQ_ADDRESS",
                            "ADDRESS_MOO_JURISTIC_HQ_ADDRESS",
                            "ADDRESS_VILLAGE_JURISTIC_HQ_ADDRESS",
                            "ADDRESS_BUILDING_JURISTIC_HQ_ADDRESS",
                            "ADDRESS_FLOOR_JURISTIC_HQ_ADDRESS",
                            "ADDRESS_ROOMNO_JURISTIC_HQ_ADDRESS",
                            "ADDRESS_SOI_JURISTIC_HQ_ADDRESS",
                            "ADDRESS_ROAD_JURISTIC_HQ_ADDRESS",
                            "ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS",
                            "ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS_TEXT",
                            "ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS",
                            "ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS_TEXT",
                            "ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS",
                            "ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS_TEXT",
                            "ADDRESS_POSTCODE_JURISTIC_HQ_ADDRESS"
                        };
                        foreach (var field in fields)
                        {
                            if (addrSection.FormData.ContainsKey(field))
                                addrSection.FormData.Remove(field);
                        }
                        result.SectionData.Add(addrSection);
                    }
                }
            }
            
            #region Call appHook to load some data from external sources
            if (trid.HasValue)
            {
                var tran = SingleFormTransaction.Get(trid.Value);

                if (tran != null)
                {
                    var apps = tran.Apps.ToArray();

                    foreach (string appSysName in apps)
                    {
                        var app = DB.Applications.FirstOrDefault(o => o.ApplicationSysName == appSysName);

                        if (app != null && !string.IsNullOrEmpty(app.AppsHookClassName))
                        {
                            var hook = (IAppsHook)Activator.CreateInstance("BizPortal.AppsHook", app.AppsHookClassName).Unwrap();

                            if (!hook.InvokeSingleForm(trid.Value, sectionGroup, ref result))
                            {
                                throw new Exception("Data not found");

                                // Frontis: เนื่องจากบาง application อาจไม่มี AppHook method นี้  การ set default ให้ return false จะทำให้ใบเหล่านั้นเกิด error จนดึงข้อมูลไม่ได้
                                //  อาจต้องเปลี่ยนให้ InvokeSingleForm return default เป็น true  หรือทำให้  hookResult เป็น object เหมือนเดิม
                            }
                        }
                    }
                }
            }
            #endregion

            return result;
        }

        [HttpGet]
        [Route("Api/V2/SingleForm/File")]
        public List<object> GetFile()
        {
            SingleFormTransaction tran = SingleFormTransaction.Get(IdentityID);
            if (tran == null || (tran != null && (tran.UploadedFiles == null || tran.UploadedFiles.Count == 0)))
            {
                return null;
            }

            var result = new List<object>();
            foreach (var group in tran.UploadedFiles)
            {
                result.Add(group);
            }

            return result;
        }
    }
}
