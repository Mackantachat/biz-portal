using BizPortal.WindowsService.Jobs;
using log4net;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace BizPortal.WindowsService
{
    //public partial class BizAutomation : ServiceBase
    //{
    //    private static ILog logger = LogManager.GetLogger("BizEventLogger");

    //    public BizAutomation()
    //    {
    //        InitializeComponent();
    //    }

    //    public void OnDebug()
    //    {
    //        OnStart(null);
    //    }

    //    protected override async void OnStart(string[] args)
    //    {
    //        logger.Info("BizAutomation started.");

    //        IScheduler sched = await StdSchedulerFactory.GetDefaultScheduler();
    //        sched.Start();

    //        IJobDetail reExFeiledAppHookJob = JobBuilder.Create<ReExecuteFailedAppHook>()
    //            .WithIdentity(ReExecuteFailedAppHook.JobName, "Default")
    //            .Build();

    //        ITrigger hourlyTrigger = TriggerBuilder.Create()
    //           .WithIdentity("hourlyTrigger", "Default")
    //           .WithSimpleSchedule(x => x.WithIntervalInMinutes(15).RepeatForever().WithMisfireHandlingInstructionIgnoreMisfires())
    //           .Build();

    //        var _globalJobListener = new GlobalJobListener();
    //        sched.ListenerManager.AddJobListener(_globalJobListener);

    //        await sched.ScheduleJob(reExFeiledAppHookJob, hourlyTrigger);
    //    }

    //    protected override async void OnStop()
    //    {
    //        IScheduler sched = await StdSchedulerFactory.GetDefaultScheduler();
    //        await sched.Shutdown();

    //        logger.Info("BizAutomation stopped.");
    //    }
    //}

    //public class GlobalJobListener : IJobListener
    //{
    //    public GlobalJobListener()
    //    {
    //    }

    //    public virtual string Name
    //    {
    //        get { return "MainJobListener"; }
    //    }

    //    public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default(CancellationToken))
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void JobToBeExecuted(IJobExecutionContext context)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default(CancellationToken))
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default(CancellationToken))
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
