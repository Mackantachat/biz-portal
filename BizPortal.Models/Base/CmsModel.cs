using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Models.Base
{
    public abstract class CmsModel : ManipulateModel
    {
        public bool Published { get; set; }

        public int? Ordering { get; set; }

        [ForeignKey("Thumbnail")]
        public int? ThumbnailID { get; set; }


        public virtual FileUpload Thumbnail { get; set; }
    }
}
