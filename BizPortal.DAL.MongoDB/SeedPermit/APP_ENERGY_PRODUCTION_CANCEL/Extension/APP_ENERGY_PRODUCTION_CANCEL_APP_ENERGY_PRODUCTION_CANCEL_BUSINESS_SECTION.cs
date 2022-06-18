using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ENERGY_PRODUCTION_CANCEL
{
    public partial class APP_ENERGY_PRODUCTION_CANCEL_APP_ENERGY_PRODUCTION_CANCEL_BUSINESS_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_ENERGY_PRODUCTION_CANCEL_BUSINESS_SECTION_CANCEL_TYPE_FOR_RENEWABLE_ENERGY()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ENERGY_PRODUCTION_CANCEL_INFO_SECTION_CANCEL_TYPE",
                        ControlAnswer = "SOME_RENEWABLE",
                    },
                },
            };
        }
    }
}
