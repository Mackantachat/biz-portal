using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ORGANIC_PLANT_PRODUCTION_RENEW
{
    public partial class APP_ORGANIC_PLANT_PRODUCTION_RENEW_APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_PRODUCE_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_PRODUCE_SECTION()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_INFO_SECTION_RENEW_TYPE",
                        ControlAnswer = "GROUP",
                    },
                },
            };
        }

        private static FormControlDisableCondition DISABLE_APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_PRODUCE_SECTION()
        {
            return new FormControlDisableCondition
            {
                Conditions = new FormControlDisableCondition.ControlWithAnswer[]
                {
                    new FormControlDisableCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_RENEW_INFO_SECTION_RENEW_TYPE",
                        ControlAnswer = "STANDALONE",
                    },
                },
            };
        }

        private static FormControl.NumberSettings SETTING_NUMBER_APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_PRODUCE_SECTION_BENEFIT_PER_YEAR()
        {
            return new FormControl.NumberSettings
            {
                Min = "0",
                Max = int.MaxValue.ToString(),
                Step = "0.01"
            };
        }
    }
}
