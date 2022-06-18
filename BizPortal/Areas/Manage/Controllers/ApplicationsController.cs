using BizPortal.Integrated;
using BizPortal.Models;
using BizPortal.ViewModels;
using BizPortal.ViewModels.Select2;
using Mapster;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using XperiCode.PluploadMvc;

namespace BizPortal.Areas.Manage.Controllers
{
    [Authorize(Roles = ConfigurationValues.ROLES_ADMIN_NAME)]
    public class ApplicationsController : ManageControllerBase
    {
        // GET: Manage/Applications
        public ActionResult Index()
        {
            ViewBag.ActiveMenu = PageNameBackendEnum.APP_MANAGE.GetEnumStringValue();
            return View();
        }

        public ActionResult Add()
        {
            ViewBag.jsonSPersons = "[]";
            ViewBag.jsonSPositions = "[]";
            ViewBag.jsonPED = "[]";
            ViewBag.jsonPEDJuristics = "[]";
            ViewBag.ActiveMenu = PageNameBackendEnum.APP_MANAGE.GetEnumStringValue();
            ViewBag.OrganizationList = new SelectList(GetOrganizationList(), "Value", "Text");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(ApplicationViewModel model)
        {



            if (!ModelState.IsValid)
            {
                ViewBag.OrganizationList = new SelectList(GetOrganizationList(), "Value", "Text");
                return View(model);
            }




            if (!DB.Organizations.Where(o => o.OrgCode == model.OrgCode).Any())
            {
                EGovServices svc = new EGovServices();
                Integrated.Models.Organization egovOrg = svc.GetOrganizationByOrgCode(model.OrgCode, "th-TH");
                if (egovOrg != null)
                {
                    Organization org = new Organization()
                    {
                        OrgCode = egovOrg.Code,
                        OrgSysName = egovOrg.Name,
                        MinistryCode = egovOrg.MinistryCode,
                        DepartmentCode = egovOrg.DepartmentCode,
                        DivisionCode = egovOrg.DivisionCode,
                        LogoUrl = egovOrg.LogoURL,
                        Url = egovOrg.Url,
                        OrganizationTranslations = new HashSet<OrganizationTranslation>()
                    };
                    DB.Organizations.Add(org);
                    OrganizationTranslation orgTH = new OrganizationTranslation()
                    {
                        LanguageID = DB.Languages.Where(o => o.TwoLetterISOLanguageName == "th").Select(o => o.LanguageID).Single(),
                        OrgName = egovOrg.Name,
                        Address = egovOrg.Address,
                        Abbreviation = egovOrg.Abbreviation
                    };
                    org.OrganizationTranslations.Add(orgTH);

                    egovOrg = svc.GetOrganizationByOrgCode(model.OrgCode, "en-US");
                    if (egovOrg != null)
                    {
                        OrganizationTranslation orgEN = new OrganizationTranslation()
                        {
                            LanguageID = DB.Languages.Where(o => o.TwoLetterISOLanguageName == "en").Select(o => o.LanguageID).Single(),
                            OrgName = egovOrg.Name,
                            Address = egovOrg.Address,
                            Abbreviation = egovOrg.Abbreviation
                        };
                        org.OrganizationTranslations.Add(orgEN);
                    }
                }
            }

            Application app = new Application();
            ApplicationTranslation appTran = new ApplicationTranslation()
            {
                LanguageID = DB.CurrentLanguageID.Value
            };
            SigningPerson appSignPerson = new SigningPerson();
            SigningPosition appSignPosition = new SigningPosition();
            SigningExtendedData appPaymentExtendData = new SigningExtendedData();
            TypeAdapter.Adapt<ApplicationViewModel, Application>(model, app);
            TypeAdapter.Adapt<ApplicationViewModel, ApplicationTranslation>(model, appTran);
            TypeAdapter.Adapt<ApplicationViewModel, SigningPerson>(model, appSignPerson);
            TypeAdapter.Adapt<ApplicationViewModel, SigningPosition>(model, appSignPosition);
            TypeAdapter.Adapt<ApplicationViewModel, SigningExtendedData>(model, appPaymentExtendData);

            app.Remark = app.ShowRemark ? app.Remark : string.Empty;
            app.CitizenRemark = app.CitizenShowRemark ? app.CitizenRemark : string.Empty;

            app.ApplicationTranslations.Add(appTran);




            //add signature
            app.IsEnableELicense = app.IsEnableELicense ? app.IsEnableELicense : false;
            app.SigningDocumentType = app.IsEnableELicense ? app.SigningDocumentType : string.Empty;
            app.SigningType = app.IsEnableELicense ? app.SigningType : string.Empty;
            app.SigningDocumentCitizenTemplateID = app.SigningDocumentType != string.Empty ? app.SigningDocumentCitizenTemplateID : string.Empty;
            app.SigningDocumentJuristicTemplateID= app.SigningDocumentType != string.Empty ? app.SigningDocumentJuristicTemplateID : string.Empty;
            if (app.SigningType == EDocumentType.OrgByPerson.ToString() || app.SigningType == EDocumentType.Personal.ToString())
                {
                    var SigningPositions = Request["lsSigningPositions"];
                    var SigningPersons = Request["lsSigningPersons"];
                    var ds_SigningPositions = JsonConvert.DeserializeObject<ICollection<SigningPosition>>(SigningPositions);
                    var ds_SigningPersons = JsonConvert.DeserializeObject<ICollection<SigningPerson>>(SigningPersons);

                    if (ds_SigningPositions.Count == 0 || ds_SigningPersons.Count == 0)
                    {
                      
                        ViewBag.jsonSPersons = "[]";
                        ViewBag.jsonSPositions = "[]";
                        ViewBag.OrganizationList = new SelectList(GetOrganizationList(), "Value", "Text");
                        ViewBag.Error = ("ข้อมูลตำแหน่งหรือบุคคลผู้มีอำนาจการลงลายมือชื่อแบบดิจิทัลไม่ครบถ้วน กรุณาลองใหม่อีกครั้ง");

                        return View(model);
                    }
                    else
                    {
                        foreach (var sp in ds_SigningPositions)
                        {
                            appSignPosition = sp;
                            app.SigningPositions.Add(appSignPosition);
                        }
                        foreach (var item in ds_SigningPersons)
                        {
                            appSignPerson = item;
                            app.SigningPersons.Add(appSignPerson);
                        }

                    }



                    // DB.SigningPersons.AddRange(ds_SigningPersons);
                

                // DB.SigningPersons.AddRange(ds_SigningPersons);
            }
            // 
            if ((app.SigningDocumentType == EDocumentPermitType.Template.ToString() && app.SigningType != EDocumentType.NotSign.ToString()))
            {
                var PaymentExtendData = Request["lsPED"];

                var ds_PaymentExtendData = JsonConvert.DeserializeObject<ICollection<SigningExtendedData>>(PaymentExtendData);

                if (ds_PaymentExtendData.Count > 0)
                {
                    foreach (var ped in ds_PaymentExtendData)
                    {
                        appPaymentExtendData = ped;
                        app.SigningExtendedDatas.Add(appPaymentExtendData);
                    }
                }
                var PaymentExtendDataJuristic = Request["lsPEDJuristics"];

                var ds_PaymentExtendDataJuristic = JsonConvert.DeserializeObject<ICollection<SigningExtendedData>>(PaymentExtendDataJuristic);

                if (ds_PaymentExtendDataJuristic.Count > 0)
                {
                    foreach (var ped in ds_PaymentExtendDataJuristic)
                    {
                        appPaymentExtendData = ped;
                        app.SigningExtendedDatas.Add(appPaymentExtendData);
                    }
                }
            }
          
            //
            //DB.SigningPersons.Add(appSign);
            DB.Applications.Add(app);
            // Update Logo
            IPluploadContext pluploadContext = null;
            IEnumerable<HttpPostedFileBase> uploadedFiles = null;

            if (model.FileUploadRefCode != null && !string.IsNullOrEmpty(model.FileUploadName))
            {
                pluploadContext = HttpContext.GetPluploadContext();
                uploadedFiles = pluploadContext.GetFiles(model.FileUploadRefCode.Value.ToString());

                if (uploadedFiles != null && uploadedFiles.Count() > 0)
                {
                    var file = uploadedFiles.Where(o => o.FileName == model.FileUploadName).FirstOrDefault();
                    if (file != null)
                    {
                        var imagePath = "~/Uploads/apps/logos/";

                        FileUpload fileUpload = new FileUpload();
                        fileUpload.FileSysName = string.Format("{0}{1}", Guid.NewGuid(), Path.GetExtension(file.FileName));
                        fileUpload.FileName = file.FileName;
                        fileUpload.AbsolutePath = imagePath + fileUpload.FileSysName;
                        fileUpload.ContentLength = file.ContentLength;
                        fileUpload.ContentType = file.ContentType;

                        file.SaveAs(Server.MapPath(fileUpload.AbsolutePath));

                        DB.Entry(fileUpload).State = System.Data.Entity.EntityState.Added;
                        app.LogoFileUpload = fileUpload;
                    }
                }
            }

            DB.SaveChanges();

            return RedirectToAction("Edit", new { id = app.ApplicationID });
        }

        public ActionResult Edit(int id)
        {
            ViewBag.ActiveMenu = PageNameBackendEnum.APP_MANAGE.GetEnumStringValue();
            ApplicationViewModel model = DB.Applications.Where(o => o.ApplicationID == id)
                .Select(o => new ApplicationViewModel()
                {
                    ApplicationID = o.ApplicationID,
                    ApplicationSysName = o.ApplicationSysName,
                    OrgCode = o.OrgCode,
                    ConsumerKey = o.ConsumerKey,
                    ApplicationUrl = o.ApplicationUrl,
                    AppsHookClassName = o.AppsHookClassName,
                    SingleFormEnabled = o.SingleFormEnabled,
                    TemporaryDisable = o.TemporaryDisable,
                    TemporaryRemark = o.TemporaryRemark,
                    HandbookUrl = o.HandbookUrl,
                    OperatingCost = o.OperatingCost,
                    OperatingDays = o.OperatingDays,
                    LogoFileID = o.LogoFileID,
                    OperatingCostType = o.OperatingCostType,
                    OperatingCost2 = o.OperatingCost2,
                    OperatingDayType = o.OperatingDayType,
                    OperatingDays2 = o.OperatingDays2,
                    ShowRemark = o.ShowRemark,
                    Remark = o.Remark,
                    CitizenApplicationUrl = o.CitizenApplicationUrl,
                    CitizenHandbookUrl = o.CitizenHandbookUrl,
                    CitizenOperatingCostType = o.CitizenOperatingCostType,
                    CitizenOperatingCost = o.CitizenOperatingCost,
                    CitizenOperatingCost2 = o.CitizenOperatingCost2,
                    CitizenOperatingDayType = o.CitizenOperatingDayType,
                    CitizenOperatingDays = o.CitizenOperatingDays,
                    CitizenOperatingDays2 = o.CitizenOperatingDays2,
                    CitizenShowRemark = o.CitizenShowRemark,
                    CitizenRemark = o.CitizenRemark,
                    StatusSequence = o.StatusSequence,
                    IsEnableELicense = o.IsEnableELicense,
                    SigningType = o.SigningType,
                    SigningDocumentType = o.SigningDocumentType,
                    SigningDocumentCitizenTemplateID = o.SigningDocumentCitizenTemplateID,
                    SigningDocumentJuristicTemplateID = o.SigningDocumentJuristicTemplateID
                }).SingleOrDefault();

            if (model == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            var SPersons = DB.SigningPersons.Where(o => o.ApplicationID == id).ToList();

            ViewBag.jsonSPersons = JsonConvert.SerializeObject(SPersons);

            var SPositions = DB.SigningPositions.Where(o => o.ApplicationID == id).ToList();

            ViewBag.jsonSPositions = JsonConvert.SerializeObject(SPositions);
            var PEDs = DB.SigningExtendedDatas.Where(o => o.ApplicationID == id && o.UserType == UserTypeEnum.Citizen).ToList();
            ViewBag.jsonPED = JsonConvert.SerializeObject(PEDs);
            var PEDJuristics = DB.SigningExtendedDatas.Where(o => o.ApplicationID == id && o.UserType == UserTypeEnum.Juristic).ToList();
            ViewBag.jsonPEDJuristics = JsonConvert.SerializeObject(PEDJuristics);
            var tran = DB.ApplicationTranslations.Where(o => o.ApplicationID == id && o.LanguageID == DB.CurrentLanguageID)
               .Select(o => new { o.ApplicationName, o.ApplicationDetail, o.ApprovedMailMessage, o.RejectedMailMessage, o.SubmitMailMessage }).SingleOrDefault();

            if (tran != null)
            {
                model.ApplicationName = tran.ApplicationName;
                model.ApplicationDetail = tran.ApplicationDetail;
                model.ApprovedMailMessage = tran.ApprovedMailMessage;
                model.RejectedMailMessage = tran.RejectedMailMessage;
                model.SubmitMailMessage = tran.SubmitMailMessage;
            }

            ViewBag.OrganizationList = new SelectList(GetOrganizationList(), "Value", "Text");

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id, ApplicationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.OrganizationList = new SelectList(GetOrganizationList(), "Value", "Text");
                return View(model);
            }

            if (!DB.Organizations.Where(o => o.OrgCode == model.OrgCode).Any())
            {
                EGovServices svc = new EGovServices();
                Integrated.Models.Organization egovOrg = svc.GetOrganizationByOrgCode(model.OrgCode, "th-TH");
                if (egovOrg != null)
                {
                    Organization org = new Organization()
                    {
                        OrgCode = egovOrg.Code,
                        OrgSysName = egovOrg.Name,
                        MinistryCode = egovOrg.MinistryCode,
                        DepartmentCode = egovOrg.DepartmentCode,
                        DivisionCode = egovOrg.DivisionCode,
                        LogoUrl = egovOrg.LogoURL,
                        Url = egovOrg.Url,
                        OrganizationTranslations = new HashSet<OrganizationTranslation>()
                    };
                    DB.Organizations.Add(org);
                    OrganizationTranslation orgTH = new OrganizationTranslation()
                    {
                        LanguageID = DB.Languages.Where(o => o.TwoLetterISOLanguageName == "th").Select(o => o.LanguageID).Single(),
                        OrgName = egovOrg.Name,
                        Address = egovOrg.Address,
                        Abbreviation = egovOrg.Abbreviation
                    };
                    org.OrganizationTranslations.Add(orgTH);

                    egovOrg = svc.GetOrganizationByOrgCode(model.OrgCode, "en-US");
                    if (egovOrg != null)
                    {
                        OrganizationTranslation orgEN = new OrganizationTranslation()
                        {
                            LanguageID = DB.Languages.Where(o => o.TwoLetterISOLanguageName == "en").Select(o => o.LanguageID).Single(),
                            OrgName = egovOrg.Name,
                            Address = egovOrg.Address,
                            Abbreviation = egovOrg.Abbreviation
                        };
                        org.OrganizationTranslations.Add(orgEN);
                    }
                }
            }

            Application app = DB.Applications.Where(o => o.ApplicationID == id).SingleOrDefault();
            if (app == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            DB.Entry(app).State = System.Data.Entity.EntityState.Modified;

            ApplicationTranslation appTran = DB.ApplicationTranslations.Where(o => o.ApplicationID == id && o.Language.TwoLetterISOLanguageName == CurrentCulture).SingleOrDefault();
            if (appTran == null)
            {
                appTran = new ApplicationTranslation()
                {
                    LanguageID = DB.CurrentLanguageID.Value
                };
                app.ApplicationTranslations.Add(appTran);
            }
            else
            {
                DB.Entry(appTran).State = System.Data.Entity.EntityState.Modified;
            }
            SigningPerson appSignPerson = new SigningPerson();
            SigningPosition appSignPosition = new SigningPosition();
            SigningExtendedData appPaymentExtendData = new SigningExtendedData();
         
            TypeAdapter.Adapt<ApplicationViewModel, Application>(model, app);
            TypeAdapter.Adapt<ApplicationViewModel, ApplicationTranslation>(model, appTran);
            TypeAdapter.Adapt<ApplicationViewModel, SigningPerson>(model, appSignPerson);
            TypeAdapter.Adapt<ApplicationViewModel, SigningPosition>(model, appSignPosition);
            TypeAdapter.Adapt<ApplicationViewModel, SigningExtendedData>(model, appPaymentExtendData);

            app.Remark = app.ShowRemark ? app.Remark : string.Empty;
            app.CitizenRemark = app.CitizenShowRemark ? app.CitizenRemark : string.Empty;
            app.TemporaryRemark = app.TemporaryDisable ? app.TemporaryRemark : string.Empty;
            // sign e-doc
            app.IsEnableELicense = app.IsEnableELicense ? app.IsEnableELicense : false;
            app.SigningDocumentType = app.IsEnableELicense ? app.SigningDocumentType : string.Empty;
            app.SigningType = app.IsEnableELicense ? app.SigningType : string.Empty;
            //SigningPerson sp = DB.SigningPersons.Where(o => o.ApplicationID == id ).SingleOrDefault(); 
            //DB.SigningPersons.Remove(sp);
            app.SigningDocumentCitizenTemplateID = app.SigningDocumentType != string.Empty ? app.SigningDocumentCitizenTemplateID : string.Empty;
            app.SigningDocumentJuristicTemplateID = app.SigningDocumentType != string.Empty ? app.SigningDocumentJuristicTemplateID : string.Empty;
            DB.SigningPersons.RemoveRange(DB.SigningPersons.Where(o => o.ApplicationID == id));
            DB.SigningPositions.RemoveRange(DB.SigningPositions.Where(o => o.ApplicationID == id));
            DB.SigningExtendedDatas.RemoveRange(DB.SigningExtendedDatas.Where(o => o.ApplicationID == id));
            if (app.IsEnableELicense)
            {
            if (app.SigningType == EDocumentType.OrgByPerson.ToString() || app.SigningType == EDocumentType.Personal.ToString())
            {
                var SigningPositions = Request["lsSigningPositions"];
                var SigningPersons = Request["lsSigningPersons"];
                var ds_SigningPositions = JsonConvert.DeserializeObject<ICollection<SigningPosition>>(SigningPositions);
                var ds_SigningPersons = JsonConvert.DeserializeObject<ICollection<SigningPerson>>(SigningPersons);

                if (ds_SigningPositions.Count == 0 || ds_SigningPersons.Count == 0)
                {
                    var SPersons = DB.SigningPersons.Where(o => o.ApplicationID == id).ToList();

                    ViewBag.jsonSPersons = JsonConvert.SerializeObject(SPersons);

                    var SPositions = DB.SigningPositions.Where(o => o.ApplicationID == id).ToList();

                    ViewBag.jsonSPositions = JsonConvert.SerializeObject(SPositions);
                    ViewBag.ActiveMenu = PageNameBackendEnum.APP_MANAGE.GetEnumStringValue();
                    ViewBag.OrganizationList = new SelectList(GetOrganizationList(), "Value", "Text");
                    ViewBag.Error = ("ข้อมูลตำแหน่งหรือบุคคลผู้มีอำนาจการลงลายมือชื่อแบบดิจิทัลไม่ครบถ้วน กรุณาลองใหม่อีกครั้ง");

                    return View(model);
                }
                else
                {
                    foreach (var sp in ds_SigningPositions)
                    {
                        appSignPosition = sp;
                        app.SigningPositions.Add(appSignPosition);
                    }
                    foreach (var item in ds_SigningPersons)
                    {
                        appSignPerson = item;
                        app.SigningPersons.Add(appSignPerson);
                    }
                    
                }



                // DB.SigningPersons.AddRange(ds_SigningPersons);
            }

            if ((app.SigningDocumentType == EDocumentPermitType.Template.ToString() && app.SigningType != EDocumentType.NotSign.ToString()))
            {
                var PaymentExtendData = Request["lsPED"];

                var ds_PaymentExtendData = JsonConvert.DeserializeObject<ICollection<SigningExtendedData>>(PaymentExtendData);

                if (ds_PaymentExtendData.Count > 0)
                {
                    foreach (var ped in ds_PaymentExtendData)
                    {
                        appPaymentExtendData = ped;
                        app.SigningExtendedDatas.Add(appPaymentExtendData);
                    }
                }
                var PaymentExtendDataJuristic = Request["lsPEDJuristics"];

                var ds_PaymentExtendDataJuristic = JsonConvert.DeserializeObject<ICollection<SigningExtendedData>>(PaymentExtendDataJuristic);

                if (ds_PaymentExtendDataJuristic.Count > 0)
                {
                    foreach (var ped in ds_PaymentExtendDataJuristic)
                    {
                        appPaymentExtendData = ped;
                        app.SigningExtendedDatas.Add(appPaymentExtendData);
                    }
                }
            }
            }
            // Update Logo
            IPluploadContext pluploadContext = null;
            IEnumerable<HttpPostedFileBase> uploadedFiles = null;

            if (model.FileUploadRefCode != null && !string.IsNullOrEmpty(model.FileUploadName))
            {
                pluploadContext = HttpContext.GetPluploadContext();
                uploadedFiles = pluploadContext.GetFiles(model.FileUploadRefCode.Value.ToString());

                if (uploadedFiles != null && uploadedFiles.Count() > 0)
                {
                    var file = uploadedFiles.Where(o => o.FileName == model.FileUploadName).FirstOrDefault();
                    if (file != null)
                    {
                        var imagePath = "~/Uploads/apps/logos/";

                        FileUpload fileUpload = new FileUpload();
                        fileUpload.FileSysName = string.Format("{0}{1}", Guid.NewGuid(), Path.GetExtension(file.FileName));
                        fileUpload.FileName = file.FileName;
                        fileUpload.AbsolutePath = imagePath + fileUpload.FileSysName;
                        fileUpload.ContentLength = file.ContentLength;
                        fileUpload.ContentType = file.ContentType;

                        file.SaveAs(Server.MapPath(fileUpload.AbsolutePath));

                        DB.Entry(fileUpload).State = System.Data.Entity.EntityState.Added;
                        app.LogoFileUpload = fileUpload;
                    }
                }
            }
            else
            {
                DB.Entry(app).Property(o => o.LogoFileID).IsModified = false;
            }

            DB.SaveChanges();

            return RedirectToAction("Edit", new { id = app.ApplicationID });
        }
    }
}