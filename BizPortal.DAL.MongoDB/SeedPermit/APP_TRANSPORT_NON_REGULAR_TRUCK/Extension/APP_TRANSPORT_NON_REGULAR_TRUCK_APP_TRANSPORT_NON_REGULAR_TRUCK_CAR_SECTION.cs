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
    public partial class APP_TRANSPORT_NON_REGULAR_TRUCK_APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION_LICENSE()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION_REQUEST_TYPE",
                        ControlAnswer = "HAVE_CAR",
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION_CASSIS()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION_REQUEST_TYPE",
                        ControlAnswer = "HAVE_CAR",
                    },
                },
            };
        }
        private static FormControlDisplayCondition CONDITION_APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION_PROVINCE()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION_REQUEST_TYPE",
                        ControlAnswer = "HAVE_CAR",
                    },
                },
            };
        }
        private static FormControlDisplayCondition CONDITION_APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION_ENGINE()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION_REQUEST_TYPE",
                        ControlAnswer = "HAVE_CAR",
                    },
                },
            };
        }

        private static Select2Opt[] DROPDOWN_APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION_BRAND()
        {
            return FormSectionRow.optCarBrand;
        }

        private static FormControlDisplayCondition CONDITION_APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION_BRAND()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION_REQUEST_TYPE",
                        ControlAnswer = "HAVE_CAR",
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION_OTHER()
        {
            return new FormControlDisplayCondition
            {
                IsOr = false,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION_REQUEST_TYPE",
                        ControlAnswer = "HAVE_CAR",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION_BRAND",
                        ControlAnswer = "OTHER",
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION_TYPE()
        {
            return new FormControlDisplayCondition
            {
                IsOr = true,
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION_REQUEST_TYPE",
                        ControlAnswer = "HAVE_CAR",
                    },
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION_REQUEST_TYPE",
                        ControlAnswer = "NOT_HAVE_CAR",
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION_AMOUNT()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION_REQUEST_TYPE",
                        ControlAnswer = "NOT_HAVE_CAR",
                    },
                },
            };
        }
        private static string APP_TRANSPORT_NON_REGULAR_TRUCK_CAR_SECTION_PROVINCE_URL()
        {
            return "~/Api/v2/Geo/Provinces";
        }
    }
}
