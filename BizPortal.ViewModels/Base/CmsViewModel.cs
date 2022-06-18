using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BizPortal.ViewModels.Base
{
    [Bind()]
    public abstract class CmsViewModel
    {
        [Display(Name = "LABEL_PUBLISH", ResourceType = typeof(Resources.Global))]
        public bool Published { get; set; }

        public int? Ordering { get; set; }

        public int? ThumbnailID { get; set; }
    }
}
