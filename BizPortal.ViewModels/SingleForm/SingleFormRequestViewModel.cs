using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels.SingleForm
{
    public class SingleFormRequestViewModel
    {
        public string IdentityID { get; set; }

        public UserTypeEnum IdentityType { get; set; }

        [Display(Name = "SINGLEFORM_STATUS_REQUEST", ResourceType = typeof(Resources.ApplicationStatusRequests))]
        public ApplicationStatusV2Enum Status { get; set; }

        public List<SingleFormSectionDataViewModel> SectionData { get; set; }

        public Guid? BatchID { get; set; }

        public Guid? TransactionID { get; set; }

        public int appStep { get; set; }
        public string businessId { get; set; }
    }
}
