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
    public partial class APP_CLINIC_LICENSE_APP_CLINIC_LICENSE_DETAIL_SECTION_EN
    {
        private static Select2Opt[] DROPDOWN_APP_CLINIC_LICENSE_DETAIL_SECTION_EN_START_TIME_EN()
        {
            return FormSectionRow.optWorkingTime;
        }
        private static Select2Opt[] DROPDOWN_APP_CLINIC_LICENSE_DETAIL_SECTION_EN_TIME_OUT_EN()
        {
            return FormSectionRow.optHospitalWorkingTime;
        }
    }
}
