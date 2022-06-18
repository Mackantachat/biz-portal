using BizPortal.Utils.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels
{
    public class EmailViewModel
    {
        [Required(ErrorMessageResourceName="PLEASE_SELECT_QUESTION", ErrorMessageResourceType = typeof(Resources.ContactUs))]
        public string Question { get; set; }

        [Required(ErrorMessageResourceName = "PLEASE_ENTER_NAME", ErrorMessageResourceType = typeof(Resources.ContactUs))]
        public string Name { get; set; }

        
        [Required(ErrorMessageResourceName = "PLEASE_ENTER_CITIZENID", ErrorMessageResourceType = typeof(Resources.ContactUs))]
        public string CitizenId { get; set; }

        


        [Required(ErrorMessageResourceName = "PLEASE_ENTER_MESSAGE", ErrorMessageResourceType = typeof(Resources.ContactUs))]
        public string Message { get; set; }


        [RequiredIf("IsContactBack", true, ErrorMessageResourceName = "EMAIL_INVALID", ErrorMessageResourceType = typeof(Resources.ContactUs))]
        //[Required(ErrorMessageResourceName = "PLEASE_ENTER_EMAIL", ErrorMessageResourceType = typeof(Resources.ContactUs))]
        public string Email { get; set; }

        public bool IsContactBack { get; set; }

        [RequiredIf("IsContactBack", true, ErrorMessageResourceName = "PLEASE_ENTER_TELEPHONE", ErrorMessageResourceType = typeof(Resources.ContactUs))]
        public string Telephone { get; set; }

    }
}
