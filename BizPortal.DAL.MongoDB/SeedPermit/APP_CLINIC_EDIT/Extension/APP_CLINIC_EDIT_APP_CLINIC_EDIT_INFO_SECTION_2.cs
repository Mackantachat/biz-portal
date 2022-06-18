using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_CLINIC_EDIT
{
    public partial class APP_CLINIC_EDIT_APP_CLINIC_EDIT_INFO_SECTION_2
    {
        private static FormControlDisplayCondition CONDITION_APP_CLINIC_EDIT_INFO_SECTION_2()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_CLINIC_EDIT_INFO_SECTION_PURPOSE__PURPOSE_CHANGE_DETAIL",
                        ControlAnswer = "true",
                    },
                },
            };
        }

        private static FormControlDisableCondition DISABLE_APP_CLINIC_EDIT_INFO_SECTION_2()
        {
            return new FormControlDisableCondition
            {
                Conditions = new FormControlDisableCondition.ControlWithAnswer[]
                {
                    new FormControlDisableCondition.ControlWithAnswer
                    {
                        ControlName = "APP_CLINIC_EDIT_INFO_SECTION_PURPOSE__PURPOSE_CHANGE_DETAIL",
                        ControlAnswer = "false",
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_CLINIC_EDIT_INFO_SECTION_2_REQUEST_CHANGE()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_CLINIC_EDIT_INFO_SECTION_PURPOSE__PURPOSE_CHANGE_DETAIL",
                        ControlAnswer = "true",
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_CLINIC_EDIT_INFO_SECTION_2_NAME()
        {
            return new FormControlDisplayCondition
            {
                //IsOr = false,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    //new FormControlDisplayCondition.ControlWithAnswer
                    //{
                    //    ControlName = "APP_CLINIC_EDIT_INFO_SECTION_PURPOSE__PURPOSE_CHANGE_DETAIL",
                    //    ControlAnswer = "true",
                    //},
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_CLINIC_EDIT_INFO_SECTION_2_REQUEST_CHANGE__REQUEST_CHANGE_NAME",
                        ControlAnswer = "true",
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_CLINIC_EDIT_INFO_SECTION_2_NAME_ENG()
        {
            return new FormControlDisplayCondition
            {
                //IsOr = false,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    //new FormControlDisplayCondition.ControlWithAnswer
                    //{
                    //    ControlName = "APP_CLINIC_EDIT_INFO_SECTION_PURPOSE__PURPOSE_CHANGE_DETAIL",
                    //    ControlAnswer = "true",
                    //},
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_CLINIC_EDIT_INFO_SECTION_2_REQUEST_CHANGE__REQUEST_CHANGE_NAME",
                        ControlAnswer = "true",
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_CLINIC_EDIT_INFO_SECTION_2_ADDRESS()
        {
            return new FormControlDisplayCondition
            {
                //IsOr = false,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    //new FormControlDisplayCondition.ControlWithAnswer
                    //{
                    //    ControlName = "APP_CLINIC_EDIT_INFO_SECTION_PURPOSE__PURPOSE_CHANGE_DETAIL",
                    //    ControlAnswer = "true",
                    //},
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_CLINIC_EDIT_INFO_SECTION_2_REQUEST_CHANGE__REQUEST_CHANGE_ADDRESS",
                        ControlAnswer = "true",
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_CLINIC_EDIT_INFO_SECTION_2_NAME_CUS()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_CLINIC_EDIT_INFO_SECTION_2_REQUEST_CHANGE__REQUEST_CHANGE_NAME",
                        ControlAnswer = "true",
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_CLINIC_EDIT_INFO_SECTION_2_ADDRESS_CUS()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_CLINIC_EDIT_INFO_SECTION_2_REQUEST_CHANGE__REQUEST_CHANGE_ADDRESS",
                        ControlAnswer = "true",
                    },
                },
            };
        }

    }
}
