using BizPortal.Areas.WebApi.Controllers;
using BizPortal.DAL.MongoDB;
using BizPortal.Utils.Annotations;
using BizPortal.Utils.Extensions;
using BizPortal.Utils.Helpers;
using BizPortal.ViewModels;
using BizPortal.ViewModels.V2;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.Objects;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BizPortal.Areas.WebApiV2.Controllers
{
    //[FilterIPAddress("::1,164.115.9.140,164.115.15.240,164.115.9.152,164.115.17.22,164.115.17.23,10.10.44.55,10.111.17.22,10.111.17.23")]
    public class DataController : ApiControllerBase
    {
        [HttpGet]
        [Route("Api/V2/Data/ApplicationRequests/List")]
        public List<Dictionary<string,string>> List()
        {
            var status = new ApplicationStatusV2Enum[] {
                    ApplicationStatusV2Enum.COMPLETED,
                    ApplicationStatusV2Enum.CHECK,
                    ApplicationStatusV2Enum.PENDING,
                    ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE,
                    ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE,
                    ApplicationStatusV2Enum.REJECTED,
                    ApplicationStatusV2Enum.WAITING
            };
            var collection = MongoFactory.GetApplicationRequestCollection();
            var repo = collection.AsQueryable();
            var query = repo.AsQueryable();
            var orgnull = query.Where(o => o.OrgCode == null || o.OrgNameTH == null || o.OrgAddress == null).ToList();

            if (orgnull != null && orgnull.Count > 0)
            {
                foreach (var req in orgnull)
                {
                    var vm = DB.Applications.Where(o => !o.IsDeleted && o.ApplicationID == req.ApplicationID)
                            .GroupJoin(DB.ApplicationTranslations.Where(o => o.LanguageID == DB.CurrentLanguageID), l => l.ApplicationID, r => r.ApplicationID, (l, r) => new { Application = l, Translation = r.FirstOrDefault() })
                            .GroupJoin(DB.OrganizationTranslations, l => l.Application.OrgCode, r => r.OrgCode, (l, r) => new { Application = l.Application, Translation = l.Translation, OrgTranslation = r.FirstOrDefault() })
                            .Select(o => new
                            {
                                AppSysName = o.Application.ApplicationSysName,
                                AppName = o.Translation != null ? o.Translation.ApplicationName : o.Application.ApplicationSysName,
                                OrgCode = o.Application.OrgCode,
                                OrganizationName = o.OrgTranslation != null ? o.OrgTranslation.OrgName : o.Application.Organization.OrgSysName,
                                OrgAddress = o.OrgTranslation != null ? o.OrgTranslation.Address : "",
                            }).FirstOrDefault();

                    if (vm != null)
                    {
                        req.PermitName = vm.AppName;
                        req.OrgNameTH = vm.OrganizationName;
                        req.OrgAddress = vm.OrgAddress;
                        req.OrgCode = vm.OrgCode;

                        req.Update();
                    }
                }
            }

            query = query.Where(o => status.Contains(o.Status));// && o.ApplicationRequestNumber != null

            var selectQuery = query.Select(o => new ApplicationRequestDetail
            {
                //ApplicationRequestNumber = o.ApplicationRequestNumber,
                ApplicationName = o.PermitName,
                ApplicationID = o.ApplicationID,
                ApplicationType = o.ApplicationType == null ? "" : o.ApplicationType,
                IdentityID = o.IdentityID,
                IdentityName = o.IdentityName,
                IdentityType = o.IdentityType,
                AreaText = o.Amphur + " " + o.Province,
                CreatedDate = o.CreatedDate,
                UpdatedDate = o.UpdatedDate,
                ExpectSLADate = o.ExpectedFinishDate,
                ExpectedFinishDate = o.ExpectedFinishDate,
                Status = o.Status,
                StatusOther = o.StatusOther == null ? "" : o.StatusOther,
                OrgCode = o.OrgCode,
                Duration = o.Duration,
                ApplicationRequestID = o.ApplicationRequestID,
                ProvinceID = o.ProvinceID,
                Province = o.Province,
                Amphur = o.Amphur,
                AmphurID = o.AmphurID,
                TumbolID = o.TumbolID,
                Tumbol = o.Tumbol
            }).ToList();

            foreach (var data in selectQuery)
            {
                if (data.ApplicationID == 9)
                {
                    ApplicationStatusV2Enum[] invalidStatus = new ApplicationStatusV2Enum[] { ApplicationStatusV2Enum.COMPLETED, ApplicationStatusV2Enum.REJECTED };//, ApplicationStatusV2Enum.FAILED };

                    if (!invalidStatus.Contains(data.Status))
                    {
                        // Set VAT Status manually
                        Dictionary<string, string> vatArgs = new Dictionary<string, string>();
                        vatArgs.Add("TaxID", data.IdentityID);
                        var vatStatus = Api.Get(ConfigurationManager.AppSettings["VAT_STATUS_WS_URL"], vatArgs);

                        if (vatStatus != null && vatStatus.HasValues)
                        {
                            var request = ApplicationRequestEntity.Get(data.ApplicationRequestID);

                            if (vatStatus["responseData"]["VatApr"].ToString() == "1" || vatStatus["responseData"]["VatStatus"].ToString() == "Y")
                            {
                                data.Status = request.Status = ApplicationStatusV2Enum.COMPLETED;
                                data.StatusOther = request.StatusOther = string.Empty;
                            }
                            else if (vatStatus["responseData"]["VatApr"].ToString() == "2")
                            {
                                data.Status = request.Status = ApplicationStatusV2Enum.REJECTED;
                                data.StatusOther = request.StatusOther = string.Empty;
                            }
                            else if (vatStatus["responseData"]["VatApr"].ToString() == "0")
                            {
                                data.Status = request.Status = ApplicationStatusV2Enum.CHECK;
                                data.StatusOther = request.StatusOther = string.Empty;
                            }
                            else
                            {
                                data.Status = request.Status = ApplicationStatusV2Enum.FAILED;
                                data.StatusOther = request.StatusOther = ApplicationStatusOtherValueConst.RESENDABLE;
                            }
                            request.Update();
                        }
                    }

                    ApplicationStatusV2Enum[] pendingStatus = new ApplicationStatusV2Enum[] { ApplicationStatusV2Enum.WAITING, ApplicationStatusV2Enum.CHECK };
                    if (pendingStatus.Contains(data.Status))
                    {
                        data.Status = ApplicationStatusV2Enum.COMPLETED;
                        data.StatusName = Resources.ApplicationStatusRequests.STATUS_PENDING;
                    }
                    else if (data.Status == ApplicationStatusV2Enum.COMPLETED)
                    {
                        data.StatusName = "เป็นผู้ประกอบการจดทะเบียน เว้นแต่เข้าลักษณะที่ไม่ออกใบทะเบียน";
                    }
                    else
                    {
                        data.StatusName = ResourceHelper.GetResourceWord("STATUS_" + data.Status, "ApplicationStatusRequests", CurrentCulture);
                    }
                }
                else
                {
                    data.StatusName = ResourceHelper.GetResourceWord("STATUS_" + data.Status, "ApplicationStatusRequests", CurrentCulture);
                }

                if (data.Duration.HasValue)
                {
                    data.ExpectSLADateTxt = data.ExpectSLADate.HasValue ? data.ExpectSLADate.Value.ToLocalTime().ToString("dd/MM/yyyy") : "";
                }

                data.ApplicationName = DB.ApplicationTranslations.Where(o => o.ApplicationID == data.ApplicationID && o.Language.TwoLetterISOLanguageName == CurrentCulture).Select(o => o.ApplicationName).FirstOrDefault();
                data.StatusOtherName = ResourceHelper.GetResourceWord("STATUS_OTHER_" + data.StatusOther, "ApplicationStatusRequests", CurrentCulture);
                data.OrganizationName = DB.OrganizationTranslations.Where(o => o.OrgCode == data.OrgCode && o.Language.TwoLetterISOLanguageName == CurrentCulture).Select(o => o.OrgName).FirstOrDefault();

                if (string.IsNullOrEmpty(data.IdentityName))
                {
                    string name = DB.Users.Where(o => o.JuristicID == data.IdentityID || o.CitizenID == data.IdentityID).Select(o => o.FullName).FirstOrDefault();

                    if (!string.IsNullOrEmpty(name))
                    {
                        var request = ApplicationRequestEntity.Get(data.ApplicationRequestID);
                        data.IdentityName = request.IdentityName = name;
                        request.Update();
                    }
                }

            }

            var list = new List<Dictionary<string, string>>();

            foreach (var detail in selectQuery)
            {
                Dictionary<string, string> dic = new Dictionary<string, string>()
                {
                    //{"ApplicationRequestNumber",detail.ApplicationRequestNumber},
                    {"ApplicationName",detail.ApplicationName },
                    {"ApplicationID",detail.ApplicationID.ToString()},
                    {"ApplicationType",detail.ApplicationType },
                    {"IdentityID",SecurityHelper.MD5EncryptToHex(detail.IdentityID)},
                    //{"IdentityName",detail.IdentityName },
                    {"IdentityType",detail.IdentityType.GetEnumStringValue() },
                    {"CreatedDate",detail.CreatedDateTxt },
                    {"CreatedYear",CultureInfo.CurrentCulture.Calendar.GetYear(detail.CreatedDate).ToString() },
                    {"CreatedMonth",CultureInfo.CurrentCulture.Calendar.GetMonth(detail.CreatedDate).ToString()},
                    {"UpdatedDate",detail.UpdatedDateTxt },
                    //{"ExpectSLADate",detail.ExpectSLADateTxt == null? "": detail.ExpectSLADateTxt},
                    {"ExpectedFinishDate", detail.ExpectedFinishDate.ToString() },
                    {"Status",detail.StatusName },
                    {"StatusOther",detail.StatusOtherName},
                    {"OrgCode", detail.OrgCode},
                    {"OrgName", detail.OrganizationName},
                    {"TumbolID",detail.TumbolID.ToString() },
                    {"Tumbol",detail.Tumbol == null? "":detail.Tumbol },
                    {"AmphurID", detail.AmphurID.ToString()},
                    {"AmphurText", detail.Amphur == null? "":detail.Amphur},
                    {"ProvinceID",detail.ProvinceID.ToString()},
                    {"ProvinceText", detail.Province == null? "":detail.Province}
                };
                list.Add(dic);
            }

            return list;
        }

        [HttpGet]
        [Route("Api/V2/Data/GetApplications")]
        public List<Organization> Get()
        {
            List<Organization> result = new List<Organization>();
            var status = new ApplicationStatusV2Enum[] {
                    ApplicationStatusV2Enum.COMPLETED,
                    ApplicationStatusV2Enum.CHECK,
                    ApplicationStatusV2Enum.PENDING,
                    ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE,
                    ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE,
                    ApplicationStatusV2Enum.REJECTED,
                    ApplicationStatusV2Enum.WAITING
            };

            var repo = MongoFactory.GetApplicationRequestCollection().AsQueryable();
            var query = repo.Select(o => o).Where(o => status.Contains(o.Status)).ToList();

            var orgnull = query.Where(o => o.OrgCode == null || o.OrgNameTH == null || o.OrgAddress == null).ToList();
            if (orgnull != null && orgnull.Count > 0)
            {
                foreach (var req in orgnull)
                {
                    var vm = DB.Applications.Where(o => !o.IsDeleted && o.ApplicationID == req.ApplicationID)
                            .GroupJoin(DB.ApplicationTranslations.Where(o => o.LanguageID == DB.CurrentLanguageID), l => l.ApplicationID, r => r.ApplicationID, (l, r) => new { Application = l, Translation = r.FirstOrDefault() })
                            .GroupJoin(DB.OrganizationTranslations, l => l.Application.OrgCode, r => r.OrgCode, (l, r) => new { Application = l.Application, Translation = l.Translation, OrgTranslation = r.FirstOrDefault() })
                            .Select(o => new
                            {
                                AppSysName = o.Application.ApplicationSysName,
                                AppName = o.Translation != null ? o.Translation.ApplicationName : o.Application.ApplicationSysName,
                                OrgCode = o.Application.OrgCode,
                                OrganizationName = o.OrgTranslation != null ? o.OrgTranslation.OrgName : o.Application.Organization.OrgSysName,
                                OrgAddress = o.OrgTranslation != null ? o.OrgTranslation.Address : "",
                            }).FirstOrDefault();

                    if (vm != null)
                    {
                        req.PermitName = vm.AppName;
                        req.OrgNameTH = vm.OrganizationName;
                        req.OrgAddress = vm.OrgAddress;
                        req.OrgCode = vm.OrgCode;

                        req.Update();
                    }
                }
            }

            //var query = repo.GroupBy(o => new { o.ApplicationID, o.OrgCode, o.OrgNameTH }).ToList();
            var q = query.Select(o => o).Where(o => o.ApplicationID != 0).ToList();
            var orgs = q.Where(o => o.OrgCode != null || o.OrgNameTH != null).GroupBy(o => new { o.OrgCode, o.OrgNameTH }).ToList();
            var appTransDb = DB.ApplicationTranslations.Where(o => o.LanguageID == 1).ToList();

            foreach (var org in orgs)
            {
                Organization organization = new Organization();
                var apps = query.Select(o => o).Where(o => o.OrgCode == org.Key.OrgCode).GroupBy(o => new { o.ApplicationID }).ToList();
                var total = query.Where(o => o.OrgCode == org.Key.OrgCode && status.Contains(o.Status) && o.ApplicationRequestNumber != null).Count();

                organization.OrgCode = org.Key.OrgCode;
                organization.OrgName = org.Key.OrgNameTH;
                organization.Total = total;
                organization.Applications = new List<Application>();

                foreach (var app in apps)
                {
                    var appName = appTransDb.Where(o => o.ApplicationID == app.Key.ApplicationID).Select(o => o.ApplicationName).SingleOrDefault();
                    var appDetail = query.Where(o => o.ApplicationID == app.Key.ApplicationID && o.OrgCode == org.Key.OrgCode && status.Contains(o.Status) && o.ApplicationRequestNumber != null);

                    Application application = new Application();
                    application.ApplicationID = app.Key.ApplicationID.ToString();
                    application.ApplicationName = appName;
                    application.Total = appDetail.Count();
                    application.ApplicationDetails = new List<Dictionary<string,string>>();

                    //Get App Detail
                    //var appDetail = repo.Where(o => o.OrgCode == org.Key.OrgCode && o.ApplicationID == app.Key.ApplicationID && status.Contains(o.Status) && o.ApplicationRequestNumber != null);
                    var selectQuery = appDetail.Select(o => new ApplicationRequestDetail {
                        ApplicationRequestNumber = o.ApplicationRequestNumber,
                        ApplicationName = o.PermitName,
                        ApplicationID = o.ApplicationID,
                        IdentityID = o.IdentityID,
                        IdentityName = o.IdentityName,
                        IdentityType = o.IdentityType,
                        AreaText = o.Amphur + " " + o.Province,
                        CreatedDate = o.CreatedDate,
                        UpdatedDate = o.UpdatedDate,
                        ExpectSLADate = o.ExpectedFinishDate,
                        Status = o.Status,
                        StatusOther = o.StatusOther == null ? "" : o.StatusOther,
                        OrgCode = o.OrgCode,
                        Duration = o.Duration, 
                        ApplicationRequestID = o.ApplicationRequestID,                     
                        ProvinceID = o.ProvinceID,
                        Province = o.Province,
                        Amphur = o.Amphur,
                        AmphurID = o.AmphurID,
                    }).ToList();

                    foreach (var data in selectQuery)
                    {
                        if (data.ApplicationID == 9)
                        {
                            ApplicationStatusV2Enum[] invalidStatus = new ApplicationStatusV2Enum[] { ApplicationStatusV2Enum.COMPLETED, ApplicationStatusV2Enum.REJECTED };//, ApplicationStatusV2Enum.FAILED };

                            if (!invalidStatus.Contains(data.Status))
                            {
                                // Set VAT Status manually
                                Dictionary<string, string> vatArgs = new Dictionary<string, string>();
                                vatArgs.Add("TaxID", data.IdentityID);
                                var vatStatus = Api.Get(ConfigurationManager.AppSettings["VAT_STATUS_WS_URL"], vatArgs);

                                if (vatStatus != null && vatStatus.HasValues)
                                {
                                    var request = ApplicationRequestEntity.Get(data.ApplicationRequestID);

                                    if (vatStatus["responseData"]["VatApr"].ToString() == "1" || vatStatus["responseData"]["VatStatus"].ToString() == "Y")
                                    {
                                        data.Status = request.Status = ApplicationStatusV2Enum.COMPLETED;
                                        data.StatusOther = request.StatusOther = string.Empty;
                                    }
                                    else if (vatStatus["responseData"]["VatApr"].ToString() == "2")
                                    {
                                        data.Status = request.Status = ApplicationStatusV2Enum.REJECTED;
                                        data.StatusOther = request.StatusOther = string.Empty;
                                    }
                                    else if (vatStatus["responseData"]["VatApr"].ToString() == "0")
                                    {
                                        data.Status = request.Status = ApplicationStatusV2Enum.CHECK;
                                        data.StatusOther = request.StatusOther = string.Empty;
                                    }
                                    else
                                    {
                                        data.Status = request.Status = ApplicationStatusV2Enum.FAILED;
                                        data.StatusOther = request.StatusOther = ApplicationStatusOtherValueConst.RESENDABLE;
                                    }
                                    request.Update();
                                }
                            }

                            ApplicationStatusV2Enum[] pendingStatus = new ApplicationStatusV2Enum[] { ApplicationStatusV2Enum.WAITING, ApplicationStatusV2Enum.CHECK };
                            if (pendingStatus.Contains(data.Status))
                            {
                                data.Status = ApplicationStatusV2Enum.COMPLETED;
                                data.StatusName = Resources.ApplicationStatusRequests.STATUS_PENDING;
                            }
                            else if (data.Status == ApplicationStatusV2Enum.COMPLETED)
                            {
                                data.StatusName = "เป็นผู้ประกอบการจดทะเบียน เว้นแต่เข้าลักษณะที่ไม่ออกใบทะเบียน";
                            }
                            else
                            {
                                data.StatusName = ResourceHelper.GetResourceWord("STATUS_" + data.Status, "ApplicationStatusRequests", CurrentCulture);
                            }
                        }
                        else
                        {
                            data.StatusName = ResourceHelper.GetResourceWord("STATUS_" + data.Status, "ApplicationStatusRequests", CurrentCulture);
                        }

                        if (data.Duration.HasValue)
                        {
                            data.ExpectSLADateTxt = data.ExpectSLADate.HasValue ? data.ExpectSLADate.Value.ToLocalTime().ToString("dd/MM/yyyy") : "";
                        }

                        data.ApplicationName = DB.ApplicationTranslations.Where(o => o.ApplicationID == data.ApplicationID && o.Language.TwoLetterISOLanguageName == CurrentCulture).Select(o => o.ApplicationName).FirstOrDefault();
                        data.StatusOtherName = ResourceHelper.GetResourceWord("STATUS_OTHER_" + data.StatusOther, "ApplicationStatusRequests", CurrentCulture);
                        data.OrganizationName = DB.OrganizationTranslations.Where(o => o.OrgCode == data.OrgCode && o.Language.TwoLetterISOLanguageName == CurrentCulture).Select(o => o.OrgName).FirstOrDefault();

                        if (string.IsNullOrEmpty(data.IdentityName))
                        {
                            string name = DB.Users.Where(o => o.JuristicID == data.IdentityID || o.CitizenID == data.IdentityID).Select(o => o.FullName).FirstOrDefault();

                            if (!string.IsNullOrEmpty(name))
                            {
                                var request = ApplicationRequestEntity.Get(data.ApplicationRequestID);
                                data.IdentityName = request.IdentityName = name;
                                request.Update();
                            }
                        }

                    }

                    List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
                    
                    foreach(var detail in selectQuery)
                    {
                        Dictionary<string, string> dic = new Dictionary<string, string>()
                        {
                            //{"ApplicationRequestNumber",detail.ApplicationRequestNumber},
                            {"ApplicationName",detail.ApplicationName },
                            {"ApplicationID",detail.ApplicationID.ToString()},
                            //{"IdentityName",detail.IdentityName },
                            {"IdentityType",detail.IdentityType.GetEnumStringValue() },
                            {"AreaText",detail.AreaText },
                            {"CreatedDate",detail.CreatedDateTxt },
                            {"UpdatedDate",detail.UpdatedDateTxt },
                            {"ExpectSLADate",detail.ExpectSLADateTxt },
                            {"Status",detail.StatusName },
                            {"StatusOther",detail.StatusOtherName}
                        };
                        list.Add(dic);
                    }
                    application.ApplicationDetails = list;
                    organization.Applications.Add(application);
                }

                result.Add(organization);

            }

            //foreach (var app in query)
            //{
            //    // get application name
            //    var appName = appTransDb.Where(o => o.ApplicationID == app.Key.ApplicationID).Select(o => o.ApplicationName).SingleOrDefault();
            //    var appCount = repo.Where(o => o.ApplicationID == app.Key.ApplicationID && o.OrgCode == app.Key.OrgCode).Count();           
            //}


            return result;
        }

        [HttpGet]
        [Route("Api/V2/Data/GetApplicationAmount")]
        public Dictionary<string, int> ApplicationAmount()
        {
            Dictionary<string, int> result = new Dictionary<string, int>();
            var repo = MongoFactory.GetApplicationRequestCollection().AsQueryable();

            // Count
            //total
            var appTotal = repo.Select(o => o).Count();
            //success total
            var successTotal = repo.Where(o => o.Status == ApplicationStatusV2Enum.COMPLETED
            || o.Status == ApplicationStatusV2Enum.REJECTED).Count();

            //cancel total
            var cancelTotal = repo.Where(o => o.Status == ApplicationStatusV2Enum.CANCELED
            || o.Status == ApplicationStatusV2Enum.FAILED
            || o.Status == ApplicationStatusV2Enum.DRAFT).Count();

            //processing total
            var processingTotal = repo.Where(o => o.Status == ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE
            || o.Status == ApplicationStatusV2Enum.PENDING
            || o.Status == ApplicationStatusV2Enum.INCOMPLETE
            || o.Status == ApplicationStatusV2Enum.WAITING
            || o.Status == ApplicationStatusV2Enum.CHECK
            || o.Status == ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE).Count();


            result = new Dictionary<string, int>()
            {
                {"Total",appTotal },
                { "SuccessTotal", successTotal},
                { "ProcessingTotal",processingTotal},
                {"CancelTotal", cancelTotal }
            };

            return result;
        }
      
        [HttpGet]
        [Route("Api/V2/Data/ApplicationsMonth")]
        public List<Year> Search()
        {
            //Dictionary<string, string> result = new Dictionary<string, string>();
            //List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            List<Year> result = new List<Year>();

            var repo = MongoFactory.GetApplicationRequestCollection().AsQueryable();

            //if(Year == 0)
            {

                var query = repo.Select(o => o).Where(o => o.CreatedDate != null).GroupBy(o => o.CreatedDate.Year).ToList();
                foreach (var item in query)
                {
                    Year year = new Year();
                    //var yearTotal = repo.Select(o => o).Where(o => o.CreatedDate.Year == item.Key).Count();
                    var yearTotal = repo.Select(o => o).Where(o => o.CreatedDate >= new DateTime(item.Key, 1, 1) && o.CreatedDate < new DateTime(item.Key + 1, 1, 1)).Count();
                    year.YearID = item.Key;
                    year.Total = yearTotal;
                    year.Months = new List<Month>();

                    //List<Month> mList = new List<Month>();

                    var months = repo.Select(o => o).Where(o => o.CreatedDate >= new DateTime(item.Key, 1, 1) && o.CreatedDate < new DateTime(item.Key + 1, 1, 1)).GroupBy(o => o.CreatedDate.Month).ToList();

                    foreach (var month in months)
                    {
                        Month m = new Month();
                        //var total = month.Select(o => new { o.CreatedDate}).Where(o => o.CreatedDate.Month == month.Key).Count();
                        int total = 0;
                        if (month.Key == 12)
                        {
                            total = repo.Where(o => o.CreatedDate >= new DateTime(item.Key, month.Key, 1) && o.CreatedDate < new DateTime(item.Key + 1, 1, 1)).Count();
                        } else
                        {
                            total = repo.Where(o => o.CreatedDate >= new DateTime(item.Key, month.Key, 1) && o.CreatedDate < new DateTime(item.Key, month.Key + 1, 1)).Count();
                        }
                        var monthName = string.Empty;
                        switch (month.Key)
                        {
                            case 1:
                                monthName = "January";
                                break;
                            case 2:
                                monthName = "February";
                                break;
                            case 3:
                                monthName = "March";
                                break;
                            case 4:
                                monthName = "April";
                                break;
                            case 5:
                                monthName = "May";
                                break;
                            case 6:
                                monthName = "June";
                                break;
                            case 7:
                                monthName = "July";
                                break;
                            case 8:
                                monthName = "August";
                                break;
                            case 9:
                                monthName = "September";
                                break;
                            case 10:
                                monthName = "October";
                                break;
                            case 11:
                                monthName = "November";
                                break;
                            case 12:
                                monthName = "December";
                                break;
                        }
                        //dic = new Dictionary<string, string>()
                        //{
                        //    { "Month",month.Key.ToString() },
                        //    { "MonthName",monthName},
                        //    { "Total",total.ToString() }
                        //};
                        m.MonthID = month.Key;
                        m.MonthName = monthName;
                        m.Total = total;
                        year.Months.Add(m);
                    }

                    // Year
                    //result = new Dictionary<string, string>()
                    //{
                    //    { "Year", item.Key.ToString() },
                    //    { "Total", yearTotal.ToString()},
                    //    { "Months", "" }
                    //};

                    result.Add(year);
                }
            }
            return result;
        }
    }

    public class Organization
    {
        public string OrgCode { get; set; }
        public string OrgName { get; set; }
        public int Total { get; set; }
        public List<Application> Applications { get; set; }
    }

    public class Application
    {
        public string ApplicationID { get; set; }
        public string ApplicationName { get; set; }
        public int Total { get; set; }
        //public List<ApplicationRequestDetail> ApplicationRequestDetails { get; set; }
        public List<Dictionary<string, string>> ApplicationDetails { get; set; }
    }

    public class Year
    {
        public int YearID { get; set; }
        public int Total { get; set; }
        public List<Month> Months { get; set; }

    }

    public class Month
    {
        public int MonthID { get; set; }
        public string MonthName { get; set; }
        public int Total { get; set; }
    }

    public class ApplicationRequestDetail
    {
        public Guid ApplicationRequestID { get; set; }
        public string ApplicationRequestNumber { get; set; }
        public string ApplicationName { get; set; }
        public int ApplicationID { get; set; }
        public string IdentityID { get; set; }
        public string IdentityName { get; set; }
        public UserTypeEnum IdentityType { get; set; } //UserTypeEnum
        public string AreaText { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedDateTxt { get { return CreatedDate.ToLocalTime().ToStringFormat(); } }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedDateTxt { get { return UpdatedDate.ToLocalTime().ToStringFormat(); } }
        public DateTime? ExpectSLADate { get; set; }
        public bool ExceedSLA
        {
            get
            {
                return ExpectSLADate.HasValue ? (DateTime.Now.Date > ExpectSLADate.Value) : false;
            }
        }
        public string ExpectSLADateTxt { get; set; }
        public ApplicationStatusV2Enum Status { get; set; }
        public string StatusOther { get; set; }

        public string StatusName { get; set; }
        public Nullable<int> Duration { get; set; }
        public string StatusOtherName { get; set; }
        public string OrganizationName { get; set; }
        public string OrgCode { get; set; }
        public int? ProvinceID { get; set; }
        public string Province { get; set; }
        public int? AmphurID { get; set; }
        public string Amphur { get; set; }
        public int? TumbolID { get; set; }
        public string Tumbol { get; set; }
        public DateTime? ExpectedFinishDate { get; set; }
        public string ApplicationType { get; set; }
        //public string ExpectedFinishDateTxt { get { return ExpectedFinishDate.ToLocalTime().ToStringFormat(); } }
    }
}
