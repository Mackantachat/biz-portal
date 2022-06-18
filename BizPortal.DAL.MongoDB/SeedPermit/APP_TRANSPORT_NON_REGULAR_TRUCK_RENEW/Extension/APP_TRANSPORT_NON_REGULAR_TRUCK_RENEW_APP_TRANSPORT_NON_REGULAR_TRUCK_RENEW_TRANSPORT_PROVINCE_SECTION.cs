using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW
{
    public partial class APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_TRANSPORT_PROVINCE_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_TRANSPORT_PROVINCE_SECTION()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_INFO_SECTION_TRANSPORT_IN",
                        ControlAnswer = "OTHER",
                    },
                },
            };
        }

        private static FormControlDisableCondition DISABLE_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_TRANSPORT_PROVINCE_SECTION()
        {
            return new FormControlDisableCondition
            {
                Conditions = new FormControlDisableCondition.ControlWithAnswer[]
                {
                    new FormControlDisableCondition.ControlWithAnswer
                    {
                        ControlName = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_INFO_SECTION_TRANSPORT_IN",
                        ControlAnswer = "KINGDOM",
                    },
                },
            };
        }

        private static string APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_TRANSPORT_PROVINCE_SECTION_PROVINCE_URL()
        {
            return "~/Api/v2/Geo/Provinces";
        }
    }
}
