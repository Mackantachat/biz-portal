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
    public partial class APP_ORGANIC_PLANT_PRODUCTION_EDIT_APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_NAME_THAI_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_NAME_THAI_SECTION()
        {
            return new FormControlDisplayCondition
            {
                IsOr = false,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_SECTION_LICENCE_CHECKED_STANDALONE_INFO_SECTION_2_EDIT__STANDALONE_INFO_SECTION_2_CHECKED",
                        ControlAnswer = "true",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_SECTION_LICENCE_LICENSE_INFORMATION__NAME_THAI",
                        ControlAnswer = "true",
                    },
                },
            };
        }
        private static FormControlDisableCondition DISABLE_APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_NAME_THAI_SECTION()
        {
            return new FormControlDisableCondition
            {
                IsOr = true,
                Conditions = new FormControlDisableCondition.ControlWithAnswer[]
                {
                    new FormControlDisableCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_SECTION_LICENCE_CHECKED_STANDALONE_INFO_SECTION_2_EDIT__STANDALONE_INFO_SECTION_2_CHECKED",
                        ControlAnswer = "false",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_SECTION_LICENCE_LICENSE_INFORMATION__NAME_THAI",
                        ControlAnswer = "false",
                    },
                },
            };
        }
        private static FormControlDisplayCondition CONDITION_APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_NAME_THAI_SECTION_INFORMATION_NAME_THAI_HEADER()
        {
            return new FormControlDisplayCondition
            {
                IsOr = false,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_SECTION_LICENCE_CHECKED_STANDALONE_INFO_SECTION_2_EDIT__STANDALONE_INFO_SECTION_2_CHECKED",
                        ControlAnswer = "true",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_SECTION_LICENCE_LICENSE_INFORMATION__NAME_THAI",
                        ControlAnswer = "true",
                    },
                }
            };
        }
        private static FormControlDisplayCondition CONDITION_APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_NAME_THAI_SECTION_INFORMATION_NAME_THAI_TEXT()
        {
            return new FormControlDisplayCondition
            {
                IsOr = false,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_SECTION_LICENCE_CHECKED_STANDALONE_INFO_SECTION_2_EDIT__STANDALONE_INFO_SECTION_2_CHECKED",
                        ControlAnswer = "true",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_STANDALONE_INFO_SECTION_LICENCE_LICENSE_INFORMATION__NAME_THAI",
                        ControlAnswer = "true",
                    },
                },
            };
        }
    }
}
