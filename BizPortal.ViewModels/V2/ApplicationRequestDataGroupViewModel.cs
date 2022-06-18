using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels.V2
{
    public class ApplicationRequestDataGroupViewModel
    {
        public ApplicationRequestDataGroupViewModel()
        {
            Visible = true;
            Data = new Dictionary<string, object>();
        }

        [Required]
        public string GroupName { get; set; }

        public string GroupDescription { get; set; }

        public bool Visible { get; set; }

        public Dictionary<string, object> Data { get; set; }
    }
}
