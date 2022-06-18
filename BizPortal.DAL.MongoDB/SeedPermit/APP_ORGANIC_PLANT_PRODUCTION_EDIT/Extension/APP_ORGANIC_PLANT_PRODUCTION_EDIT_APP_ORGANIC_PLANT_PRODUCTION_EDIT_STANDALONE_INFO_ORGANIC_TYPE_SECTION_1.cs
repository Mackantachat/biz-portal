﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ORGANIC_PLANT_PRODUCTION_EDIT
{
    public partial class APP_ORGANIC_PLANT_PRODUCTION_EDIT_APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ORGANIC_TYPE_SECTION_1
    {
        private static FormControlDisplayCondition CONDITION_APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ORGANIC_TYPE_SECTION_1()
        {
            return new FormControlDisplayCondition
            {
                IsOr = false,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_INFO_SECTION_EDIT_TYPE",
                        ControlAnswer = "STANDALONE",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ORGANIC_TYPE_SECTION_CHECKED_STANDALONE_INFO_ORGANIC_TYPE_EDIT__STANDALONE_INFO_ORGANIC_TYPE_CHECKED",
                        ControlAnswer = "true",
                    },
                },
            };
        }
        private static FormControlDisableCondition DISABLE_APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ORGANIC_TYPE_SECTION_1()
        {
            return new FormControlDisableCondition
            {
                IsOr = true,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_INFO_SECTION_EDIT_TYPE",
                        ControlAnswer = "GROUP",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ORGANIC_TYPE_SECTION_CHECKED_STANDALONE_INFO_ORGANIC_TYPE_EDIT__STANDALONE_INFO_ORGANIC_TYPE_CHECKED",
                        ControlAnswer = "false",
                    },
                },
            };
        }
        private static FormControl.NumberSettings SETTING_NUMBER_APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ORGANIC_TYPE_SECTION_1_ORGANIC_PRODUCE_AMOUNT()
        {
            return new FormControl.NumberSettings
            {
                Min = "1",
                Max = int.MaxValue.ToString(),
                Step = "0.01"
            };
        }
        private static FormControl.NumberSettings SETTING_NUMBER_APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_ORGANIC_TYPE_SECTION_1_ORGANIC_AREA()
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
