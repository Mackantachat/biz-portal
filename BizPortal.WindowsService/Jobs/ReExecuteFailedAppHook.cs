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

namespace BizPortal.WindowsService.Jobs
{
    /// <summary>
    /// สำหรับดึงข้อมูล AppHook ที่ได้รับคำสั่งให้ทำงานอีกทั้งเนื่องจากสาเหตุต่างๆ เช่น ไม่สามารถเชื่อมต่อ server ปลายทางได้ เป็นต้น
    /// </summary>
    [DisallowConcurrentExecution]
    public class ReExecuteFailedAppHook : IJob
    {
        private static ILog logger = LogManager.GetLogger("BizEventLogger");
        public const string JobName = "ReExecuteFailedAppHook";

        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                logger.Info(JobName + " started.");

                var repo = MongoFactory.GetApplicationRequestCollection().AsQueryable();
                var requests = repo.Where(o => o.AppHookInfo.Schedule && o.AppHookInfo.ScheduleDate <= DateTime.Now && o.AppHookInfo.ScheduleCount <= 6).ToList();

                if (requests.Count > 0)
                {
                    logger.InfoFormat("{0:#,##0 request(s) found.", requests.Count);
                    ApplicationDbContext db = new ApplicationDbContext();

                    foreach (var model in requests)
                    {
                        var app = db.Applications.Where(w => w.ApplicationID == model.ApplicationID).SingleOrDefault();
                        if (app == null)
                        {
                            logger.WarnFormat("Request {0}'s application not found.", model.ApplicationRequestNumber);
                            break;
                        }

                        // App Hook
                        if (!string.IsNullOrEmpty(app.AppsHookClassName))
                        {
                            IAppsHook hook = (IAppsHook)Activator.CreateInstance("BizPortal.AppsHook", app.AppsHookClassName).Unwrap();
                            var request = ApplicationRequestEntity.Get(model.ApplicationRequestID);
                            ApplicationRequestViewModel viewModel = new ApplicationRequestViewModel();
                            TypeAdapter.Adapt(model, viewModel);

                            AppsStage hookStage = EnumUtils.GetEnum<AppsStage>(model.AppHookInfo.AppsStage);

                            logger.InfoFormat("Request {0} is executing...", model.ApplicationRequestNumber);

                            var invokeResult = hook.Invoke(hookStage, viewModel, model.AppHookInfo, ref request);
                            if (invokeResult.Success)
                            {
                                request = ApplicationRequestEntity.Get(request.ApplicationRequestID);
                                
                                // Update AppHookInfo
                                if (invokeResult.Data != null)
                                {
                                    request.AppHookInfo = invokeResult.Data;
                                }

                                // Update FileUploaded
                                if (viewModel.UploadedFiles != null)
                                {
                                    foreach (var group in viewModel.UploadedFiles)
                                    {
                                        if (group == null || group.Files == null)
                                        {
                                            continue;
                                        }

                                        var groupDB = request.UploadedFiles.Where(o => o.FileGroupID == group.FileGroupID).SingleOrDefault();
                                        if (groupDB != null)
                                        {
                                            TypeAdapter.Adapt<FileGroup, FileGroupEntity>(group, groupDB);
                                            groupDB.Update();
                                        }
                                    }
                                }
                                request.Update();
                            }
                            else
                            {
                                request.AppHookInfo.ScheduleCount++;
                                request.AppHookInfo.ScheduleDate = DateTime.Now.AddHours(request.AppHookInfo.ScheduleCount);
                                request.Update();
                            }

                            logger.InfoFormat("Request {0} is executed.", model.ApplicationRequestNumber);
                        }
                        else
                        {
                            logger.WarnFormat("{0} application has no AppHook.", app.ApplicationSysName);
                        }
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
    }
}
