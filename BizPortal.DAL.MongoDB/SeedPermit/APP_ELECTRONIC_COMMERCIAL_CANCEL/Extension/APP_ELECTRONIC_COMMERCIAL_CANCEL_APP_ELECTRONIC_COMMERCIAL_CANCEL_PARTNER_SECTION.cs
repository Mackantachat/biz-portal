using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ELECTRONIC_COMMERCIAL_CANCEL
{
    public partial class APP_ELECTRONIC_COMMERCIAL_CANCEL_APP_ELECTRONIC_COMMERCIAL_CANCEL_PARTNER_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_CANCEL_PARTNER_SECTION()
        {
            return new FormControlDisableCondition
            {
                IsOr = true,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_CANCEL_REQUEST_SECTION_JURISTIC_TYPE",
                        ControlAnswer = "ห้างหุ้นส่วนสามัญนิติบุคคล",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_CANCEL_REQUEST_SECTION_JURISTIC_TYPE",
                        ControlAnswer = "ห้างหุ้นส่วนจำกัด",
                    },
                },
            };
        }

        private static Select2Opt[] DROPDOWN_APP_ELECTRONIC_COMMERCIAL_CANCEL_PARTNER_SECTION_TITLE()
        {
            return FormSectionRow.optPersonTitle;
        }

        private static Select2Opt[] DROPDOWN_APP_ELECTRONIC_COMMERCIAL_CANCEL_PARTNER_SECTION_NATIONALITY()
        {
            return FormSectionRow.optNationality;
        }

        private static Select2Opt[] DROPDOWN_APP_ELECTRONIC_COMMERCIAL_CANCEL_PARTNER_SECTION_RACE()
        {
            return FormSectionRow.optNation;
        }
    }
}
