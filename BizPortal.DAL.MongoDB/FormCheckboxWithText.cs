using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.DAL.MongoDB
{
    public class FormCheckboxWithText
    {
        public string CheckboxWithTextOptName { get; set; }
        public string CheckboxWithTextOptValue { get; set; }
        public bool IsText { get; set; }
        public int ColFixed { get; set; }
    }
}
