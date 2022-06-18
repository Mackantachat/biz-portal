using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ELECTRONIC_COMMERCIAL
{
    public partial class APP_ELECTRONIC_COMMERCIAL_APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION()
        {
            return new FormControlDisplayCondition
            {
                IsOr = true,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_REQUEST_SECTION_JURISTIC_TYPE",
                        ControlAnswer = "ห้างหุ้นส่วนสามัญนิติบุคคล",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_REQUEST_SECTION_JURISTIC_TYPE",
                        ControlAnswer = "ห้างหุ้นส่วนจำกัด",
                    },
                },
            };
        }

        private static string APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION_TITLE_URL()
        {
            return "~/Api/v2/DBD/NameTitles";
        }

        private static Select2Opt[] DROPDOWN_APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION_NATIONALITY()
        {
            return FormSectionRow.optNationality;
        }

        private static Select2Opt[] DROPDOWN_APP_ELECTRONIC_COMMERCIAL_PARTNER_SECTION_RACE()
        {
            return FormSectionRow.optNation;
        }
    }
}
