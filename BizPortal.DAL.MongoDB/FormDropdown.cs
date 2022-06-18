using BizPortal.ViewModels.Select2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.DAL.MongoDB
{
    public class FormDropdown
    {
        public string DropdownName { get; set; }
        public Select2Opt[] Select2Opt { get; set; }
    }
}
