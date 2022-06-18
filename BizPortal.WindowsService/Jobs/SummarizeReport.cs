using Common.Logging;
using Quartz;
using System;
using System.Linq;
using MongoDB.Driver;
using Mapster;
using BizPortal.AppsHook;
using BizPortal.DAL;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.V2;
using BizPortal.Utils.Extensions;
using MongoDB.Bson;
using System.Threading.Tasks;
using System.Configuration;
using BizPortal.Models;

namespace BizPortal.WindowsService.Jobs
{
    [DisallowConcurrentExecution]
    public class SummarizeReport : IJob
    {
        private static ILog logger = LogManager.GetLogger("BizEventLogger");
        public const string JobName = "SummarizeReport";
        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                logger.Info(JobName + " started.");
                Console.WriteLine(JobName + " started.");

                ApplicationDbContext db = new ApplicationDbContext();

                var repo = MongoFactory.GetApplicationRequestCollection().AsQueryable();
                var requests = repo.Where(x => !x.isDone).ToList();

                Console.WriteLine("{0:#,##0} request(s) found.", requests.Count);
                logger.InfoFormat("{0:#,##0} request(s) found.", requests.Count);

                if (requests.Count > 0)
                {
                    foreach (var request in requests)
                    {
                        var permitSummaryTrans = db.PermitSummaryReports.Where(x => x.ApplicationRequestID == request.ApplicationRequestID.ToString()).FirstOrDefault();
                        if (permitSummaryTrans == null)
                        {
                            permitSummaryTrans = new PermitSummaryReport
                            {
                                IdentityType = request.IdentityType.ToString(),
                                ApplicationID = request.ApplicationID,
                                ApplicationRequestID = request.ApplicationRequestID.ToString(),
                                ApplicationRequestNumber = request.ApplicationRequestNumber,
                                OrgNameTH = request.OrgNameTH,
                                OrgCode = request.OrgCode,
                                PermitName = request.PermitName,
                                AppSysName = request.AppSysName,
                                RequestDate = request.CreatedDate,
                                Status = request.Status.ToString(),
                                StatusOther = request.StatusOther,
                                Fee = request.Fee,
                                EMSFee = request.EMSFee,
                                IdentityID = request.IdentityID,
                                IdentityName = request.IdentityName,
                                CreatedDate = request.CreatedDate,
                                UpdatedDate = request.UpdatedDate,
                                ExpectedFinishDate = request.ExpectedFinishDate,
                                ProvinceID = request.ProvinceID,
                                Province = request.Province,
                                AmphurID = request.AmphurID,
                                Amphur = request.Amphur,
                                TumbolID = request.TumbolID,
                                Tumbol = request.Tumbol,
                                JobUpdatedDate = DateTime.Now,
                            };

                            updateJobStatusDate(request, permitSummaryTrans);
                            permitSummaryTrans = db.PermitSummaryReports.Add(permitSummaryTrans);
                            db.SaveChanges();
                        }
                        else
                        {
                            permitSummaryTrans.IdentityType = request.IdentityType.ToString();
                            permitSummaryTrans.ApplicationID = request.ApplicationID;
                            permitSummaryTrans.ApplicationRequestID = request.ApplicationRequestID.ToString();
                            permitSummaryTrans.ApplicationRequestNumber = request.ApplicationRequestNumber;
                            permitSummaryTrans.OrgNameTH = request.OrgNameTH;
                            permitSummaryTrans.OrgCode = request.OrgCode;
                            permitSummaryTrans.PermitName = request.PermitName;
                            permitSummaryTrans.RequestDate = request.CreatedDate;
                            permitSummaryTrans.Fee = request.Fee;
                            permitSummaryTrans.EMSFee = request.EMSFee;
                            permitSummaryTrans.IdentityID = request.IdentityID;
                            permitSummaryTrans.IdentityName = request.IdentityName;
                            permitSummaryTrans.CreatedDate = request.CreatedDate;
                            permitSummaryTrans.UpdatedDate = request.UpdatedDate;
                            permitSummaryTrans.ExpectedFinishDate = request.ExpectedFinishDate;
                            permitSummaryTrans.ProvinceID = request.ProvinceID;
                            permitSummaryTrans.Province = request.Province;
                            permitSummaryTrans.AmphurID = request.AmphurID;
                            permitSummaryTrans.Amphur = request.Amphur;
                            permitSummaryTrans.TumbolID = request.TumbolID;
                            permitSummaryTrans.Tumbol = request.Tumbol;
                            permitSummaryTrans.JobUpdatedDate = DateTime.Now;
                            permitSummaryTrans.Status = request.Status.ToString();
                            permitSummaryTrans.StatusOther = request.StatusOther;

                            updateJobStatusDate(request, permitSummaryTrans);
                            db.SaveChanges();
                        }

                        Console.WriteLine("Request No. {0}, Done at {1}", request.ApplicationRequestNumber, DateTime.Now.ToString("dd/MM/yy hh:mm:ss"));
                        logger.InfoFormat("Request No. {0}, Done at {1}", request.ApplicationRequestNumber, DateTime.Now.ToString("dd/MM/yy hh:mm:ss"));
                    }
                }
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Error occurs, {0}", ex.Message);
            }
            finally
            {
                logger.Info(JobName + " finished");
            }

            return Task.FromResult(0);
        }

        private void updateJobStatusDate(ApplicationRequestEntity request, PermitSummaryReport permitSummaryTrans)
        {
            switch (request.Status)
            {
                case (ApplicationStatusV2Enum.APPROVED_WAITING_PAY_FEE):
                    permitSummaryTrans.JobApprovedWaitingPayFeeDate = request.UpdatedDate;
                    break;

                case (ApplicationStatusV2Enum.CANCELED):
                    permitSummaryTrans.JobCanceledDate = request.UpdatedDate;
                    break;

                case (ApplicationStatusV2Enum.CHECK):
                    permitSummaryTrans.JobCheckDate = request.UpdatedDate;
                    break;

                case (ApplicationStatusV2Enum.COMPLETED):
                    permitSummaryTrans.JobCompletedDate = request.UpdatedDate;
                    request.isDone = true;
                    request.Update();
                    break;

                case (ApplicationStatusV2Enum.FAILED):
                    permitSummaryTrans.JobFailedDate = request.UpdatedDate;
                    break;

                case (ApplicationStatusV2Enum.INCOMPLETE):
                    permitSummaryTrans.JobIncompleteDate = request.UpdatedDate;
                    break;

                case (ApplicationStatusV2Enum.PAID_FEE_CREATING_LICENSE):
                    permitSummaryTrans.JobPaidFeeCreatingLicenseDate = request.UpdatedDate;
                    break;

                case (ApplicationStatusV2Enum.PENDING):
                    permitSummaryTrans.JobPendingDate = request.UpdatedDate;
                    break;

                case (ApplicationStatusV2Enum.REJECTED):
                    permitSummaryTrans.JobRejectedDate = request.UpdatedDate;
                    request.isDone = true;
                    request.Update();
                    break;

                case (ApplicationStatusV2Enum.WAITING):
                    permitSummaryTrans.JobWaitingDate = request.UpdatedDate;
                    break;

                default:
                    break;
            }
        }
    }
}
