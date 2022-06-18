using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_CLINIC_LICENSE
{
    public partial class APP_CLINIC_LICENSE_APP_CLINIC_LICENSE_DETAIL_SECTION
    {
        private static Select2Opt[] DROPDOWN_APP_CLINIC_LICENSE_DETAIL_SECTION_START_TIME()
        {
            return FormSectionRow.optWorkingTime;
        }
        private static Select2Opt[] DROPDOWN_APP_CLINIC_LICENSE_DETAIL_SECTION_TIME_OUT()
        {
            return FormSectionRow.optWorkingTime;
        }
    }
}
