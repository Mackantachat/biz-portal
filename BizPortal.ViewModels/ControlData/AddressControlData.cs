using BizPortal.ViewModels.Select2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels.ControlData
{
    public class AddressControlData
    {
        public string Branch { get; set; }
        public string Address { get; set; }
        public string Deed { get; set; }
        public string Moo { get; set; }
        public string Soi { get; set; }
        public string Yak { get; set; }
        public string Village { get; set; }
        public string Building { get; set; }
        public string Floor { get; set; }
        public string Road { get; set; }
        public Select2Opt Amphur { get; set; }
        public Select2Opt Tumbol { get; set; }
        public Select2Opt Province { get; set; }
        public string PostCode { get; set; }
        public string Telephone { get; set; }
        public string TelephoneExt { get; set; }
        public string Fax { get; set; }

        public string SelectedAmphurText { get; set; }
        public string SelectedTumbolText { get; set; }
        public string SelectedProvinceText { get; set; }

        public string Mobile { get; set; }
        public string Email { get; set; }
    }
}
