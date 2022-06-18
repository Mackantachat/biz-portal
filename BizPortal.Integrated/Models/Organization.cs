using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Integrated.Models
{
    public class Organization
    {
        public string Abbreviation { get; set; }

        public string Address { get; set; }

        public string Code { get; set; }

        public string DepartmentCode { get; set; }

        public string Detail { get; set; }

        public string DivisionCode { get; set; }

        public int Level { get; set; }

        public string LogoURL { get; set; }

        public string MinistryCode { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Url { get; set; }
    }
}
