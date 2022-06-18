using BizPortal.Models.Routes;
using System.Web.Mvc;

namespace BizPortal.Areas.Landing
{
    public class LandingAreaRegistration : DomainAreaRegistration
    {
        public override string AreaName 
        {
            get 
            {
                return "Landing";
            }
        }
    }
}