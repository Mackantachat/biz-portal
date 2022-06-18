using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.DAL.MongoDB
{
    public class FormRadioGroupWithText
    {
        public string RadioGroupName { get; set; }
        public FormRadioButtonWithText[] RadioButtons { get; set; }
    }

    public class FormRadioButtonWithText
    {
        public string RadioButtonValue { get; set; }
        public string RadioButtonText { get; set; }
        public bool RadioWithText { get; set; }

    }
}
