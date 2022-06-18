using BizPortal.Areas.WebApi.Controllers;
using BizPortal.DAL.MongoDB;
using BizPortal.Utils.Helpers;
using BizPortal.ViewModels;
using BizPortal.ViewModels.Select2;
using BizPortal.ViewModels.V2;
using EGA.Owin.Security.EGAOpenID;
using EGA.Owin.Security.Utils.Extensions;
using Mapster;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Resources;
using System.Web.Http;

namespace BizPortal.Areas.WebApiV2.Controllers
{
    [Authorize]
    public class ApplicationRequestsController : ApiControllerBase
    {
        [HttpPost]
        [Route("Api/V2/ApplicationRequests/List")]
        public DataTablesResult<ApplicationRequestSearchResultViewModel> List(JuristicApplicationStatusRequestDataTables dataTables)
        {
            var result = new DataTablesResult<ApplicationRequestSearchResultViewModel>();

            var repo = MongoFactory.GetApplicationRequestCollection().AsQueryable();
            var query = repo.AsQueryable();
            var isAgent = true;

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

            if (User.IsInRole(ConfigurationValues.ROLES_ADMIN_NAME) || User.IsInRole(ConfigurationValues.ROLES_OPDC_ADMIN_NAME) || User.IsInRole(ConfigurationValues.ROLES_OPDC_AGENT_NAME))
            {
                if (!string.IsNullOrEmpty(dataTables.OrgCode))
                {
                    query = query.Where(o => o.OrgCode == dataTables.OrgCode);
                }
                ApplicationStatusV2Enum[] statuses = new ApplicationStatusV2Enum[] {
                    ApplicationStatusV2Enum.COMPLETED,
                    ApplicationStatusV2Enum.CHECK,
                    ApplicationStatusV2Enum.PENDING,
                    ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE,
                    ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE,
                    ApplicationStatusV2Enum.REJECTED,
                    ApplicationStatusV2Enum.WAITING
                };
                query = query.Where(o => statuses.Contains(o.Status));
            }
            else if (User.IsInRole(ConfigurationValues.ROLES_ORG_AGENT_NAME))
            {
                query = query.Where(o => o.OrgCode == OrganizationID);
                ApplicationStatusV2Enum[] statuses = new ApplicationStatusV2Enum[] {
                    ApplicationStatusV2Enum.COMPLETED,
                    ApplicationStatusV2Enum.CHECK,
                    ApplicationStatusV2Enum.PENDING,
                    ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE,
                    ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE,
                    ApplicationStatusV2Enum.REJECTED,
                    ApplicationStatusV2Enum.WAITING
                };
                query = query.Where(o => statuses.Contains(o.Status));
            }
            else
            {
                var ident = User.Identity;
                string citizenID = ident.GetClaimValueOrDefault(EGAOpenIDAttributeExchangeType.CitizenID);
                string juristicID = ident.GetClaimValueOrDefault(EGAOpenIDAttributeExchangeType.JuristicID);
                query = query.Where(o => o.IdentityID == citizenID || o.IdentityID == juristicID);
                isAgent = false;
            }

            result.RecordsTotal = query.Count();

            if (dataTables.ApplicationID != null)
            {
                query = query.Where(o => o.ApplicationID == dataTables.ApplicationID.Value);
            }

            if (!string.IsNullOrEmpty(dataTables.IdentityID))
            {
                query = query.Where(o => o.IdentityID == dataTables.IdentityID);
            }

            if (!string.IsNullOrEmpty(dataTables.ApplicationStatusID))
            {
                ApplicationStatusV2Enum status = EnumUtils.GetEnum<ApplicationStatusV2Enum>(dataTables.ApplicationStatusID);
                query = query.Where(o => o.Status == status);
            }

            if (!string.IsNullOrEmpty(dataTables.SearchKeyword))
            {
                query = query.Where(o => o.IdentityID.Contains(dataTables.SearchKeyword)
                    || o.IdentityName.Contains(dataTables.SearchKeyword)
                    || o.ApplicationRequestNumber.Contains(dataTables.SearchKeyword)
                    || o.ApplicationRequestNumberAgent.Contains(dataTables.SearchKeyword));
            }

            if (!string.IsNullOrEmpty(dataTables.ApplicationStatusOther))
            {
                if (dataTables.ApplicationStatusOther == "DONE")
                {
                    query = query.Where(o => o.StatusOther == dataTables.ApplicationStatusOther || o.StatusOther == "REJECT");
                }
                else if (dataTables.ApplicationStatusOther == "WAITING_AGENT_CHECK_CORRECTION")
                {
                    query = query.Where(o => o.IsAgentCheckUserCorrection);
                }
                else
                {
                    query = query.Where(o => o.StatusOther == dataTables.ApplicationStatusOther);
                }
            }

            if (dataTables.AmphurID.HasValue)
            {
                query = query.Where(o => o.AmphurID == dataTables.AmphurID);
            }

            if (dataTables.ProvinceID.HasValue)
            {
                query = query.Where(o => o.ProvinceID == dataTables.ProvinceID);
            }

            if (isAgent && !dataTables.ShowCompleted)
            {
                // เรียงจากวันที่ยื่นเรื่องจากเก่าไปใหม่ แต่เปิดมาจะไม่แสดงคำร้องที่เสร็จสิ้นแล้วโดยอัตโนมัติ
                query = query.Where(o => o.Status != ApplicationStatusV2Enum.COMPLETED && o.Status != ApplicationStatusV2Enum.REJECTED);
            }

            result.RecordsFiltered = query.Count();

            var selectQuery = query.Select(o => new ApplicationRequestSearchResultViewModel
            {
                StatusOtherPrevious = (o.Transactions != null && o.Transactions.Count > 1) ? o.Transactions[o.Transactions.Count() - 2].StatusOther : "",
                //StatusOtherName = o.GetStatusOtherText(),
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
            });
            if (isAgent)
            {
                result.Data = dataTables.GenerateSearchQuery<ApplicationRequestSearchResultViewModel>(selectQuery, "UpdatedDateByRequestor", "DESC").ToList();
            }
            else
            {
                result.Data = dataTables.GenerateSearchQuery<ApplicationRequestSearchResultViewModel>(selectQuery, "UpdatedDateByAgent", "DESC").ToList();
            }

            foreach (var data in result.Data)
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

            return result;
        }

        [HttpPost]
        [Route("Api/V2/ApplicationRequests/Manage/List")]
        public DataTablesResult<ApplicationRequestSearchResultViewModel> MangeList(JuristicApplicationStatusRequestDataTables dataTables)
        {
            var result = new DataTablesResult<ApplicationRequestSearchResultViewModel>();
            var isAdmin = User.IsInRole(ConfigurationValues.ROLES_ADMIN_NAME);
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

            if (isAdmin)
            {
                // all
            }
            else if (isOrgAdmin)
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
            result.RecordsTotal = query.Count();

            if (dataTables.ApplicationID != null)
            {
                query = query.Where(o => o.ApplicationID == dataTables.ApplicationID.Value);
            }

            if (!string.IsNullOrEmpty(dataTables.IdentityID))
            {
                query = query.Where(o => o.IdentityID == dataTables.IdentityID);
            }

            if (!string.IsNullOrEmpty(dataTables.ApplicationStatusID))
            {
                ApplicationStatusV2Enum applicationStatus = EnumUtils.GetEnum<ApplicationStatusV2Enum>(dataTables.ApplicationStatusID);
                query = query.Where(o => o.Status == applicationStatus);
            }

            if (!string.IsNullOrEmpty(dataTables.SearchKeyword))
            {
                string keyWord = dataTables.SearchKeyword.Trim();
                List<int> matchedAppIDs = DB.ApplicationTranslations.Where(o => o.Application.IsDeleted == false
                                                                            && o.ApplicationName.Contains(keyWord))
                                                                            .Select(o => o.ApplicationID)
                                                                            .Distinct()
                                                                            .ToList();

                query = query.Where(o => o.IdentityID.Contains(keyWord)
                    || (o.IdentityName != null && o.IdentityName.Contains(keyWord))
                    || (o.ApplicationRequestNumber != null && o.ApplicationRequestNumber.Contains(keyWord))
                    || (o.ApplicationRequestNumberAgent != null && o.ApplicationRequestNumberAgent.Contains(keyWord))
                    || matchedAppIDs.Contains(o.ApplicationID)
                );
            }

            if (!string.IsNullOrEmpty(dataTables.ApplicationStatusOther))
            {
                if (dataTables.ApplicationStatusOther == "DONE")
                {
                    query = query.Where(o => o.StatusOther == dataTables.ApplicationStatusOther || o.StatusOther == "REJECT");
                }
                else if (dataTables.ApplicationStatusOther == "WAITING_AGENT_CHECK_CORRECTION")
                {
                    query = query.Where(o => o.IsAgentCheckUserCorrection);
                }
                else
                {
                    query = query.Where(o => o.StatusOther == dataTables.ApplicationStatusOther);
                }
            }

            if (!string.IsNullOrEmpty(dataTables.OrgCode))
            {
                query = query.Where(e => e.OrgCode == dataTables.OrgCode);
            }

            if (dataTables.AmphurID.HasValue)
            {
                query = query.Where(o => o.AmphurID == dataTables.AmphurID);
            }

            if (dataTables.ProvinceID.HasValue)
            {
                query = query.Where(o => o.ProvinceID == dataTables.ProvinceID);
            }

            if (!dataTables.ShowCompleted)
            {
                query = query.Where(o => o.Status != ApplicationStatusV2Enum.COMPLETED && o.Status != ApplicationStatusV2Enum.REJECTED);
            }

            result.RecordsFiltered = query.Count();

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
                PaymentMethod = o.PaymentMethod
            });

            result.Data = dataTables.GenerateSearchQuery<ApplicationRequestSearchResultViewModel>(selectQuery, "CreatedDate", "DESC").ToList();


            foreach (var data in result.Data)
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

                // Payment Status
                if (data.Status == ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE && data.StatusOther == ApplicationStatusOtherValueConst.WAITING_AGENT_WORKING && data.PaymentMethod == "BILL_PAYMENT")
                {
                    var paymentTrans = PaymentTransaction.Get(data.IdentityID, data.ApplicationRequestID);
                    if (paymentTrans != null && paymentTrans.Status == (int)CGDPaymentStatus.Success)
                    {
                        data.StatusName = "ชำระเงินแล้ว";
                    }
                }
            }

            return result;
        }

        [HttpGet]
        [Route("Api/V2/ApplicationRequests/{id}/Draft")]
        public ApplicationRequestViewModel GetDraft(int id)
        {
            ApplicationRequestEntity model = ApplicationRequestEntity.Get(id, IdentityID, IdentityType, new ApplicationStatusV2Enum[] { ApplicationStatusV2Enum.DRAFT });
            if (model == null || (model != null && model.Status != ApplicationStatusV2Enum.DRAFT))
                return null;

            model.ApplicationName = DB.ApplicationTranslations.Where(o => o.ApplicationID == model.ApplicationID && o.Language.TwoLetterISOLanguageName == CurrentCulture).Select(o => o.ApplicationName).FirstOrDefault();
            int statusID = (int)model.Status;
            model.StatusName = ResourceHelper.GetResourceWord("STATUS_" + model.Status, "ApplicationStatusRequests", CurrentCulture);

            ApplicationRequestViewModel result = new ApplicationRequestViewModel();
            TypeAdapter.Adapt<ApplicationRequestEntity, ApplicationRequestViewModel>(model, result);
            ResourceManager rm = new ResourceManager(typeof(Resources.FileType));
            foreach (var group in result.UploadedFiles)
            {
                foreach (var file in group.Files)
                {
                    if (string.IsNullOrEmpty(file.FileTypeName))
                    {
                        if (!string.IsNullOrEmpty(file.FileTypeCode))
                        {
                            file.FileTypeName = rm.GetString(file.FileTypeCode);

                            if (string.IsNullOrEmpty(file.FileTypeName) && file.Extras != null)
                            {
                                foreach (var extra in file.Extras)
                                {
                                    file.FileTypeName = rm.GetString(extra.Value);
                                }
                            }
                            else if (string.IsNullOrEmpty(file.FileTypeName))
                            {
                                file.FileTypeName = file.FileTypeCode;
                            }
                        }

                    }
                }
            }

            return result;
        }

        [Route("Api/v2/ApplicationRequests/ApplictionRequestDropdown")]
        [HttpGet]
        public object ApplictionRequestDropdown(string appsysname)
        {
            var appNumbers = MongoFactory.GetApplicationRequestCollection().AsQueryable()
            .Where(u => u.Status == ApplicationStatusV2Enum.COMPLETED && u.StatusOther == "DONE"
            && u.AppSysName == appsysname && u.IdentityID == IdentityID);

            List<Select2Opt> options = new List<Select2Opt>();
            foreach (var appNumber in appNumbers)
            {
                options.Add(new Select2Opt() { ID = appNumber.ApplicationRequestNumber, Text = appNumber.ApplicationRequestNumber });
            }

            return new { results = options.ToArray() };
        }

        [Route("Api/v2/ApplicationRequests/ApplictionRequestReturn")]
        [HttpGet]
        public object ApplictionRequestReturn(string txt_search, string appsysname)
        {
            var db = MongoFactory.GetApplicationRequestCollection().AsQueryable();
            ApplicationRequestEntity apps = null;
            apps = db.Where(p => p.ApplicationRequestNumber == txt_search
            && p.AppSysName == appsysname
            && p.IdentityID == IdentityID
            && p.Status == ApplicationStatusV2Enum.COMPLETED
            && p.StatusOther == "DONE"
            ).FirstOrDefault();

            if (apps != null)
            {
                return apps;
            }
            else
            {
                return "Data not found";
            }
            // return apps;
        }


    }
}
