using BizPortal.Models.Routes;
using System.Web.Mvc;

namespace BizPortal.Areas.Apps
{
    public class AppsAreaRegistration : DomainAreaRegistration
    {
        public override string AreaName 
        {
            get 
            {
                return "Apps";
            }
        }
    }
}