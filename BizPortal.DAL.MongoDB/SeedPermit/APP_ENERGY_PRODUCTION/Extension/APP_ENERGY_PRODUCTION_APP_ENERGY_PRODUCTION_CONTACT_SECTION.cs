using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_ENERGY_PRODUCTION
{
    public partial class APP_ENERGY_PRODUCTION_APP_ENERGY_PRODUCTION_CONTACT_SECTION
    {
        private static Select2Opt[] DROPDOWN_APP_ENERGY_PRODUCTION_CONTACT_INFORMATION_TITLE()
        {
            return FormSectionRow.optPersonTitle;
        }
    }
}
