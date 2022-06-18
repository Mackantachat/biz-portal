using BizPortal.Areas.WebApi.Controllers;
using BizPortal.ViewModels.Select2;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BizPortal.Areas.WebApiV2.Controllers
{
    public class MEAServiceController : ApiControllerBase
    {
        [Route("Api/v2/MEA/OSS")]
        [HttpGet]
        public object OSS()
        {
            List<Select2Opt> options = new List<Select2Opt>();

            if (IdentityType == UserTypeEnum.Juristic)
            {
                options = new List<Select2Opt>()
                {
                    new Select2Opt () { ID = Resources.Apps_Utility_MEAOSS.T515A230V1P2W, Text = Resources.Apps_Utility_MEAOSS.T515A230V1P2W },
                    new Select2Opt () { ID = Resources.Apps_Utility_MEAOSS.T1545A230V1P2W, Text = Resources.Apps_Utility_MEAOSS.T1545A230V1P2W },
                    new Select2Opt () { ID = Resources.Apps_Utility_MEAOSS.T30100A230V1P2W, Text = Resources.Apps_Utility_MEAOSS.T30100A230V1P2W },
                    new Select2Opt () { ID = Resources.Apps_Utility_MEAOSS.T50150A230V1P2W, Text = Resources.Apps_Utility_MEAOSS.T50150A230V1P2W },
                    new Select2Opt () { ID = Resources.Apps_Utility_MEAOSS.T1545A230400V3P4W, Text = Resources.Apps_Utility_MEAOSS.T1545A230400V3P4W },
                    new Select2Opt () { ID = Resources.Apps_Utility_MEAOSS.T30100A230400V3P4W, Text = Resources.Apps_Utility_MEAOSS.T30100A230400V3P4W },
                    new Select2Opt () { ID = Resources.Apps_Utility_MEAOSS.T50150A230400V3P4W, Text = Resources.Apps_Utility_MEAOSS.T50150A230400V3P4W },
                    new Select2Opt () { ID = Resources.Apps_Utility_MEAOSS.T200A230400V3P4W, Text = Resources.Apps_Utility_MEAOSS.T200A230400V3P4W },
                    new Select2Opt () { ID = Resources.Apps_Utility_MEAOSS.T400A230400V3P4W, Text = Resources.Apps_Utility_MEAOSS.T400A230400V3P4W }
                };
            }
            else if (IdentityType == UserTypeEnum.Citizen)
            {
                options = new List<Select2Opt>()
                {
                    new Select2Opt () { ID = "WM01010", Text = Resources.Apps_Utility_MEAOSS.WM01010 },
                    new Select2Opt () { ID = "WM01020", Text = Resources.Apps_Utility_MEAOSS.WM01020 },
                    new Select2Opt () { ID = "WM01030", Text = Resources.Apps_Utility_MEAOSS.WM01030 },
                    new Select2Opt () { ID = "WM01040", Text = Resources.Apps_Utility_MEAOSS.WM01040 },
                    new Select2Opt () { ID = "WM02010", Text = Resources.Apps_Utility_MEAOSS.WM02010 },
                    new Select2Opt () { ID = "WM02020", Text = Resources.Apps_Utility_MEAOSS.WM02020 },
                    new Select2Opt () { ID = "WM02030", Text = Resources.Apps_Utility_MEAOSS.WM02030 },
                    new Select2Opt () { ID = "WM02040", Text = Resources.Apps_Utility_MEAOSS.WM02040 },
                    new Select2Opt () { ID = "WM02050", Text = Resources.Apps_Utility_MEAOSS.WM02050 },
                    new Select2Opt () { ID = "WM03030", Text = Resources.Apps_Utility_MEAOSS.WM03030 },
                    new Select2Opt () { ID = "WM03040", Text = Resources.Apps_Utility_MEAOSS.WM03040 },
                    new Select2Opt () { ID = "WM03050", Text = Resources.Apps_Utility_MEAOSS.WM03050 },
                    new Select2Opt () { ID = "WM03060", Text = Resources.Apps_Utility_MEAOSS.WM03060 },
                    new Select2Opt () { ID = "WM03070", Text = Resources.Apps_Utility_MEAOSS.WM03070 },
                    new Select2Opt () { ID = "WM03080", Text = Resources.Apps_Utility_MEAOSS.WM03080 },
                    new Select2Opt () { ID = "WM03090", Text = Resources.Apps_Utility_MEAOSS.WM03090 },
                    new Select2Opt () { ID = "WM03100", Text = Resources.Apps_Utility_MEAOSS.WM03100 },
                    new Select2Opt () { ID = "WM03110", Text = Resources.Apps_Utility_MEAOSS.WM03110 },
                    new Select2Opt () { ID = "WM03120", Text = Resources.Apps_Utility_MEAOSS.WM03120 },
                    new Select2Opt () { ID = "WM03130", Text = Resources.Apps_Utility_MEAOSS.WM03130 },
                    new Select2Opt () { ID = "WM03140", Text = Resources.Apps_Utility_MEAOSS.WM03140 },
                    new Select2Opt () { ID = "WM03150", Text = Resources.Apps_Utility_MEAOSS.WM03150 },
                    new Select2Opt () { ID = "WM03160", Text = Resources.Apps_Utility_MEAOSS.WM03160 },
                    new Select2Opt () { ID = "WM03170", Text = Resources.Apps_Utility_MEAOSS.WM03170 },
                    new Select2Opt () { ID = "WM03180", Text = Resources.Apps_Utility_MEAOSS.WM03180 },
                    new Select2Opt () { ID = "WM03190", Text = Resources.Apps_Utility_MEAOSS.WM03190 },
                    new Select2Opt () { ID = "WM03200", Text = Resources.Apps_Utility_MEAOSS.WM03200 },
                    new Select2Opt () { ID = "WM03210", Text = Resources.Apps_Utility_MEAOSS.WM03210 },
                    new Select2Opt () { ID = "WM03220", Text = Resources.Apps_Utility_MEAOSS.WM03220 },
                    new Select2Opt () { ID = "WM03230", Text = Resources.Apps_Utility_MEAOSS.WM03230 },
                    new Select2Opt () { ID = "WM03240", Text = Resources.Apps_Utility_MEAOSS.WM03240 },
                    new Select2Opt () { ID = "WM03250", Text = Resources.Apps_Utility_MEAOSS.WM03250 },
                    new Select2Opt () { ID = "WM03260", Text = Resources.Apps_Utility_MEAOSS.WM03260 },
                    new Select2Opt () { ID = "WM03270", Text = Resources.Apps_Utility_MEAOSS.WM03270 },
                    new Select2Opt () { ID = "WM03280", Text = Resources.Apps_Utility_MEAOSS.WM03280 },
                    new Select2Opt () { ID = "WM03290", Text = Resources.Apps_Utility_MEAOSS.WM03290 },
                    new Select2Opt () { ID = "WM03300", Text = Resources.Apps_Utility_MEAOSS.WM03300 },
                    new Select2Opt () { ID = "WM03310", Text = Resources.Apps_Utility_MEAOSS.WM03310 },
                    new Select2Opt () { ID = "WM03320", Text = Resources.Apps_Utility_MEAOSS.WM03320 },
                    new Select2Opt () { ID = "WM03330", Text = Resources.Apps_Utility_MEAOSS.WM03330 },
                    new Select2Opt () { ID = "WM03340", Text = Resources.Apps_Utility_MEAOSS.WM03340 },
                    new Select2Opt () { ID = "WM03350", Text = Resources.Apps_Utility_MEAOSS.WM03350 },
                    new Select2Opt () { ID = "WM03360", Text = Resources.Apps_Utility_MEAOSS.WM03360 },
                    new Select2Opt () { ID = "WM03370", Text = Resources.Apps_Utility_MEAOSS.WM03370 },
                    new Select2Opt () { ID = "WM04020", Text = Resources.Apps_Utility_MEAOSS.WM04020 },
                    new Select2Opt () { ID = "WM04030", Text = Resources.Apps_Utility_MEAOSS.WM04030 },
                    new Select2Opt () { ID = "WM04040", Text = Resources.Apps_Utility_MEAOSS.WM04040 },
                    new Select2Opt () { ID = "WM04050", Text = Resources.Apps_Utility_MEAOSS.WM04050 },
                    new Select2Opt () { ID = "WM04060", Text = Resources.Apps_Utility_MEAOSS.WM04060 },
                    new Select2Opt () { ID = "WM04070", Text = Resources.Apps_Utility_MEAOSS.WM04070 },
                    new Select2Opt () { ID = "WM04080", Text = Resources.Apps_Utility_MEAOSS.WM04080 },
                    new Select2Opt () { ID = "WM04090", Text = Resources.Apps_Utility_MEAOSS.WM04090 },
                    new Select2Opt () { ID = "WM04100", Text = Resources.Apps_Utility_MEAOSS.WM04100 },
                    new Select2Opt () { ID = "WM04110", Text = Resources.Apps_Utility_MEAOSS.WM04110 },
                    new Select2Opt () { ID = "WM04120", Text = Resources.Apps_Utility_MEAOSS.WM04120 },
                    new Select2Opt () { ID = "WM04130", Text = Resources.Apps_Utility_MEAOSS.WM04130 },
                    new Select2Opt () { ID = "WM04140", Text = Resources.Apps_Utility_MEAOSS.WM04140 },
                    new Select2Opt () { ID = "WM04150", Text = Resources.Apps_Utility_MEAOSS.WM04150 },
                    new Select2Opt () { ID = "WM04160", Text = Resources.Apps_Utility_MEAOSS.WM04160 },
                    new Select2Opt () { ID = "WM04170", Text = Resources.Apps_Utility_MEAOSS.WM04170 },
                    new Select2Opt () { ID = "WM04180", Text = Resources.Apps_Utility_MEAOSS.WM04180 },
                    new Select2Opt () { ID = "WM04190", Text = Resources.Apps_Utility_MEAOSS.WM04190 },
                    new Select2Opt () { ID = "WM04200", Text = Resources.Apps_Utility_MEAOSS.WM04200 },
                    new Select2Opt () { ID = "WM04210", Text = Resources.Apps_Utility_MEAOSS.WM04210 },
                    new Select2Opt () { ID = "WM04220", Text = Resources.Apps_Utility_MEAOSS.WM04220 },
                    new Select2Opt () { ID = "WM04230", Text = Resources.Apps_Utility_MEAOSS.WM04230 },
                    new Select2Opt () { ID = "WM04240", Text = Resources.Apps_Utility_MEAOSS.WM04240 },
                    new Select2Opt () { ID = "WM04250", Text = Resources.Apps_Utility_MEAOSS.WM04250 },
                    new Select2Opt () { ID = "WM04260", Text = Resources.Apps_Utility_MEAOSS.WM04260 },
                    new Select2Opt () { ID = "WM04270", Text = Resources.Apps_Utility_MEAOSS.WM04270 },
                    new Select2Opt () { ID = "WM04280", Text = Resources.Apps_Utility_MEAOSS.WM04280 },
                    new Select2Opt () { ID = "WM04290", Text = Resources.Apps_Utility_MEAOSS.WM04290 },
                    new Select2Opt () { ID = "WM04300", Text = Resources.Apps_Utility_MEAOSS.WM04300 },
                    new Select2Opt () { ID = "WM04310", Text = Resources.Apps_Utility_MEAOSS.WM04310 },
                    new Select2Opt () { ID = "WM04320", Text = Resources.Apps_Utility_MEAOSS.WM04320 },
                    new Select2Opt () { ID = "WM04330", Text = Resources.Apps_Utility_MEAOSS.WM04330 },
                    new Select2Opt () { ID = "WM04340", Text = Resources.Apps_Utility_MEAOSS.WM04340 },
                    new Select2Opt () { ID = "WM04350", Text = Resources.Apps_Utility_MEAOSS.WM04350 },
                    new Select2Opt () { ID = "WM04360", Text = Resources.Apps_Utility_MEAOSS.WM04360 }
                };
            }

            return new { results = options.ToArray() };
        }
    }
}
