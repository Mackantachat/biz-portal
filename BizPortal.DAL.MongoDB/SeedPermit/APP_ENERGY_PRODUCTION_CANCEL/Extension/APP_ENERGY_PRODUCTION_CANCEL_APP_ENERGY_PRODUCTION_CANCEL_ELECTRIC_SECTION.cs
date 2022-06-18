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
    public partial class APP_ENERGY_PRODUCTION_CANCEL_APP_ENERGY_PRODUCTION_CANCEL_ELECTRIC_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_ENERGY_PRODUCTION_CANCEL_ELECTRIC_SECTION()
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
        private static FormControl.NumberSettings SETTING_NUMBER_APP_ENERGY_PRODUCTION_CANCEL_ELECTRIC_SECTION_KVA()
        {
            return new FormControl.NumberSettings
            {
                    Min = "1",
                    Max = int.MaxValue.ToString(),
                    Step = "0.01"
            };
        }
        private static FormControl.NumberSettings SETTING_NUMBER_APP_ENERGY_PRODUCTION_CANCEL_ELECTRIC_SECTION_VOLTAGE()
        {
            return new FormControl.NumberSettings
            {
                Min = "1",
                Max = int.MaxValue.ToString(),
                Step = "0.01"
            };
        }
        private static FormControl.NumberSettings SETTING_NUMBER_APP_ENERGY_PRODUCTION_CANCEL_ELECTRIC_SECTION_AMP()
        {
            return new FormControl.NumberSettings
            {
                Min = "1",
                Max = int.MaxValue.ToString(),
                Step = "0.01"
            };
        }
        private static FormControl.NumberSettings SETTING_NUMBER_APP_ENERGY_PRODUCTION_CANCEL_ELECTRIC_SECTION_PERCENT()
        {
            return new FormControl.NumberSettings
            {
                Min = "1",
                Max = int.MaxValue.ToString(),
                Step = "0.01"
            };
        }
    }
}
