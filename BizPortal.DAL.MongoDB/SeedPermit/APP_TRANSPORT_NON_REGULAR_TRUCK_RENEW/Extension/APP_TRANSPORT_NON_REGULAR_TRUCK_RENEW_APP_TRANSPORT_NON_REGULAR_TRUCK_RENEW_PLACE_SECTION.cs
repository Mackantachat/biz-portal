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
    public partial class APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_PLACE_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_PLACE_SECTION()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_TRANFER_PLACE_SECTION_HAVE_TRANFER",
                        ControlAnswer = "HAVE_TRANFER",
                    },
                },
            };
        }

        private static FormControlDisableCondition DISABLE_APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_PLACE_SECTION()
        {
            return new FormControlDisableCondition
            {
                Conditions = new FormControlDisableCondition.ControlWithAnswer[]
                {
                    new FormControlDisableCondition.ControlWithAnswer
                    {
                        ControlName = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW_TRANFER_PLACE_SECTION_HAVE_TRANFER",
                        ControlAnswer = "NOT_HAVE_TRANFER",
                    },
                },
            };
        }
    }
}
