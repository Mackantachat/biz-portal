using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BizPortal.Models.Base
{
    [Bind()]
    public abstract class ManipulateModel
    {
        public bool IsDeleted { get; set; }

        [Display(Name = "Created By")]
        [ForeignKey("CreatedUser")]
        [StringLength(128)]
        [Required]
        public string CreatedUserID { get; set; }

        [Display(Name = "Created Date")]
        [Required]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Updated By")]
        [ForeignKey("UpdatedUser")]
        [StringLength(128)]
        public string UpdatedUserID { get; set; }

        [Display(Name = "Updated Date")]
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("DeletedUser")]
        [StringLength(128)]
        public string DeletedUserID { get; set; }

        public DateTime? DeletedDate { get; set; }

        public virtual ApplicationUser CreatedUser { get; set; }

        public virtual ApplicationUser UpdatedUser { get; set; }

        public virtual ApplicationUser DeletedUser { get; set; }
    }
}
