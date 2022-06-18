using BizPortal.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Models
{
    public class MemberServiceArea : ManipulateModel
    {
        public int MemberServiceAreaID { get; set; }

        public int MemberServiceID { get; set; }

        public virtual MemberService MemberService { get; set; }

        public int ProvinceID { get; set; }

        public int DistrictID { get; set; }

        public int SectionID { get; set; }

        public string Province { get; set; }

        public string District { get; set; }

        public string Section { get; set; }

    }
}
