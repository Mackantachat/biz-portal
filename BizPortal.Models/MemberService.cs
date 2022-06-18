using BizPortal.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Models
{
    public class MemberService : ManipulateModel
    {
        public int MemberServiceID { get; set; }

        public int ApplicationID { get; set; }

        public string UserID { get; set; }

        [ForeignKey("UserID")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual Application Application { get; set; }

        public virtual ICollection<MemberServiceArea> MemberServiceAreas { get; set; }
    }
}
