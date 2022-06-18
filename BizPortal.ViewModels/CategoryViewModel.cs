using BizPortal.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels
{
    public class CategoryViewModel : CmsViewModel
    {
        public int CategoryID { get; set; }

        [Required, StringLength(450)]
        public string CategoryName { get; set; }

        public string Description { get; set; }

        public int SectionID { get; set; }

        public string SectionName { get; set; }
    }
}
