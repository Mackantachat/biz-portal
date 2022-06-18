using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using BizPortal.DAL.MongoDB;
using BizPortal.ViewModels.Select2;

namespace BizPortal.SeedPermit.APP_TRANSPORT_NON_REGULAR_TRUCK
{
    public partial class APP_TRANSPORT_NON_REGULAR_TRUCK_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_2
    {
        private static FormControlDisplayCondition CONDITION_APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_2_DATETIME()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_TRANSPORT_NON_REGULAR_TRUCK_MAINTENANCE_SECTION_2_READY",
                        ControlAnswer = "WITHIN",
                    },
                },
            };
        }
    }
}
