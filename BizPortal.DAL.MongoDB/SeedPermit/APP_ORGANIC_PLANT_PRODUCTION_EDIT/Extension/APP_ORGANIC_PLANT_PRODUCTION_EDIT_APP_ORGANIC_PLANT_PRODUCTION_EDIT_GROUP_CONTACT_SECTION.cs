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
    public partial class APP_ORGANIC_PLANT_PRODUCTION_EDIT_APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_CONTACT_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_CONTACT_SECTION()
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
                        ControlName = "APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_CONTACT_SECTION_CHECKED_GROUP_CONTACT_SECTION_EDIT__GROUP_CONTACT_SECTION_CHECKED",
                        ControlAnswer = "true",
                    },
                },
            };
        }
        private static Select2Opt[] DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_CONTACT_SECTION_FIRST_TITLE()
        {
            return FormSectionRow.optPersonTitle;
        }
        private static Select2Opt[] DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_EDIT_GROUP_CONTACT_SECTION_SECOND_TITLE()
        {
            return FormSectionRow.optPersonTitle;
        }
    }
    
}
