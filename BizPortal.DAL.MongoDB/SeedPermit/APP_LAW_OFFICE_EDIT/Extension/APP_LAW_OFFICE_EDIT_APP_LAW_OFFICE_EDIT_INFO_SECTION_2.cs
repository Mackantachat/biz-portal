using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_LAW_OFFICE_EDIT
{
    public partial class APP_LAW_OFFICE_EDIT_APP_LAW_OFFICE_EDIT_INFO_SECTION_2
    {
        private static FormControlDisplayCondition CONDITION_APP_LAW_OFFICE_EDIT_INFO_SECTION_2_OTHER()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_LAW_OFFICE_EDIT_INFO_SECTION_2_CHECKBOX__OTHER",
                        ControlAnswer = "true",
                    },
                },
            };
        }
    }
}
