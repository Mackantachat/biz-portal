using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ORGANIC_PLANT_PRODUCTION_EDIT
{
    public partial class APP_ORGANIC_PLANT_PRODUCTION_EDIT_APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_SECTION_LICENCE
    {
        private static FormControlDisplayCondition CONDITION_APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_SECTION_LICENCE()
        {
            return new FormControlDisplayCondition
            {
                //IsOr = false,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    //new FormControlDisplayCondition.ControlWithAnswer
                    //{
                    //    ControlName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_INFO_SECTION_EDIT_TYPE",
                    //    ControlAnswer = "STANDALONE",
                    //},
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_SECTION_LICENCE_CHECKED_STANDALONE_INFO_SECTION_2_EDIT__STANDALONE_INFO_SECTION_2_CHECKED",
                        ControlAnswer = "true",
                    },
                },
            };
        }
        private static FormControlDisableCondition DISABLE_APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_SECTION_LICENCE()
        {
            return new FormControlDisableCondition
            {
                //IsOr = true,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    //new FormControlDisplayCondition.ControlWithAnswer
                    //{
                    //    ControlName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_INFO_SECTION_EDIT_TYPE",
                    //    ControlAnswer = "STANDALONE",
                    //},
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_SECTION_LICENCE_CHECKED_STANDALONE_INFO_SECTION_2_EDIT__STANDALONE_INFO_SECTION_2_CHECKED",
                        ControlAnswer = "false",
                    },
                },
            };
        }
        private static FormControlDisplayCondition CONDITION_APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_SECTION_LICENCE_LICENSE_INFORMATION()
        {
            return new FormControlDisplayCondition
            {
                //IsOr = false,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    //new FormControlDisplayCondition.ControlWithAnswer
                    //{
                    //    ControlName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_INFO_SECTION_EDIT_TYPE",
                    //    ControlAnswer = "STANDALONE",
                    //},
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_SECTION_LICENCE_CHECKED_STANDALONE_INFO_SECTION_2_EDIT__STANDALONE_INFO_SECTION_2_CHECKED",
                        ControlAnswer = "true",
                    },
                },
            };
        }
        private static FormControlDisplayCondition CONDITION_APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_SECTION_LICENCE_INFORMATION_SHOW_ADDRESS()
        {
            return new FormControlDisplayCondition
            {
                //IsOr = false,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    //new FormControlDisplayCondition.ControlWithAnswer
                    //{
                    //    ControlName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_INFO_SECTION_EDIT_TYPE",
                    //    ControlAnswer = "STANDALONE",
                    //},
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_SECTION_LICENCE_CHECKED_STANDALONE_INFO_SECTION_2_EDIT__STANDALONE_INFO_SECTION_2_CHECKED",
                        ControlAnswer = "true",
                    },
                },
            };
        }
    }
}
