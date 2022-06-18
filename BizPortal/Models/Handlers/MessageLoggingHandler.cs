using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using EGA.EGA_CentralLog.Util;

namespace BizPortal.Models.Handlers
{
    public class MessageLoggingHandler : MessageHandler
    {
        protected override async Task IncommingMessageAsync(string correlationId, string requestInfo, byte[] message, Guid consumerKey)
        {
            // *** เอา command ออก ก่อน push code
            //await Task.Run(() =>
            //    LogProvider.AddLog(ConfigurationManager.AppSettings["ConsumerKey"], LogAction.Access, LogLevel.Verbose, "Request - " + requestInfo, consumerKey, correlationId, "", Encoding.UTF8.GetString(message), LogResult.Unknown));
        }

        protected override async Task OutgoingMessageAsync(string correlationId, string requestInfo, byte[] message, Guid consumerKey)
        {
            // *** เอา command ออก ก่อน push code
            //await Task.Run(() =>
            //    LogProvider.AddLog(ConfigurationManager.AppSettings["ConsumerKey"], LogAction.Access, LogLevel.Verbose, "Response - " + requestInfo, consumerKey, correlationId, "", Encoding.UTF8.GetString(message), LogResult.Unknown));
        }
    }
}