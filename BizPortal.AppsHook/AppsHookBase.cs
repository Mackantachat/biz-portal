using System;
using System.Collections.Generic;
using BizPortal.ViewModels.V2;
using BizPortal.DAL.MongoDB;
using Newtonsoft.Json.Linq;
using EGA.WS;
using System.Configuration;
using BizPortal.DAL;
using BizPortal.ViewModels.SingleForm;
using System.Threading.Tasks;
using BizPortal.Models;

namespace BizPortal.AppsHook
{
    public abstract class AppsHookBase : IAppsHook
    {
        private ApplicationDbContext _db;
        private EGAWSAPI _api;

        public EGAWSAPI Api
        {
            get
            {
                if (_api == null)
                {
                    _api = EGAWSAPI.CreateInstance(ConfigurationManager.AppSettings["ConsumerKey"], ConfigurationManager.AppSettings["ConsumerSecret"]);
                }

                return _api;
            }
        }

        public ApplicationDbContext DB
        {
            get
            {
                if (_db == null)
                    _db = new ApplicationDbContext();
                return _db;// ?? HttpContext.GetOwinContext().Get<ApplicationDbContext>();
            }
            protected set
            {
                _db = value;
            }
        }

        public string DetailViewName { get; set; }

        public string TrackDetailViewName { get; set; }

        public bool ShowPermitSummaryInSingleFormConfirmScreen
        {
            get
            {
                return false;
            }
        }

        public bool PermitCanBeDeliveredOnPayment
        {
            get
            {
                return false;
            }
        }

        public virtual bool HasOrgPdfForm { get; } = false;

        public string PrintFormTitle
        {
            get
            {
                return null;
            }
        }

        public string PrintFormHeaderRight
        {
            get
            {
                return null;
            }
        }

        public abstract InvokeResult Invoke(AppsStage stage, ApplicationRequestViewModel model, AppHookInfo appHookInfo, ref ApplicationRequestEntity request);

        public virtual Dictionary<string, string> TranslateKeyValue(ApplicationRequestViewModel model)
        {
            return new Dictionary<string, string>();
        }

        public abstract RenderDataResult RenderData(RenderStage stage, ApplicationRequestViewModel model);

        protected AppHookInfo GenerateAppsHookData(AppsHookResult result, AppsStage stage, string message, string data = null, bool schedule = false, DateTime? scheduleDate = null, int scueduleCount = 1)
        {
            AppHookInfo info = new AppHookInfo()
            {
                Result = result.ToString(),
                ExceuteDate = DateTime.Now,
                Schedule = schedule,
                ScheduleCount = schedule ? scueduleCount : 0,
                ScheduleDate = schedule ? scheduleDate != null ? scheduleDate : DateTime.Now.AddHours(scueduleCount) : null,
                Message = message,
                Data = data,
                AppsStage = stage.ToString()
            };

            return info;
        }

        protected JObject GetCitizenProfile(string citizenID)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("CitizenID", citizenID);
            var profile = Api.Get("/dopa/personal/profile/normal", args);
            return profile;
        }

        protected JObject GetJuristicCertificate(string taxID)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("JuristicID", taxID);
            var certificate = Api.Get("/dbd/v2/juristic/certificate/signed");
            return certificate;
        }

        public abstract decimal? CalculateFee(List<ISectionData> sectionData);

        public virtual decimal CalculateEMSFee(List<ISectionData> sectionData)
        {
            return 50;
        }

        public virtual byte[] GetOrgPdfFormContent(ApplicationRequestEntity data, Func<string, string> serverMapPathFunc) => null;

        public virtual bool IsEnabledChat()
        {
            return false;
        }

        public virtual bool IsEnabledExportData(ApplicationStatusV2Enum status)
        {
            return false;
        }

        public virtual bool InvokeSingleForm(Guid trid, string currentSectionGroup, ref SingleFormRequestViewModel model)
        {
            return true;
        }

        public virtual FileMetadataEntity InvokeFilePreDoc(string IdentityID, string FileTypeCode)
        {
            return null;
        }

        public virtual string GenerateRequestData(Guid ApplicationRequestID)
        {
            return string.Empty;
        }

        public virtual JObject GenerateELicenseData(Guid ApplicationRequestID)
        {
            return null;
        }

        public abstract ApplicationRequestTransactionEntity GenerateBizReceipt(ApplicationRequestEntity request, ApplicationRequestTransactionEntity trans, ApplicationUser user);

    }
}
