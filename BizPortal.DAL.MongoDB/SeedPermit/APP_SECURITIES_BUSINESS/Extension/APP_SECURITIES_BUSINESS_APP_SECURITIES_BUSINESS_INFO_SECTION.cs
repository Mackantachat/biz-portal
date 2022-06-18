using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_SECURITIES_BUSINESS
{
    public partial class APP_SECURITIES_BUSINESS_APP_SECURITIES_BUSINESS_INFO_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_SECURITIES_BUSINESS_INFO_SECTION_SHARE_TEXT()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_SECURITIES_BUSINESS_INFO_SECTION_SHARE_TEXT_HIDDEN",
                        ControlAnswer = "true"
                    }
                }
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_SECURITIES_BUSINESS_INFO_SECTION_PERCENT_SHARE_ORDINARY()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_SECURITIES_BUSINESS_INFO_SECTION_PERCENT_SHARE_ORDINARY_HIDDEN",
                        ControlAnswer = "true"
                    }
                }
            };
        }
    }
}
