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
    public partial class APP_ORGANIC_PLANT_PRODUCTION_RENEW_APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION()
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
        private static Select2Opt[] DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION_FIRST_TITLE()
        {
            return FormSectionRow.optPersonTitle;
        }
        private static Select2Opt[] DROPDOWN_APP_ORGANIC_PLANT_PRODUCTION_RENEW_GROUP_CONTACT_SECTION_SECOND_TITLE()
        {
            return FormSectionRow.optPersonTitle;
        }
    }
}
