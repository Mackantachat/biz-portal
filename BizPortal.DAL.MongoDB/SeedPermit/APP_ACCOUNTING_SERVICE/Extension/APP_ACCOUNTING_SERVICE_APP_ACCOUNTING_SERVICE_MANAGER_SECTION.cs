using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ACCOUNTING_SERVICE
{
    public partial class APP_ACCOUNTING_SERVICE_APP_ACCOUNTING_SERVICE_MANAGER_SECTION
    {
        private static Select2Opt[] DROPDOWN_APP_ACCOUNTING_SERVICE_MANAGER_SECTION_TITLE()
        {
            return FormSectionRow.optPersonTitleTH_EN;
        }
    }
}
