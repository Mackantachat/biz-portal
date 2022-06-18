using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_SECURITIES_BUSINESS
{
    public partial class APP_SECURITIES_BUSINESS_APP_SECURITIES_BUSINESS_CITIZEN_MAJOR_SHAREHOLDER
    {
        private static Select2Opt[] DROPDOWN_APP_SECURITIES_BUSINESS_CITIZEN_MAJOR_SHAREHOLDER_NATIONALITY()
        {
            return FormSectionRow.optNationality;
        }

        private static Select2Opt[] DROPDOWN_APP_SECURITIES_BUSINESS_CITIZEN_MAJOR_SHAREHOLDER_TITLE()
        {
            return FormSectionRow.optPersonTitle;
        }

        private static Select2Opt[] DROPDOWN_APP_SECURITIES_BUSINESS_CITIZEN_MAJOR_SHAREHOLDER_TITLE_EN()
        {
            return FormSectionRow.optPersonTitleEN;
        }

        private static Select2Opt[] DROPDOWN_APP_SECURITIES_BUSINESS_CITIZEN_MAJOR_SHAREHOLDER_TYPE()
        {
            return new Select2Opt[] {
                new Select2Opt()
                {
                    ID = "1",
                    Text = "ผู้ถือหุ้นทางตรง"
                },
                new Select2Opt()
                {
                    ID = "2",
                    Text = "ผู้ถือหุ้นทางอ้อม"
                }
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_SECURITIES_BUSINESS_APP_SECURITIES_BUSINESS_CITIZEN_MAJOR_SHAREHOLDER()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_SECURITIES_BUSINESS_MAJOR_SHAREHOLDER_MAJOR_SHAREHOLDER_TYPE__CITIZEN",
                        ControlAnswer = "true",
                    },
                },
            };
        }

    }
}
