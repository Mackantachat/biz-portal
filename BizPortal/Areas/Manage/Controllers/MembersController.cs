using BizPortal.Models;
using BizPortal.ViewModels;
using Microsoft.AspNet.Identity;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using static BizPortal.Utils.Epplus;

namespace BizPortal.Areas.Manage.Controllers
{
    [Authorize(Roles = ConfigurationValues.ROLES_ADMIN_NAME + "," + ConfigurationValues.ROLES_OPDC_ADMIN_NAME + "," + ConfigurationValues.ROLES_ORG_ADMIN_NAME)]
    public class MembersController : ManageControllerBase
    {
        // GET: Manage/Members
        public ActionResult Index()
        {
            var roles = DB.Roles.ToList();
            var isAdmin = User.IsInRole(ConfigurationValues.ROLES_ADMIN_NAME);
            var isOPDCAdmin = User.IsInRole(ConfigurationValues.ROLES_OPDC_ADMIN_NAME);
            var isOrgAdmin = User.IsInRole(ConfigurationValues.ROLES_ORG_ADMIN_NAME);


            ViewBag.ActiveMenu = PageNameBackendEnum.MEMBER_MANAGE.GetEnumStringValue();
            ViewBag.IsAdmin = isAdmin;
            ViewBag.IsOPDCAdmin = isOPDCAdmin;
            ViewBag.IsOrgAdmin = isOrgAdmin;
            ViewBag.AdminRoleId = roles.Where(e => e.Name == ConfigurationValues.ROLES_ADMIN_NAME).Select(e => e.Id).FirstOrDefault();
            ViewBag.OPDCAdminRoleId = roles.Where(e => e.Name == ConfigurationValues.ROLES_OPDC_ADMIN_NAME).Select(e => e.Id).FirstOrDefault();
            ViewBag.OrgAdminRoleId = roles.Where(e => e.Name == ConfigurationValues.ROLES_ORG_ADMIN_NAME).Select(e => e.Id).FirstOrDefault();
            ViewBag.OrganizationList = new SelectList(GetOrganizationList(DropdownlistType.NONE, true), "Value", "Text");
            ViewBag.RolesList = new SelectList(GetRolesList(true), "Value", "Text");
            ViewBag.UserTypeList = new SelectList(GetUserTypesList(true, isOrgAdmin ? ConfigurationValues.ROLES_ORG_ADMIN_NAME : ""), "Value", "Text");

            return View();
        }

        public ActionResult Edit(string id)
        {
            var roles = DB.Roles.ToList();
            var isAdmin = User.IsInRole(ConfigurationValues.ROLES_ADMIN_NAME);
            var isOPDCAdmin = User.IsInRole(ConfigurationValues.ROLES_OPDC_ADMIN_NAME);
            var isOrgAdmin = User.IsInRole(ConfigurationValues.ROLES_ORG_ADMIN_NAME);
            var services = GetServiceList("", true);
            var manageServices = GetManageServiceList(CurrentUserID, OrganizationID);
            var manageServiceIds = manageServices.Select(e => e.Value).ToList();
            var currentLaguageId = CurrentCulture == "th" ? 1 : 2;
            var model = DB.Users
                          .Where(o => o.Id == id)
                          .Select(o => new MemberViewModel()
                          {
                            Username = o.UserName,
                            FullName = o.FullName,
                            RoleIds = o.Roles.Select(p => p.RoleId),
                            Id = o.Id,
                            UserType = o.UserType,
                            LockoutEndDateUtc = o.LockoutEndDateUtc,
                            OrgName = o.Organization.OrganizationTranslations.Where(p => p.Language.TwoLetterISOLanguageName == "th").FirstOrDefault().OrgName,
                            OrgCode = o.Organization.OrganizationTranslations.Where(p => p.Language.TwoLetterISOLanguageName == "th").FirstOrDefault().OrgCode
                          })
                          .SingleOrDefault();

            var memberManageServices = DB.MemberManageServices
                                         .Where(e => e.UserID == id && !e.IsDeleted)
                                         .Select(e => e.ApplicationID)
                                         .ToList();

            var memberServices = DB.MemberServices
                                   .Include(e => e.Application.Organization)
                                   .Include(e => e.Application.ApplicationTranslations)
                                   .Where(e => e.UserID == id && !e.IsDeleted)
                                   .Select(e => new MemberServiceViewModel
                                   {
                                       OrgCode = e.Application.OrgCode,
                                       OrgName = e.Application.Organization.OrgSysName,
                                       ServiceId = e.ApplicationID,
                                       Service = e.Application.ApplicationTranslations.Where(t => t.ApplicationID == e.ApplicationID && t.LanguageID == currentLaguageId).Select(t => t.ApplicationName).FirstOrDefault() + " - " + e.Application.Organization.OrgSysName,
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

            if (model == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            if (isOrgAdmin)
            {
                model.OrgCode = OrganizationID;
            }

            model.ManageServices = memberManageServices;
            model.Services = memberServices;

            ViewBag.IsAdmin = isAdmin;
            ViewBag.IsOPDCAdmin = isOPDCAdmin;
            ViewBag.IsOrgAdmin = isOrgAdmin;
            ViewBag.AdminRoleId = roles.Where(e => e.Name == ConfigurationValues.ROLES_ADMIN_NAME).Select(e => e.Id).FirstOrDefault();
            ViewBag.OrgAdminRoleId = roles.Where(e => e.Name == ConfigurationValues.ROLES_ORG_ADMIN_NAME).Select(e => e.Id).FirstOrDefault();
            ViewBag.OrgAgentRoleId = roles.Where(e => e.Name == ConfigurationValues.ROLES_ORG_AGENT_NAME).Select(e => e.Id).FirstOrDefault();
            ViewBag.RolesList = new SelectList(GetRolesList(), "Value", "Text");
            ViewBag.OrganizationList = new SelectList(GetOrganizationList(), "Value", "Text");
            ViewBag.UserTypeList = new SelectList(GetUserTypesList(false, isOrgAdmin ? ConfigurationValues.ROLES_ORG_ADMIN_NAME : ""), "Value", "Text");
            ViewBag.ServiceList = services.Select(e => new { id = e.Value, text = e.Text, group = e.Group != null ? e.Group.Name : "" }).ToList();
            ViewBag.ManageServiceList = manageServices.Select(e => new { id = e.Value, text = e.Text, group = e.Group != null ? e.Group.Name : "" }).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(string id, MemberViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var ts = DB.Database.BeginTransaction())
                {
                    try
                    {
                        var roles = DB.Roles.ToList();
                        var adminRoleId = roles.Where(e => e.Name == ConfigurationValues.ROLES_ADMIN_NAME).Select(e => e.Id).FirstOrDefault();
                        var opdcAdminRoleId = roles.Where(e => e.Name == ConfigurationValues.ROLES_OPDC_ADMIN_NAME).Select(e => e.Id).FirstOrDefault();
                        var orgAdminRoleId = roles.Where(e => e.Name == ConfigurationValues.ROLES_ORG_ADMIN_NAME).Select(e => e.Id).FirstOrDefault();
                        var orgAgentRoleId = roles.Where(e => e.Name == ConfigurationValues.ROLES_ORG_AGENT_NAME).Select(e => e.Id).FirstOrDefault();
                        var isAdmin = User.IsInRole(ConfigurationValues.ROLES_ADMIN_NAME);
                        var isOPDCAdmin = User.IsInRole(ConfigurationValues.ROLES_OPDC_ADMIN_NAME);
                        var isOrgAdmin = User.IsInRole(ConfigurationValues.ROLES_ORG_ADMIN_NAME);
                        var user = DB.Users.Where(e => e.Id == id).FirstOrDefault();

                        // roles
                        var formRoles = new string[] { };

                        // ต้องเป็นเจ้าหน้าที่รัฐเท่านั้นถึงจะมี roles
                        if (model.RoleIds != null && model.UserType == UserTypeEnum.GovernmentAgent.GetEnumStringValue())
                        {
                            if (isOPDCAdmin)
                            {
                                // ถ้า admin opdc มาแก้ไขจะไม่สามารถให้สิทธิ์ admin และ admin opdc กับ user ได้
                                model.RoleIds = model.RoleIds.Where(e => e != adminRoleId || e != opdcAdminRoleId).ToList();
                            }
                            else if (isOrgAdmin)
                            {
                                // ถ้า admin organization มาแก้ไขจะสามารถให้สิทธิ์ได้แค่ org agent เท่านั้น
                                if (model.UserType == UserTypeEnum.GovernmentAgent.GetEnumStringValue())
                                {
                                    model.RoleIds = new List<string> { orgAgentRoleId };
                                }
                                else
                                {
                                    model.RoleIds = new List<string> { };
                                }
                            }


                            formRoles = roles.Where(o => model.RoleIds.Contains(o.Id)).Select(o => o.Name).ToArray();
                        }

                        var userRoles = UserManager.GetRoles(id).ToArray();
                        var removeRoles = userRoles.Where(o => !formRoles.Contains(o)).ToArray();
                        var newRoles = formRoles.Where(o => !userRoles.Contains(o)).ToArray();

                        UserManager.RemoveFromRoles(id, removeRoles);
                        UserManager.AddToRoles(id, newRoles);
                        DB.SaveChanges();

                        // user type and organization
                        if (user != null)
                        {
                            user.UserType = model.UserType;
                            user.OrgCode = model.UserType == UserTypeEnum.GovernmentAgent.GetEnumStringValue() ? (isAdmin || isOPDCAdmin ? model.OrgCode : OrganizationID) : null;
                            DB.SaveChanges();
                        }

                        // manage services
                        var memberManageServices = DB.MemberManageServices
                                                     .Where(e => e.UserID == id && !e.IsDeleted)
                                                     .ToList();

                        if (model.ManageServices != null && model.RoleIds != null && model.RoleIds.Contains(orgAdminRoleId) && model.UserType == UserTypeEnum.GovernmentAgent.GetEnumStringValue())
                        {
                            // add new
                            foreach (var serviceId in model.ManageServices.Where(e => e != 0))
                            {
                                var memberManageService = new MemberManageService
                                {
                                    UserID = id,
                                    ApplicationID = serviceId
                                };

                                DB.MemberManageServices.Add(memberManageService);
                            }
                        }

                        // delete all current member manage cervice
                        foreach (var service in memberManageServices)
                        {
                            service.IsDeleted = true;
                            service.DeletedUserID = CurrentUserID;
                            service.DeletedDate = DateTime.Now;
                        }

                        DB.SaveChanges();


                        // sevices
                        var memberServices = DB.MemberServices
                                               .Include(e => e.MemberServiceAreas)
                                               .Where(e => e.UserID == id && !e.IsDeleted)
                                               .ToList();

                        if (model.Services != null && model.RoleIds != null && model.RoleIds.Contains(orgAgentRoleId) && model.UserType == UserTypeEnum.GovernmentAgent.GetEnumStringValue())
                        {
                            // add new
                            foreach (var service in model.Services.Where(e => e.ServiceId != -1))
                            {
                                var memberService = new MemberService
                                {
                                    UserID = id,
                                    ApplicationID = service.ServiceId,

                                };

                                if (service.Areas != null && service.Areas.Count() > 0)
                                {
                                    memberService.MemberServiceAreas = service.Areas.Select(e => new MemberServiceArea
                                    {
                                        ProvinceID = e.ProvinceId,
                                        DistrictID = e.DistrictId,
                                        SectionID = e.SectionId,
                                        Province = e.ProvinceId == -1 ? null : e.Province,
                                        District = e.DistrictId == -1 ? null : e.District,
                                        Section = e.SectionId == -1 ? null : e.Section
                                    }).ToList();
                                }

                                DB.MemberServices.Add(memberService);
                            }
                        }

                        // delete all current member service
                        foreach (var service in memberServices)
                        {
                            service.IsDeleted = true;
                            service.DeletedUserID = CurrentUserID;
                            service.DeletedDate = DateTime.Now;

                            foreach (var area in service.MemberServiceAreas)
                            {
                                area.IsDeleted = true;
                                area.DeletedUserID = CurrentUserID;
                                area.DeletedDate = DateTime.Now;
                            }
                        }

                        DB.SaveChanges();

                        ts.Commit();

                        return Json(new { Status = true, Message = Resources.Member.MSG_UPDATE_SUCCESS, Data = "" }, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception ex)
                    {
                        ts.Rollback();
                    }
                }
            }

            return Json(new { Status = false, Message = Resources.Member.MSG_UPDATE_ERROR, Data = "" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ExportMemberServices(string userId)
        {
            var isAdmin = User.IsInRole(ConfigurationValues.ROLES_ADMIN_NAME);
            var isOPDCAdmin = User.IsInRole(ConfigurationValues.ROLES_OPDC_ADMIN_NAME);
            var currentLaguageId = CurrentCulture == "th" ? 1 : 2;
            var fileName = "เอกสารนำเข้าข้อมูล";
            var template = HttpContext.Server.MapPath("~/Content/filetemplates/ImportUserServiceTemplate.xlsx");
            var services = GetServiceList("", true);

            if (!(isAdmin || isOPDCAdmin))
            {
                var memberManageServiceIds = DB.MemberManageServices
                                               .Where(e => e.UserID == CurrentUserID && !e.IsDeleted)
                                               .Select(e => e.ApplicationID.ToString())
                                               .ToList();

                services = services.Where(e => memberManageServiceIds.Contains(e.Value)).ToList();
            }

            var serviceIds = services.Select(e => e.Value).ToList();

            var templateData = new List<ImportTemplateViewModel> {
                                    new ImportTemplateViewModel {
                                        InputTitle = "",
                                        InputMessage = "",
                                        ColIndex = 1,
                                        RowIndex = 2,
                                        Data = services.Select(e => e.Text).ToList()
                                    },
                                    new ImportTemplateViewModel {
                                        InputTitle = "",
                                        InputMessage = "",
                                        ColIndex = 7,
                                        RowIndex = 2,
                                        Data = services.Select(e => e.Value).ToList()
                                    }
                               };

            var data = DB.MemberServices
                         .Include(e => e.Application.ApplicationTranslations)
                         .Include(e => e.Application.Organization.OrganizationTranslations)
                         .Where(e => e.UserID == userId && !e.IsDeleted && serviceIds.Contains(e.ApplicationID.ToString()))
                         .SelectMany(e => e.MemberServiceAreas.Where(f => !f.IsDeleted).DefaultIfEmpty(), (a, b) => new { ApplicationName = a.Application.ApplicationTranslations.Where(c => c.LanguageID == currentLaguageId).Select(c => c.ApplicationName).FirstOrDefault() + " - " + a.Application.Organization.OrganizationTranslations.Where(c => c.LanguageID == currentLaguageId).Select(c => c.OrgName).FirstOrDefault(), Province = (b.Province == null ? "ทุกพื้นที่" : b.Province), District =  b.District, Section = b.Section })
                         .OrderBy(e => e.ApplicationName)
                         .ThenBy(e => e.Province)
                         .ThenBy(e => e.District)
                         .ThenBy(e => e.Section)
                         .ToList()
                         .Select(e => new List<string> {
                            e.ApplicationName,
                            e.Province,
                            e.District,
                            e.Section
                         })
                         .ToList();

            var reportStream = GenerateImportTemplate(template, templateData, 4, data);
            return File(reportStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName + ".xlsx");
        }

        [HttpPost]
        public ActionResult ImportMemberServices(string userId)
        {
            var isAdmin = User.IsInRole(ConfigurationValues.ROLES_ADMIN_NAME);
            var isOPDCAdmin = User.IsInRole(ConfigurationValues.ROLES_OPDC_ADMIN_NAME);

            try
            {
                if (Request.Files.Count < 1)
                {
                    throw new Exception("เอกสารนำเข้าข้อมูลไม่ถูกต้อง กรุณาทำการตรวจสอบเอกสาร");
                }

                if (Request.Files[0].ContentType != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    throw new Exception("ชนิดของเอกสารนำเข้าข้อมูลไม่ถูกต้อง ระบบรองรับเฉพระเอกสารประเภท .xlsx เท่านั้น");
                }

                var file = Request.Files[0];
                var fileStream = new MemoryStream();
                file.InputStream.CopyTo(fileStream);
                var excel = new ExcelPackage(fileStream);
                var workSheet = excel.Workbook.Worksheets.First();

                int startRow = 4;
                int startColumn = 1;
                int endRow = workSheet.GetAmountOfImportRow(new List<string> { "A", "B", "C", "D" });
                int endColumn = 4;

                using (var ts = DB.Database.BeginTransaction())
                {
                    var errorCount = 0;

                    // init data
                    var services = GetServiceList("", true).Select(e => e.Value);

                    if (!(isAdmin || isOPDCAdmin))
                    {
                        var memberManageServiceIds = DB.MemberManageServices
                                                       .Where(e => e.UserID == CurrentUserID && !e.IsDeleted)
                                                       .Select(e => e.ApplicationID.ToString())
                                                       .ToList();

                        services = services.Where(e => memberManageServiceIds.Contains(e)).ToList();
                    }

                    // clear data
                    var memberServices = DB.MemberServices.Where(e => e.UserID == userId && services.Any(a => a == e.ApplicationID.ToString())).ToList();
                    var memberServiceIds = memberServices.Select(e => e.MemberServiceID).ToList();
                    var memberServiceAreas = DB.MemberServiceAreas.Where(e => memberServiceIds.Contains(e.MemberServiceID)).ToList();

                    DB.MemberServiceAreas.RemoveRange(memberServiceAreas);
                    DB.MemberServices.RemoveRange(memberServices);
                    DB.SaveChanges();

                    // import data
                    var CurrentDate = DateTime.Now;
                    var importedMemberServices = new List<MemberService>();
                    var importedMemberServiceAreas = new List<MemberServiceArea>();

                    for (int i = startRow; i <= endRow; i++)
                    {
                        ExcelRange rowCalulateData = workSheet.Cells[i, endColumn + 2, i, endColumn + 5];
                        ExcelRange rowHighlight = workSheet.Cells[i, startColumn, i, endColumn + 1];
                        ExcelRange remark = workSheet.Cells[i, endColumn + 1];
                        ExcelRange applicationID = workSheet.Cells[i, endColumn + 2];
                        ExcelRange provinceID = workSheet.Cells[i, endColumn + 3];
                        ExcelRange districtID = workSheet.Cells[i, endColumn + 4];
                        ExcelRange sectionID = workSheet.Cells[i, endColumn + 5];

                        // calulate data
                        applicationID.Formula = "INDEX(ข้อมูลพื้นฐาน!G:G,  MATCH(A" + i + ",ข้อมูลพื้นฐาน!A:A,0))";
                        provinceID.Formula = "INDEX(ข้อมูลพื้นฐาน!H:H,  MATCH(B" + i + ",ข้อมูลพื้นฐาน!B:B,0))";
                        districtID.Formula = "INDEX(ข้อมูลพื้นฐาน!I:I, MATCH(C" + i + ",ข้อมูลพื้นฐาน!D:D,0))";
                        sectionID.Formula = "INDEX(ข้อมูลพื้นฐาน!J:J,  MATCH(D" + i + ",ข้อมูลพื้นฐาน!F:F,0))";

                        ExcelRange row = workSheet.Cells[i, startColumn, i, endColumn + 5];
                        row.Calculate();

                        if (!row.IsNullOrEmpty())
                        {
                            try
                            {
                                var importData = row.ConvertRowToObjects<MemberServiceImportViewModel>();

                                // ถ้าไม่มี data จะ ignore 
                                if (!(string.IsNullOrEmpty(importData.ApplicationName) && string.IsNullOrEmpty(importData.Province) && string.IsNullOrEmpty(importData.District) && string.IsNullOrEmpty(importData.Section)))
                                {
                                    int memberServiceId = importedMemberServices.Where(e => e.ApplicationID == importData.ApplicationID)
                                                                           .Select(e => e.MemberServiceID)
                                                                           .FirstOrDefault();
                                    // check import data
                                    var errorMessages = new List<string>();

                                    if (userId == null || !DB.Users.Any(e => e.Id == userId))
                                    {
                                        errorMessages.Add("ไม่พบผู้ใช้งานนี้ในระบบ");
                                    }

                                    if (!(isAdmin || isOPDCAdmin))
                                    {
                                        var aid = importData.ApplicationID.ToString();
                                        if (!services.Contains(aid))
                                        {
                                            throw new ImportException("ไม่สามารถแก้ไขข้อมูลได้ เนื่องจากท่านไม่ได้รับสิทธิ์ในการจัดการบริการนี้");
                                        }
                                    }

                                    if (importData.ApplicationID == 0)
                                    {
                                        errorMessages.Add("ไม่พบบริการนี้ในระบบ");
                                    }

                                    // กรณีเลือกจังหวัดเป็นทุกพื้นที่
                                    if (importData.ProvinceID == -1)
                                    {
                                        if (!string.IsNullOrEmpty(importData.District))
                                        {
                                            errorMessages.Add("อำเภอไม่ถูกต้อง");
                                        }

                                        if (!string.IsNullOrEmpty(importData.Section))
                                        {
                                            errorMessages.Add("ตำบลไม่ถูกต้อง");
                                        }
                                    }
                                    else
                                    {
                                        // กรณีไม่เลือกจังหวัด
                                        if (importData.ProvinceID == 0)
                                        {
                                            errorMessages.Add("ไม่พบจังหวัดนี้ในระบบ");
                                        }
                                        else
                                        {
                                            // กรณีไม่เลือกอำเภอ แต่เลือกตำบล
                                            if (importData.DistrictID == 0 && importData.SectionID > 0)
                                            {
                                                errorMessages.Add("อำเภอไม่ถูกต้อง");
                                            }
                                        }
                                    }

                                    if (errorMessages.Count > 0)
                                    {
                                        throw new ImportException(string.Join(", ", errorMessages));
                                    }

                                    // import member service
                                    if (memberServiceId == 0)
                                    {
                                        var memberService = new MemberService
                                        {
                                            UserID = userId,
                                            ApplicationID = importData.ApplicationID,
                                            CreatedUserID = CurrentUserID,
                                            CreatedDate = CurrentDate,
                                            UpdatedUserID = CurrentUserID,
                                            UpdatedDate = CurrentDate,
                                            IsDeleted = false
                                        };

                                        DB.MemberServices.Add(memberService);
                                        DB.SaveChanges();

                                        importedMemberServices.Add(memberService);
                                        memberServiceId = memberService.MemberServiceID;
                                    }

                                    // import member service area
                                    if (memberServiceId > 0)
                                    {
                                        var memberServiceArea = new MemberServiceArea
                                        {
                                            MemberServiceID = memberServiceId,
                                            Province = importData.Province,
                                            District = importData.District,
                                            Section = importData.Section,
                                            ProvinceID = string.IsNullOrEmpty(importData.Province) ? -1 : importData.ProvinceID,
                                            DistrictID = string.IsNullOrEmpty(importData.District) ? -1 : importData.DistrictID,
                                            SectionID = string.IsNullOrEmpty(importData.Section) ? -1 : importData.SectionID,
                                            CreatedUserID = CurrentUserID,
                                            CreatedDate = CurrentDate,
                                            UpdatedUserID = CurrentUserID,
                                            UpdatedDate = CurrentDate,
                                            IsDeleted = false
                                        };

                                        var isDupplicate = importedMemberServiceAreas.Any(e => e.MemberServiceID == memberServiceArea.MemberServiceID && e.ProvinceID == memberServiceArea.ProvinceID && e.DistrictID == memberServiceArea.DistrictID && e.SectionID == memberServiceArea.SectionID);
                                        var isAllArea = (memberServiceArea.ProvinceID == -1 && memberServiceArea.DistrictID == -1 && memberServiceArea.SectionID == -1);

                                        if (!(isDupplicate || isAllArea))
                                        {
                                            DB.MemberServiceAreas.Add(memberServiceArea);
                                            DB.SaveChanges();

                                            importedMemberServiceAreas.Add(memberServiceArea);
                                        }
                                    }
                                    else
                                    {
                                        throw new ImportException("ไม่สามารถเพิ่มบริการนี้ได้");
                                    }
                                }

                                remark.Value = "";
                                remark.Style.Font.Color.SetColor(Color.Black);
                                rowHighlight.Style.Fill.PatternType = ExcelFillStyle.None;
                            }
                            catch (Exception ex)
                            {
                                var message = ex.Message;

                                if (ex.GetType() != typeof(ImportException))
                                {
                                    message = "เกิดข้อผิดพลาด ex : " + ex.Message;
                                }

                                remark.Value = message;
                                remark.Style.Font.Color.SetColor(Color.Red);
                                rowHighlight.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                rowHighlight.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFCCCC"));

                                errorCount = errorCount + 1;
                            }
                        }

                        // clear calculate data
                        applicationID.Value = "";
                        provinceID.Value = "";
                        districtID.Value = "";
                        sectionID.Value = "";
                    }

                    if (errorCount == 0)
                    {
                        ts.Commit();
                        return Json(new { Status = true, Message = "นำเข้าข้อมูลสำเร็จ" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var filename = CurrentDate.ToString("yyyyMMddhhmmss") + "_" + file.FileName;
                        var fileRelativePath = "~/content/upload/importMembersService/";
                        var fileAbsolutePath = Server.MapPath(fileRelativePath) + filename;
                        var fileInfo = new System.IO.FileInfo(fileAbsolutePath);

                        excel.SaveAs(fileInfo);

                        return Json(new { Status = false, Message = "ไม่สามารถนำเข้าข้อมูลได้ กรุณาตรวจสอบเอกสาร", Url = Url.Content(fileRelativePath + filename) }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}