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
    public partial class APP_ELECTRONIC_COMMERCIAL_CANCEL_APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION
    {
        private static Select2Opt[] DROPDOWN_APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_TITLE()
        {
            return FormSectionRow.optPersonTitle;
        }

        private static Select2Opt[] DROPDOWN_APP_ELECTRONIC_COMMERCIAL_CANCEL_MANAGER_SECTION_NATIONALITY()
        {
            return FormSectionRow.optNationality;
        }
    }
}
