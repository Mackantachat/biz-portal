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
    public partial class APP_ELECTRONIC_COMMERCIAL_APP_ELECTRONIC_COMMERCIAL_OTHER_ELECTRONIC_SECTION
    {
        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_OTHER_ELECTRONIC_SECTION()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_INFO_SECTION_REQUEST_TYPE",
                        ControlAnswer = "ELECTRONIC",
                    },
                },
            };
        }

        private static FormControlDisableCondition DISABLE_APP_ELECTRONIC_COMMERCIAL_OTHER_ELECTRONIC_SECTION()
        {
            return new FormControlDisableCondition
            {
                Conditions = new FormControlDisableCondition.ControlWithAnswer[]
                {
                    new FormControlDisableCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_INFO_SECTION_REQUEST_TYPE",
                        ControlAnswer = "NORMAL",
                    },
                },
            };
        }

        /* 2019-08-19: ย้ายไปไว้ที่ APP_ELECTRONIC_COMMERCIAL_APP_ELECTRONIC_COMMERCIAL_WEBSITE_SECTION
        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_OTHER_ELECTRONIC_SECTION_WEBSITE()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_OTHER_ELECTRONIC_SECTION_ELECTRONIC_MEDIA",
                        ControlAnswer = "WEBSITE",
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_OTHER_ELECTRONIC_SECTION_LINE()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_OTHER_ELECTRONIC_SECTION_ELECTRONIC_MEDIA",
                        ControlAnswer = "LINE",
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_OTHER_ELECTRONIC_SECTION_INSTRAGRAM()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_OTHER_ELECTRONIC_SECTION_ELECTRONIC_MEDIA",
                        ControlAnswer = "INSTRAGRAM",
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_OTHER_ELECTRONIC_SECTION_FACEBOOK()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_OTHER_ELECTRONIC_SECTION_ELECTRONIC_MEDIA",
                        ControlAnswer = "FACEBOOK",
                    },
                },
            };
        }

        private static FormControlDisplayCondition CONDITION_APP_ELECTRONIC_COMMERCIAL_OTHER_ELECTRONIC_SECTION_APPLICATION()
        {
            return new FormControlDisplayCondition
            {
                Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                {
                    new FormControlDisplayCondition.ControlWithAnswer
                    {
                        ControlName = "APP_ELECTRONIC_COMMERCIAL_OTHER_ELECTRONIC_SECTION_ELECTRONIC_MEDIA",
                        ControlAnswer = "APPLICATION",
                    },
                },
            };
        }
        */
    }
}
