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
    public partial class APP_ORGANIC_PLANT_PRODUCTION_EDIT_APP_ORGANIC_PLANT_PRODUCTION_EDIT_INFO_SECTION
    {
        //private static FormControlDisplayCondition CONDITION_APP_ORGANIC_PLANT_PRODUCTION_EDIT_INFO_SECTION_STANDALONE_ADDRESS_EDIT()
        //{
        //    return new FormControlDisplayCondition
        //    {
        //        Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
        //        {
        //            new FormControlDisplayCondition.ControlWithAnswer
        //            {
        //                ControlName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_INFO_SECTION_EDIT_TYPE",
        //                ControlAnswer = "STANDALONE",
        //            },
        //        },
        //    };
        //}
        private static FormControlDisplayCondition CONDITION_APP_ORGANIC_PLANT_PRODUCTION_EDIT_INFO_SECTION_STANDALONE_INFO_ORGANIC_TYPE_EDIT()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_INFO_SECTION_EDIT_TYPE",
                        ControlAnswer = "STANDALONE",
                    },
                },
            };
        }
        private static FormControlDisplayCondition CONDITION_APP_ORGANIC_PLANT_PRODUCTION_EDIT_INFO_SECTION_STANDALONE_INFO_SECTION_2_EDIT()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_INFO_SECTION_EDIT_TYPE",
                        ControlAnswer = "STANDALONE",
                    },
                },
            };
        }
        private static FormControlDisplayCondition CONDITION_APP_ORGANIC_PLANT_PRODUCTION_EDIT_INFO_SECTION_GROUP_PRESIDENT_EDIT()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_INFO_SECTION_EDIT_TYPE",
                        ControlAnswer = "GROUP",
                    },
                },
            };
        }
        private static FormControlDisplayCondition CONDITION_APP_ORGANIC_PLANT_PRODUCTION_EDIT_INFO_SECTION_GROUP_PRESIDENT_AS_SAME_PERSON()
        {
            return new FormControlDisplayCondition
            {
                IsOr = false,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_INFO_SECTION_EDIT_TYPE",
                        ControlAnswer = "GROUP",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_INFO_SECTION_GROUP_PRESIDENT_EDIT__GROUP_PRESIDENT_CHECKED",
                        ControlAnswer = "true",
                    },
                },
            };
        }
    }
}
