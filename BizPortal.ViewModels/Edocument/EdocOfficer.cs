using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels.Edocument
{
    public class EdocOfficer
    {
        public Guid ApplicationRequestID { get; set; }
        public int DocumentType { get; set; } // ประเภทเอกสาร  (1 เอกสารจากหน่วยงาน)  (2 เอกสารจากระบบ)
        public int SigningType { get; set; }  // ประเภทผู้ลงนาม  (1 หน่วยงาน) (2 บุคคล)
        public List<Signer> Signers { get; set; }
        public DateTime InformDate { get; set; }
    }

    public class Signer
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string CitizenID { get; set; }
        public bool? IsSigned { get; set; }
        public string Remark { get; set; }

    }
}
