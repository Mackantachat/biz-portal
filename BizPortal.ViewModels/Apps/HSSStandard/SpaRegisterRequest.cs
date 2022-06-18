using BizPortal.ViewModels.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels.Apps.HSSStandard
{
    public class SpaRegisterRequest
    {
        public Dictionary<string, SpaRegisterFormData> Data { get; set; }
        public List<HSSFileMetaData> Files { get; set; }
    }

    public class SpaRegisterFormData
    {
        public string ApplicationRequestID { get; set; }
        public string ApplicationRequestNumber { get; set; }
        public int ApplicationID { get; set; }
        public string IdentityID { get; set; }
        public string IdentityType { get; set; }
        public string IdentityName { get; set; }
        public Dictionary<string, ApplicationRequestDataGroupViewModel> DataFiltered { get; set; }
        public FileGroup[] UploadedFiles { get; set; }
        public string PaymentMedthod { get; set; }
        public string PermitDeliveryType { get; set; }
        public string PermitDeliveryAddress { get; set; }
    }
}
