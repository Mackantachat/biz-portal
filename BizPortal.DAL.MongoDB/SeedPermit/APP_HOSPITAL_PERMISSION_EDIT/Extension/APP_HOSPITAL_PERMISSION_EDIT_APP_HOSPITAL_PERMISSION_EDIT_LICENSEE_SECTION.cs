using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_HOSPITAL_PERMISSION_EDIT
{
    public partial class APP_HOSPITAL_PERMISSION_EDIT_APP_HOSPITAL_PERMISSION_EDIT_LICENSEE_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_HOSPITAL_PERMISSION_EDIT_LICENSEE_SECTION()
        {
            return new FormControlDisplayCondition
            {
                IsOr = false,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    //new FormControlDisplayCondition.ControlWithAnswer
                    //{
                    //    ControlName = "APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_PURPOSE__PURPOSE_CHANGE_DETAIL",
                    //    ControlAnswer = "true",
                    //},
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2_REQUEST_CHANGE__REQUEST_CHANGE_LICENSEE",
                        ControlAnswer = "true",
                    },
                },
            };
        }

        private static FormControlDisableCondition DISABLE_APP_HOSPITAL_PERMISSION_EDIT_LICENSEE_SECTION()
        {
            return new FormControlDisableCondition
            {
                Conditions = new FormControlDisableCondition.ControlWithAnswer[]
                {
                    new FormControlDisableCondition.ControlWithAnswer
                    {
                        ControlName = "APP_HOSPITAL_PERMISSION_EDIT_INFO_SECTION_2_REQUEST_CHANGE__REQUEST_CHANGE_LICENSEE",
                        ControlAnswer = "false",
                    },
                },
            };
        }

        private static Select2Opt[] DROPDOWN_APP_HOSPITAL_PERMISSION_EDIT_LICENSEE_SECTION_TITLE()
        {
            return FormSectionRow.optPersonTitle;
        }
    }
}
