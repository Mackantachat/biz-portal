using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels.Edocument
{
    public class EdocUser
    {
        public string CitizenID { get; set; }
        public List<SignDocument> SignDocuments { get; set; }

    }


    public  class SignDocument
    {
        public Guid ApplicationRequestId { get; set; }      
        public DateTime SignDate { get; set; }  //วันที่ลงนาม
        public DateTime InformDate { get; set; } //วันที่แจ้ง
        public string Remark { get; set; }
        public bool? IsSigned { get; set; }

    }
}
