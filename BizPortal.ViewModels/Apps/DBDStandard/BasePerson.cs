using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels.Apps.DBDStandard
{
    public class BasePerson : BaseAddress
    {
        public string identityID { get; set; }
        public string birthDate { get; set; }
        public int age { get; set; }
        public string nationality { get; set; }
        public string nationCode { get; set; }
        public string race { get; set; }
        public string titleCode { get; set; }
        public string firstNameTH { get; set; }
        public string lastNameTH { get; set; }
        public string firstNameEN { get; set; }
        public string lastNameEN { get; set; }
    }
}
