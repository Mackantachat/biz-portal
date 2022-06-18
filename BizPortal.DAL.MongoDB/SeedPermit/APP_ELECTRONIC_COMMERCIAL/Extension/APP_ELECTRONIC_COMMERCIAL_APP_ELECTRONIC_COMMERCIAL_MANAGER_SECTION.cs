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
    public partial class APP_ELECTRONIC_COMMERCIAL_APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION
    {
        private static string APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_TITLE_URL()
        {
            return "~/Api/v2/DBD/NameTitles";
        }

        private static Select2Opt[] DROPDOWN_APP_ELECTRONIC_COMMERCIAL_MANAGER_SECTION_NATIONALITY()
        {
            return FormSectionRow.optNationality;
        }
    }
}
