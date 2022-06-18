using BizPortal.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels
{

    public class MemberServiceImportViewModel
    {
        [Column(0)]
        public string ApplicationName { get; set; }

        [Column(1)]
        public string Province { get; set; }

        [Column(2)]
        public string District { get; set; }

        [Column(3)]
        public string Section { get; set; }

        [Column(5)]
        public int ApplicationID { get; set; }

        [Column(6)]
        public int ProvinceID { get; set; }

        [Column(7)]
        public int DistrictID { get; set; }

        [Column(8)]
        public int SectionID { get; set; }
    }
}
