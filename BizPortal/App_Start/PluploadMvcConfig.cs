using System.Web.Mvc;
using XperiCode.PluploadMvc;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(BizPortal.App_Start.PluploadMvcConfig), "PreStart")]

namespace BizPortal.App_Start
{
    public class PluploadMvcConfig
    {
        public static void PreStart() 
        { 
            ModelBinders.Binders.Add(typeof(PluploadFileCollection), new PluploadModelBinder());
			PluploadContext.CleanupFiles();
        }
    }
}
