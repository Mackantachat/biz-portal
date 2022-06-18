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
    public partial class APP_ELECTRONIC_COMMERCIAL_APP_ELECTRONIC_COMMERCIAL_STOCK_SECTION_2
    {
        private static Select2Opt[] DROPDOWN_APP_ELECTRONIC_COMMERCIAL_STOCK_SECTION_2_NATIONALITY()
        {
            return FormSectionRow.optNationality;
        }
    }
}
