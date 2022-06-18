using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_LAW_OFFICE
{
    public partial class APP_LAW_OFFICE_APP_LAW_OFFICE_LAWYER_SECTION
    {
        private static Select2Opt[] DROPDOWN_APP_LAW_OFFICE_LAWYER_SECTION_TITLE()
        {
            return FormSectionRow.optPersonTitle;
        }
    }
}
