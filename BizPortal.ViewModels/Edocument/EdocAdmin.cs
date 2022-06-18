using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels.Edocument
{
    public class EdocAdmin
    {
        public int AppID { get; set; }
        public int DocumentType { get; set; } // ประเภทเอกสาร  (1 เอกสารจากหน่วยงาน)  (2 เอกสารจากระบบ)
        public int TemplateID { get; set; }   
        public int SigningType { get; set; }  // ประเภทผู้ลงนาม  (1 หน่วยงาน) (2 บุคคล)
        public List<ConfigSigner> Signers { get; set; }
      
    }

    public class ConfigSigner
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string CitizenID { get; set; }
        public Guid FileID { get; set; }
      
    }






}
