using BizPortal.Areas.WebApi.Controllers;
using BizPortal.DAL.MongoDB;
using BizPortal.Utils.Helpers;
using BizPortal.ViewModels.V2;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using BizPortal.ViewModels.Report.Page3;
using BizPortal.Models;
using System;
using System.Globalization;
using BizPortal.ViewModels.Report.Page2;
using BizPortal.ViewModels;
using BizPortal.Integrated;
using BizPortal.ViewModels.Select2;
using System.Web.Script.Serialization;

namespace BizPortal.Areas.WebApiV2.Controllers
{
    public class ReportOnProcessRequestController : ApiControllerBase
    {       

        [Route("Api/v2/ReportOnProcessRequestController/GetReportObj")]
        [HttpGet]
        public PermitRequestStatisticsParameterList GetReportObj()
        {
            PermitRequestStatisticsParameterList model = new PermitRequestStatisticsParameterList();
            model.YearList = getRequestYearList();
            model.MonthList = getMonthList();
            model.RequestorTypeList = getRequestorTypeList();
            model.OrganizationList = getOrganizationList();
            model.PermitList = getPermitList();
            return model;
        }

        private List<Select2Opt> getRequestYearList()
        {
            var collection = MongoFactory.GetApplicationRequestCollection();
            var repo = collection.AsQueryable();
            var query = repo.AsQueryable();
            var yearList = query.Select(x => new ApplicationRequestSearchResultViewModel
            {
                CreatedDate = x.CreatedDate
            }).ToList();
            var years = yearList.GroupBy(x => x.CreatedDate.Year).Select(y => new Select2Opt
            {
                ID = y.Key.ToString(),
                Text = "พ.ศ." + (y.Key + 543).ToString()
            }).ToList();
            return years;
        }

        private List<Select2Opt> getMonthList()
        {
            string[] month = CultureInfo.GetCultureInfo("th-TH").DateTimeFormat.MonthNames;
            var monthsList = month.Where(x => !string.IsNullOrEmpty(x)).Select(x => new Select2Opt
            {
                ID = DateTime.ParseExact(x, "MMMM", CultureInfo.GetCultureInfo("th-TH")).ToString("MMM", CultureInfo.InvariantCulture),
                Text = x
            }).ToList();
            return monthsList;
        }

        private List<Select2Opt> getRequestorTypeList()
        {
            var identityType = new List<Select2Opt>
            {
                new Select2Opt
                {
                    ID = UserTypeEnum.Citizen.ToString(),
                    Text = "บุคคลธรรมดา"
                },
                new Select2Opt
                {
                    ID = UserTypeEnum.Juristic.ToString(),
                    Text = "นิติบุคคล"
                }
            };
            return identityType;
        }

        private List<Select2Opt> getOrganizationList()
        {
            var isAdmin = User.IsInRole(ConfigurationValues.ROLES_ADMIN_NAME);
            var isOPDCAdmin = User.IsInRole(ConfigurationValues.ROLES_OPDC_ADMIN_NAME);
            var isOPDCAgent = User.IsInRole(ConfigurationValues.ROLES_OPDC_AGENT_NAME);
            var isOrgAdmin = User.IsInRole(ConfigurationValues.ROLES_ORG_ADMIN_NAME);
            var isOrgAgent = User.IsInRole(ConfigurationValues.ROLES_ORG_AGENT_NAME);
            var query = DB.OrganizationTranslations.Where(w => w.Language.TwoLetterISOLanguageName == CurrentCulture).Select(o => new Select2Opt { ID = o.OrgCode.ToString(), Text = o.OrgName });

            if (isAdmin || isOPDCAdmin)
            {
                return query.ToList();
            }
            else if (isOrgAdmin || isOPDCAgent || isOrgAgent)
            {
                return query.Where(x => x.ID == OrganizationID).ToList();
            }
            else
            {
                return null;
            }
        }

        private List<Select2Opt> getPermitList()
        {
            var isAdmin = User.IsInRole(ConfigurationValues.ROLES_ADMIN_NAME);
            var isOPDCAdmin = User.IsInRole(ConfigurationValues.ROLES_OPDC_ADMIN_NAME);
            var isOPDCAgent = User.IsInRole(ConfigurationValues.ROLES_OPDC_AGENT_NAME);
            var isOrgAdmin = User.IsInRole(ConfigurationValues.ROLES_ORG_ADMIN_NAME);
            var isOrgAgent = User.IsInRole(ConfigurationValues.ROLES_ORG_AGENT_NAME);
            var currentLaguageId = CurrentCulture == "th" ? 1 : 2;
            var manageServices = getManageServiceList(CurrentUserID, OrganizationID);
            var manageServiceIds = manageServices.Select(e => e.Value).ToList();
            var appTranslationName = DB.ApplicationTranslations.Where(w => w.Language.TwoLetterISOLanguageName == CurrentCulture).Select(o => new Select2Opt { ID = o.ApplicationID.ToString(), Text = o.ApplicationName });

            if (isAdmin || isOPDCAdmin || isOPDCAgent || isOrgAdmin)
            {
                return manageServices.Select(x => new Select2Opt
                {
                    ID = x.Value,
                    Text = appTranslationName.Where(y => y.ID == x.Value).FirstOrDefault().Text

                }).ToList();
            }
            else
            {
                var memberServices = DB.MemberServices
                                       .Include(e => e.Application.Organization)
                                       .Include(e => e.Application.ApplicationTranslations)
                                       .Where(e => e.UserID == CurrentUserID && !e.IsDeleted)
                                       .Select(e => new MemberServiceViewModel
                                       {
                                           OrgCode = e.Application.OrgCode,
                                           OrgName = e.Application.Organization.OrgSysName,
                                           ServiceId = e.ApplicationID,
                                           Service = e.Application.ApplicationTranslations.Where(t => t.ApplicationID == e.ApplicationID && t.LanguageID == currentLaguageId).Select(t => t.ApplicationName).FirstOrDefault(),
                                           Areas = e.MemberServiceAreas
                                                    .Where(f => f.ProvinceID > 0 && !f.IsDeleted)
                                                    .Select(f => new MemberServiceAreaViewModel
                                                    {
                                                        ProvinceId = f.ProvinceID,
                                                        Province = f.Province,
                                                        DistrictId = f.DistrictID,
                                                        District = f.District,
                                                        SectionId = f.SectionID,
                                                        Section = f.Section
                                                    })
                                                    .OrderBy(o => o.Province)
                                                    .ThenBy(o => o.District)
                                                    .ThenBy(o => o.Section)
                                                    .ToList(),
                                           IsCanManage = (isAdmin || isOPDCAdmin) ? true : manageServiceIds.Contains(e.ApplicationID.ToString())
                                       })
                                       .OrderBy(e => e.IsCanManage)
                                       .ThenBy(e => e.OrgName)
                                       .ThenBy(e => e.Service)
                                       .ToList();

                return memberServices.Select(x => new Select2Opt
                {
                    ID = x.ServiceId.ToString(),
                    Text = x.Service
                }).ToList();
            }
        }

        private List<System.Web.Mvc.SelectListItem> getManageServiceList(string id, string orgCode)
        {
            var currentLanguageId = CurrentCulture == "th" ? 1 : 2;
            var manageServiceList = new List<System.Web.Mvc.SelectListItem>();

            if (User.IsInRole(ConfigurationValues.ROLES_ADMIN_NAME) || User.IsInRole(ConfigurationValues.ROLES_OPDC_ADMIN_NAME))
            {
                var serviceList = DB.ApplicationTranslations
                                    .Include(e => e.Application)
                                    .Include(e => e.Application.Organization)
                                    .Where(w => w.Language.TwoLetterISOLanguageName == CurrentCulture && !w.Application.IsDeleted)
                                    .ToList();

                manageServiceList = serviceList.Select(e => new System.Web.Mvc.SelectListItem() { Value = e.ApplicationID.ToString(), Text = e.ApplicationName + " - " + e.Application.Organization.OrgSysName, Group = new System.Web.Mvc.SelectListGroup { Name = e.Application.OrgCode } })
                                               .OrderBy(e => e.Value == orgCode)
                                               .ThenBy(e => e.Text)
                                               .ToList();
            }
            else
            {
                var memberManageList = DB.MemberManageServices
                                         .Include(e => e.Application.Organization)
                                         .Include(e => e.Application.ApplicationTranslations)
                                         .Where(e => e.UserID == id && !e.IsDeleted)
                                         .ToList();

                if (memberManageList.Count > 0)
                {
                    manageServiceList = memberManageList.Select(e => new System.Web.Mvc.SelectListItem() { Value = e.ApplicationID.ToString(), Text = e.Application.ApplicationTranslations.Where(t => t.ApplicationID == e.ApplicationID && t.LanguageID == currentLanguageId).Select(t => t.ApplicationName).FirstOrDefault() + " - " + e.Application.Organization.OrgSysName, Group = new System.Web.Mvc.SelectListGroup { Name = e.Application.OrgCode } })
                                                        .OrderBy(e => e.Value == orgCode)
                                                        .ThenBy(e => e.Text)
                                                        .ToList();
                }
                else
                {
                    var serviceList = DB.ApplicationTranslations
                                    .Include(e => e.Application)
                                    .Include(e => e.Application.Organization)
                                    .Include(e => e.Application.ApplicationTranslations)
                                    .Where(w => w.Language.TwoLetterISOLanguageName == CurrentCulture && w.Application.OrgCode == orgCode && !w.Application.IsDeleted)
                                    .ToList();

                    manageServiceList = serviceList.Select(e => new System.Web.Mvc.SelectListItem() { Value = e.ApplicationID.ToString(), Text = e.ApplicationName + " - " + e.Application.Organization.OrgSysName, Group = new System.Web.Mvc.SelectListGroup { Name = e.Application.OrgCode } })
                                                   .OrderBy(e => e.Value == orgCode)
                                                   .ThenBy(e => e.Text)
                                                   .ToList();
                }
            }
            return manageServiceList;
        }

        [Route("Api/v2/ReportOnProcessRequestController/GetApplicationRequest")]
        [HttpGet]
        public List<ApplicationModel> GetAllApplication()
        {
            List<ApplicationModel> model = new List<ApplicationModel>();
            var collection = MongoFactory.GetApplicationRequestCollection();
            var repo = collection.AsQueryable();
            var query = repo.AsQueryable();            
            foreach (var x in query)
            {
                string strStatus = string.Empty;
                string userType = string.Empty;
                if (x.ExpectedFinishDate == null)
                {
                    strStatus = "ยังไม่เริ่มนับ SLA";
                }
                else
                {
                    if (x.UpdatedDate <= x.ExpectSLADate)
                    {
                        strStatus = "ทันกำหนด";
                    }
                    else if (x.UpdatedDate > x.ExpectSLADate)
                    {
                        strStatus = "ไม่ทันกำหนด";
                    }
                }
                object objExpectDate = null;
                if (x.ExpectedFinishDate == null)
                {
                    objExpectDate = string.Empty;
                }
                else
                {
                    objExpectDate = x.ExpectedFinishDate;
                }

                if (x.IdentityType == UserTypeEnum.Citizen)
                {
                    userType = "บุคคลธรรมดา";
                }
                if (x.IdentityType == UserTypeEnum.Juristic)
                {
                    userType = "นิติบุคคล";
                }

                model.Add(new ApplicationModel
                {
                    CreatedDate = x.CreatedDate,
                    ApplicationRequestNumber = x.ApplicationRequestNumber,
                    PermitName = x.PermitName,
                    IdentityName = x.IdentityName,
                    ExpectedFinishDate = objExpectDate,
                    Status = x.Status.ToString().Equals("CHECK") ? "ตรวจสอบคำขอเบื้องต้น" :
                                         x.Status.ToString().Equals("PENDING") ? "อยู่ระหว่างการพิจารณา" :
                                         x.Status.ToString().Equals("APPROVED_WAITING_PAY_FEE") ? "อนุมัติแล้วรอชำระค่าธรรมเนียม" :
                                         x.Status.ToString().Equals("PAID_FEE_CREATING_LICENSE") ? "ออกใบอนุญาต" :
                                         x.Status.ToString().Equals("REJECTED") ? "ยกเลิกการดำเนินการ" :
                                         x.Status.ToString().Equals("COMPLETED") ? "ยื่นเรื่องเสร็จสมบูรณ์" :
                                         x.Status.ToString().Equals("FAILED") ? "ยื่นคำร้องไม่สำเร็จ" :
                                         x.Status.ToString().Equals("CANCELED") ? "ผู้ประกอบการยกเลิก" : "status",
                    StatusOperation = strStatus,
                    OrgName = x.OrgNameTH,
                    IdentityType = userType,
                    ProvinceID = x.ProvinceID,
                    AmphurID = x.AmphurID,
                    ApplicationID = x.ApplicationID,
                    OrganizationID = x.OrgCode,
                    Year = x.CreatedDate.Year
                });
            }
           
            AppContextModel.ListModel = model;
            return model;
        }

        [Route("Api/v2/ReportOnProcessRequestController/GetAppStatusNbr")]
        [HttpGet]
        public object GetAppStatusNbr()
        {
            List<AppStatusNbr> obj = new List<AppStatusNbr>();
            int countCompApp = 0;
            int countNoCompApp = 0;
            foreach (var x in AppContextModel.ListModel)
            {
                if (x.StatusOperation.Equals("ทันกำหนด"))
                {
                    countCompApp = countCompApp + 1;
                }
                if (x.StatusOperation.Equals("ยังไม่เริ่มนับ SLA"))
                {
                    countNoCompApp = countNoCompApp + 1;
                }
  
            }
           
            obj.Add(new AppStatusNbr
            {
                StrStatus = "ทันกำหนด",
                StatusNbr = countCompApp,
            });
            obj.Add(new AppStatusNbr
            {
                StrStatus = "ยังไม่เริ่มนับ SLA",
                StatusNbr = countNoCompApp,
            });
            return Json(obj);
        }

        [Route("Api/v2/ReportOnProcessRequestController/GetAppSepByOrg")]
        [HttpGet]
        public object GetAppSepOrg()
        {
            List<AppSepByOrg> model = new List<AppSepByOrg>();
            foreach (var x in AppContextModel.ListModel)
            {
                if (model.Any(i => i.OrgName.Equals(x.OrgName)))
                {
                    foreach (var y in model.Where(a => a.OrgName.Equals(x.OrgName)))
                    {
                        y.AppSepOrg = y.AppSepOrg + 1;
                        break;
                    }
                    continue;
                }

                model.Add(new AppSepByOrg
                {
                    OrgName = x.OrgName,
                    AppSepOrg = 1,
                });
            }
            return Json(model);
        }

        [Route("Api/v2/ReportOnProcessRequestController/GetAppSepByApp")]
        [HttpGet]
        public object GetAppSepApp()
        {
            List<AppSepByApp> model = new List<AppSepByApp>();
            foreach (var x in AppContextModel.ListModel)
            {
                if (model.Any(i => i.AppName.Equals(x.PermitName)))
                {
                    foreach (var y in model.Where(a => a.AppName.Equals(x.PermitName)))
                    {
                        y.AppNbr = y.AppNbr + 1;
                        break;
                    }
                    continue;
                }

                model.Add(new AppSepByApp
                {
                    AppName = x.PermitName,
                    AppNbr = 1,
                });
            }
            return Json(model);

        }

        [Route("Api/v2/ReportOnProcessRequestController/GetAppStatusCheck")]
        [HttpGet]
        public object GetAppStatusCheck()
        {

            AppStatusCheck model = new AppStatusCheck();
            foreach (var x in AppContextModel.ListModel.Where(b => b.Status.Equals("ตรวจสอบคำขอเบื้องต้น") || b.Status.Equals("อยู่ระหว่างการพิจารณา") 
            || b.Status.Equals("อนุมัติแล้วรอชำระค่าธรรมเนียม") || b.Status.Equals("ออกใบอนุญาต")))
            {
                switch (x.Status)
                {
                    case "ตรวจสอบคำขอเบื้องต้น":
                        model.CHECK += 1;
                        break;
                    case "อยู่ระหว่างการพิจารณา":
                        model.PENDING += 1;
                        break;
                    case "อนุมัติแล้วรอชำระค่าธรรมเนียม":
                        model.APPROVED_WAITING_PAY_FEE += 1;
                        break;
                    case "ออกใบอนุญาต":
                        model.PAID_FEE_CREATING_LICENSE += 1;
                        break;
                }
            }

            return Json(model);
        }

        [Route("Api/v2/ReportOnProcessRequestController/FilterModel")]
        [HttpPost]
        public object FilterModel([FromBody] ParmJson model)
        {

            DateTime? modeldatestart = null;
            DateTime? modeldatestorp = null;

            if (!string.IsNullOrEmpty(model.CboDateStart))
            {
                modeldatestart = Convert.ToDateTime(model.CboDateStart);
            }          

            if (!string.IsNullOrEmpty(model.CboDateStop))
            {
                modeldatestorp = Convert.ToDateTime(model.CboDateStop);
            }
            
            string CboUserType;
            if (model.CboUserType.Equals("Citizen"))
            {
                CboUserType = "บุคคลธรรมดา";
            }
            else if (model.CboUserType.Equals("Juristic"))
            {
                CboUserType = "นิติบุคคล";
            }
            else
            {
                CboUserType = string.Empty;
            }

            int? CboProvince = null;
            int? CboDistrict = null;
            int? CboPermit = null;

            if (!model.CboProvince.Equals(null))
            {
                CboProvince = Convert.ToInt32(model.CboProvince);
            }

            if (!model.CboDistrict.Equals(null))
            {
                CboDistrict = Convert.ToInt32(model.CboDistrict);
            }
            
            if (!model.CboPermit.Equals(null))
            {
                CboPermit = Convert.ToInt32(model.CboPermit);
            }
            
            int? CboYear = model.CboYear;
            DateTime? CboDateStart = modeldatestart;
            DateTime? CboDateStop = modeldatestorp;
            //CboDateStart = (CboDateStart.Value.Year) + 543
            string CboOrg = model.CboOrg;        

            AppContextModel.R2D2 = AppContextModel.ListModel.Where(o =>
                    (o.IdentityType.Equals((CboUserType.Equals(string.Empty)    ? o.IdentityType    : CboUserType)))    
                 && (o.OrganizationID.Equals((CboOrg.Equals(string.Empty)       ? o.OrganizationID  : CboOrg)))
                && (o.ProvinceID.Equals(CboProvince.Equals(null)                ? o.ProvinceID      : CboProvince))
                && (o.AmphurID.Equals(CboDistrict.Equals(null)                  ? o.AmphurID        : CboDistrict))
                && (o.ApplicationID.Equals(CboPermit.Equals(null)               ? o.ApplicationID   : CboPermit)) 
                && (o.Year.Equals(CboYear.Equals(null)                          ? o.Year            : CboYear))
            ).ToList();

            if (!CboDateStart.Equals(null))
            {
                AppContextModel.R2D2 = AppContextModel.R2D2.Where(o =>
                (o.CreatedDate.Value.Date >= CboDateStart.Value.Date.AddYears(543) 
                    && o.CreatedDate.Value.Date <= CboDateStop.Value.Date.AddYears(543))
                ).ToList();
            }

            return AppContextModel.R2D2;
        }

        [Route("Api/v2/ReportOnProcessRequestController/FilterOverviewChart")]
        [HttpGet]
        public object FilterOverviewChart()
        {
            List<AppStatusNbr> obj = new List<AppStatusNbr>();
            int countCompApp = 0;
            int countNoCompApp = 0;
            foreach (var x in AppContextModel.R2D2)
            {
                if (x.StatusOperation.Equals("ทันกำหนด"))
                {
                    countCompApp = countCompApp + 1;
                }
                if (x.StatusOperation.Equals("ยังไม่เริ่มนับ SLA"))
                {
                    countNoCompApp = countNoCompApp + 1;
                }

            }

            obj.Add(new AppStatusNbr
            {
                StrStatus = "ทันกำหนด",
                StatusNbr = countCompApp,
            });
            obj.Add(new AppStatusNbr
            {
                StrStatus = "ยังไม่เริ่มนับ SLA",
                StatusNbr = countNoCompApp,
            });
            return Json(obj);
        }

        [Route("Api/v2/ReportOnProcessRequestController/FilterSepByOrg")]
        [HttpGet]
        public object FilterSepByOrg()
        {
            List<AppSepByOrg> model = new List<AppSepByOrg>();
            foreach (var x in AppContextModel.R2D2)
            {
                if (model.Any(i => i.OrgName.Equals(x.OrgName)))
                {
                    foreach (var y in model.Where(a => a.OrgName.Equals(x.OrgName)))
                    {
                        y.AppSepOrg = y.AppSepOrg + 1;
                        break;
                    }
                    continue;
                }

                model.Add(new AppSepByOrg
                {
                    OrgName = x.OrgName,
                    AppSepOrg = 1,
                });
            }
            return Json(model);
        }

        [Route("Api/v2/ReportOnProcessRequestController/FilterSepByPermit")]
        [HttpGet]
        public object FilterSepByPermit()
        {
            List<AppSepByApp> model = new List<AppSepByApp>();
            foreach (var x in AppContextModel.R2D2)
            {
                if (model.Any(i => i.AppName.Equals(x.PermitName)))
                {
                    foreach (var y in model.Where(a => a.AppName.Equals(x.PermitName)))
                    {
                        y.AppNbr = y.AppNbr + 1;
                        break;
                    }
                    continue;
                }

                model.Add(new AppSepByApp
                {
                    AppName = x.PermitName,
                    AppNbr = 1,
                });
            }
            return Json(model);

        }

        [Route("Api/v2/ReportOnProcessRequestController/FilterToTableStatusCheck")]
        [HttpGet]
        public object FilterToTableStatusCheck()
        {

            AppStatusCheck model = new AppStatusCheck();
            foreach (var x in AppContextModel.R2D2.Where(b => b.Status.Equals("ตรวจสอบคำขอเบื้องต้น") || b.Status.Equals("อยู่ระหว่างการพิจารณา")
            || b.Status.Equals("อนุมัติแล้วรอชำระค่าธรรมเนียม") || b.Status.Equals("ออกใบอนุญาต")))
            {
                switch (x.Status)
                {
                    case "ตรวจสอบคำขอเบื้องต้น":
                        model.CHECK += 1;
                        break;
                    case "อยู่ระหว่างการพิจารณา":
                        model.PENDING += 1;
                        break;
                    case "อนุมัติแล้วรอชำระค่าธรรมเนียม":
                        model.APPROVED_WAITING_PAY_FEE += 1;
                        break;
                    case "ออกใบอนุญาต":
                        model.PAID_FEE_CREATING_LICENSE += 1;
                        break;
                }
            }

            return Json(model);
        }

    }

    public static class AppContextModel
    {
        public static List<ApplicationModel> ListModel { get; set; }

        public static List<ApplicationModel> R2D2 { get; set; }
    }

    public class ParmJson
    {
        public int? CboYear { get; set; }

        public string CboDateStart { get; set; }

        public string CboDateStop { get; set; }

        public string CboUserType { get; set; }

        public string CboOrg { get; set; }

        public int? CboProvince { get; set; }

        public int? CboDistrict { get; set; }

        public int? CboPermit { get; set; }

    }
}
