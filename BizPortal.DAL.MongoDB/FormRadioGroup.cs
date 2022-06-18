using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.DAL.MongoDB
{
    public class FormRadioGroup
    {
        public string RadioGroupName { get; set; }
        public FormRadioButton[] RadioButtons { get; set; }
    }

    public class FormRadioButton
    {
        public string RadioButtonValue { get; set; }
        public string RadioButtonText { get; set; }
    }

    
}
