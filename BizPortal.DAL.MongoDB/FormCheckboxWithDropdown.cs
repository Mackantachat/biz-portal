using BizPortal.ViewModels.Select2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.DAL.MongoDB
{
    public class FormCheckboxWithDropdown
    {
        public string CheckboxWithDropdownOptName { get; set; }
        public Select2Opt[] CheckboxWithDropdownOpts { get; set; }
    }
}
