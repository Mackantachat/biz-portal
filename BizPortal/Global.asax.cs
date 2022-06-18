using BizPortal.DAL.MongoDB;
using BizPortal.Models.Handlers;
using Mapster;
using System;
using System.Configuration;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ExpressiveAnnotations.Attributes;
using ExpressiveAnnotations.MvcUnobtrusive.Validators;
using BizPortal.ViewModels;
using BizPortal.Models;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using log4net;
using BizPortal.Utils.Helpers;

namespace BizPortal
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            WebApiConfig.RegisterWebApiFilters(GlobalConfiguration.Configuration.Filters);

            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(RequiredIfAttribute), typeof(RequiredIfValidator));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(AssertThatAttribute), typeof(AssertThatValidator));

            // Logging
            GlobalConfiguration.Configuration.MessageHandlers.Add(new MessageLoggingHandler());

            TypeAdapterConfig<FileItem, FileMetadataEntity>.NewConfig()
                .Map(d => d.CreatedDate, s => s.CreationDate)
                .Map(d => d.FileID, s => s.FileId)
                .Map(d => d.FileName, s => s.Name);

            TypeAdapterConfig<ApplicationViewModel, Application>.NewConfig()
                .Map(d => d.OperatingCost, s => s.OperatingCost, cond => !string.IsNullOrEmpty(cond.OperatingCostType))
                .Map(d => d.OperatingCost2, s => s.OperatingCost2, cond => cond.OperatingCostType == "Range")
                .Map(d => d.CitizenOperatingCost, s => s.CitizenOperatingCost, cond => !string.IsNullOrEmpty(cond.CitizenOperatingCostType))
                .Map(d => d.CitizenOperatingCost2, s => s.CitizenOperatingCost2, cond => cond.CitizenOperatingCostType == "Range")
                //.Map(d => d.OperatingDayType, s => s.OperatingDayType)
                //.Map(d => d.CitizenOperatingDayType, s => s.CitizenOperatingDayType)
                .Map(d => d.OperatingDays, s => s.OperatingDays, cond => !string.IsNullOrEmpty(cond.OperatingDayType))
                .Map(d => d.OperatingDays2, s => s.OperatingDays2, cond => cond.OperatingDayType == "Range")
                .Map(d => d.CitizenOperatingDays, s => s.CitizenOperatingDays, cond => !string.IsNullOrEmpty(cond.CitizenOperatingDayType))
                .Map(d => d.CitizenOperatingDays2, s => s.CitizenOperatingDays2, cond => cond.CitizenOperatingDayType == "Range");

            BsonSerializer.RegisterSerializer(typeof(DateTime), DateTimeSerializer.LocalInstance);
        }

        protected void Application_Error()
        {
            var ex = Server.GetLastError();
            var logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            logger.Error("Application_Error : " + ex.Message, ex);
        }
    }
}
