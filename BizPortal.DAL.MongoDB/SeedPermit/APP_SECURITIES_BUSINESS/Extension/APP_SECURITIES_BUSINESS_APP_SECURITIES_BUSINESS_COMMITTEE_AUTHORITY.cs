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
    public partial class APP_SECURITIES_BUSINESS_APP_SECURITIES_BUSINESS_COMMITTEE_AUTHORITY
    {
        private static Select2Opt[] DROPDOWN_APP_SECURITIES_BUSINESS_COMMITTEE_AUTHORITY_COMMITTEE_AUTHORITY_TITLE()
        {
            return FormSectionRow.optPersonTitle;
        }

        private static Select2Opt[] DROPDOWN_APP_SECURITIES_BUSINESS_COMMITTEE_AUTHORITY_COMMITTEE_AUTHORITY_TITLE_EN()
        {
            return FormSectionRow.optPersonTitleEN;
        }
        private static Select2Opt[] DROPDOWN_APP_SECURITIES_BUSINESS_COMMITTEE_AUTHORITY_COMMITTEE_AUTHORITY_NATIONALITY()
        {
            return FormSectionRow.optNationality;
        }
    }
}
