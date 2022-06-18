using BizPortal.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels
{
    public class SectionViewModel : CmsViewModel
    {
        public int? SectionID { get; set; }

        [Required, StringLength(450)]
        public string SectionName { get; set; }

        public string Description { get; set; }
    }
}
