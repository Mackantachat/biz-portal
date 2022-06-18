using BizPortal.ViewModels.Select2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.DAL.MongoDB
{
    public class FormRadioGroupWithDropdown
    {
        public string RadioGroupName { get; set; }
        public FormRadioButtonWithDropdown[] RadioButtons { get; set; }
    }

    public class FormRadioButtonWithDropdown
    {
        public bool IsDefaultChecked { get; set; }
        public string RadioButtonValue { get; set; }
        public string RadioButtonText { get; set; }
        public bool RadioWithDropdown { get; set; }
        public bool NoRadioButtonText { get; set; }
        public FormDropdown[] DropdownOpt { get; set; }
    }
}
