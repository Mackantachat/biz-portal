using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BizPortal.Models
{
    public class SigningTemplate
    {
        public SigningTemplate()
        {
            SigningPersons = new HashSet<SigningPerson>();
            SigningPositions = new HashSet<SigningPosition>();
            SigningExtendedDatas = new HashSet<SigningExtendedData>();
        }

        public int SigningTemplateID { get; set; }

        public string SigningTemplateName { get; set; }

        public string SigningDocumentTemplateID { get; set; }

        public UserTypeEnum  UserType { get; set; }

        public virtual ICollection<SigningPerson> SigningPersons { get; set; }

        public virtual ICollection<SigningPosition> SigningPositions { get; set; }

        public virtual ICollection<SigningExtendedData> SigningExtendedDatas { get; set; }

        [ForeignKey("Application")]
        public int ApplicationID { get; set; }

        public virtual Application Application { get; set; }

    }
}
