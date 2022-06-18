using BizPortal.WindowsService.Jobs;
using Common.Logging;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;

namespace BizPortal.WindowsService
{
    public partial class BizJobScheduler : ServiceBase
    {
        private static ILog logger = LogManager.GetLogger("BizEventLogger");
        private static int summarizeReportJobHourStartAt = int.TryParse(ConfigurationManager.AppSettings["SummarizeReportJobHourStartAt"], out summarizeReportJobHourStartAt) ? summarizeReportJobHourStartAt : 0;
        private static int summarizeReportJobMinuteStartAt = int.TryParse(ConfigurationManager.AppSettings["SummarizeReportJobMinuteStartAt"], out summarizeReportJobMinuteStartAt) ? summarizeReportJobMinuteStartAt : 0;
        public BizJobScheduler()
        {
            InitializeComponent();
        }

        public void OnDebug()
        {
            OnStart(null);
        }

        protected override async void OnStart(string[] args)
        {
            logger.Info("BizJobScheduler - started.");

            IScheduler sched = await StdSchedulerFactory.GetDefaultScheduler();
            await sched.Start();

            IJobDetail summarizeReportJob = JobBuilder.Create<SummarizeReport>()
                .WithIdentity(SummarizeReport.JobName, "Default")
                .Build();

            ITrigger summarizeReportTrigger = TriggerBuilder.Create()
               .WithIdentity("summarizeReportTrigger", "Default")
               .WithDailyTimeIntervalSchedule(s => s.WithIntervalInHours(12).OnEveryDay().StartingDailyAt(new TimeOfDay(summarizeReportJobHourStartAt, summarizeReportJobMinuteStartAt)))
               .Build();

            //ITrigger minutelyTrigger = TriggerBuilder.Create()
            //   .WithIdentity("hourlyTrigger", "Default")
            //   .WithSimpleSchedule(x => x.WithIntervalInMinutes(5).RepeatForever().WithMisfireHandlingInstructionIgnoreMisfires())
            //   .Build();

            await sched.ScheduleJob(summarizeReportJob, summarizeReportTrigger);
        }

        protected override async void OnStop()
        {
            IScheduler sched = await StdSchedulerFactory.GetDefaultScheduler();
            await sched.Shutdown();

            logger.Info("BizJobScheduler - stopped.");
        }
    }
}
