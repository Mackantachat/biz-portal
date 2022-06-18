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
using BizPortal.ViewModels.Report.Page1;
using BizPortal.Models;
using System;
using System.Globalization;
using BizPortal.ViewModels.Report.Page2;
using BizPortal.ViewModels;
using BizPortal.Integrated;
using BizPortal.ViewModels.Select2;

namespace BizPortal.Areas.WebApiV2.Controllers
{
    public class ReportController : ApiControllerBase
    {
        [Route("Api/v2/Report/SummaryPermitByBusinessGroup")]
        [HttpPost]
        public object SummaryPermitByBusinessGroup([FromBody]SummaryPermitGroupApiQueryModel model)
        {
            var isAdmin = User.IsInRole(ConfigurationValues.ROLES_ADMIN_NAME);
            var isOPDCAdmin = User.IsInRole(ConfigurationValues.ROLES_OPDC_ADMIN_NAME);
            var isOPDCAgent = User.IsInRole(ConfigurationValues.ROLES_OPDC_AGENT_NAME);
            var isOrgAdmin = User.IsInRole(ConfigurationValues.ROLES_ORG_ADMIN_NAME);
            var isOrgAgent = User.IsInRole(ConfigurationValues.ROLES_ORG_AGENT_NAME);

            List<ApplicationRequestSearchResultViewModel[]> dataSet = new List<ApplicationRequestSearchResultViewModel[]>();
            List<List<SummaryPermitByBusinessGroupModel>> summaryPermitByBusinessGroupModelsList = new List<List<SummaryPermitByBusinessGroupModel>>();
            List<BusinessGroup> businessesList = DB.BusinessGroups.ToList();
            List<ApplicationRequestSearchResultViewModel> reqList = getRequestList();
            var yearGroup = getRequestYearList();

            List<int> yearsInt = new List<int>();
            if (model.Year != null)
            {
                yearsInt = Array.ConvertAll(model.Year, s => int.Parse(s)).ToList();
            }
            else
            {
                yearsInt = yearGroup.Select(x => Convert.ToInt32(x.ID)).ToList();
                //yearsInt = reqList.GroupBy(x => x.CreatedDate.Year).Select(x => x.Key).ToList();
            }
            foreach (int year in yearsInt)
            {
                dataSet.Add(reqList.Where(x => x.CreatedDate.Year == year).ToArray());
            }

            List<int?> monthsInt = new List<int?>();
            if (model.Quater != null)
            {
                foreach (string q in model.Quater)
                {
                    switch (q)
                    {
                        case "q1":
                            monthsInt.AddRange(new int?[] { 1, 2, 3 });
                            break;
                        case "q2":
                            monthsInt.AddRange(new int?[] { 4, 5, 6 });
                            break;
                        case "q3":
                            monthsInt.AddRange(new int?[] { 7, 8, 9 });
                            break;
                        case "q4":
                            monthsInt.AddRange(new int?[] { 10, 11, 12 });
                            break;
                    }
                }
            }

            if (model.Month != null)
            {
                TextInfo info = CultureInfo.CurrentCulture.TextInfo;
                foreach (string month in model.Month)
                {
                    int monthInt = DateTime.ParseExact(info.ToTitleCase(month), "MMM", CultureInfo.InvariantCulture).Month;
                    if (!monthsInt.Any(x => x == monthInt))
                    {
                        monthsInt.Add(monthInt);
                    }
                }
            }

            for (int i = 0; i < dataSet.Count; i++)
            {
                var data = dataSet[i];
                int year = yearsInt[i];

                //// Filter Only BusinessId not equal null.
                data = data.Where(x => !string.IsNullOrEmpty(x.BusinessID)).ToArray();

                var resultData = data.GroupBy(x => x.BusinessID).Select(y => new SummaryPermitByBusinessGroupModel()
                {
                    BusinessID = !string.IsNullOrEmpty(y.Key) ? y.Key : "-1",
                    BusinessNameTH = !string.IsNullOrEmpty(y.Key) ? DB.BusinessGroups.Where(x => x.BusinessSysName == y.Key).FirstOrDefault().BusinessNameTH : string.Empty,
                    BusinessNameEN = !string.IsNullOrEmpty(y.Key) ? DB.BusinessGroups.Where(x => x.BusinessSysName == y.Key).FirstOrDefault().BusinessNameEN : string.Empty,
                    Year = year,
                    QtyPermitInThisYear = data.Where(x => x.CreatedDate.Year == year && x.BusinessID == y.Key).Count(),
                    QtyPermitEachQuaterIfQuery = getQuaterValue(getMonthsValueByBusinessGroup(monthsInt, y.Key, data)),
                    QtyPermitEachMonthIfQuery = getMonthsValueByBusinessGroup(monthsInt, y.Key, data),
                    DisplayValue = monthsInt.Count > 0 ? getMonthsValueByBusinessGroup(monthsInt, y.Key, data).Sum() : data.Where(x => x.CreatedDate.Year == year && x.BusinessID == y.Key).Count()
                }).ToList();

                List<SummaryPermitByBusinessGroupModel> summaryPermitByBusinessGroupModels = new List<SummaryPermitByBusinessGroupModel>();
                
                foreach (BusinessGroup businessGroup in businessesList)
                {
                    summaryPermitByBusinessGroupModels.Add(new SummaryPermitByBusinessGroupModel()
                    {
                        BusinessID = businessGroup.BusinessSysName,
                        BusinessNameEN = businessGroup.BusinessNameEN,
                        BusinessNameTH = businessGroup.BusinessNameTH,
                        Tooltip = null,
                        Year = year,
                        QtyPermitInThisYear = null,
                        QtyPermitEachQuaterIfQuery = null,
                        QtyPermitEachMonthIfQuery = null
                    });
                }

                // for BusinessID = null
                summaryPermitByBusinessGroupModels.Add(new SummaryPermitByBusinessGroupModel()
                {
                    BusinessID = "-1",
                    BusinessNameEN = "Business Undefined",
                    BusinessNameTH = "ไม่มีธุรกิจ",
                    DisplayValue = null,
                    Tooltip = null,
                    Year = year,
                    QtyPermitInThisYear = null,
                    QtyPermitEachQuaterIfQuery = null,
                    QtyPermitEachMonthIfQuery = null
                });
                

                foreach (SummaryPermitByBusinessGroupModel item in resultData)
                {
                    var selectItem = summaryPermitByBusinessGroupModels.Where(x => x.BusinessID == item.BusinessID).FirstOrDefault();
                    if (selectItem != null)
                    {
                        selectItem.Year = item.Year;
                        selectItem.QtyPermitInThisYear = item.QtyPermitInThisYear;
                        selectItem.QtyPermitEachQuaterIfQuery = item.QtyPermitEachQuaterIfQuery;
                        selectItem.QtyPermitEachMonthIfQuery = item.QtyPermitEachMonthIfQuery;
                        selectItem.DisplayValue = item.DisplayValue;
                    }
                    else
                    {
                        summaryPermitByBusinessGroupModels.Add(item);
                    }
                }
                summaryPermitByBusinessGroupModelsList.Add(summaryPermitByBusinessGroupModels);
            }

            var result = new
            {
                dataset = summaryPermitByBusinessGroupModelsList,
                yearSearch = yearGroup,
            };

            return Json(result);
        }

        [Route("Api/v2/Report/SummaryPermitByMomentGroup")]
        [HttpPost]
        public object SummaryPermitByMomentGroup([FromBody]SummaryPermitGroupApiQueryModel model)
        {
            var isAdmin = User.IsInRole(ConfigurationValues.ROLES_ADMIN_NAME);
            var isOPDCAdmin = User.IsInRole(ConfigurationValues.ROLES_OPDC_ADMIN_NAME);
            var isOPDCAgent = User.IsInRole(ConfigurationValues.ROLES_OPDC_AGENT_NAME);
            var isOrgAdmin = User.IsInRole(ConfigurationValues.ROLES_ORG_ADMIN_NAME);
            var isOrgAgent = User.IsInRole(ConfigurationValues.ROLES_ORG_AGENT_NAME);

            List<ApplicationRequestSearchResultViewModel[]> dataSet = new List<ApplicationRequestSearchResultViewModel[]>();
            List<List<SummaryPermitByMomentGroupModel>> summaryPermitByMomentGroupModelsList = new List<List<SummaryPermitByMomentGroupModel>>();
            List<ApplicationTranslation> apptranList = DB.ApplicationTranslations.Where(w => w.Language.TwoLetterISOLanguageName == CurrentCulture).ToList();
            List<ApplicationRequestSearchResultViewModel> reqList = getRequestList();
            var yearGroup = getRequestYearList();

            List<int> yearsInt = new List<int>();
            if (model.Year != null)
            {
                yearsInt = Array.ConvertAll(model.Year, s => int.Parse(s)).ToList();
            }
            else
            {
                yearsInt = yearGroup.Select(x => Convert.ToInt32(x.ID)).ToList();
            }
            foreach (int year in yearsInt)
            {
                dataSet.Add(reqList.Where(x => x.CreatedDate.Year == year).ToArray());
            }

            List<int?> monthsInt = new List<int?>();
            if (model.Quater != null)
            {
                foreach (string q in model.Quater)
                {
                    switch (q)
                    {
                        case "q1":
                            monthsInt.AddRange(new int?[] { 1, 2, 3 });
                            break;
                        case "q2":
                            monthsInt.AddRange(new int?[] { 4, 5, 6 });
                            break;
                        case "q3":
                            monthsInt.AddRange(new int?[] { 7, 8, 9 });
                            break;
                        case "q4":
                            monthsInt.AddRange(new int?[] { 10, 11, 12 });
                            break;
                    }
                }
            }

            if (model.Month != null)
            {
                TextInfo info = CultureInfo.CurrentCulture.TextInfo;
                foreach (string month in model.Month)
                {
                    int monthInt = DateTime.ParseExact(info.ToTitleCase(month), "MMM", CultureInfo.InvariantCulture).Month;
                    if (!monthsInt.Any(x => x == monthInt))
                    {
                        monthsInt.Add(monthInt);
                    }
                }
            }

            for (int i = 0; i < dataSet.Count; i++)
            {
                var data = dataSet[i];
                int year = yearsInt[i];

                var resultData = data.GroupBy(x => x.ApplicationID).Select(y => new SummaryPermitByMomentGroupModel()
                {
                    ApplicationID = y.Key.ToString(),
                    ApplicationName = apptranList.Where(x => x.ApplicationID == y.Key).FirstOrDefault().ApplicationName,
                    Year = year,
                    QtyPermitInThisYear = data.Where(x => x.CreatedDate.Year == year && x.ApplicationID == y.Key).Count(),
                    QtyPermitEachQuaterIfQuery = getQuaterValue(getMonthsValueByMoment(monthsInt, y.Key, data)),
                    QtyPermitEachMonthIfQuery = getMonthsValueByMoment(monthsInt, y.Key, data),
                    DisplayValue = monthsInt.Count > 0 ? getMonthsValueByMoment(monthsInt, y.Key, data).Sum() : data.Where(x => x.CreatedDate.Year == year && x.ApplicationID == y.Key).Count()
                }).ToList();

                List<SummaryPermitByMomentGroupModel> summaryPermitByMomentGroupModels = new List<SummaryPermitByMomentGroupModel>();
                if (isAdmin || isOPDCAdmin)
                {
                    foreach (ApplicationTranslation apptran in apptranList)
                    {
                        summaryPermitByMomentGroupModels.Add(new SummaryPermitByMomentGroupModel()
                        {
                            ApplicationID = apptran.ApplicationID.ToString(),
                            ApplicationName = apptran.ApplicationName,
                            DisplayValue = null,
                            Tooltip = null,
                            Year = year,
                            QtyPermitInThisYear = null,
                            QtyPermitEachQuaterIfQuery = null,
                            QtyPermitEachMonthIfQuery = null
                        });
                    }
                }
                else
                {
                    List<Select2Opt> permitCanAccessList = getPermitList().OrderBy(x => x.ID, new SemiNumericComparer()).ToList();
                    if (permitCanAccessList.Count() > 0)
                    {
                        foreach (Select2Opt permitCanAccess in permitCanAccessList)
                        {
                            var apptran = apptranList.Where(x => x.ApplicationID == Convert.ToInt32(permitCanAccess.ID)).FirstOrDefault();
                            summaryPermitByMomentGroupModels.Add(new SummaryPermitByMomentGroupModel()
                            {
                                ApplicationID = apptran != null ? apptran.ApplicationID.ToString() : string.Empty,
                                ApplicationName = apptran != null ? apptran.ApplicationName : string.Empty,
                                DisplayValue = null,
                                Tooltip = null,
                                Year = year,
                                QtyPermitInThisYear = null,
                                QtyPermitEachQuaterIfQuery = null,
                                QtyPermitEachMonthIfQuery = null
                            });
                        }
                    }
                    else
                    {
                        var orgAgentApptranList = apptranList.Where(x => 
                                DB.Applications.Where(y => 
                                y.OrgCode == OrganizationID).Select(y => 
                                y.ApplicationID).Contains(x.ApplicationID)).ToList();
                        foreach (ApplicationTranslation apptran in orgAgentApptranList)
                        {
                            summaryPermitByMomentGroupModels.Add(new SummaryPermitByMomentGroupModel()
                            {
                                ApplicationID = apptran.ApplicationID.ToString(),
                                ApplicationName = apptran.ApplicationName,
                                DisplayValue = null,
                                Tooltip = null,
                                Year = year,
                                QtyPermitInThisYear = null,
                                QtyPermitEachQuaterIfQuery = null,
                                QtyPermitEachMonthIfQuery = null
                            });
                        }
                    }
                }

                foreach (SummaryPermitByMomentGroupModel item in resultData)
                {
                    var selectItem = summaryPermitByMomentGroupModels.Where(x => x.ApplicationID == item.ApplicationID).FirstOrDefault();
                    if (selectItem != null)
                    {
                        selectItem.Year = item.Year;
                        selectItem.QtyPermitInThisYear = item.QtyPermitInThisYear;
                        selectItem.QtyPermitEachQuaterIfQuery = item.QtyPermitEachQuaterIfQuery;
                        selectItem.QtyPermitEachMonthIfQuery = item.QtyPermitEachMonthIfQuery;
                        selectItem.DisplayValue = item.DisplayValue;
                    }
                    else
                    {
                        summaryPermitByMomentGroupModels.Add(item);
                    }
                }
                summaryPermitByMomentGroupModelsList.Add(summaryPermitByMomentGroupModels);
            }

            var result = new
            {
                dataset = summaryPermitByMomentGroupModelsList,
                yearSearch = yearGroup,
            };

            return Json(result);
        }

        [Route("Api/v2/Report/SummaryPermitByProvinceGroup")]
        [HttpPost]
        public object SummaryPermitByProvinceGroup([FromBody]SummaryPermitGroupApiQueryModel model)
        {
            var isAdmin = User.IsInRole(ConfigurationValues.ROLES_ADMIN_NAME);
            var isOPDCAdmin = User.IsInRole(ConfigurationValues.ROLES_OPDC_ADMIN_NAME);
            var isOPDCAgent = User.IsInRole(ConfigurationValues.ROLES_OPDC_AGENT_NAME);
            var isOrgAdmin = User.IsInRole(ConfigurationValues.ROLES_ORG_ADMIN_NAME);
            var isOrgAgent = User.IsInRole(ConfigurationValues.ROLES_ORG_AGENT_NAME);

            List<ApplicationRequestSearchResultViewModel[]> dataSet = new List<ApplicationRequestSearchResultViewModel[]>();
            List<List<SummaryPermitByProvinceGroupModel>> summaryPermitByProvinceGroupModelsList = new List<List<SummaryPermitByProvinceGroupModel>>();
            var provinces = GeoService.Provinces(string.Empty, string.Empty, CurrentCulture).ToArray();
            List<ApplicationRequestSearchResultViewModel> reqList = getRequestList();
            var yearGroup = getRequestYearList();

            List<int> yearsInt = new List<int>();
            if (model.Year != null)
            {
                yearsInt = Array.ConvertAll(model.Year, s => int.Parse(s)).ToList();
            }
            else
            {
                yearsInt = yearGroup.Select(x => Convert.ToInt32(x.ID)).ToList();
            }
            foreach (int year in yearsInt)
            {
                dataSet.Add(reqList.Where(x => x.CreatedDate.Year == year).ToArray());
            }

            List<int?> monthsInt = new List<int?>();
            if (model.Quater != null)
            {
                foreach (string q in model.Quater)
                {
                    switch (q)
                    {
                        case "q1":
                            monthsInt.AddRange(new int?[] { 1, 2, 3 });
                            break;
                        case "q2":
                            monthsInt.AddRange(new int?[] { 4, 5, 6 });
                            break;
                        case "q3":
                            monthsInt.AddRange(new int?[] { 7, 8, 9 });
                            break;
                        case "q4":
                            monthsInt.AddRange(new int?[] { 10, 11, 12 });
                            break;
                    }
                }
            }

            if (model.Month != null)
            {
                TextInfo info = CultureInfo.CurrentCulture.TextInfo;
                foreach (string month in model.Month)
                {
                    int monthInt = DateTime.ParseExact(info.ToTitleCase(month), "MMM", CultureInfo.InvariantCulture).Month;
                    if (!monthsInt.Any(x => x == monthInt))
                    {
                        monthsInt.Add(monthInt);
                    }
                }
            }

            for (int i = 0; i < dataSet.Count; i++)
            {
                var data = dataSet[i];
                int year = yearsInt[i];

                var resultData = data.GroupBy(x => x.ProvinceID).Select(y => new SummaryPermitByProvinceGroupModel()
                {
                    ProvinceID = y.Key.HasValue ? y.Key.ToString() : "-1",
                    ProvinceName = y.Key.HasValue ? provinces.Where(x => Convert.ToInt32(x.ID) == y.Key).FirstOrDefault().Text : string.Empty,
                    Year = year,
                    QtyPermitInThisYear = data.Where(x => x.CreatedDate.Year == year && x.ProvinceID == y.Key).Count(),
                    QtyPermitEachQuaterIfQuery = getQuaterValue(getMonthsValueByProvince(monthsInt, y.Key, data)),
                    QtyPermitEachMonthIfQuery = getMonthsValueByProvince(monthsInt, y.Key, data),
                    DisplayValue = monthsInt.Count > 0 ? getMonthsValueByProvince(monthsInt, y.Key, data).Sum() : data.Where(x => x.CreatedDate.Year == year && x.ProvinceID == y.Key).Count()
                }).ToList();

                List<SummaryPermitByProvinceGroupModel> summaryPermitByProvinceGroupModels = new List<SummaryPermitByProvinceGroupModel>();
                if (isAdmin || isOPDCAdmin || isOPDCAgent || isOrgAdmin)
                {
                    foreach (ProvinceSelect2Opt province in provinces)
                    {
                        summaryPermitByProvinceGroupModels.Add(new SummaryPermitByProvinceGroupModel()
                        {
                            ProvinceID = province.ID,
                            ProvinceName = province.Text,
                            DisplayValue = null,
                            Tooltip = null,
                            Year = year,
                            QtyPermitInThisYear = null,
                            QtyPermitEachQuaterIfQuery = null,
                            QtyPermitEachMonthIfQuery = null
                        });
                    }

                    // for ProvinceID = null
                    summaryPermitByProvinceGroupModels.Add(new SummaryPermitByProvinceGroupModel()
                    {
                        ProvinceID = "-1",
                        ProvinceName = "ไม่สังกัดจังหวัดใด",
                        DisplayValue = null,
                        Tooltip = null,
                        Year = year,
                        QtyPermitInThisYear = null,
                        QtyPermitEachQuaterIfQuery = null,
                        QtyPermitEachMonthIfQuery = null
                    });
                }
                else
                {
                    var currentLaguageId = CurrentCulture == "th" ? 1 : 2;
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
                                       })
                                       .OrderBy(e => e.Service)
                                       .ToList();
                    foreach (var service in memberServices)
                    {
                        foreach (var area in service.Areas)
                        {
                            var exist = summaryPermitByProvinceGroupModels.Where(x => x.ProvinceID == area.ProvinceId.ToString()).FirstOrDefault();
                            if (exist == null)
                            {
                                summaryPermitByProvinceGroupModels.Add(new SummaryPermitByProvinceGroupModel()
                                {
                                    ProvinceID = area.ProvinceId.ToString(),
                                    ProvinceName = area.Province,
                                    DisplayValue = null,
                                    Tooltip = null,
                                    Year = year,
                                    QtyPermitInThisYear = null,
                                    QtyPermitEachQuaterIfQuery = null,
                                    QtyPermitEachMonthIfQuery = null
                                });
                            }
                        }
                    }
                }

                foreach (SummaryPermitByProvinceGroupModel item in resultData)
                {
                    var selectItem = summaryPermitByProvinceGroupModels.Where(x => x.ProvinceID == item.ProvinceID).FirstOrDefault();
                    if (selectItem != null)
                    {
                        selectItem.Year = item.Year;
                        selectItem.QtyPermitInThisYear = item.QtyPermitInThisYear;
                        selectItem.QtyPermitEachQuaterIfQuery = item.QtyPermitEachQuaterIfQuery;
                        selectItem.QtyPermitEachMonthIfQuery = item.QtyPermitEachMonthIfQuery;
                        selectItem.DisplayValue = item.DisplayValue;
                    }
                    else
                    {
                        summaryPermitByProvinceGroupModels.Add(item);
                    }
                }
                summaryPermitByProvinceGroupModelsList.Add(summaryPermitByProvinceGroupModels);
            }

            var result = new
            {
                dataset = summaryPermitByProvinceGroupModelsList,
                yearSearch = yearGroup,
            };

            return Json(result);
        }

        private int?[] getMonthsValueByBusinessGroup(List<int?> monthInt, string bid, ApplicationRequestSearchResultViewModel[] appList)
        {
            int?[] monthValueArray = new int?[12];
            for (int i = 0; i < monthValueArray.Length; i++)
            {
                if (monthInt.Contains(i + 1))
                {
                    monthValueArray[i] = appList.Where(x => x.BusinessID == bid && x.CreatedDate.Month == i + 1).Count();
                }
                else
                {
                    monthValueArray[i] = null;
                }
            }
            return monthValueArray;
        }

        private int?[] getMonthsValueByMoment(List<int?> monthInt, int appid, ApplicationRequestSearchResultViewModel[] appList)
        {
            int?[] monthValueArray = new int?[12];
            for (int i = 0; i < monthValueArray.Length; i++)
            {
                if (monthInt.Contains(i + 1))
                {
                    monthValueArray[i] = appList.Where(x => x.ApplicationID == appid && x.CreatedDate.Month == i + 1).Count();
                }
                else
                {
                    monthValueArray[i] = null;
                }
            }
            return monthValueArray;
        }

        private int?[] getMonthsValueByProvince(List<int?> monthInt, int? pid, ApplicationRequestSearchResultViewModel[] appList)
        {
            int?[] monthValueArray = new int?[12];
            for (int i = 0; i < monthValueArray.Length; i++)
            {
                if (monthInt.Contains(i + 1))
                {
                    monthValueArray[i] = appList.Where(x => x.ProvinceID == pid && x.CreatedDate.Month == i + 1).Count();
                }
                else
                {
                    monthValueArray[i] = null;
                }
            }
            return monthValueArray;
        }

        private int?[] getQuaterValue(int?[] monthValueArray)
        {
            int?[] quaterValueArray = new int?[4];
            for (int j = 0; j < quaterValueArray.Length; j++)
            {
                var quaterTargetValue = monthValueArray.ToList().Skip(j * 3).Take(3);
                if (quaterTargetValue.Any(x => x != null))
                {
                    quaterValueArray[j] = quaterTargetValue.Sum();
                }
                else
                {
                    quaterValueArray[j] = null;
                }
            }
            return quaterValueArray;
        }

        [Route("Api/v2/Report/GetStatisticsReportParameterList")]
        [HttpGet]
        public object GetStatisticsReportParameterList()
        {
            PermitRequestStatisticsParameterList model = new PermitRequestStatisticsParameterList();
            model.YearList = getRequestYearList();
            model.MonthList = getMonthList();
            model.RequestorTypeList = getRequestorTypeList();
            model.OrganizationList = getOrganizationList();
            model.PermitList = getPermitList();
            return model;
        }

        [Route("Api/v2/Report/StatisticsReport")]
        [HttpPost]
        public object StatisticsReport([FromBody]PermitRequestStatistics model)
        {
            var inProgressStatus = new ApplicationStatusV2Enum[] {
                    ApplicationStatusV2Enum.CHECK,
                    ApplicationStatusV2Enum.PENDING,
                    ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE,
                    ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE,
                    ApplicationStatusV2Enum.WAITING
            };
            List<ApplicationRequestSearchResultViewModel> reqList = getRequestList();
            List<ApplicationRequestSearchResultViewModel> reqFilterList = new List<ApplicationRequestSearchResultViewModel>();
            reqFilterList.AddRange(reqList);
            if (!string.IsNullOrEmpty(model.RequestorSelected))
            {
                reqFilterList = reqFilterList.Where(x => x.IdentityType.ToString() == model.RequestorSelected).ToList();
            }
            if (!string.IsNullOrEmpty(model.OrganizationSelected))
            {
                reqFilterList = reqFilterList.Where(x => x.OrgCode == model.OrganizationSelected).ToList();
            }
            if (!string.IsNullOrEmpty(model.ProvinceSelected))
            {
                reqFilterList = reqFilterList.Where(x => x.ProvinceID.HasValue && x.ProvinceID.Value == Convert.ToInt32(model.ProvinceSelected)).ToList();
            }
            if (!string.IsNullOrEmpty(model.DistrictSelected))
            {
                reqFilterList = reqFilterList.Where(x => x.AmphurID.HasValue && x.AmphurID.Value == Convert.ToInt32(model.DistrictSelected)).ToList();
            }
            if (!string.IsNullOrEmpty(model.PermitSelected))
            {
                reqFilterList = reqFilterList.Where(x => x.ApplicationID == Convert.ToInt32(model.PermitSelected)).ToList();
            }

            #region All Permit
            model.AllPermit = reqFilterList.Count();
            model.AllPermitCompleted = reqFilterList.Where(x => x.Status == ApplicationStatusV2Enum.COMPLETED).Count();
            model.AllPermitInProgress = reqFilterList.Where(x => inProgressStatus.Contains(x.Status)).Count();
            model.AllPermitInExpectedFinishDate = reqFilterList.Where(x => x.UpdatedDate <= x.ExpectSLADate).Count();
            model.AllPermitFee = reqFilterList.Where(x => x.Fee.HasValue).Select(x => x.Fee).Sum();
            #endregion

            #region All Permit Latest Month
            DateTime targetDate = DateTime.Now;
            DateTime firstDayOfCurrentMonth = new DateTime(targetDate.Year, targetDate.Month, 1, 0, 0, 0, DateTime.Now.Kind);
            targetDate = targetDate.AddMonths(-1);
            DateTime firstDayOfLatestMonth = new DateTime(targetDate.Year, targetDate.Month, 1, 0, 0, 0, DateTime.Now.Kind);
            model.AllPermitLatestMonth = reqFilterList.Where(x => x.UpdatedDate.Date >= firstDayOfLatestMonth.Date && x.UpdatedDate.Date < firstDayOfCurrentMonth.Date).Count();
            model.AllPermitCompletedLatestMonth = reqFilterList.Where(x => x.Status == ApplicationStatusV2Enum.COMPLETED && x.UpdatedDate.Date >= firstDayOfLatestMonth.Date && x.UpdatedDate.Date < firstDayOfCurrentMonth.Date).Count();
            model.AllPermitInProgressLatestMonth = reqFilterList.Where(x => inProgressStatus.Contains(x.Status) && x.UpdatedDate.Date >= firstDayOfLatestMonth.Date && x.UpdatedDate.Date < firstDayOfCurrentMonth.Date).Count();
            model.AllPermitInExpectedFinishDateLatestMonth = reqFilterList.Where(x => x.UpdatedDate <= x.ExpectSLADate && x.UpdatedDate.Date >= firstDayOfLatestMonth.Date && x.UpdatedDate.Date < firstDayOfCurrentMonth.Date).Count();
            model.AllPermitFeeLatestMonth = reqFilterList.Where(x => x.Fee.HasValue && x.UpdatedDate.Date >= firstDayOfLatestMonth.Date && x.UpdatedDate.Date < firstDayOfCurrentMonth.Date).Select(x => x.Fee).Sum();
            #endregion

            #region All Permit Selected Year. Default this year if empty
            int yearSelected = string.IsNullOrEmpty(model.YearSelected) ? DateTime.Now.Year : Convert.ToInt32(model.YearSelected);
            reqFilterList = reqFilterList.Where(x => x.UpdatedDate.Year == yearSelected).ToList();
            model.AllPermitYearSelected = reqFilterList.Count();
            model.AllPermitCompletedYearSelected = reqFilterList.Where(x => x.Status == ApplicationStatusV2Enum.COMPLETED).Count();
            model.AllPermitInProgressYearSelected = reqFilterList.Where(x => inProgressStatus.Contains(x.Status)).Count();
            model.AllPermitInExpectedFinishDateYearSelected = reqFilterList.Where(x => x.UpdatedDate <= x.ExpectSLADate).Count();
            model.AllPermitFeeYearSelected = reqFilterList.Where(x => x.Fee.HasValue).Select(x => x.Fee).Sum();
            #endregion

            if (!string.IsNullOrEmpty(model.MonthSelected))
            {
                int monthSelected = DateTime.ParseExact(model.MonthSelected, "MMM", CultureInfo.InvariantCulture).Month;
                targetDate = new DateTime(yearSelected, monthSelected, 1);
                reqFilterList = reqFilterList.Where(x => x.UpdatedDate.Date >= targetDate.Date && x.UpdatedDate.Date < targetDate.AddMonths(1).Date).ToList();
            }

            #region SLA
            model.slaDataSet = new PermitRequestStatistics.SLADataSet();
            model.slaDataSet.monthLabels = reqFilterList.GroupBy(x => x.UpdatedDate.Month).Select(x => CultureInfo.GetCultureInfo("th-TH").DateTimeFormat.GetMonthName(x.Key)).ToArray();
            model.slaDataSet.PermitNotStartedYet = reqFilterList.GroupBy(x => x.UpdatedDate.Month).Select(x => (int?)x.Where(y => !y.ExpectSLADate.HasValue).Count()).ToArray();
            model.slaDataSet.PermitInTime = reqFilterList.GroupBy(x => x.UpdatedDate.Month).Select(x => (int?)x.Where(y => y.ExpectSLADate.HasValue && y.UpdatedDate <= y.ExpectSLADate.Value).Count()).ToArray();
            model.slaDataSet.PermitOutTime = reqFilterList.GroupBy(x => x.UpdatedDate.Month).Select(x => (int?)x.Where(y => y.ExpectSLADate.HasValue && y.UpdatedDate > y.ExpectSLADate.Value).Count()).ToArray();
            model.slaDataSet.PermitDoneInTime = reqFilterList.GroupBy(x => x.UpdatedDate.Month).Select(x => (int?)x.Where(y => y.Status == ApplicationStatusV2Enum.COMPLETED && y.ExpectSLADate.HasValue && y.UpdatedDate <= y.ExpectSLADate.Value).Count()).ToArray();
            model.slaDataSet.PermitDoneOutTime = reqFilterList.GroupBy(x => x.UpdatedDate.Month).Select(x => (int?)x.Where(y => y.Status == ApplicationStatusV2Enum.COMPLETED && y.ExpectSLADate.HasValue && y.UpdatedDate > y.ExpectSLADate.Value).Count()).ToArray();
            #endregion

            #region IdentityType
            model.CitizenCount = reqFilterList.Where(x => x.IdentityType == UserTypeEnum.Citizen).Count();
            model.JuristicCount = reqFilterList.Where(x => x.IdentityType == UserTypeEnum.Juristic).Count();
            #endregion

            #region RequestByOrganization
            model.requestByOrganization = new PermitRequestStatistics.RequestByOrganization();
            var orgList = DB.OrganizationTranslations.Where(w => w.Language.TwoLetterISOLanguageName == CurrentCulture).ToList();
            model.requestByOrganization.OrganizationLabelsByCitizen = reqFilterList.Where(x => x.IdentityType == UserTypeEnum.Citizen).GroupBy(x => x.OrgCode).OrderByDescending(x => x.Count()).Select(x => orgList.Where(y => y.OrgCode == x.Key).FirstOrDefault().OrgName).ToArray();
            model.requestByOrganization.OrganizationRequestCountByCitizen = reqFilterList.Where(x => x.IdentityType == UserTypeEnum.Citizen).GroupBy(x => x.OrgCode).OrderByDescending(x => x.Count()).Select(x => (int?)x.Count()).ToArray();
            model.requestByOrganization.OrganizationLabelsByJuristic = reqFilterList.Where(x => x.IdentityType == UserTypeEnum.Juristic).GroupBy(x => x.OrgCode).OrderByDescending(x => x.Count()).Select(x => orgList.Where(y => y.OrgCode == x.Key).FirstOrDefault().OrgName).ToArray();
            model.requestByOrganization.OrganizationRequestCountByJuristic = reqFilterList.Where(x => x.IdentityType == UserTypeEnum.Juristic).GroupBy(x => x.OrgCode).OrderByDescending(x => x.Count()).Select(x => (int?)x.Count()).ToArray();
            #endregion

            #region RequestByPermitName
            model.requestByPermitName = new PermitRequestStatistics.RequestByPermitName();
            var appNameList = DB.ApplicationTranslations.Where(w => w.Language.TwoLetterISOLanguageName == CurrentCulture).ToList();
            model.requestByPermitName.PermitNameLabelsByCitizen = reqFilterList.Where(x => x.IdentityType == UserTypeEnum.Citizen).GroupBy(x => x.ApplicationID).OrderByDescending(x => x.Count()).Select(x => appNameList.Where(y => y.ApplicationID == x.Key).FirstOrDefault().ApplicationName).ToArray();
            model.requestByPermitName.PermitNameRequestCountByCitizen = reqFilterList.Where(x => x.IdentityType == UserTypeEnum.Citizen).GroupBy(x => x.ApplicationID).OrderByDescending(x => x.Count()).Select(x => (int?)x.Count()).ToArray();
            model.requestByPermitName.PermitNameLabelsByJuristic = reqFilterList.Where(x => x.IdentityType == UserTypeEnum.Juristic).GroupBy(x => x.ApplicationID).OrderByDescending(x => x.Count()).Select(x => appNameList.Where(y => y.ApplicationID == x.Key).FirstOrDefault().ApplicationName).ToArray();
            model.requestByPermitName.PermitNameRequestCountByJuristic = reqFilterList.Where(x => x.IdentityType == UserTypeEnum.Juristic).GroupBy(x => x.ApplicationID).OrderByDescending(x => x.Count()).Select(x => (int?)x.Count()).ToArray();
            #endregion

            #region Used Operation Days Each Status
            Dictionary<string, List<decimal>> totalDay = new Dictionary<string, List<decimal>>();

            foreach (var request in reqFilterList)
            {
                ApplicationRequestTransactionEntity selectedTrans;
                ApplicationRequestTransactionEntity nextTran;
                List<ApplicationRequestTransactionEntity> transList = new List<ApplicationRequestTransactionEntity>();
                decimal? durationDay = null;
                var appRequest = ApplicationRequestEntity.Get(request.ApplicationRequestID);
                transList.AddRange(appRequest.Transactions.Where(x => x.Status != ApplicationStatusV2Enum.CHECK && x.Status != ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE && x.Status != ApplicationStatusV2Enum.PENDING));
                var lastCheckStatus = appRequest.Transactions.Where(x => x.Status == ApplicationStatusV2Enum.CHECK).OrderByDescending(x => x.UpdatedDate).FirstOrDefault();
                if (lastCheckStatus != null)
                {
                    transList.Add(lastCheckStatus);
                }

                var lastPendingStatus = appRequest.Transactions.Where(x => x.Status == ApplicationStatusV2Enum.PENDING).OrderByDescending(x => x.UpdatedDate).FirstOrDefault();
                if (lastPendingStatus != null)
                {
                    transList.Add(lastPendingStatus);
                }

                var lastApprovedPaidFeeStatus = appRequest.Transactions.Where(x => x.Status == ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE).OrderBy(x => x.UpdatedDate).FirstOrDefault();
                if (lastApprovedPaidFeeStatus != null)
                {
                    transList.Add(lastApprovedPaidFeeStatus);
                }
                transList = transList.OrderBy(x => x.UpdatedDate).ToList();

                for (int i = 0; i < transList.Count() - 1; i++)
                {
                    selectedTrans = transList.ElementAtOrDefault(i);
                    nextTran = transList.ElementAtOrDefault(i + 1);
                    if (selectedTrans != null && nextTran != null)
                    {
                        if (nextTran.Status == ApplicationStatusV2Enum.REJECTED)
                        {
                            durationDay = Convert.ToDecimal((nextTran.UpdatedDate.Date - selectedTrans.UpdatedDate.Date).TotalDays);
                            if (!totalDay.ContainsKey(selectedTrans.Status.GetEnumName() + "_REJECTED"))
                            {
                                totalDay.Add(selectedTrans.Status.GetEnumName() + "_REJECTED", new List<decimal>());
                            }
                            if (durationDay.HasValue)
                            {
                                totalDay[selectedTrans.Status.GetEnumName() + "_REJECTED"].Add(durationDay.Value);
                            }
                        }
                        else
                        {
                            durationDay = Convert.ToDecimal((nextTran.UpdatedDate.Date - selectedTrans.UpdatedDate.Date).TotalDays);
                            if (!totalDay.ContainsKey(selectedTrans.Status.GetEnumName()))
                            {
                                totalDay.Add(selectedTrans.Status.GetEnumName(), new List<decimal>());
                            }
                            if (durationDay.HasValue)
                            {
                                totalDay[selectedTrans.Status.GetEnumName()].Add(durationDay.Value);
                            }
                        }
                    }
                }
            }
            model.CheckStatusAverageDay = totalDay.ContainsKey(ApplicationStatusV2Enum.CHECK.GetEnumName()) ?
                (decimal?)totalDay[ApplicationStatusV2Enum.CHECK.GetEnumName()].Sum() / totalDay[ApplicationStatusV2Enum.CHECK.GetEnumName()].Count() : null;
            model.PendingStatusAverageDay = totalDay.ContainsKey(ApplicationStatusV2Enum.PENDING.GetEnumName()) ?
                (decimal?)totalDay[ApplicationStatusV2Enum.PENDING.GetEnumName()].Sum() / totalDay[ApplicationStatusV2Enum.PENDING.GetEnumName()].Count() : null;
            model.PaidFeeStatusAverageDay = totalDay.ContainsKey(ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE.GetEnumName()) ?
                (decimal?)totalDay[ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE.GetEnumName()].Sum() / totalDay[ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE.GetEnumName()].Count() : null;
            model.PublishPermitStatusAverageDay = totalDay.ContainsKey(ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE.GetEnumName()) ?
                (decimal?)totalDay[ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE.GetEnumName()].Sum() / totalDay[ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE.GetEnumName()].Count() : null;
            model.RejectedOnCheckStatus = totalDay.ContainsKey(ApplicationStatusV2Enum.CHECK.GetEnumName() + "_REJECTED") ?
                totalDay[ApplicationStatusV2Enum.CHECK.GetEnumName() + "_REJECTED"].Count() : 0;
            model.RejectedOnPendingStatus = totalDay.ContainsKey(ApplicationStatusV2Enum.PENDING.GetEnumName() + "_REJECTED") ?
                totalDay[ApplicationStatusV2Enum.PENDING.GetEnumName() + "_REJECTED"].Count() : 0;
            #endregion

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

        private List<ApplicationRequestSearchResultViewModel> getRequestList()
        {
            var isAdmin = User.IsInRole(ConfigurationValues.ROLES_ADMIN_NAME);
            var isOPDCAdmin = User.IsInRole(ConfigurationValues.ROLES_OPDC_ADMIN_NAME);
            var isOPDCAgent = User.IsInRole(ConfigurationValues.ROLES_OPDC_AGENT_NAME);
            var isOrgAdmin = User.IsInRole(ConfigurationValues.ROLES_ORG_ADMIN_NAME);
            var isOrgAgent = User.IsInRole(ConfigurationValues.ROLES_ORG_AGENT_NAME);
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

            if (isAdmin || isOPDCAdmin)
            {
                // all
            }
            else if (isOrgAdmin || isOPDCAgent)
            {
                var serviceIds = DB.MemberManageServices
                                   .Where(e => e.UserID == CurrentUserID && !e.IsDeleted)
                                   .Select(e => e.ApplicationID)
                                   .ToList();

                // ถ้าไม่ set member manage service จะดูบริการได้เฉพาะที่อยู่ในหน่วยงานตัวเองเป็นค่าตั้งต้น
                if (serviceIds.Count > 0)
                {
                    query = query.Where(e => serviceIds.Contains(e.ApplicationID));
                }
                else
                {
                    query = query.Where(o => o.OrgCode == OrganizationID);
                }
            }
            else if (isOrgAgent)
            {
                var services = DB.MemberServices
                                 .Include(e => e.MemberServiceAreas)
                                 .Where(e => e.UserID == CurrentUserID && !e.IsDeleted)
                                 .ToList();

                // ถ้าไม่ set member service จะดูบริการได้เฉพาะที่อยู่ในหน่วยงานของตัวเองเป็นค่าตั้งต้น
                if (services.Count > 0)
                {
                    var builder = Builders<ApplicationRequestEntity>.Filter;
                    var filters = new List<FilterDefinition<ApplicationRequestEntity>>();

                    foreach (var service in services)
                    {
                        var filter = builder.Where(e => e.ApplicationID == service.ApplicationID);
                        var filterAreas = new List<FilterDefinition<ApplicationRequestEntity>>();

                        foreach (var area in service.MemberServiceAreas)
                        {
                            if (area.ProvinceID > 0)
                            {
                                var filterArea = builder.Where(e => e.ProvinceID == area.ProvinceID);

                                if (area.DistrictID > 0)
                                {
                                    filterArea &= builder.Where(e => e.AmphurID == area.DistrictID);

                                    if (area.SectionID > 0)
                                    {
                                        filterArea &= builder.Where(e => e.TumbolID == area.SectionID);
                                    }
                                }

                                filterAreas.Add(filterArea);
                            }
                        }

                        if (filterAreas.Count > 0)
                        {
                            filter &= builder.Or(filterAreas);
                        }

                        filters.Add(filter);
                    }

                    query = collection.Find(builder.Or(filters)).ToList().AsQueryable();
                }
                else
                {
                    query = query.Where(o => o.OrgCode == OrganizationID);
                }
            }

            query = query.Where(o => status.Contains(o.Status));

            var selectQuery = query.Select(o => new ApplicationRequestSearchResultViewModel
            {
                StatusOtherPrevious = (o.Transactions != null && o.Transactions.Count > 1) ? o.Transactions[o.Transactions.Count() - 2].StatusOther : "",
                ApplicationRequestNumber = o.ApplicationRequestNumber,
                ApplicationRequestNumberAgent = o.ApplicationRequestNumberAgent,
                ApplicationID = o.ApplicationID,
                ApplicationRequestID = o.ApplicationRequestID,
                CreatedDate = o.CreatedDate,
                UpdatedDate = o.UpdatedDate,
                UpdatedDateByAgent = o.UpdatedDateByAgent,
                UpdatedDateByRequestor = o.UpdatedDateByRequestor,
                IdentityID = o.IdentityID,
                IdentityName = o.IdentityName,
                IdentityType = o.IdentityType,
                OrgCode = o.OrgCode,
                Status = o.Status,
                StatusOther = o.StatusOther == null ? "" : o.StatusOther,
                isViewed = o.isViewed,
                Duration = o.Duration,
                ExpectSLADate = o.ExpectedFinishDate,
                ProvinceID = o.ProvinceID,
                Province = o.Province,
                Amphur = o.Amphur,
                AmphurID = o.AmphurID,
                BusinessID = o.BusinessId,
                Fee = o.Fee
            });

            var reqList = selectQuery.ToList();

            foreach (var data in reqList)
            {
                // Set VAT Status manually
                if (data.ApplicationID == 9)
                {
                    ApplicationStatusV2Enum[] invalidStatus = new ApplicationStatusV2Enum[] { ApplicationStatusV2Enum.COMPLETED, ApplicationStatusV2Enum.REJECTED };//, ApplicationStatusV2Enum.FAILED };

                    if (!invalidStatus.Contains(data.Status))
                    {
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

                var hideSlaStatus = new ApplicationStatusV2Enum[] { ApplicationStatusV2Enum.INCOMPLETE, ApplicationStatusV2Enum.WAITING, ApplicationStatusV2Enum.CHECK };
                if (data.Duration.HasValue && !hideSlaStatus.Contains(data.Status))
                {
                    data.ExpectSLADateTxt = data.ExpectSLADate.HasValue ? data.ExpectSLADate.Value.ToLocalTime().ToString("dd/MM/yyyy") : "";
                }

                data.ApplicationName = DB.ApplicationTranslations.Where(o => o.ApplicationID == data.ApplicationID && o.Language.TwoLetterISOLanguageName == CurrentCulture).Select(o => o.ApplicationName).FirstOrDefault();
                data.OrganizationName = DB.OrganizationTranslations.Where(o => o.OrgCode == data.OrgCode && o.Language.TwoLetterISOLanguageName == CurrentCulture).Select(o => o.OrgName).FirstOrDefault();
                data.StatusOtherName = ResourceHelper.GetResourceWord("STATUS_OTHER_" + data.StatusOther, "ApplicationStatusRequests", CurrentCulture);

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
            return reqList;
        }
    }

    public class SemiNumericComparer : IComparer<string>
    {
        /// <summary>
        /// Method to determine if a string is a number
        /// </summary>
        /// <param name="value">String to test</param>
        /// <returns>True if numeric</returns>
        public static bool IsNumeric(string value)
        {
            return int.TryParse(value, out _);
        }

        /// <inheritdoc />
        public int Compare(string s1, string s2)
        {
            const int S1GreaterThanS2 = 1;
            const int S2GreaterThanS1 = -1;

            var IsNumeric1 = IsNumeric(s1);
            var IsNumeric2 = IsNumeric(s2);

            if (IsNumeric1 && IsNumeric2)
            {
                var i1 = Convert.ToInt32(s1);
                var i2 = Convert.ToInt32(s2);

                if (i1 > i2)
                {
                    return S1GreaterThanS2;
                }

                if (i1 < i2)
                {
                    return S2GreaterThanS1;
                }

                return 0;
            }

            if (IsNumeric1)
            {
                return S2GreaterThanS1;
            }

            if (IsNumeric2)
            {
                return S1GreaterThanS2;
            }

            return string.Compare(s1, s2, true, CultureInfo.InvariantCulture);
        }
    }
}