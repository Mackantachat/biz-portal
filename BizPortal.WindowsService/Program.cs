using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.WindowsService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            log4net.Config.XmlConfigurator.Configure();

#if DEBUG
            //BizJobScheduler service = new BizJobScheduler();
            //service.OnDebug();
            //System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new BizJobScheduler()
            };
            ServiceBase.Run(ServicesToRun);
#else
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new BizJobScheduler()
            };
            ServiceBase.Run(ServicesToRun);
#endif
        }
    }
}
